﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Common.Models.AuthenticationModels
{
    public class RegisterData
    {
        public User User { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
