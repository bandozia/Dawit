using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Dawit.Infrastructure.Service.Messaging.Rabbit
{
    public class RabbitProducer : IMsgProducer
    {        
        private readonly IMsgContext<IModel> _context;

        public RabbitProducer(IMsgContext<IModel> context)
        {
            _context = context;           
        }

        public bool AddEventToQueue(string queueName, string msg)
        {
            return CreateTaskEvent(queueName, Encoding.UTF8.GetBytes(msg));
        }

        public bool AddEventToQueue<T>(string queueName, T data)
        {
            var body = EncodeBodyData(data);
            return CreateTaskEvent(queueName, body);
        }

        private bool CreateTaskEvent(string queueName, byte[] body)
        {
            var queue = _context.ProducerChannel.QueueDeclare(queueName, true, false, false, null);
            
            if (queue is not null)
            {
                var properties = _context.ProducerChannel.CreateBasicProperties();
                properties.Persistent = true;

                _context.ProducerChannel.BasicPublish(string.Empty, queueName, properties, body);
                return true;
            }
            else
                return false;
        }

        private static byte[] EncodeBodyData<T>(T data)
        {
            string dataString = JsonConvert.SerializeObject(data);
            return Encoding.UTF8.GetBytes(dataString);
        }
             
    }
}
