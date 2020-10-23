using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;

namespace Dawit.Infrastructure.Service.Messaging.Rabbit
{
    public class RabbitConsumer : IMsgConsumer
    {
        private readonly IModel channel;

        public RabbitConsumer()
        {
            channel = CreateChannel();
            Console.WriteLine("CRIADO!!!!");
        }

        public void AddQueueToConsume<T>(string queueName, bool durable, Action<T> msgReceivedCallback)
        {
            channel.QueueDeclare(queueName, durable, false, false);
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (s, ea) =>
            {
                T bodyObj = DecodeBodyData<T>(ea.Body.ToArray());
                msgReceivedCallback.Invoke(bodyObj);
                channel.BasicAck(ea.DeliveryTag, false);
            };

            channel.BasicConsume(queueName, false, consumer);
        }
               

        private T DecodeBodyData<T>(byte[] body)
        {
            string bodyString = Encoding.UTF8.GetString(body);
            return JsonConvert.DeserializeObject<T>(bodyString);
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
