using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawit.Domain.Model.Neural
{
    public class NeuralMetric : BaseModel
    {
        public NeuralJob NeuralJob { get; set; }
        public int Epoch { get; set; }
        public int EpochDuration { get; set; }
        public double Accuracy { get; set; }
        public double ValidationAccuracy { get; set; }
        public double Loss { get; set; }
        public double ValidationLoss { get; set; }

        [NotMapped]
        public Guid JobId { get; set; }


    }
}
