using System.ComponentModel.DataAnnotations;

namespace ProsjektOppgaveWebAPI.Models.ViewModel;

public class PostViewModel
{
    public int PostId { get; set; }
    [Required(ErrorMessage = "Post Title Required"), StringLength(50)]
    public string Title { get; set; }
    [Required(ErrorMessage = "Content Required"), StringLength(750)]
    public string Content { get; set; }
    public int BlogId { get; set; }
    
}