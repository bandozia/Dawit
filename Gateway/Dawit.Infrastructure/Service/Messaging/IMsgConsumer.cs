using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawit.Infrastructure.Service.Messaging
{
    public interface IMsgConsumer
    {
        void AddQueueToConsume<T>(string queueName, bool durable, Action<T> msgReceivedCallback);
        void AddQueueToConsume<T>(string queueName, bool durable, Func<T, Task> msgReceivedCallback);

    }
}
