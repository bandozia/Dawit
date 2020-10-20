using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawit.Infrastructure.Service.Messaging
{
    public interface IMsgProducer
    {
        bool AddEventToQueue(string queueName, string msg);
        bool AddEventToQueue<T>(string queueName, T data);
    }
}
