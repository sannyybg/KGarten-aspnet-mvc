using Kindergarten3.Context;
using Kindergarten3.Filters;
using Kindergarten3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kindergarten3.Controllers
{
    [SessionFilter]
    [Authorize]
    public class AdminController : Controller
    {
        KgContext _kgContext = new KgContext();


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddScience()
        {
            return View(new Science());
        }

        [HttpPost]
        public ActionResult AddScience(Science newscience)
        {
            if (!ModelState.IsValid)
            {
                List<KeyValuePair<string, ModelState>> errorList = ModelState.Where(x => x.Value.Errors.Count > 0).ToList();

                ViewBag.ErrorList = errorList;
                return AddScience();
            }

            var result = _kgContext.Sciences.Where(x => x.Name.ToLower() == newscience.Name.ToLower()).FirstOrDefault();

            if (result != null)
            {
                ModelState.AddModelError("Name", "ეს საგანი უკვე დამატებულია");
                return AddScience();
            }



            _kgContext.Sciences.Add(newscience);
            _kgContext.SaveChanges();

            return RedirectToAction("index");
        }

        public ActionResult AddTeacher()
        {
            return View(new Teacher());
        }

        [HttpPost]
        public ActionResult AddTeacher(Teacher newteacher)
        {
            if (!ModelState.IsValid)
            {
                List<KeyValuePair<string, ModelState>> errorList = ModelState.Where(x => x.Value.Errors.Count > 0).ToList();

                ViewBag.ErrorList = errorList;
                return AddTeacher();
            }

            var result = _kgContext.Teachers.Where(x => x.Username.ToLower() == newteacher.Username.ToLower()).FirstOrDefault();

            if (result != null)
            {
                ModelState.AddModelError("Username", "ეს მასწავლებელი უკვე დამატებულია");
                return AddTeacher();
            }

            var image = Request.Files["Photo"];

            if (image != null)
            {
                var filename = Path.GetFileNameWithoutExtension(image.FileName);
                var ext = Path.GetExtension(image.FileName);
                filename = $"{filename}{DateTime.Now.ToString("yymmssfff")}{ext}";
                newteacher.PhotoURL = $"/App_Files/Images/{filename}";
                image.SaveAs(Path.Combine(Server.MapPath("~/App_Files/Images/"), filename));
            }

            Uri uri = new Uri(Request.Url.AbsoluteUri);

            var urlHost = uri.Scheme + Uri.SchemeDelimiter + uri.Host + ":" + uri.Port;

            var text = $"Username: {newteacher.Username}, Password: {newteacher.Password}, სისტემაში შესასვლელად გადადით ლინკზე: {urlHost}/Account/Login";
            //SendMeil(newteacher.Email, text);

            newteacher.Roles = _kgContext.Roles.Where(x => x.RoleName == "Teacher").ToList();

            _kgContext.Teachers.Add(newteacher);
            _kgContext.SaveChanges();

            return RedirectToAction("index");
        }

        public ActionResult AddLesson()
        {
            ViewBag.TeacherId = new SelectList(_kgContext.Teachers, "TeacherId", "Username", 1);
            ViewBag.ScienceId = new SelectList(_kgContext.Sciences, "ScienceId", "Name", 1);
            return View(new Lesson());
        }

        [HttpPost]
        public ActionResult AddLesson(Lesson newlesson)
        {
            if (!ModelState.IsValid)
            {
                List<KeyValuePair<string, ModelState>> errorList = ModelState.Where(x => x.Value.Errors.Count > 0).ToList();

                ViewBag.ErrorList = errorList;
                return AddLesson();
            }

            var science = _kgContext.Sciences.Where(x => x.ScienceId == newlesson.ScienceId).FirstOrDefault();
            string sciencename = science.Name;

            newlesson.LessonName = sciencename + " " + newlesson.Dayofweek + " " + newlesson.StartHour;

            _kgContext.Lessons.Add(newlesson);
            _kgContext.SaveChanges();

            return RedirectToAction("index");
        }

        private void SendMeil(string to, string text)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.From = new System.Net.Mail.MailAddress("sannyybg@gmail.com");
            mail.To.Add(to);
            mail.Subject = "Kindergarten Username, password";
            mail.Body = text;
            mail.IsBodyHtml = true;

            System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient("in-v3.mailjet.com");
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("b75cfa38ea9ea632fd334a6cd62ba54d", "6ac57d919fbb7d38f1b87ed7895df893");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);

        }

        public ActionResult ActivationTable()
        {
            var act = _kgContext.ActivateTables.Where(x => x.Name == "Tableactivation").FirstOrDefault();

            return View(act);
        }

        [HttpPost]
        public ActionResult ActivationTable(ActivateTable newact)
        {
            newact.Name = "Tableactivation";
            _kgContext.Entry(newact).State = System.Data.Entity.EntityState.Modified;
            _kgContext.SaveChanges();

            return View("Index");
        }

    }
}