using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Dawit.Infrastructure.Service.Messaging.Rabbit
{
    public class RabbitContext : IMsgContext<IModel>
    {
        public IModel ProducerChannel { get; set; }
        public IModel ConsumerChannel { get; set; }

        public RabbitContext()
        {
            //TODO: load from appsettings or enviroment
            var conn = new ConnectionFactory
            {
                HostName = "broker",
                UserName = "dawit",
                Password = "brokerpass"
            }
            .CreateConnection();

            ProducerChannel = conn.CreateModel();
            ConsumerChannel = conn.CreateModel();
        }
                
    }
}
