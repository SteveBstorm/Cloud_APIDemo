using Cloud_APIDemo.Models;
using Microsoft.AspNetCore.SignalR;

namespace Cloud_APIDemo.Hubs
{
    public class MovieHub : Hub
    {
        public async Task NewMovie()
        {
            await Clients.All.SendAsync("NotifyNewMovie");
        }
    }
}
