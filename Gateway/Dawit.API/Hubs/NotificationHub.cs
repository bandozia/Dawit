using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dawit.Infrastructure.Service.Signal;

namespace Dawit.API.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly IConnectionMapping<Guid> _connectionMapping;

        public NotificationHub(IConnectionMapping<Guid> connectionMapping)
        {
            _connectionMapping = connectionMapping;
        }
                
        public async Task SubscribeToNetwork(string networkId, int eventType)
        {
            //TODO: user provider, add network identity to user
            await _connectionMapping.Add(Guid.Parse(networkId), 0, Context.ConnectionId);
            await Clients.Client(Context.ConnectionId).SendAsync("onSubscripted", "success");
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            //TODO: remove from distribuited cache 
            return base.OnDisconnectedAsync(exception);
        }
    }
}
