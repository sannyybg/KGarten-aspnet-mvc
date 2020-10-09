using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kindergarten3.Models
{
    public class ActivateTable
    {
        public int ActivateTableId { get; set; }

        public string Name { get; set; }

        public bool ActivateStatus { get; set; } = false;
    }
}