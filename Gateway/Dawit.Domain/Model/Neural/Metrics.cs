using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawit.Domain.Model.Neural
{
    public class Metrics
    {
        public string JobId { get; set; }
        public int Epoch { get; set; }
        public double Accuracy { get; set; }
        public double ValidationAccuracy { get; set; }
    }
}
