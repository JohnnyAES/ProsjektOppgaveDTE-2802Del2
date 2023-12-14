using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProsjektOppgaveWebAPI.Models;
using ProsjektOppgaveWebAPI.Models.ViewModel;
using ProsjektOppgaveWebAPI.Services;

namespace ProsjektOppgaveWebAPI.Controllers;

[Route("/[controller]")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IBlogService _service;

    public PostController(IBlogService service)
    {
        _service = service;
    }


    [HttpGet]
    public async Task<IEnumerable<Post>> GetPosts(int blogId)
    {
        return await _service.GetPostsForBlog(blogId);
    }
    
    
    [HttpGet("{id:int}")]
    public Post? GetPost([FromRoute] int id)
    {
        return _service.GetPost(id);
    }
    
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PostViewModel post)
    {
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var blog = _service.GetBlog(post.BlogId);
        if (blog != null && blog.Status != 1) return BadRequest("This blog is closed for new posts and comments!");

        var newPost = new Post
        {
            Title = post.Title,
            Content = post.Content,
            BlogId = post.BlogId,
            
        };
        var username = User.Claims.FirstOrDefault()?.Value;
        
        await _service.SavePost(newPost, username);
        return CreatedAtAction("GetPosts", new { id = post.BlogId }, post);
    }

    
    [Authorize]
    [HttpPut("{id:int}")]
    public IActionResult Update([FromRoute] int id, [FromBody] PostViewModel post)
    {
        if (id != post.PostId)
            return BadRequest();

        var existingPost = _service.GetPost(id);
        if (existingPost is null)
            return NotFound();
        
        var username = User.Claims.FirstOrDefault()?.Value;
        if (existingPost.Owner.UserName != username)
        {
            return Unauthorized();
        }
        var newPost = new Post
        {
            PostId = post.PostId,
            Title = post.Title,
            Content = post.Content,
            BlogId = post.BlogId
        };
        
        _service.SavePost(newPost, username);

        return NoContent();
    }

    
    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var post = _service.GetPost(id);
        if (post is null)
            return NotFound();
        
        var username = User.Claims.FirstOrDefault()?.Value;
        if (post.Owner.UserName != username)
        {
            return Unauthorized();
        }

        _service.DeletePost(id, username);

        return NoContent();
    }
}