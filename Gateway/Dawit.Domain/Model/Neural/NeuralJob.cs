﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawit.Domain.Model.Neural
{
    public class NeuralJob : BaseModel
    {
        //TODO: add user
        public string Name { get; set; }
        public IEnumerable<NeuralMetric> Metrics { get; set; }
               
    }
}
