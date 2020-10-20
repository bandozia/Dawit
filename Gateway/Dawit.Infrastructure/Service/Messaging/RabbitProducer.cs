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
            var queue = channel.QueueDeclare(queueName);
            if (queue is not null)
            {
                channel.BasicPublish("", queueName, null, Encoding.UTF8.GetBytes(msg));
                return true;
            }
            else
                return false;
        }

        public bool AddEventToQueue<T>(string queueName, T data)
        {
            var queue = channel.QueueDeclare(queueName);
            if (queue is not null)
            {
                string dataString = JsonConvert.SerializeObject(data);
                byte[] body = Encoding.UTF8.GetBytes(dataString);

                channel.BasicPublish("", queueName, null, body);
                return true;
            }
            else
                return false;
        }


        private void CreateChannel()
        {
            //TODO: carregar pelo appsettings
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
