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
    public class UserAdminsController : Controller
    {
        private PegasusPlusDBEntities db = new PegasusPlusDBEntities();
        private UserAdmins loggedAdmin;

        public ActionResult Login()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
            }
            else
            {
                loggedAdmin = db.UserAdmins.Where(u => u.Username == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
                if (loggedAdmin != null)
                {
                    ViewBag.loggedUser = loggedAdmin.FullName;
                    //SetLoginStatus(loggedAdmin, true);
                    return RedirectToAction("Index", "Admin");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Username,Password")]  UserAdminViewModel model)
        {
            var user = db.UserAdmins.Where(u => u.Username == model.Username && u.Password == model.Password).FirstOrDefault();

            if (user != null)
            {
                AdminPrincipalSerializeModel serializeModel = new AdminPrincipalSerializeModel();
                serializeModel.UserId = model.UserID;
                serializeModel.Username = model.Username;
                serializeModel.FullName = model.FullName;

                string userData = JsonConvert.SerializeObject(serializeModel);
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, user.Username, DateTime.Now, DateTime.Now.AddMinutes(Kerberos.TICKET_TIMEOUT_MINUTES), false, userData);
                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                Response.Cookies.Add(faCookie);

                return RedirectToAction("Index", "Admin");
            }
            ModelState.AddModelError("", "Το όνομα χρήστη ή/και ο κωδ.πρόσβασης δεν είναι σωστά");
            return View(model);
        }

        public ActionResult Login2()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
            }
            else
            {
                loggedAdmin = db.UserAdmins.Where(u => u.Username == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
                if (loggedAdmin != null)
                {
                    ViewBag.loggedUser = loggedAdmin.FullName;
                    //SetLoginStatus(loggedAdmin, true);
                    return RedirectToAction("Index", "Admin");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login2([Bind(Include = "Username,Password")]  UserAdminViewModel model)
        {
            var user = db.UserAdmins.Where(u => u.Username == model.Username && u.Password == model.Password).FirstOrDefault();

            if (user != null)
            {
                WriteUserCookie(model);

                return RedirectToAction("UserTeachersList", "UserTeachers");
            }
            ModelState.AddModelError("", "Το όνομα χρήστη ή/και ο κωδ.πρόσβασης δεν είναι σωστά");
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult LogOut(UserAdmins userAdmin)
        {
            var user = db.UserAdmins.Where(u => u.Username == userAdmin.Username && u.Password == userAdmin.Password).FirstOrDefault();

            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public void WriteUserCookie(UserAdminViewModel user)
        {
            // var roles = user.ROLES.Select(m => m.ROLE_NAME).ToArray();

            AdminPrincipalSerializeModel serializeModel = new AdminPrincipalSerializeModel();
            serializeModel.UserId = user.UserID;
            serializeModel.Username = user.Username;
            serializeModel.FullName = user.FullName;
            //serializeModel.roles = roles;

            string userData = JsonConvert.SerializeObject(serializeModel);
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, user.Username, DateTime.Now, DateTime.Now.AddMinutes(Kerberos.TICKET_TIMEOUT_MINUTES), false, userData);
            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(faCookie);
        }

        public UserAdmins GetLoginAdmin()
        {
            loggedAdmin = db.UserAdmins.Where(u => u.Username == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
            ViewBag.loggedUser = loggedAdmin.FullName;

            return loggedAdmin;
        }

        public List<UserAdminViewModel> GetAdminList()
        {
            var admins = (from a in db.UserAdmins
                          select new UserAdminViewModel
                          {
                              UserID = a.UserID,
                              Username = a.Username,
                              Password = a.Password,
                              FullName = a.FullName,
                              CreateDate = a.CreateDate,
                          }).ToList();

            return admins;
        }

        public ActionResult AdminAccounts()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMINS");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        #region Grid CRUD Functions

        public ActionResult Admin_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetAdminList();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Admin_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<UserAdminViewModel> userAdmins)
        {
            var results = new List<UserAdminViewModel>();
            foreach (var userAdmin in userAdmins)
            {
                if (userAdmin != null && ModelState.IsValid)
                {
                    UserAdmins newUserAdmin = new UserAdmins()
                    {
                        Username = userAdmin.Username,
                        Password = userAdmin.Password,
                        FullName = userAdmin.FullName,
                        CreateDate = userAdmin.CreateDate
                    };
                    db.UserAdmins.Add(newUserAdmin);
                    db.SaveChanges();
                    results.Add(userAdmin);
                }
            }
            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Admin_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<UserAdminViewModel> userAdmins)
        {
            if (userAdmins != null && ModelState.IsValid)
            {
                foreach (var userAdmin in userAdmins)
                {
                    UserAdmins entity = db.UserAdmins.Where(m => m.UserID.Equals(userAdmin.UserID)).FirstOrDefault();

                    entity.Username = userAdmin.Username;
                    entity.Password = userAdmin.Password;
                    entity.FullName = userAdmin.FullName;
                    entity.CreateDate = userAdmin.CreateDate;

                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return Json(userAdmins.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Admin_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<UserAdminViewModel> userAdmins)
        {
            if (userAdmins.Any())
            {
                foreach (var userAdmin in userAdmins)
                {
                    if (userAdmin != null)
                    {
                        UserAdmins modifiedAdmin = db.UserAdmins.Find(userAdmin.UserID);
                        db.Entry(modifiedAdmin).State = EntityState.Deleted;
                        db.UserAdmins.Remove(modifiedAdmin);
                        db.SaveChanges();
                    }
                }
            }
            return Json(userAdmins.ToDataSourceResult(request, ModelState));
        }

        #endregion

    }
}