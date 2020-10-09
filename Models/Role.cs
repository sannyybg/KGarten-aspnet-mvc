using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kindergarten3.Models
{
    public class Role
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public List<Student> Students { get; set; }

        public List<Teacher> Techers { get; set; }

        public List<Admin> Admins { get; set; }
    }
}