using ProsjektOppgaveWebAPI.Models;

namespace ProsjektOppgaveWebAPI.Services.TagServices;

public interface ITagService
{
    Task<IEnumerable<Tag>> GetTags();
    Task Save(Tag tag);
    Task CreateTagRelation(PostTagRelations pTagRelation);
}