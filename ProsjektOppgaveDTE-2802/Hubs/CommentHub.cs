using Microsoft.AspNetCore.SignalR;
using ProsjektOppgaveBlazor.data.Models;

namespace ProsjektOppgaveDTE_2802.Hubs;

public class CommentHub : Hub
{
    public async Task SendComment(Comment comment)
    {
        await Clients.All.SendAsync("NewComment", comment);
    }

    public async Task DeleteComment(int id)
    {
        await Clients.All.SendAsync("RemoveComment", id);
    }
}