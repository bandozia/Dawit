using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawit.Infrastructure.Service.Messaging
{
    public interface IMsgContext<T>
    {
        T ProducerChannel { get; set; }
        T ConsumerChannel { get; set; }               
        
    }
}
