using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dawit.Infrastructure.Service.Signal;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Dawit.API.Service.Extensions;

namespace Dawit.API.Service.Signal
{
    public class MemoryCacheMapping : IConnectionMapping<Guid>
    {
        private readonly IDistributedCache _cache;

        public MemoryCacheMapping(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task Add(Guid key, int eventType, string connId)
        {
            var connections = await _cache.GetAsync(key.ToString());

            var subscriptions = connections != null
                ? connections.ToSubscriptions()
                : new List<EventSubscription>();

            if (!subscriptions.Any(s => s.EventType == eventType && s.ConnectionId == connId))
            {
                subscriptions.Add(new EventSubscription(eventType, connId));
                await _cache.SetAsync(key.ToString(), Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(subscriptions)));
            }
        }

        public async Task<List<EventSubscription>> GetAllByKey(Guid key)
        {
            var connections = await _cache.GetAsync(key.ToString());
            return connections.ToSubscriptions();
        }

        public async Task Remove(Guid key, string connId)
        {
            await Remove(key, s => s.ConnectionId == connId);            
        }

        public async Task Remove(Guid key, string connId, int eventType)
        {
            await Remove(key, s => s.ConnectionId == connId && s.EventType == eventType);            
        }

        private async Task Remove(Guid key, Predicate<EventSubscription> pred)
        {
            var subscriptions = (await _cache.GetAsync(key.ToString())).ToSubscriptions();
            if (subscriptions is not null)
            {
                subscriptions?.RemoveAll(pred);
                await _cache.SetAsync(key.ToString(), subscriptions.ToBytes());
            }
        }

        public async Task RemoveAll(Guid key)
        {
            await _cache.RemoveAsync(key.ToString());
        }

       
        

    }
}
