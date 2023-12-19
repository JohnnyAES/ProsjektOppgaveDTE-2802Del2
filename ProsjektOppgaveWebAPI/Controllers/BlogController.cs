using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using ProsjektOppgaveWebAPI.Models;
using ProsjektOppgaveWebAPI.Models.ViewModel;
using ProsjektOppgaveWebAPI.Services;

namespace ProsjektOppgaveWebAPI.Controllers;

[Route("/[controller]")]
[ApiController]
public class BlogController : ControllerBase
{
    private readonly IBlogService _service;
    
    public BlogController(IBlogService service)
    {
        _service = service;
    }


    [HttpGet]
    public async Task<IEnumerable<Blog>> GetAll()
    {
        return await _service.GetAllBlogs();
    }


    [HttpGet("{id:int}")]
    public IActionResult Get([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var blog = _service.GetBlog(id);
        if (blog == null)
        {
            return NotFound();
        }
        return Ok(blog);
    }


    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BlogViewModel blog)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var newBlog = new Blog
        {
            Name = blog.Name,
            Status = blog.Status
        };

        var username = User.Claims.FirstOrDefault()?.Value;
        await _service.Save(newBlog, username);

        return CreatedAtAction("Get", new { id = blog.BlogId }, newBlog);
    }


    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BlogViewModel blog)
    {
        if (id != blog.BlogId)
            return BadRequest("Id not matching");
        
        var existingBlog = _service.GetBlog(id);
        
        if (existingBlog is null)
            return NotFound("Blog not found");
        
        var newBlog = new Blog
        {
            BlogId = id,
            Name = blog.Name,
            Status = blog.Status
        };
        
        var username = User.Claims.FirstOrDefault()?.Value;
        if (existingBlog.Owner.UserName != username)
        {
            return Unauthorized("feil bruker");
        }
        
        await _service.Save(newBlog, username);

        return NoContent();
    }

    
    [Authorize]
    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var blog = _service.GetBlog(id);
        if (blog is null)
            return NotFound();
        
        var username = User.Claims.FirstOrDefault()?.Value;
        if (blog.Owner.UserName != username)
        {
            return Unauthorized();
        }

        _service.Delete(id, username);

        return NoContent();
    }
}