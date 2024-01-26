using Microsoft.AspNetCore.SignalR;

namespace Cloud_APIDemo.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("NewMessage", message);
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await SendToGroup(groupName, $"L'utilisateur avec l'id {Context.ConnectionId} vient de nous rejoindre");
        }

        public async Task SendToGroup(string groupName, string message)
        {
            await Clients.Group(groupName).SendAsync("messageFrom"+groupName, message);
        }
    }
}
/*
 1) préparer le hub
 2) builder.Services.AddSignalRCore();
 3) app.MapHub<votreHub>("cheminDAcces");

 +++ Bien penser à configurer les cors +++
 */