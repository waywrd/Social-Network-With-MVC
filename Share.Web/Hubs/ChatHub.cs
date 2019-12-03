using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Share.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Share.Web.Hubs
{
    public class ChatHub:Hub
    {

        

        

        [Authorize]
        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync(
                "NewMessage",
                new Message { User = this.Context.User.Identity.Name, message = message });
        }

        public Task EnterGroup(string group)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, group);
        }
        
        public Task SendMessageToGroup(string group,string message)
        {
            return Clients.Group(group).SendAsync("NewMessage", message);
        }

        
    }
}
