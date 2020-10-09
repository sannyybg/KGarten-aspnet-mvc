using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kindergarten3.Models
{
    public class LessonTableItem
    {
        [Key]
        public int TableItemId { get; set; }

        public int StudentId { get; set; }

        [DisplayName("სტუდენტის სახელი")]
        public string StudentName { get; set; }

        public int TeacherId { get; set; }

        [DisplayName("მასწავლებელი")]
        public string TeacherName { get; set; }

        [DisplayName("დასახელება")]
        public string LessonName { get; set; }

        [DisplayName("დაწყების დრო")]
        public string StartTime { get; set; }

    }
}