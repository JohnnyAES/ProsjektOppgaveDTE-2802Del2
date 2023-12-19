using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using ProsjektOppgaveWebAPI.Data;
using ProsjektOppgaveWebAPI.Models;

namespace ProsjektOppgaveWebAPI.Services.TagServices;

public class TagService : ITagService
{
    private readonly BlogDbContext _db;
    private readonly UserManager<IdentityUser> _manager;
    
    public TagService(UserManager<IdentityUser> userManager, BlogDbContext db)
    {
        _manager = userManager;
        _db = db;
    }
    
    public async Task<IEnumerable<Tag>> GetTags()
    {
        try
        {
            var tags = _db.Tag
                .ToList();

            return tags;
        }
        catch (NullReferenceException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
        
            return new List<Tag>();
        }
    }
    
    public async Task Save(Tag tag)
    {
        var existingTag = _db.Tag.Find(tag.Id);
        if (existingTag == null)
        {
            _db.Tag.Add(tag);
            await _db.SaveChangesAsync();
        }
    }

    public async Task CreateTagRelation(PostTagRelations pTagRelation)
    {
        _db.PostTagRelations.Add(pTagRelation);
        await _db.SaveChangesAsync();

    }
    
}