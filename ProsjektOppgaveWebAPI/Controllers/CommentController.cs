using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProsjektOppgaveWebAPI.Models;
using ProsjektOppgaveWebAPI.Models.ViewModel;
using ProsjektOppgaveWebAPI.Services.CommentServices;

namespace ProsjektOppgaveWebAPI.Controllers;

[Route("/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentService _service;

    public CommentController(ICommentService service)
    {
        _service = service;
    }
    
    
    [HttpGet]
    public async Task<IEnumerable<Comment>> GetComments(int postId)
    {
        return await _service.GetCommentsForPost(postId);
    }
    
    
    [HttpGet("{id:int}")]
    public Comment? GetComment([FromRoute] int id)
    {
        return _service.GetComment(id);
    }
    
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CommentViewModel comment)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var newComment = new Comment
        {
            Text = comment.Text,
            PostId = comment.PostId
        };
        
        var username = User.Claims.FirstOrDefault()?.Value;
        await _service.Save(newComment, username);
        return CreatedAtAction("GetComment", new { id = comment.PostId }, comment);
    }
    
    
    [Authorize]
    [HttpPut("{id:int}")]
    public IActionResult Update([FromRoute] int id, [FromBody] CommentViewModel comment)
    {
        if (id != comment.CommentId)
            return BadRequest();

        var existingComment = _service.GetComment(id);
        if (existingComment is null)
            return NotFound();
        
        var username = User.Claims.FirstOrDefault()?.Value;
        if (existingComment.Owner.UserName != username)
        {
            return Unauthorized();
        }
        
        var newComment = new Comment
        {
            CommentId = comment.CommentId,
            Text = comment.Text,
            PostId = comment.PostId
        };
        
        _service.Save(newComment, username);

        return NoContent();
    }
    
    
    [Authorize]
    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var comment = _service.GetComment(id);
        if (comment is null)
            return NotFound();
        
        var username = User.Claims.FirstOrDefault()?.Value;
        if (comment.Owner.UserName != username)
        {
            return Unauthorized();
        }
        
        _service.Delete(id, username);

        return NoContent();
    }
}