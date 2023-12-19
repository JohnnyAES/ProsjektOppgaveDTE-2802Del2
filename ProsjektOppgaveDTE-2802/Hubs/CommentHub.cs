using Microsoft.AspNetCore.SignalR;
using ProsjektOppgaveBlazor.data.Models;
using ProsjektOppgaveBlazor.data.Models.ViewModel;

namespace ProsjektOppgaveDTE_2802.Hubs;

public class CommentHub : Hub
{
    public async Task AddNewComment(Comment comment)
    {
        await Clients.All.SendAsync("NewComment", comment);
    }

    public async Task EditComment(Comment comment)
    {
        await Clients.All.SendAsync("UpdateComment", comment);
    }

    public async Task DeleteComment(int id)
    {
        await Clients.All.SendAsync("RemoveComment", id);
    }
}