using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamaWeb.Hubs
{
    [Authorize]
    public class GameHub:Hub
    {
        private static Dictionary<string, string> connectionsNgroup = new Dictionary<string, string>();
        public async Task SendMessage(string message,string grupname)
        {
            var t = Clients.Group(grupname);
            await Clients.Group(grupname).SendAsync("recivemessage", message, Context.User.Identity.Name);
        }

        public async Task AddGrup(string grupname)
        {
            var y = Groups;
            if (connectionsNgroup.ContainsKey(Context.ConnectionId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, connectionsNgroup[Context.ConnectionId]);
                connectionsNgroup.Remove(Context.ConnectionId);                
            }
            connectionsNgroup.Add(Context.ConnectionId, grupname);
            await Groups.AddToGroupAsync(Context.ConnectionId, grupname);
        }

        public override Task OnConnectedAsync()
        {
            
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (connectionsNgroup.ContainsKey(Context.ConnectionId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, connectionsNgroup[Context.ConnectionId]);
                connectionsNgroup.Remove(Context.ConnectionId);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
