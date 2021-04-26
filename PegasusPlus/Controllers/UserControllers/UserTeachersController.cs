using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using PegasusPlus.BPM;
using PegasusPlus.DAL;
using PegasusPlus.DAL.Security;
using PegasusPlus.Models;
using PegasusPlus.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PegasusPlus.Controllers.UserControllers
{
    public class UserTeachersController : Controller
    {
        private PegasusPlusDBEntities db = new PegasusPlusDBEntities();
        Kerberos kerberos = new Kerberos();


        public ActionResult TaxisNetLogin()
        {
            // ---- This is the actual Url and it works ------------
            string url = "http://auth.oaed.gr/Default.aspx?iek=true";
            return new RedirectResult(url);
        }

        #region NEW USER-REGISTER USING TAXISnet

        // GET: RegisterUser <- accepts a random integer as Id of ΑΦΜ
        public ActionResult RegisterUser(int rnd_numberID = 0, string Afm = null)
        {
            ViewBag.loggedUser = "(χωρίς σύνδεση)";
            string userAFM = "";

            if (!string.IsNullOrEmpty(Afm))
                userAFM = Afm;
            else
                userAFM = GetTaxisnetAfm(rnd_numberID);

            if (string.IsNullOrEmpty(userAFM))
            {
                string msg = "Δεν βρέθηκαν στοιχεία εισόδου (ΑΦΜ) από το TAXISnet";
                return RedirectToAction("ErrorUser", "UserTeachers", new { notify = msg });
            }
            UserTeacherViewModel model = GetUserTeacherFromDB(userAFM);
            if (model == null)
            {
                UserTeacherViewModel newmodel = new UserTeacherViewModel()
                {
                    UserAfm = userAFM,
                    Username = userAFM,
                    Password = "XXXXXXXXXX",
                    CreateDate = DateTime.Now
                };
                return View(newmodel);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult RegisterUser(UserTeacherViewModel UserTeacher, int rnd_numberID = 0, string Afm = null)
        {
            string userAFM = "";

            if (!string.IsNullOrEmpty(Afm))
                userAFM = Afm;
            else
                userAFM = GetTaxisnetAfm(rnd_numberID);

            var user = GetUserTeacherFromDB(userAFM);
            if (user != null)
            {
                WriteUserCookie(user);
                DeleteTaxisRecord(rnd_numberID);

                return RedirectToAction("Index", "Teachers");
            }

            // User does not exist, so create one
            if (ModelState.IsValid)
            {
                UserTeachers entity = new UserTeachers()
                {
                    UserAfm = userAFM,
                    Username = userAFM,
                    Password = "XXXXXXXXXX",
                    CreateDate = DateTime.Now
                };
                db.UserTeachers.Add(entity);
                db.SaveChanges();

                UserTeacherViewModel data = GetUserTeacherFromDB(userAFM);
                WriteUserCookie(data);
                DeleteTaxisRecord(rnd_numberID);

                return RedirectToAction("Index", "Teachers");
            }
            UserTeacher.UserAfm = userAFM;
            UserTeacher.Username = userAFM;
            UserTeacher.Password = "XXXXXXXXXX";
            return View(UserTeacher);
        }

        public void WriteUserCookie(UserTeacherViewModel user)
        {
            if (user != null)
            {
                CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                serializeModel.UserId = user.UserID;
                serializeModel.Username = user.Username;
                serializeModel.Afm = user.UserAfm;

                string userData = JsonConvert.SerializeObject(serializeModel);
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, user.UserAfm, DateTime.Now, DateTime.Now.AddMinutes(Kerberos.TICKET_TIMEOUT_MINUTES), false, userData);
                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                Response.Cookies.Add(faCookie);
            }
        }

        public void DeleteTaxisRecord(int rnd_numberID = 0)
        {
            var data = (from d in db.TAXISNET where d.RANDOM_NUMBER == rnd_numberID select d).FirstOrDefault();
            if (data == null)
                return;

            TAXISNET entity = db.TAXISNET.Find(data.TAXISNET_ID);
            if (entity != null)
            {
                db.Entry(entity).State = EntityState.Deleted;
                db.TAXISNET.Remove(entity);
                db.SaveChanges();
            };
        }

        public UserTeacherViewModel GetUserTeacherFromDB(string Afm)
        {
            var data = (from d in db.UserTeachers
                        where d.UserAfm == Afm
                        select new UserTeacherViewModel
                        {
                            UserID = d.UserID,
                            UserAfm = d.UserAfm,
                            Username = d.Username,
                            Password = d.Password,
                            CreateDate = d.CreateDate
                        }).FirstOrDefault();
            return (data);
        }

        #endregion


        #region USER-TEACHER SELECTOR FOR TESTING

        public ActionResult UserTeachersList()
        {
            string userTxt = "(χωρίς σύνδεση)";
            ViewBag.loggedUser = userTxt;

            return View();
        }

        public ActionResult UserTeacher_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetUserTeacherListFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<UserTeacherSelectViewModel> GetUserTeacherListFromDB()
        {
            var users = (from a in db.sqlUserTeacherSelect
                         orderby a.FullName
                         select new UserTeacherSelectViewModel
                         {
                             UserID = a.UserID,
                             UserAfm = a.UserAfm,
                             CreateDate = a.CreateDate,
                             FullName = a.FullName
                         }).ToList();

            return users;
        }

        #endregion


        #region GETTERS AND MISCELLANEOUS

        public string GetTaxisnetAfm(int rnd_numberID = 0)
        {
            string safm = "";
            var data = (from d in db.TAXISNET where d.RANDOM_NUMBER == rnd_numberID select d).FirstOrDefault();
            if (data != null)
            {
                safm = data.TAXISNET_AFM;
            }
            return safm;
        }

        public bool AccountExists(string safm)
        {
            var user = db.UserTeachers.Where(u => u.UserAfm == safm).FirstOrDefault();

            if (user != null) return true;
            else return false;

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

        [AllowAnonymous]
        public ActionResult LogOut(UserTeachers userTeacher)
        {
            var user = db.UserTeachers.Where(u => u.Username == userTeacher.Username).FirstOrDefault();

            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }


        public ActionResult Error(string notify = null)
        {
            if (notify != null)
                this.AddNotification(notify, NotificationType.WARNING);

            return View();
        }

        public ActionResult ErrorUser(string notify = null)
        {
            if (notify != null)
                this.AddNotification(notify, NotificationType.WARNING);

            return View();
        }

        #endregion
    }
}