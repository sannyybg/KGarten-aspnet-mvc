using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Kindergarten3.Models
{
    public class Science
    {
        public int ScienceId { get; set; }

        [DisplayName("საგნის დასახელება")]
        public string Name { get; set; }

        public List<Lesson> Lessons { get; set; }
    }
}