using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoWorld.ViewModels
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ErrorMessage { get; set; }
    }
}