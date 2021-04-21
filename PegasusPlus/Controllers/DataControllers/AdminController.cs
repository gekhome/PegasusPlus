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
using PegasusPlus.Notification;

namespace PegasusPlus.Controllers.DataControllers
{
    public class AdminController : Controller
    {
        private PegasusPlusDBEntities db = new PegasusPlusDBEntities();
        private UserAdmins loggedAdmin;
        private int prokirixiId;
        Common c = new Common();

        public ActionResult Index(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "UserAdmins");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            if (notify != null)
            {
                this.ShowMessage(MessageType.Warning, notify);
            }
            return View();
        }




        #region ΜΗΤΡΩΟ ΕΚΠΑΙΔΕΥΤΙΚΩΝ


        public ActionResult TeacherDataPrint()
        {
            prokirixiId = c.GetAdminProkirixiID();

            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "UserAdmins");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();

                TeacherRegistryParameters parameters = new TeacherRegistryParameters();
                parameters.SchoolID = 1;
                parameters.ProkirixiID = prokirixiId;

                return View(parameters);
            }
        }

        #endregion



        #region GETTERS


        public UserAdmins GetLoginAdmin()
        {
            loggedAdmin = db.UserAdmins.Where(u => u.Username == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
            ViewBag.loggedAdmin = loggedAdmin;
            ViewBag.loggedUser = loggedAdmin.FullName;
            return loggedAdmin;
        }

        #endregion
    }
}