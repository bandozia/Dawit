using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawit.Domain.Model.Neural
{
    public class JobResult
    {        
        public Guid JobId { get; set; }
        public List<NeuralMetric> Metrics { get; set; }
    }
}
