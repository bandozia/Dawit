using Dawit.Domain.Model.Neural;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dawit.API.Service.Neural
{
    public class NeuralChangeCallbacks
    {
        public HashSet<Action<NeuralMetric>> ProgressCallbacks { get; private set; }
        public HashSet<Action<JobResult>> CompleteCallbacks { get; private set; }

        public NeuralChangeCallbacks()
        {
            ProgressCallbacks = new HashSet<Action<NeuralMetric>>();
            CompleteCallbacks = new HashSet<Action<JobResult>>();
        }

        public NeuralChangeCallbacks AddCallback(Action<NeuralMetric> onProgress)
        {
            ProgressCallbacks.Add(onProgress);
            return this;
        }

        public NeuralChangeCallbacks AddCallback(Action<JobResult> onComplete)
        {
            CompleteCallbacks.Add(onComplete);
            return this;
        }

        public NeuralChangeCallbacks RemoveCallback<T>(Action<T> callback)
        {
            if (ProgressCallbacks.RemoveWhere(c => c == callback as Action<NeuralMetric>) < 1)
                CompleteCallbacks.RemoveWhere(c => c == callback as Action<JobResult>);

            return this;
        }

        public int CountCallbacks() => ProgressCallbacks.Count + CompleteCallbacks.Count;
        
    }
}
