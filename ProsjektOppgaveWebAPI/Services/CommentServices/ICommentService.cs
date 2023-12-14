using System.Security.Principal;
using ProsjektOppgaveWebAPI.Models;
using ProsjektOppgaveWebAPI.Models.ViewModel;

namespace ProsjektOppgaveWebAPI.Services.CommentServices;

public interface ICommentService
{
    Task<IEnumerable<Comment>> GetCommentsForPost(int postId);

    Comment? GetComment(int id);
    
    Task Save(Comment comment, string username);

    Task Delete(int id, string username);

    CommentViewModel GetCommentViewModel(int id);
}