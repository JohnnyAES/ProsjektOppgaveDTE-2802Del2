using Microsoft.AspNetCore.Identity;

namespace ProsjektOppgaveWebAPI.Models;

public class Blog
{
    public int BlogId { get; set; }
    public string Name { get; set; }
    public string OwnerId { get; set; }
    public IdentityUser Owner { get; set; }
    public List<Post> Posts { get; set; }
    public int Status { get; set; }
}