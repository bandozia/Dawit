using Dawit.Infrastructure.Service.Signal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawit.API.Service.Extensions
{
    public static class SubscriptionsHelper
    {
        public static List<EventSubscription> ToSubscriptions(this byte[] bytes)
        {
            if (bytes is null) return null;
            try
            {
                return JsonConvert.DeserializeObject<List<EventSubscription>>(Encoding.UTF8.GetString(bytes));
            }
            catch
            {
                return null;
            }
        }

        public static byte[] ToBytes(this List<EventSubscription> subscriptions) => subscriptions switch
        {
            not null => Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(subscriptions)),
            _ => null
        };


    }
}
