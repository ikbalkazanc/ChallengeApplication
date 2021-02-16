using Challenge.API.Hubs.Contract;
using Challenge.Core.Redis;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.API.Hubs
{
    public class MessageHub : Hub , IMessageHub
    {
        private readonly IRedisService _redis;
        public MessageHub(IRedisService redis)
        {
            _redis = redis;
        }
        
        public async void TextBroadcast(string message)
        {
            await Clients.All.SendAsync("new_user",message);
        }

        public async void TextMessage(string receiver,string message)
        {
            var connectionId = await _redis.GetConnectionId(receiver);
            if(connectionId != null) { 
             await Clients.Client(connectionId).SendAsync("new_message",message);
            }
        }
        public override Task OnConnectedAsync()
        {
            string email = Context.User.Identity.Name;
            _redis.SaveUser(Context.ConnectionId, email);
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            _redis.RemoveUser(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
