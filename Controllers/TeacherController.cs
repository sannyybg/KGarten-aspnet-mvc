using Kindergarten3.Context;
using Kindergarten3.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kindergarten3.Controllers
{
    [SessionFilter]
    [Authorize]
    public class TeacherController : Controller
    {
        KgContext _kgContext = new KgContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TeacherTable()
        {
            var teacherusername = User.Identity.Name;
            var teacher = _kgContext.Teachers.Where(x => x.Username == teacherusername).FirstOrDefault();

            var lessons = _kgContext.Lessons.Where(x => x.TeacherId == teacher.TeacherId).ToList();

            return View(lessons);
        }

        public ActionResult StudentsList(int id)
        {

            var StudentIdsList = _kgContext.LessonTableItems.Where(x => x.TeacherId == id).Select(ms => ms.StudentId).ToList();

            List<string> studentList = new List<string>();

            foreach (var item in StudentIdsList)
            {
                var student = _kgContext.Students.Where(x => x.StudentId == item).FirstOrDefault();
                string Fullname = student.FirstName + " " + student.LastName;

                studentList.Add(Fullname);
            }

            return View(studentList);
        }
    }
}