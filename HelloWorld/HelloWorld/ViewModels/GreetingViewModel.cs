using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloWorld.ViewModels
{
    public class GreetingViewModel
    {//this is a comment to check my source code check in
        public string Greeting { get; set; }
        public DateTime CurrentDate { get; set; }
        public Guid UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
    }
}