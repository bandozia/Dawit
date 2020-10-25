using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawit.Domain.Model.Neural
{
    public class NeuralJobResult
    {
        public NeuralJob NeuralJob { get; set; }
        public Metrics Metrics { get; set; }
    }
}
