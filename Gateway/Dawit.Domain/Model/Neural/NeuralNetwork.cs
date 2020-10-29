using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawit.Domain.Model.Neural
{
    public enum NeuralJobStatus
    {
        IDLE,
        TRAINING,
        TRAINED,
        FAILED
    }
    public class NeuralNetwork : BaseModel
    {
        //TODO: add user        
        public string Name { get; set; }
        public NeuralJobStatus Status { get; set; }
        public IEnumerable<NeuralMetric> Metrics { get; set; }
               
    }
}
