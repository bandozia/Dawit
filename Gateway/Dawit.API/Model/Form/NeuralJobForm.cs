﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dawit.API.Model.Form
{
    public class NeuralJobForm
    {
        [Required]
        public string Name { get; set; }
    }
}
