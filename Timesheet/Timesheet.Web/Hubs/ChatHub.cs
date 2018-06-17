using Microsoft.AspNetCore.SignalR;
using System;

namespace ChatSample.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            var time = DateTime.Now.ToShortTimeString();
            // Call the broadcastMessage method to update clients.
            Clients.All.SendAsync("broadcastMessage", time, name, message);
        }
    }
}