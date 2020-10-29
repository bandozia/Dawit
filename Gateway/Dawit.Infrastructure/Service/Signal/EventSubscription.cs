using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawit.Infrastructure.Service.Signal
{    
    public class EventSubscription
    {
        public int EventType { get; private set; }
        public string ConnectionId { get; private set; }

        public EventSubscription(int eventType, string connectionId)
        {
            EventType = eventType;
            ConnectionId = connectionId;
        }

    }
}
