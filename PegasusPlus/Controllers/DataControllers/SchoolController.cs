using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

using PegasusPlus.DAL;
using PegasusPlus.Models;
using PegasusPlus.BPM;
using PegasusPlus.Extensions;

namespace PegasusPlus.Controllers.DataControllers
{
    public class SchoolController : Controller
    {
        private PegasusPlusDBEntities db = new PegasusPlusDBEntities();
        private UserSchools loggedSchool;
        private int prokirixiId;

        Common c = new Common();


        public ActionResult Index(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "UserSchools");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            prokirixiId = c.GetActiveProkirixiID();

            if (notify != null)
            {
                this.AddNotification(notify, NotificationType.WARNING);
            }
            return View();
        }



        #region GETTERS

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

        #endregion
    }
}