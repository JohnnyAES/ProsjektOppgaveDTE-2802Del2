using Microsoft.AspNetCore.Mvc;
using Moq;
using ProsjektOppgaveBlazor.data.Models.ViewModel;
using ProsjektOppgaveWebAPI.Controllers;
using ProsjektOppgaveWebAPI.Models;
using ProsjektOppgaveWebAPI.Services.TagServices;

namespace ProsjektOppgaveWebAPITest;

public class TagControllerTest
{
    private readonly Mock<ITagService> _serviceMock;
    private readonly TagController _controller;

    public TagControllerTest()
    {
        _serviceMock = new Mock<ITagService>();
        _controller = new TagController(_serviceMock.Object);
    }
    
    private static IEnumerable<Tag> GetTestTags()
    {
        return new List<Tag>
        {
            new Tag(),
            new Tag(),
            new Tag()
        };
    }
    
    // GET
    [Fact]
    public async Task Get_all_tags()
    {
        // Arrange
        _serviceMock.Setup(service => service.GetTags())
            .Returns(Task.FromResult(GetTestTags()));

        // Act
        var result = await _controller.GetTags();

        // Assert
        Assert.IsType<List<Tag>>(result);
    }
    
    
    // POST
    [Fact]
    public async Task Create_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("error", "some error");

        // Act
        var result = await _controller.Create(new Tag());

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Create_ReturnsOk_WhenModelStateIsValid()
    {
        // Arrange
        var tag = new Tag();

        // Act
        var result = await _controller.Create(tag);

        // Assert
        Assert.IsType<CreatedAtActionResult>(result);
        _serviceMock.Verify(x => x.Save(tag), Times.Once);
    }
    
    // POST
    [Fact]
    public async Task Create_Relation_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("error", "some error");

        // Act
        var result = await _controller.CreateRelation(new PostTagRelationsViewModel());

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Create_Relation_ReturnsOk_WhenModelStateIsValid()
    {
        // Arrange
        var pTagRelationViewModel = new PostTagRelationsViewModel();
        
        // Act
        var result = await _controller.CreateRelation(pTagRelationViewModel);

        // Assert
        Assert.IsType<OkResult>(result);
       
    }
}
