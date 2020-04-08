using chatProject1.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chatProject1.Hubs
{
   public class connection
    {
        public string name { get; set; }
        public string connectionID { get; set; }
    }
    public  class ChatHub:Hub
    {
        //Dictionary<string, string> connectedUsers = new Dictionary<string, string>();
        public static List<connection> connectedUsers = new List<connection>();
        public  override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("SendAction", Context.User.Identity.Name, true);
            
            //return base.OnConnectedAsync(); 
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            //var item = connectedUsers.First(kvp => kvp.Value == Context.ConnectionId);
            var item = connectedUsers.Where(u => u.name == Context.User.Identity.Name).First();
            //connectedUsers.Remove(item.Key);
            connectedUsers.Remove(item);
            await Clients.All.SendAsync("SendAction", Context.User.Identity.Name, false);
            //return base.OnDisconnectedAsync(exception);
        }
        public async Task SendMessage( string name,string message)
        {
            var item = connectedUsers.Where(u => u.name == name).First();
            await Clients.Client(item.connectionID).SendAsync("ReceiveMessage", Context.User.Identity.Name , message);
        }
        public async Task SendStatus(bool action)
        {
            await Clients.All.SendAsync("ReceiveStatus", Context.User.Identity.Name, action);
        }
        public Task SendMessageToCaller(string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", message);
        }

        public Task SendMessageToGroup(string message)
        {
            return Clients.Group("SignalR Users").SendAsync("ReceiveMessage", message);
        }
        public string GetConnectionId()
        {
            if (Context.User.Identity.Name != null)
            {
                connectedUsers.Add(new connection { name = Context.User.Identity.Name, connectionID = Context.ConnectionId });
            }
            return Context.ConnectionId;
        }
    }
}
