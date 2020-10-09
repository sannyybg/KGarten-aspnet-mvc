using Kindergarten3.Context;
using Kindergarten3.Filters;
using Kindergarten3.Models;
using Kindergarten3.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Kindergarten3.Controllers
{
    [SessionFilter]
    public class AccountController : Controller
    {

        KgContext _kgContext = new KgContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SignUpStudent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUpStudent(Student newstudent)
        {
            if (!ModelState.IsValid)
            {
                List<KeyValuePair<string, ModelState>> errorList = ModelState.Where(x => x.Value.Errors.Count > 0).ToList();

                ViewBag.ErrorList = errorList;
                return SignUpStudent();
            }

            var currentstudent = _kgContext.Students.Where(x => x.Username.ToLower() == newstudent.Username.ToLower()).FirstOrDefault();

            if (currentstudent != null)
            {
                ModelState.AddModelError("Username", "ეს username უკვე დამატებულია");
                return SignUpStudent();
            }

            newstudent.Roles = _kgContext.Roles.Where(x =>x.RoleName == "Student").ToList();

            _kgContext.Students.Add(newstudent);
            _kgContext.SaveChanges();
            return RedirectToAction("LogIn");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LogInViewModel user)
        {
            var adminexists = _kgContext.Admins.Any(x => x.Username == user.Username && x.Password == user.Password);
            var teacherexists = _kgContext.Teachers.Any(x => x.Username == user.Username && x.Password == user.Password);
            var studentexists = _kgContext.Students.Include("Roles").Where(x => x.Username == user.Username && x.Password == user.Password).FirstOrDefault();

            if(adminexists)
            {
                
                FormsAuthentication.SetAuthCookie(user.Username, false);
                return RedirectToAction("index", "Admin");
            }

            if (studentexists != null)
            {
                ViewBag.userRole = 3;
                FormsAuthentication.SetAuthCookie(user.Username, false);

                var userr = _kgContext.Students.Where(x => x.Username == user.Username).FirstOrDefault();

                

                if (userr.isActive)
                {
                    if (userr.BirthDate.Month == DateTime.Now.Month && userr.BirthDate.Day == DateTime.Now.Day)
                    {
                        ViewBag.Name = userr.FirstName;
                        return View("Birthday");
                    }
                    return RedirectToAction("index", "Admin");
                }

                else
                {
                    
                    userr.isActive = true;
                    _kgContext.SaveChanges();
                    return RedirectToAction("ChangePass");
                }
                
            }

            if (teacherexists)
            {
                FormsAuthentication.SetAuthCookie(user.Username, false);

                var userr = _kgContext.Teachers.Where(x => x.Username == user.Username).FirstOrDefault();

                if (userr.isActive)
                {
                    return RedirectToAction("index", "Admin");
                }

                else
                {
                    userr.isActive = true;
                    _kgContext.SaveChanges();
                    return RedirectToAction("ChangePass");
                }
            }

            else
            {
                ModelState.AddModelError("", "Incorrect Username or Password");
                return View();
            }

            
        }

        public ActionResult ChangePass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePass(ChangePassViewModel newpass)
        {
            var username = User.Identity.Name;
            var userstudent = _kgContext.Students.Where(x => x.Username == username).FirstOrDefault();
            var userteacher = _kgContext.Teachers.Where(x => x.Username == username).FirstOrDefault();
            var useradmin = _kgContext.Admins.Where(x => x.Username == username).FirstOrDefault();

            if (userstudent != null)
            {

                    if (userstudent.Password == newpass.CurrentPassword && newpass.NewPassword == newpass.ConfirmPassword)
                    {
                        userstudent.Password = newpass.NewPassword;
                        _kgContext.SaveChanges();
                        FormsAuthentication.SignOut();
                        Session.Abandon();
                        return View("Login");
                    }

                    if (userstudent.Password != newpass.CurrentPassword)
                    {
                        ModelState.AddModelError("CurrentPassword", "ძველი პაროლი არასწორია");
                        return RedirectToAction("ChangePass");
                    }

                    if (newpass.ConfirmPassword != newpass.NewPassword)
                    {
                        ModelState.AddModelError("ConfirmPassowrd", "ახალი პაროლები არ ემთხევა");
                        return RedirectToAction("ChangePass");
                    }

                    else
                    {
                        return RedirectToAction("ChangePass");
                    }


            }

            if (userteacher != null)
            {
                
                    if (userteacher.Password == newpass.CurrentPassword && newpass.NewPassword == newpass.ConfirmPassword)
                    {
                        userteacher.Password = newpass.NewPassword;
                        _kgContext.SaveChanges();
                        FormsAuthentication.SignOut();
                        Session.Abandon();
                        return View("Login");
                    }

                    if (userteacher.Password != newpass.CurrentPassword)
                    {
                        ModelState.AddModelError("CurrentPassword", "ძველი პაროლი არასწორია");
                        return RedirectToAction("ChangePass");
                    }

                    if (newpass.ConfirmPassword != newpass.NewPassword)
                    {
                        ModelState.AddModelError("ConfirmPassowrd", "ახალი პაროლები არ ემთხევა");
                        return RedirectToAction("ChangePass");
                    }

                    else
                    {
                        return RedirectToAction("ChangePass");
                    }

            }

            if (useradmin != null)
            {
                
                    if (useradmin.Password == newpass.CurrentPassword && newpass.NewPassword == newpass.ConfirmPassword)
                    {
                        useradmin.Password = newpass.NewPassword;
                        _kgContext.SaveChanges();
                        FormsAuthentication.SignOut();
                        Session.Abandon();
                        return View("Login");
                    }

                    if (useradmin.Password != newpass.CurrentPassword)
                    {
                        ModelState.AddModelError("CurrentPassword", "ძველი პაროლი არასწორია");
                        return RedirectToAction("ChangePass");
                    }

                    if (newpass.ConfirmPassword != newpass.NewPassword)
                    {
                        ModelState.AddModelError("ConfirmPassowrd", "ახალი პაროლები არ ემთხევა");
                        return RedirectToAction("ChangePass");
                    }

                    else
                    {
                        return RedirectToAction("ChangePass");
                    }
            }

            else
            {
                return RedirectToAction("ChangePass");
            }


        }


        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login");
        }

        [ChildActionOnly]
        public ActionResult Birthday()
        {
            return View();
        }


    }
}