using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProsjektOppgaveBlazor.data.Models.ViewModel;
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
    [HttpPost("createPostTagRelations")]
    public async Task<IActionResult> CreateRelation([FromBody] PostTagRelationsViewModel pTagRelation)

    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        PostTagRelations newPtagRelation = new PostTagRelations
        {
            PostId = pTagRelation.PostId,
            TagId = pTagRelation.TagId
        };
        await _service.CreateTagRelation(newPtagRelation);
        return Ok();
    }
    
}