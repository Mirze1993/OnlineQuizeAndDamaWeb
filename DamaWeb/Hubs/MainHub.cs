using DamaWeb.Tools;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamaWeb.Hubs
{
    public class MainHub:Hub
    {
        public async Task SetOnline(bool b)
        {
            var username = Context.User.Identity.Name;
            var userid= Convert.ToInt32(Context.User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            if (b)
            {
                OnlineUsers.AddUser(username, userid, Context.ConnectionId);
                await Groups.AddToGroupAsync(Context.ConnectionId, "OnlineUsers");
            }
            else
            {
                OnlineUsers.RemoveUsers(Context.ConnectionId);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, "OnlineUsers");
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            OnlineUsers.RemoveUsers(Context.ConnectionId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "OnlineUsers");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
