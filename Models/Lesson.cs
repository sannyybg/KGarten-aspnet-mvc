using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Kindergarten3.Models
{
    public class Lesson
    {
        public int LessonId { get; set; }

        [DisplayName("დასახელება")]
        public string LessonName { get; set; }

        [DisplayName("კვირის დღე")]
        public string Dayofweek { get; set; }

        [DisplayName("დაწყების დრო")]
        public string StartHour { get; set; }


        public Teacher Teacher { get; set; }

        public int TeacherId {get; set;}

        public Science Science { get; set; }

        public int ScienceId { get; set; }

        [DisplayName("სტუდენტების მაქს. რაოდენობა")]
        public int MaxStudentsCount { get; set; }

        [DisplayName("სტუდენტების რაოდენობა")]
        public int StudentsCount { get; set; } = 0;

        public List<Student> Students { get; set; }

    }
}