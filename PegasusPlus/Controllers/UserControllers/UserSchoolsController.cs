using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using PegasusPlus.BPM;
using PegasusPlus.DAL;
using PegasusPlus.DAL.Security;
using PegasusPlus.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PegasusPlus.Controllers.UserControllers
{
    public class UserSchoolsController : Controller
    {
        private PegasusPlusDBEntities db = new PegasusPlusDBEntities();
        private UserSchools loggedSchool;

        Common c = new Common();

        public ActionResult Login()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
            }
            else
            {
                loggedSchool = db.UserSchools.Where(u => u.Username == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
                if (loggedSchool != null)
                {
                    ViewBag.loggedUser = GetLoginSchool();

                    return RedirectToAction("Index", "School");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserSchoolViewModel model)
        {
            var user = db.UserSchools.Where(u => u.Username == model.Username && u.Password == model.Password).FirstOrDefault();

            if (user != null)
            {
                SchoolPrincipalSerializeModel serializeModel = new SchoolPrincipalSerializeModel();

                serializeModel.UserId = model.UserID;
                serializeModel.Username = model.Username;
                serializeModel.SchoolId = model.UserSchoolID ?? 0;

                string userData = JsonConvert.SerializeObject(serializeModel);
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, user.Username, DateTime.Now, DateTime.Now.AddMinutes(Kerberos.TICKET_TIMEOUT_MINUTES), false, userData);
                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                Response.Cookies.Add(faCookie);

                return RedirectToAction("Index", "School");
            }
            ModelState.AddModelError("", "Το όνομα χρήστη ή/και ο κωδ.πρόσβασης δεν είναι σωστά");
            return View(model);
        }


        [AllowAnonymous]
        public ActionResult LogOut(UserSchools userSchool)
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public UserSchools GetLoginSchool()
        {
            loggedSchool = db.UserSchools.Where(u => u.Username == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();

            int SchoolID = loggedSchool.UserSchoolID ?? 0;
            var _school = (from s in db.sqlUserSchool
                           where s.UserSchoolID == SchoolID
                           select new { s.SchoolName }).FirstOrDefault();

            ViewBag.loggedUser = _school.SchoolName;
            return loggedSchool;
        }

    }
}