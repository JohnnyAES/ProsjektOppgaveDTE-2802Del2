using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProsjektOppgaveWebAPI.Models;
using ProsjektOppgaveWebAPI.Services.TagServices;

namespace ProsjektOppgaveWebAPI.Controllers;

[Route("/[controller]")]
[ApiController]
public class TagController : ControllerBase
{
    private readonly ITagService _service;

    public TagController(ITagService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<IEnumerable<Tag>> GetTags()
    {
        return await _service.GetTags();
    }


    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Tag tag)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _service.Save(tag);

        return CreatedAtAction("GetTags", new { id = tag.Id }, tag);
    }
    
    [Authorize]
    [HttpPost("relation")]
    public async Task<IActionResult> CreateRelation([FromBody] PostTagRelations pTagRelation)

    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Console.WriteLine(" ");
        Console.WriteLine(" ey ");
        Console.WriteLine(" ");
        await _service.CreateTagRelation(pTagRelation);
        return Ok();
    }
    
}