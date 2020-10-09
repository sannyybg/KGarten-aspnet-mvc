using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kindergarten3.ViewModel
{
    public class LogInViewModel
    {
        public string Username { get; set; }

        [DataType(DataType.Password, ErrorMessage = "")]
        public string Password { get; set; }
    }
}