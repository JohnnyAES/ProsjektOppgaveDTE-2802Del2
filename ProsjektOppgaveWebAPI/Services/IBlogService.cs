using System.Security.Principal;
using ProsjektOppgaveWebAPI.Models;
using ProsjektOppgaveWebAPI.Models.ViewModel;

namespace ProsjektOppgaveWebAPI.Services;

public interface IBlogService
{
    // Blog
    Task<IEnumerable<Blog>> GetAllBlogs();

    Blog? GetBlog(int id);
    
    Task Save(Blog blog, string username);
    
    Task Delete(int id , string username);

    BlogViewModel GetBlogViewModel();

    BlogViewModel GetBlogViewModel(int id);

    
    // Post
    Task<IEnumerable<Post>> GetPostsForBlog(int blogId);

    Post? GetPost(int id);
    
    Task SavePost(Post post, string username);

    Task DeletePost(int id, string username);
    
    PostViewModel GetPostViewModel();

    PostViewModel GetPostViewModel(int id);
}