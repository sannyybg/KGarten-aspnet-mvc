using Kindergarten3.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kindergarten3.Filters
{
    public class SessionFilter : ActionFilterAttribute
    {
        KgContext _kgContext = new KgContext();
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var username = HttpContext.Current.User.Identity.Name;

            if (!string.IsNullOrEmpty(username))
            {
                var userRoles = (List<string>)filterContext.HttpContext.Session["UserRoles"];
                if (userRoles == null)
                {
                    var studentuser = _kgContext.Students.Include("Roles").Where(x => x.Username == username).FirstOrDefault();
                    var teacheruser = _kgContext.Teachers.Include("Roles").Where(x => x.Username == username).FirstOrDefault();
                    var adminuser = _kgContext.Admins.Include("Roles").Where(x => x.Username == username).FirstOrDefault();

                    if (studentuser != null)
                    {
                        filterContext.HttpContext.Session["UserRoles"] = studentuser.Roles.Select(x => x.RoleName).ToList();
                    }

                    if (teacheruser != null)
                    {
                        filterContext.HttpContext.Session["UserRoles"] = teacheruser.Roles.Select(x => x.RoleName).ToList();
                    }

                    if (adminuser != null)
                    {
                        filterContext.HttpContext.Session["UserRoles"] = adminuser.Roles.Select(x => x.RoleName).ToList();
                    }

                }
            }
        }

    }
}