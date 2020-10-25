using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using System.Threading;

namespace Dawit.Infrastructure.Service.Messaging.Rabbit
{
    public class RabbitConsumer : IMsgConsumer
    {        
        private readonly IMsgContext<IModel> _context;

        public RabbitConsumer(IMsgContext<IModel> context)
        {
            _context = context;            
        }
                
        public void AddQueueToConsume<T>(string queueName, bool durable, Action<T> msgReceivedCallback)
        {
            var consumer = RegisterConsumer(queueName, durable);
            consumer.Received += (s, ea) =>
            {
                T bodyObj = DecodeBodyData<T>(ea.Body.ToArray());
                msgReceivedCallback.Invoke(bodyObj);
                //TODO: logic for ack
                _context.ConsumerChannel.BasicAck(ea.DeliveryTag, false);
            };
        }

        public void AddQueueToConsume<T>(string queueName, bool durable, Func<T, Task> msgReceivedCallback)
        {
            var consumer = RegisterConsumer(queueName, durable);
            consumer.Received += (s, ea) =>
            {
                T bodyObj = DecodeBodyData<T>(ea.Body.ToArray());
                msgReceivedCallback.Invoke(bodyObj);
                _context.ConsumerChannel.BasicAck(ea.DeliveryTag, false);
            };
        }

        private EventingBasicConsumer RegisterConsumer(string queueName, bool durable)
        {
            _context.ConsumerChannel.QueueDeclare(queueName, durable, false, !durable);
            var consumer = new EventingBasicConsumer(_context.ConsumerChannel);
            _context.ConsumerChannel.BasicConsume(queueName, false, consumer);

            return consumer;
        }
               

        private T DecodeBodyData<T>(byte[] body)
        {
            string bodyString = Encoding.UTF8.GetString(body);
            return JsonConvert.DeserializeObject<T>(bodyString);
        }
                
       
    }
}
