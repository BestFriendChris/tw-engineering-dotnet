﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoWorld.Models;

namespace VideoWorld.ViewModels
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ErrorMessage { get; set; }

        public List<Customer> AllCustomers { get; set; }
    }
}