using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kindergarten3.Models
{
    public class Admin
    {
        public int AdminId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public List<Role> Roles { get; set; }
    }
}