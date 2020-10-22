using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Dawit.Infrastructure.Service.Messaging
{
    public class RabbitProducer : IMsgProducer
    {
        private IModel channel;

        public RabbitProducer()
        {
            CreateChannel();
        }

        public bool AddEventToQueue(string queueName, string msg)
        {
            return CreateTaskEvent(queueName, Encoding.UTF8.GetBytes(msg));
        }

        public bool AddEventToQueue<T>(string queueName, T data)
        {
            string dataString = JsonConvert.SerializeObject(data);
            byte[] body = Encoding.UTF8.GetBytes(dataString);
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

        private void CreateChannel()
        {
            //TODO: load from settings
            var factory = new ConnectionFactory
            {
                HostName = "broker",
                UserName = "dawit",
                Password = "brokerpass"
            };

            channel = factory.CreateConnection().CreateModel();
        }
    }
}
