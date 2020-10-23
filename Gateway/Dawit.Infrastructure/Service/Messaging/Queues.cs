using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawit.Infrastructure.Service.Messaging
{
    public static class Queues
    {
        public const string NN_START_TRAIN = "nn_start_train";
        public const string NN_TRAIN_PROGRESS = "nn_train_progress";
        public const string NN_TRAIN_COMPLETE = "nn_train_complete";
    }
}
