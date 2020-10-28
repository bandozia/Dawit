using Dawit.Domain.Model.Neural;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dawit.API.Service.Neural
{
    public interface INeuralJobSubscriber
    {
        public void Subscribe(Guid jobId, Action<JobResult> onComplete, Action<NeuralMetric> onProgress = null);        
        public void Unsubscribe<T>(Guid jobId, Action<T> callback);

        void NotifyTrainProgress(Guid jobId, NeuralMetric metric);
        void NotifyTrainComplete(Guid jobId, JobResult result);

    }
}
