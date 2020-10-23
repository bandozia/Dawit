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
        private readonly IModel channel;

        public RabbitProducer()
        {
            channel = CreateChannel();
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
            var queue = channel.QueueDeclare(queueName, true, false, false, null);
            
            if (queue is not null)
            {
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(string.Empty, queueName, properties, body);
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

        private static IModel CreateChannel()
        {
            //TODO: load from settings
            var factory = new ConnectionFactory
            {
                HostName = "broker",
                UserName = "dawit",
                Password = "brokerpass"
            };

            return factory.CreateConnection().CreateModel();
        }
    }
}
