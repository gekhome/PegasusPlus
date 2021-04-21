using PegasusPlus.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PegasusPlus.Controllers
{
    public class HomeController : Controller
    {
        private PegasusPlusDBEntities db = new PegasusPlusDBEntities();

        [AllowAnonymous]
        public ActionResult Index()
        {
            string userTxt = "(χωρίς σύνδεση)";
            bool AppStatusOn = true;
            try
            {
                AppStatusOn = GetApplicationStatus();
                if (AppStatusOn == false)
                {
                    return RedirectToAction("AppStatusOff", "Home");
                }
            }
            catch
            {
                return RedirectToAction("ErrorConnect", "Home");
            }

            if (isApplicationLocal())
                ViewBag.appTest = true;

            ViewBag.loggedUser = userTxt;
            ViewBag.Title = "Pegasus";
            return View();
        }

        [AllowAnonymous]
        public ActionResult AppStatusOff()
        {
            string message = "";

            message = GetStatusMessage();
            if (string.IsNullOrEmpty(message))
                message = "Η εφαρμογή είναι προσωρινά απενεργοποιημένη για εργασίες συντήρησης και αναβάθμισης.";

            ViewData["message"] = message;
            return View();
        }

        [AllowAnonymous]
        public ActionResult ErrorConnect()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Σύντομη περιγραφή της εφαρμογής";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Στοιχεία επικοινωνίας";

            return View();
        }

        public string GetStatusMessage()
        {
            var data = (from d in db.APP_STATUS select d).FirstOrDefault();

            return (data.STATUS_MESSAGE);
        }

        public bool GetApplicationStatus()
        {
            var data = (from d in db.APP_STATUS select d).FirstOrDefault();
            bool status = data.STATUS_VALUE ?? false;
            return status;
        }

        public bool isApplicationLocal()
        {
            var data = (from d in db.APP_STATUS select d).FirstOrDefault();
            bool status = data.LOCAL_TEST ?? false;
            return status;
        }

    }
}
