using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dawit.API.Model.Form
{
    public class TrainRequestForm
    {
        [Required]
        public string JobId { get; set; }
        public bool PublicStatus { get; set; }
    }
}
