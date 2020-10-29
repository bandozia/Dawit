using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawit.Infrastructure.Service.Signal
{
    public interface IConnectionMapping<T>
    {
        public Task Add(T key, int eventType, string connId);
        public Task Remove(T key, string connId);
        public Task Remove(T key, string connId, int eventType);
        public Task RemoveAll(T key);
        public Task<List<EventSubscription>> GetAllByKey(Guid key);
    }
}
