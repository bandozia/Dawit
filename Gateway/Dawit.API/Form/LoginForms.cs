﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dawit.API.From
{    
    public class LoginForm
    {
        [Required][EmailAddress]
        public string Email { get; set; }
        
        [Required][MinLength(6)]
        public string Password { get; set; }
    }
}
