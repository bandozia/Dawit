using Dawit.Domain.Model.Neural;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Dawit.API.Service.Neural
{
    public class NeuralJobSubscriberInMemory : INeuralJobSubscriber
    {       
        private readonly ConcurrentDictionary<Guid, NeuralChangeCallbacks> Subscriptions;

        public NeuralJobSubscriberInMemory()
        {
            Subscriptions = new ConcurrentDictionary<Guid, NeuralChangeCallbacks>();
        }                                 
        
        public void Subscribe(Guid jobId, Action<JobResult> onComplete, Action<NeuralMetric> onProgress = null)
        {
            if (!Subscriptions.ContainsKey(jobId))
                Subscriptions.TryAdd(jobId, new NeuralChangeCallbacks());

            Subscriptions[jobId].AddCallback(onComplete);
            
            if (onProgress is not null)
                Subscriptions[jobId].AddCallback(onProgress);
        }


        public void Unsubscribe<T>(Guid jobId, Action<T> callback)
        {
            if (Subscriptions.ContainsKey(jobId))
                Subscriptions[jobId].RemoveCallback(callback);

            if (Subscriptions[jobId].CountCallbacks() == 0)
                Subscriptions.TryRemove(jobId, out _);
        }

        public void NotifyTrainProgress(Guid jobId, NeuralMetric metric)
        {            
            if (Subscriptions.TryGetValue(jobId, out NeuralChangeCallbacks cbs))
            {
                foreach (var cb in cbs?.ProgressCallbacks)
                    cb.Invoke(metric);
            }            
        }

        public void NotifyTrainComplete(Guid jobId, JobResult result)
        {
            if (Subscriptions.TryGetValue(jobId, out NeuralChangeCallbacks cbs))
            {
                foreach (var cb in cbs?.CompleteCallbacks)
                    cb.Invoke(result);
            }            

        }
        
       
    }
}
