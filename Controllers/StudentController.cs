using Kindergarten3.Context;
using Kindergarten3.Filters;
using Kindergarten3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kindergarten3.Controllers
{
    [SessionFilter]
    [Authorize]
    public class StudentController : Controller
    {

        KgContext _kgContext = new KgContext();

        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddLessonToTable()
        {
            var activationstatus = _kgContext.ActivateTables.Any(x => x.ActivateStatus == true);

            if (activationstatus)
            {
                ViewBag.ScienceId = new SelectList(_kgContext.Sciences, "ScienceId", "Name", 0);
                var lessons = _kgContext.Lessons;

                return View(lessons);
            }

            else
            {
                return View("Disabled");
            }
            
        }

        [HttpPost]
        public ActionResult AddLessonToTable(int id)
        {
            var lesson = _kgContext.Lessons.Where(x => x.LessonId == id).FirstOrDefault();

            var teacher = _kgContext.Teachers.Where(x => x.TeacherId == lesson.TeacherId).FirstOrDefault();

            LessonTableItem newItem = new LessonTableItem();

            var studentname = User.Identity.Name;
            
            var currentstudent = _kgContext.Students.Where(x => x.Username == studentname).FirstOrDefault();

            newItem.StudentId = currentstudent.StudentId;
            newItem.StudentName = currentstudent.FirstName + " " + currentstudent.LastName;
            newItem.TeacherId = teacher.TeacherId;
            newItem.TeacherName = teacher.FirstName + " " + teacher.LastName;
            newItem.LessonName = lesson.LessonName;
            newItem.StartTime = lesson.Dayofweek + lesson.StartHour;


            var currentitem = _kgContext.LessonTableItems.Any(x => x.StartTime == newItem.StartTime && x.StudentId == newItem.StudentId);

            if (lesson.StudentsCount == lesson.MaxStudentsCount)
            {
                var data = new { Message = "<div class='alert alert-danger'>ამ საგანზე ყველა ადგილი დაკავებულია</div>" };

                return Json(data);
           
            }


            if (!currentitem)
            {

                _kgContext.LessonTableItems.Add(newItem);
                lesson.StudentsCount += 1;
                _kgContext.SaveChanges();

                var data = new { Message = "<div class='alert alert-info'>საგანი დაემატა თქვენს ცხრილს</div>" };

                return Json(data);

                //return AddLessonToTable();
            }

            else
            {

                var data = new { Message = "<div class='alert alert-danger'>ამ დროზე უკვე არჩეული გაქვთ საგანი</div>" };

                return Json(data);
            }

            
        }

     
        public ActionResult Search(int scienceId)
        {
            var science = _kgContext.Sciences.Where(x => x.ScienceId == scienceId).FirstOrDefault();
            var scname = science.Name;

            var searchedlessons = _kgContext.Lessons.Where(x => x.LessonName.Contains(scname));

            ViewBag.ScienceId = new SelectList(_kgContext.Sciences, "ScienceId", "Name", 0);

            return View("AddLessonToTable", searchedlessons);

        }

        public ActionResult LessonTable()
        {
            var username = User.Identity.Name;
            var currentstudent = _kgContext.Students.Where(x => x.Username == username).FirstOrDefault();

            var lessonstable = _kgContext.LessonTableItems.Where(x => x.StudentId == currentstudent.StudentId);

            return View(lessonstable);
        }

        public ActionResult TeacherDetail(int id)
        {
            var teacher = _kgContext.Teachers.Where(x => x.TeacherId == id).FirstOrDefault();

            return View(teacher);
        }

        [ChildActionOnly]
        public ActionResult Disabled()
        {
            return View();
        }

        public ActionResult DeleteLesson(int? id)
        {
            var currentusername = User.Identity.Name;
            var user = _kgContext.Students.Where(x => x.Username == currentusername).FirstOrDefault();

            var deletelesson = _kgContext.LessonTableItems.Where(x => x.StudentId == user.StudentId && x.TeacherId == id).FirstOrDefault();

            _kgContext.LessonTableItems.Remove(deletelesson);
            _kgContext.SaveChanges();

            return RedirectToAction("LessonTable");
        }
    }
}