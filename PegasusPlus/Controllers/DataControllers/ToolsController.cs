using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

using PegasusPlus.DAL;
using PegasusPlus.Models;
using PegasusPlus.BPM;
using PegasusPlus.Extensions;

namespace PegasusPlus.Controllers.DataControllers
{
    public class ToolsController : Controller
    {
        private PegasusPlusDBEntities db = new PegasusPlusDBEntities();
        Common c = new Common();

        private UserAdmins loggedAdmin;
        private int prokirixiId;


        #region ΣΧΟΛΙΚΑ ΕΤΗ

        public ActionResult SchoolYearsList()
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
            return View();
        }

        public ActionResult SchoolYear_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetSchoolYearsFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SchoolYear_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<SchoolYearsViewModel> data)
        {
            var results = new List<SchoolYearsViewModel>();

            foreach (var item in data)
            {
                var existingSchoolYears = db.SysSchoolYears.Where(m => m.SchoolYearText == item.SchoolYearText).Count();
                if (existingSchoolYears > 0)
                    ModelState.AddModelError("", "Το σχολικό έτος είναι ήδη καταχωρημένο. Η διαδικασία ακυρώθηκε.");

                if (item != null && ModelState.IsValid)
                {
                    SysSchoolYears newData = new SysSchoolYears()
                    {
                        SchoolYearText = item.SchoolYearText,
                        DateStart = item.DateStart,
                        DateEnd = item.DateEnd,
                    };
                    db.SysSchoolYears.Add(newData);
                    results.Add(item);
                    db.SaveChanges();
                }
            }

            return Json(data.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SchoolYear_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<SchoolYearsViewModel> data)
        {
            foreach (var item in data)
            {
                if (item != null & ModelState.IsValid)
                {
                    SysSchoolYears entity = db.SysSchoolYears.Find(item.SchoolYearID);

                    entity.SchoolYearText = item.SchoolYearText;
                    entity.DateStart = item.DateStart;
                    entity.DateEnd = item.DateEnd;

                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return Json(data.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SchoolYear_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<SchoolYearsViewModel> data)
        {
            if (data.Any())
            {
                foreach (var row in data)
                {
                    if (row != null)
                    {
                        SysSchoolYears entity = db.SysSchoolYears.Find(row.SchoolYearID);

                        db.Entry(entity).State = EntityState.Deleted;
                        db.SysSchoolYears.Remove(entity);
                        db.SaveChanges();
                    }
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState));
        }

        public List<SchoolYearsViewModel> GetSchoolYearsFromDB()
        {

            var data = (from d in db.SysSchoolYears
                        orderby d.SchoolYearText
                        select new SchoolYearsViewModel
                        {
                            SchoolYearID = d.SchoolYearID,
                            SchoolYearText = d.SchoolYearText,
                            DateStart = d.DateStart,
                            DateEnd = d.DateEnd,
                        }).ToList();

            return data;
        }

        #endregion


        #region ΑΙΤΙΕΣ ΑΠΟΚΛΕΙΣΜΟΥ

        public ActionResult ApokleismoiList()
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

            return View();
        }

        [HttpPost]
        public ActionResult Apokleismos_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetApokleismoiFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Apokleismos_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<ApoklismoiViewModel> data)
        {
            foreach (var item in data)
            {
                if (item != null && ModelState.IsValid)
                {
                    SysApoklismoi newData = new SysApoklismoi()
                    {
                        ApoklismosText = item.ApoklismosText,
                    };
                    db.SysApoklismoi.Add(newData);
                    db.SaveChanges();
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Apokleismos_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<ApoklismoiViewModel> data)
        {
            foreach (var item in data)
            {
                if (item != null && ModelState.IsValid)
                {
                    SysApoklismoi entity = db.SysApoklismoi.Find(item.ApoklismosID);
                    entity.ApoklismosText = item.ApoklismosText;

                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return Json(data.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Apokleismos_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<ApoklismoiViewModel> data)
        {
            if (data.Any())
            {
                foreach (var row in data)
                {
                    if (row != null)
                    {
                        SysApoklismoi entity = db.SysApoklismoi.Find(row.ApoklismosID);

                        db.Entry(entity).State = EntityState.Deleted;
                        db.SysApoklismoi.Remove(entity);
                        db.SaveChanges();
                    }
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState));
        }

        public List<ApoklismoiViewModel> GetApokleismoiFromDB()
        {

            var data = (from d in db.SysApoklismoi
                        orderby d.ApoklismosText
                        select new ApoklismoiViewModel
                        {
                            ApoklismosID = d.ApoklismosID,
                            ApoklismosText = d.ApoklismosText,
                        }).ToList();

            return data;
        }

        #endregion


        #region ΑΦΟΡΟΛΟΓΗΤΑ ΕΙΣΟΔΗΜΑΤΑ

        public ActionResult TaxfreeList()
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
            return View();
        }

        public ActionResult Taxfree_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetTaxfreeFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Taxfree_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<TaxFreeViewModel> data)
        {
            foreach (var item in data)
            {
                if (item != null && ModelState.IsValid)
                {
                    SysTaxFree newData = new SysTaxFree()
                    {
                        YearText = item.YearText,
                        TaxFree = item.TaxFree,
                        Nomisma = item.Nomisma
                    };
                    db.SysTaxFree.Add(newData);
                    db.SaveChanges();
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Taxfree_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<TaxFreeViewModel> data)
        {
            foreach (var item in data)
            {
                if (item != null && ModelState.IsValid)
                {
                    SysTaxFree entity = db.SysTaxFree.Find(item.YearID);

                    entity.YearText = item.YearText;
                    entity.TaxFree = item.TaxFree;
                    entity.Nomisma = item.Nomisma;

                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return Json(data.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Taxfree_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<TaxFreeViewModel> data)
        {
            if (data.Any())
            {
                foreach (var row in data)
                {
                    if (row != null)
                    {
                        SysTaxFree entity = db.SysTaxFree.Find(row.YearID);

                        db.Entry(entity).State = EntityState.Deleted;
                        db.SysTaxFree.Remove(entity);
                        db.SaveChanges();
                    }
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState));
        }

        public List<TaxFreeViewModel> GetTaxfreeFromDB()
        {
            var data = (from d in db.SysTaxFree
                        orderby d.YearText
                        select new TaxFreeViewModel
                        {
                            YearID = d.YearID,
                            YearText = d.YearText,
                            TaxFree = d.TaxFree,
                            Nomisma = d.Nomisma,
                        }).ToList();

            return data;
        }

        public ActionResult TaxFreeIncomePrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "Admins");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
                return View();
            }
        }

        #endregion


        #region ΝΟΜΟΙ ΚΑΙ ΠΕΡΙΦΕΡΕΙΕΣ

        public ActionResult PeriferiesNomoi()
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

            return View();
        }


        #region ΠΕΡΙΦΕΡΕΙΕΣ ΠΛΕΓΜΑ

        public ActionResult PeriferiaMajor_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetPeriferiaMajorFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PeriferiaMajor_Create([DataSourceRequest] DataSourceRequest request, PeriferiaMajorViewModel data)
        {
            var newdata = new PeriferiaMajorViewModel();

            var existingData = db.SysPeriferiesMajor.Where(s => s.PeriferiaMajor == data.PeriferiaMajor).Count();
            if (existingData > 0)
                ModelState.AddModelError("", "Ο Περιφέρεια αυτή είναι ήδη καταχωρημένη. Η αποθήκευση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                SysPeriferiesMajor entity = new SysPeriferiesMajor()
                {
                    PeriferiaMajor = data.PeriferiaMajor,
                };
                db.SysPeriferiesMajor.Add(entity);
                db.SaveChanges();
                data.PeriferiaMajorID = entity.PeriferiaMajorID;
                newdata = RefreshPeriferiaMajorFromDB(data.PeriferiaMajorID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PeriferiaMajor_Update([DataSourceRequest] DataSourceRequest request, PeriferiaMajorViewModel data)
        {
            var newdata = new PeriferiaMajorViewModel();

            if (data != null && ModelState.IsValid)
            {
                SysPeriferiesMajor entity = db.SysPeriferiesMajor.Find(data.PeriferiaMajorID);
                entity.PeriferiaMajor = data.PeriferiaMajor;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                newdata = RefreshPeriferiaMajorFromDB(data.PeriferiaMajorID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PeriferiaMajor_Destroy([DataSourceRequest] DataSourceRequest request, PeriferiaMajorViewModel data)
        {
            if (data != null)
            {
                SysPeriferiesMajor entity = db.SysPeriferiesMajor.Find(data.PeriferiaMajorID);

                if (!CanDeletePeriferia(data.PeriferiaMajorID))
                    ModelState.AddModelError("", "Δεν μπορεί να διαγραφεί η Περιφέρεια διότι έχει συσχετισμένους Νομούς.");

                if (entity != null && ModelState.IsValid)
                {
                    db.Entry(entity).State = EntityState.Deleted;
                    db.SysPeriferiesMajor.Remove(entity);
                    db.SaveChanges();
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public PeriferiaMajorViewModel RefreshPeriferiaMajorFromDB(int recordId)
        {
            var data = (from d in db.SysPeriferiesMajor
                        where d.PeriferiaMajorID == recordId
                        select new PeriferiaMajorViewModel
                        {
                            PeriferiaMajorID = d.PeriferiaMajorID,
                            PeriferiaMajor = d.PeriferiaMajor
                        }).FirstOrDefault();

            return data;
        }

        public List<PeriferiaMajorViewModel> GetPeriferiaMajorFromDB()
        {
            var data = (from d in db.SysPeriferiesMajor
                        orderby d.PeriferiaMajor
                        select new PeriferiaMajorViewModel
                        {
                            PeriferiaMajorID = d.PeriferiaMajorID,
                            PeriferiaMajor = d.PeriferiaMajor
                        }).ToList();

            return data;
        }

        public bool CanDeletePeriferia(int periferiaId)
        {
            var data = (from d in db.SysNomoi where d.PeriferiaMajorID == periferiaId select d).Count();
            if (data > 0)
                return false;
            return true;
        }

        #endregion

        #region ΝΟΜΟΙ ΠΛΕΓΜΑ

        public ActionResult Nomos_Read([DataSourceRequest] DataSourceRequest request, int periferiaId = 0)
        {
            var data = GetNomosFromDB(periferiaId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Nomos_Create([DataSourceRequest] DataSourceRequest request, NomosViewModel data, int periferiaId = 0)
        {
            var newdata = new NomosViewModel();

            var existingData = db.SysNomoi.Where(s => s.NomosText == data.NomosText).Count();
            if (existingData > 0)
                ModelState.AddModelError("", "Ο Νομός αυτός είναι ήδη καταχωρημένος. Η αποθήκευση ακυρώθηκε.");

            if (periferiaId == 0)
                ModelState.AddModelError("", "Πρέπει να επιλέξετε Περιφέρεια πρώτα. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                SysNomoi entity = new SysNomoi()
                {
                    NomosText = data.NomosText,
                    PeriferiaMajorID = periferiaId
                };
                db.SysNomoi.Add(entity);
                db.SaveChanges();
                data.NomosID = entity.NomosID;
                newdata = RefreshNomosFromDB(data.NomosID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Nomos_Update([DataSourceRequest] DataSourceRequest request, NomosViewModel data, int periferiaId = 0)
        {
            var newdata = new NomosViewModel();

            if (periferiaId == 0)
                ModelState.AddModelError("", "Πρέπει να επιλέξετε Περιφέρεια πρώτα. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                SysNomoi entity = db.SysNomoi.Find(data.NomosID);
                entity.NomosText = data.NomosText;
                entity.PeriferiaMajorID = periferiaId;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                newdata = RefreshNomosFromDB(data.NomosID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Nomos_Destroy([DataSourceRequest] DataSourceRequest request, NomosViewModel data)
        {
            if (data != null)
            {
                SysNomoi entity = db.SysNomoi.Find(data.NomosID);
                if (entity != null && ModelState.IsValid)
                {
                    db.Entry(entity).State = EntityState.Deleted;
                    db.SysNomoi.Remove(entity);
                    db.SaveChanges();
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public NomosViewModel RefreshNomosFromDB(int recordId)
        {
            var data = (from d in db.SysNomoi
                        where d.NomosID == recordId
                        select new NomosViewModel
                        {
                            NomosID = d.NomosID,
                            NomosText = d.NomosText,
                            PeriferiaMajorID = d.PeriferiaMajorID
                        }).FirstOrDefault();

            return data;
        }

        public List<NomosViewModel> GetNomosFromDB(int periferiaID = 0)
        {
            List<NomosViewModel> data = new List<NomosViewModel>();

            if(periferiaID > 0)
            {
                data = (from d in db.SysNomoi where d.PeriferiaMajorID == periferiaID orderby d.NomosText select new NomosViewModel
                        {
                            NomosID = d.NomosID,
                            NomosText = d.NomosText,
                            PeriferiaMajorID = d.PeriferiaMajorID
                        }).ToList();
            }
            else
            {
                data = (from d in db.SysNomoi orderby d.NomosText select new NomosViewModel
                        {
                            NomosID = d.NomosID,
                            NomosText = d.NomosText,
                            PeriferiaMajorID = d.PeriferiaMajorID
                        }).ToList();
            }

            return data;
        }

        #endregion

        #endregion


        #region ΛΟΓΑΡΙΑΣΜΟΙ ΣΧΟΛΕΙΩΝ-ΕΠΙΤΡΟΠΩΝ

        public void PopulateSchools()
        {
            var schools = (from d in db.SysSchools
                           select d).ToList();

            ViewData["schools"] = schools;
        }

        public ActionResult SchoolAccounts(string notify = null)
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

            PopulateSchools();
            if (notify != null)
                this.AddNotification(notify, NotificationType.SUCCESS);

            return View();
        }

        public List<UserSchoolViewModel> GetSchoolList()
        {
            List<UserSchoolViewModel> results = new List<UserSchoolViewModel>();
            var users = from a in db.UserSchools
                        select new UserSchoolViewModel
                        {
                            UserID = a.UserID,
                            Username = a.Username,
                            Password = a.Password,
                            UserSchoolID = a.UserSchoolID ?? 0,
                        };

            results = users.ToList();
            return results;
        }

        public ActionResult CreatePasswords()
        {
            var schools = (from s in db.UserSchools
                           select s).ToList();

            foreach (var school in schools)
            {
                school.Password = c.GeneratePassword() + string.Format("{0:00}", school.UserSchoolID);
                db.Entry(school).State = EntityState.Modified;
                db.SaveChanges();
            }

            string notify = "Η δημιουργία νέων κωδικών σχολείων ολοκληρώθηκε.";
            return RedirectToAction("SchoolAccounts", "Tools", new { notify });
        }

        public ActionResult SchoolAccountsPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "UserAdmins");
            }
            else
            {
                return View();
            }
        }


        #region Grid CRUD Functions

        [HttpPost]
        public ActionResult School_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<UserSchoolViewModel> data = GetSchoolList();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult School_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<UserSchoolViewModel> userSchools)
        {
            var results = new List<UserSchoolViewModel>();
            foreach (var userSchool in userSchools)
            {
                if (userSchool != null && ModelState.IsValid)
                {
                    UserSchools newuserSchool = new UserSchools()
                    {
                        Username = userSchool.Username,
                        Password = userSchool.Password,
                        UserSchoolID = userSchool.UserSchoolID,
                    };
                    db.UserSchools.Add(newuserSchool);
                    db.SaveChanges();
                    results.Add(userSchool);
                }
            }
            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult School_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<UserSchoolViewModel> userSchools)
        {
            if (userSchools != null)
            {
                foreach (var userSchool in userSchools)
                {
                    UserSchools entity = db.UserSchools.Where(m => m.UserID.Equals(userSchool.UserID)).FirstOrDefault();

                    entity.UserID = userSchool.UserID;
                    entity.Username = userSchool.Username;
                    entity.Password = userSchool.Password;
                    entity.UserSchoolID = userSchool.UserSchoolID;
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return Json(userSchools.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult School_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<UserAdminViewModel> userSchools)
        {
            if (userSchools.Any())
            {
                foreach (var userSchool in userSchools)
                {
                    if (userSchool != null)
                    {
                        UserSchools entity = db.UserSchools.Find(userSchool.UserID);
                        db.Entry(entity).State = EntityState.Deleted;
                        db.UserSchools.Remove(entity);
                        db.SaveChanges();
                    }
                }
            }
            return Json(userSchools.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #endregion


        #region ΛΟΓΑΡΙΑΣΜΟΙ ΕΚΠΑΙΔΕΥΤΙΚΩΝ

        public ActionResult TeacherAccounts()
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

            List<UserTeacherViewModel> userVM = GetUserTeacherListFromDB();
            return View(userVM);
        }

        public List<UserTeacherViewModel> GetUserTeacherListFromDB()
        {
            var users = (from a in db.UserTeachers
                         orderby a.UserAfm
                         select new UserTeacherViewModel
                         {
                             UserID = a.UserID,
                             Username = a.Username,
                             Password = a.Password,
                             UserAfm = a.UserAfm,
                             CreateDate = a.CreateDate,
                         }).ToList();

            return users;
        }

        public ActionResult UserTeacher_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetUserTeacherListFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserTeacherInfo_Read([DataSourceRequest] DataSourceRequest request, string afm = "")
        {
            List<TeacherAccountInfoViewModel> data = GetUserTeacherInfoFromDB(afm);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        List<TeacherAccountInfoViewModel> GetUserTeacherInfoFromDB(string afm = "")
        {
            var data = (from d in db.sqlTeacherAccountInfo
                        where d.AFM == afm
                        select new TeacherAccountInfoViewModel
                        {
                            UserID = d.UserID,
                            Username = d.Username,
                            FullName = d.FullName,
                            FatherName = d.FatherName,
                            AFM = d.AFM,
                            ADT = d.ADT,
                            Telephone = d.Telephone,
                            CellPhone = d.CellPhone,
                            Email = d.Email
                        }).ToList();
            return data;
        }

        #endregion


        #region ΕΙΔΙΚΟΤΗΤΕΣ ΠΛΕΓΜΑ ΚΑΤΑΧΩΡΗΣΗΣ

        public ActionResult EidikotitesList()
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
            populateKladoi();
            populateKladoiUnified();

            return View();
        }

        public ActionResult Eidikotita_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetEidikotitesFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Eidikotita_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<EidikotitesViewModel> data)
        {
            var results = new List<EidikotitesViewModel>();

            foreach (var item in data)
            {
                var existingEidikotites = db.SysEidikotites.Where(s => s.EidikotitaName == item.EidikotitaName && s.EidikotitaCode == item.EidikotitaCode).Count();
                if (existingEidikotites > 0)
                {
                    ModelState.AddModelError("", "Η ειδικότητα αυτή είναι ήδη καταχωρημένη. Η αποθήκευση ακυρώθηκε.");
                }
                if (item != null && ModelState.IsValid)
                {
                    SysEidikotites newData = new SysEidikotites()
                    {
                        EidikotitaCode = item.EidikotitaCode,
                        EidikotitaName = item.EidikotitaName,
                        EidikotitaUnified = item.EidikotitaUnified,
                        KladosUnified = item.KladosUnified,
                        EidikotitaKladosID = item.EidikotitaKladosID
                    };
                    db.SysEidikotites.Add(newData);
                    results.Add(item);
                    db.SaveChanges();
                }
            }
            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Eidikotita_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<EidikotitesViewModel> data)
        {
            var results = new List<EidikotitesViewModel>();
            foreach (var d in data)
            {
                if (d != null)
                {
                    SysEidikotites entity = db.SysEidikotites.Find(d.EidikotitaID);

                    entity.EidikotitaCode = d.EidikotitaCode;
                    entity.EidikotitaName = d.EidikotitaName;
                    entity.EidikotitaUnified = d.EidikotitaUnified;
                    entity.KladosUnified = d.KladosUnified;
                    entity.EidikotitaKladosID = d.EidikotitaKladosID;

                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            //db.SaveChanges();
            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Eidikotita_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<EidikotitesViewModel> data)
        {
            if (data.Any())
            {
                foreach (var row in data)
                {
                    if (row != null)
                    {
                        SysEidikotites entity = db.SysEidikotites.Find(row.EidikotitaID);
                        if (entity != null)
                        {
                            if (CanDeleteEidikotita(row.EidikotitaID))
                            {
                                db.Entry(entity).State = EntityState.Deleted;
                                db.SysEidikotites.Remove(entity);
                                db.SaveChanges();
                            }
                        }
                    }
                }
                db.SaveChanges();
            }
            return Json(data.ToDataSourceResult(request, ModelState));
        }

        public List<EidikotitesViewModel> GetEidikotitesFromDB()
        {
            var data = (from d in db.SysEidikotites
                        orderby d.EidikotitaKladosID, d.EidikotitaCode, d.EidikotitaName
                        select new EidikotitesViewModel
                        {
                            EidikotitaID = d.EidikotitaID,
                            EidikotitaCode = d.EidikotitaCode,
                            EidikotitaName = d.EidikotitaName,
                            EidikotitaUnified = d.EidikotitaUnified,
                            KladosUnified = d.KladosUnified ?? 0,
                            EidikotitaKladosID = d.EidikotitaKladosID,
                        }).ToList();

            return data;
        }

        public ActionResult EidikotitesPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "UserAdmins");
            }
            else
            {
                return View();
            }
        }

        public bool CanDeleteEidikotita(int eidikotitaId)
        {
            var data = (from d in db.ProkirixisEidikotites where d.EidikotitaID == eidikotitaId select d).ToList();

            if (data.Count > 0) return false;
            else return true;
        }

        public void populateKladoiUnified()
        {
            var data = (from k in db.SysKladosEniaios orderby k.Klados, k.KladosEniaiosID select k).ToList();

            ViewData["kladoiUnified"] = data;
            ViewData["defaultKladosUnified"] = data.First().KladosEniaiosID;
        }

        public void populateKladoi()
        {
            var kladosTypes = (from k in db.SysKlados
                               select k).ToList();

            ViewData["kladoi"] = kladosTypes;
        }

        public void populateGroups()
        {
            var groups = (from g in db.SysEidikotitesGroups select g).ToList();
            ViewData["groups"] = groups;
        }

        #endregion


        #region ΕΝΙΑΙΟΙ ΚΛΑΔΟΙ ΚΑΙ ΕΝΤΑΞΗ ΕΙΔΙΚΟΤΗΤΩΝ

        public ActionResult KladosUnifiedList()
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

            populateKlados();
            populateEidikotitesKladosEniaios();
            return View();
        }

        public ActionResult KladosUnified_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetKladosUnifiedFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult KladosUnified_Create([DataSourceRequest] DataSourceRequest request, KladosEniaiosViewModel data)
        {
            var newdata = new KladosEniaiosViewModel();

            var existingKlados = db.SysKladosEniaios.Where(s => s.KladosEniaiosText == data.KladosEniaiosText).Count();
            if (existingKlados > 0)
            {
                ModelState.AddModelError("", "Η ομάδα αυτή είναι ήδη καταχωρημένη. Η αποθήκευση ακυρώθηκε.");
            }
            if (data != null && ModelState.IsValid)
            {
                SysKladosEniaios entity = new SysKladosEniaios()
                {
                    KladosEniaiosText = data.KladosEniaiosText,
                    Klados = data.Klados
                };
                db.SysKladosEniaios.Add(entity);
                db.SaveChanges();
                data.KladosEniaiosID = entity.KladosEniaiosID;
                newdata = RefreshKladosUnifiedFromDB(data.KladosEniaiosID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult KladosUnified_Update([DataSourceRequest] DataSourceRequest request, KladosEniaiosViewModel data)
        {
            var newdata = new KladosEniaiosViewModel();

            if (data != null && ModelState.IsValid)
            {
                SysKladosEniaios entity = db.SysKladosEniaios.Find(data.KladosEniaiosID);
                entity.KladosEniaiosText = data.KladosEniaiosText;
                entity.Klados = data.Klados;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                newdata = RefreshKladosUnifiedFromDB(data.KladosEniaiosID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult KladosUnified_Destroy([DataSourceRequest] DataSourceRequest request, KladosEniaiosViewModel data)
        {
            if (data != null)
            {
                SysKladosEniaios entity = db.SysKladosEniaios.Find(data.KladosEniaiosID);
                if (entity != null && ModelState.IsValid)
                {
                    if (CanDeleteKladosUnified(data.KladosEniaiosID))
                    {
                        db.Entry(entity).State = EntityState.Deleted;
                        db.SysKladosEniaios.Remove(entity);
                        db.SaveChanges();
                    }
                    else ModelState.AddModelError("", "Ο κλάδος αυτός δεν μπορεί να διαγραφεί διότι έχει συσχετισμένες ειδικότητες.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public KladosEniaiosViewModel RefreshKladosUnifiedFromDB(int recordId)
        {
            var data = (from d in db.SysKladosEniaios
                        where d.KladosEniaiosID == recordId
                        select new KladosEniaiosViewModel
                        {
                            KladosEniaiosID = d.KladosEniaiosID,
                            KladosEniaiosText = d.KladosEniaiosText,
                            Klados = d.Klados
                        }).FirstOrDefault();

            return data;
        }

        public List<KladosEniaiosViewModel> GetKladosUnifiedFromDB()
        {

            var data = (from d in db.SysKladosEniaios
                        orderby d.Klados, d.KladosEniaiosText
                        select new KladosEniaiosViewModel
                        {
                            KladosEniaiosID = d.KladosEniaiosID,
                            KladosEniaiosText = d.KladosEniaiosText,
                            Klados = d.Klados
                        }).ToList();

            return data;
        }

        public bool CanDeleteKladosUnified(int kladosId)
        {
            var data = (from d in db.SysEidikotites where d.KladosUnified == kladosId select d).ToList();
            if (data.Count > 0) return false;
            else return true;
        }

        public ActionResult KladosUnifiedPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "UserAdmins");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
                return View();
            }
        }


        #region ΠΛΕΓΜΑ ΕΝΤΑΞΗΣ ΕΙΔΙΚΟΤΗΤΩΝ ΣΕ ΕΝΙΑΙΟ ΚΛΑΔΟ

        public ActionResult EidikotitaKladosEniaios_Read([DataSourceRequest] DataSourceRequest request, int kladosunifiedId = 0)
        {
            var data = GetEidikotitesKladosEniaios(kladosunifiedId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EidikotitaKladosEniaios_Set([DataSourceRequest] DataSourceRequest request, EidikotititesKladosEniaiosViewModel data, int kladosunifiedId = 0)
        {
            var newdata = new EidikotititesKladosEniaiosViewModel();

            if (kladosunifiedId > 0)
            {
                if (data != null)
                {
                    SysEidikotites entity = db.SysEidikotites.Find(data.EidikotitaID);
                    entity.KladosUnified = kladosunifiedId;

                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                    newdata = RefreshEdikotitesKladosEniaios(entity.EidikotitaID);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλέξετε έναν κλάδο ενοποίησης. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EidikotitaKladosEniaios_Reset([DataSourceRequest] DataSourceRequest request, EidikotititesKladosEniaiosViewModel data, int kladosunifiedId = 0)
        {
            var newdata = new EidikotititesKladosEniaiosViewModel();

            if (kladosunifiedId > 0)
            {
                if (data != null)
                {
                    SysEidikotites entity = db.SysEidikotites.Find(data.EidikotitaID);
                    entity.KladosUnified = null;

                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                    newdata = RefreshEdikotitesKladosEniaios(entity.EidikotitaID);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλέξετε έναν κλάδο ενοποίησης. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public EidikotititesKladosEniaiosViewModel RefreshEdikotitesKladosEniaios(int recordId)
        {
            var data = (from d in db.sqlEidikotititesKladosEniaios
                        where d.EidikotitaID == recordId
                        orderby d.EidikotitaKladosID, d.EidikotitaPtyxio
                        select new EidikotititesKladosEniaiosViewModel
                        {
                            EidikotitaID = d.EidikotitaID,
                            EidikotitaPtyxio = d.EidikotitaPtyxio,
                            KladosUnified = d.KladosUnified,
                            EidikotitaKladosID = d.EidikotitaKladosID
                        }).FirstOrDefault();

            return (data);

        }

        public List<EidikotititesKladosEniaiosViewModel> GetEidikotitesKladosEniaios(int kladosunifiedId = 0)
        {
            var data = (from d in db.sqlEidikotititesKladosEniaios
                        where d.KladosUnified == kladosunifiedId
                        orderby d.EidikotitaKladosID, d.EidikotitaPtyxio
                        select new EidikotititesKladosEniaiosViewModel
                        {
                            EidikotitaID = d.EidikotitaID,
                            EidikotitaPtyxio = d.EidikotitaPtyxio,
                            KladosUnified = d.KladosUnified,
                            EidikotitaKladosID = d.EidikotitaKladosID
                        }).ToList();

            return (data);
        }

        #endregion

        public void populateEidikotitesKladosEniaios()
        {
            var data = (from d in db.sqlEidikotititesKladosEniaios
                        orderby d.EidikotitaKladosID, d.EidikotitaPtyxio
                        select new EidikotititesKladosEniaiosViewModel
                        {
                            EidikotitaID = d.EidikotitaID,
                            EidikotitaPtyxio = d.EidikotitaPtyxio,
                            KladosUnified = d.KladosUnified,
                            EidikotitaKladosID = d.EidikotitaKladosID
                        }).ToList();

            ViewData["sqlEidikotites2"] = data;
            ViewData["sqlDefaultEidikotita2"] = data.First().EidikotitaID;
        }

        #endregion


        #region ΟΜΑΔΕΣ ΕΙΔΙΚΟΤΗΤΩΝ ΚΑΙ ΕΝΤΑΞΗ ΕΙΔΙΚΟΤΗΤΩΝ

        public ActionResult EidikotitesGroupsList()
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

            populateSqlEidikotites();
            populateKladoi();
            return View();
        }

        public ActionResult EidikotitaGroup_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetEidikotitesGroupsFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EidikotitaGroup_Create([DataSourceRequest] DataSourceRequest request, EidikotitesGroupsViewModel data)
        {
            var newdata = new EidikotitesGroupsViewModel();

            var existingGroups = db.SysEidikotitesGroups.Where(s => s.GroupText == data.GroupText).Count();
            if (existingGroups > 0)
            {
                ModelState.AddModelError("", "Η ομάδα αυτή είναι ήδη καταχωρημένη. Η αποθήκευση ακυρώθηκε.");
            }
            if (data != null && ModelState.IsValid)
            {
                SysEidikotitesGroups entity = new SysEidikotitesGroups()
                {
                    GroupText = data.GroupText,
                    Klados = data.Klados
                };
                db.SysEidikotitesGroups.Add(entity);
                db.SaveChanges();
                data.GroupID = entity.GroupID;
                newdata = RefreshEidikotitaGroupFromDB(data.GroupID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EidikotitaGroup_Update([DataSourceRequest] DataSourceRequest request, EidikotitesGroupsViewModel data)
        {
            var newdata = new EidikotitesGroupsViewModel();

            if (data != null && ModelState.IsValid)
            {
                SysEidikotitesGroups entity = db.SysEidikotitesGroups.Find(data.GroupID);
                entity.GroupText = data.GroupText;
                entity.Klados = data.Klados;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                newdata = RefreshEidikotitaGroupFromDB(data.GroupID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EidikotitaGroup_Destroy([DataSourceRequest] DataSourceRequest request, EidikotitesGroupsViewModel data)
        {
            if (data != null)
            {
                SysEidikotitesGroups entity = db.SysEidikotitesGroups.Find(data.GroupID);
                if (data.GroupText == null)
                {
                    ModelState.AddModelError("", "Η κενή ομάδα δεν επιτρέπεται να διαγραφεί. Η διαγραφή ακυρώθηκε.");
                }
                if (entity != null && ModelState.IsValid)
                {
                    db.Entry(entity).State = EntityState.Deleted;
                    db.SysEidikotitesGroups.Remove(entity);
                    db.SaveChanges();
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public EidikotitesGroupsViewModel RefreshEidikotitaGroupFromDB(int recordId)
        {

            var data = (from d in db.SysEidikotitesGroups
                        where d.GroupID == recordId
                        orderby d.GroupText
                        select new EidikotitesGroupsViewModel
                        {
                            GroupID = d.GroupID,
                            GroupText = d.GroupText,
                            Klados = d.Klados
                        }).FirstOrDefault();

            return data;
        }

        public List<EidikotitesGroupsViewModel> GetEidikotitesGroupsFromDB()
        {

            var data = (from d in db.SysEidikotitesGroups
                        orderby d.Klados, d.GroupText
                        select new EidikotitesGroupsViewModel
                        {
                            GroupID = d.GroupID,
                            GroupText = d.GroupText,
                            Klados = d.Klados
                        }).ToList();

            return data;
        }

        public ActionResult GroupsEidikotitesPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "UserAdmins");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
                return View();
            }
        }


        #region ΠΛΕΓΜΑ ΕΝΤΑΞΗΣ ΕΙΔΙΚΟΤΗΤΩΝ ΣΕ ΟΜΑΔΑ

        public void populateSqlEidikotites()
        {
            var data = (from d in db.sqlEidikotitesSelector
                        orderby d.EidikotitaKladosID, d.EidikotitaDesc
                        select new EidikotitesSelectorViewModel
                        {
                            EidikotitaID = d.EidikotitaID,
                            EidikotitaDesc = d.EidikotitaDesc
                        }).ToList();

            ViewData["sqlEidikotites"] = data;
            ViewData["sqlDefaultEidikotita"] = data.First().EidikotitaID;
        }

        public ActionResult sqlEidikotitaGroup_Read([DataSourceRequest] DataSourceRequest request, int groupId = 0)
        {
            var data = GetSqlEidikotitesFromDB(groupId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult sqlEidikotitaGroup_Set([DataSourceRequest] DataSourceRequest request, EidikotitesSelectorViewModel data, int groupId = 0)
        {
            var newdata = new EidikotitesSelectorViewModel();

            if (groupId > 0)
            {
                if (data != null)
                {
                    SysEidikotites entity = db.SysEidikotites.Find(data.EidikotitaID);
                    entity.EidikotitaGroupID = groupId;

                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                    newdata = RefreshSqlEdikotitesFromDB(entity.EidikotitaID);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλέξετε μια ομάδα. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult sqlEidikotitaGroup_Reset([DataSourceRequest] DataSourceRequest request, EidikotitesSelectorViewModel data, int groupId = 0)
        {
            var newdata = new EidikotitesSelectorViewModel();

            if (groupId > 0)
            {
                if (data != null)
                {
                    SysEidikotites entity = db.SysEidikotites.Find(data.EidikotitaID);
                    entity.EidikotitaGroupID = null;

                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                    newdata = RefreshSqlEdikotitesFromDB(entity.EidikotitaID);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλέξετε μια ομάδα. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public EidikotitesSelectorViewModel RefreshSqlEdikotitesFromDB(int recordId)
        {
            var data = (from d in db.sqlEidikotitesSelector
                        where d.EidikotitaID == recordId
                        select new EidikotitesSelectorViewModel
                        {
                            EidikotitaID = d.EidikotitaID,
                            EidikotitaDesc = d.EidikotitaDesc,
                            EidikotitaGroupID = d.EidikotitaGroupID
                        }).FirstOrDefault();

            return (data);

        }

        public List<EidikotitesSelectorViewModel> GetSqlEidikotitesFromDB(int groupId = 0)
        {
            var data = (from d in db.sqlEidikotitesSelector
                        where d.EidikotitaGroupID == groupId
                        orderby d.EidikotitaKladosID, d.EidikotitaDesc
                        select new EidikotitesSelectorViewModel
                        {
                            EidikotitaID = d.EidikotitaID,
                            EidikotitaDesc = d.EidikotitaDesc,
                            EidikotitaGroupID = d.EidikotitaGroupID
                        }).ToList();

            return (data);
        }

        #endregion

        #endregion


        #region ΠΡΟΚΗΡΥΞΕΙΣ ΚΑΤΑΧΩΡΗΣΗ

        public ActionResult ProkirixisList()
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
            populateSchoolYears();
            populateStatus();
            return View();
        }

        public ActionResult Prokirixis_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetProkirixisFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Prokirixis_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<ProkirixisViewModel> data)
        {
            var results = new List<ProkirixisViewModel>();

            foreach (var d in data)
            {
                var existingProkirixi = db.Prokirixis.Where(s => s.Protocol == d.Protocol).Count();
                if (existingProkirixi > 0)
                    ModelState.AddModelError("", "Η πρκήρυξη αυτή είναι ήδη καταχωρημένη. Η διαδικασία ακυρώθηκε.");

                if (d != null && ModelState.IsValid)
                {
                    Prokirixis newData = new Prokirixis()
                    {
                        Protocol = d.Protocol,
                        SchoolYear = d.SchoolYear,
                        Fek = d.Fek,
                        Dioikitis = d.Dioikitis,
                        DateStart = d.DateStart,
                        DateEnd = d.DateEnd,
                        Status = d.Status,
                        Active = d.Active,
                        Admin = d.Admin,
                        UserView = d.UserView,
                        Enstaseis = d.Enstaseis,
                    };
                    db.Prokirixis.Add(newData);
                    results.Add(d);
                    db.SaveChanges();
                }
            }
            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Prokirixis_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<ProkirixisViewModel> data)
        {
            foreach (var d in data)
            {
                if (d != null && ModelState.IsValid)
                {
                    Prokirixis entity = db.Prokirixis.Find(d.ProkirixiID);

                    entity.ProkirixiID = d.ProkirixiID;
                    entity.SchoolYear = d.SchoolYear;
                    entity.Protocol = d.Protocol;
                    entity.Fek = d.Fek;
                    entity.Dioikitis = d.Dioikitis;
                    entity.DateStart = d.DateStart;
                    entity.DateEnd = d.DateEnd;
                    entity.Status = d.Status;
                    entity.Active = d.Active;
                    entity.Admin = d.Admin;
                    entity.UserView = d.UserView;
                    entity.Enstaseis = d.Enstaseis;

                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Prokirixis_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<ProkirixisViewModel> data)
        {
            if (data.Any())
            {
                foreach (var row in data)
                {
                    if (row != null)
                    {
                        Prokirixis entity = db.Prokirixis.Find(row.ProkirixiID);
                        if (CanDeleteProkirixi(row.ProkirixiID))
                        {
                            db.Entry(entity).State = EntityState.Deleted;
                            db.Prokirixis.Remove(entity);
                            db.SaveChanges();
                        }
                        else
                            ModelState.AddModelError("", "Δεν μπορεί να διαγραφεί αυτή η προκήρυξη διότι υπάρχουν συσχετισμένες ειδικότητες ή/και αιτήσεις.");
                    }
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState));
        }

        public bool CanDeleteProkirixi(int prokirixiId)
        {
            var data = (from d in db.ProkirixisEidikotites where d.ProkirixiID == prokirixiId select d).Count();
            if (data > 0)
                return false;

            // TODO: Also add Aitiseis check here
            return false;
        }

        public List<ProkirixisViewModel> GetProkirixisFromDB()
        {
            var data = (from d in db.Prokirixis
                        orderby d.SchoolYear descending, d.DateStart descending
                        select new ProkirixisViewModel
                        {
                            ProkirixiID = d.ProkirixiID,
                            SchoolYear = d.SchoolYear,
                            Protocol = d.Protocol,
                            Fek = d.Fek,
                            Dioikitis = d.Dioikitis,
                            DateStart = d.DateStart,
                            DateEnd = d.DateEnd,
                            Status = d.Status,
                            Active = d.Active ?? false,
                            Admin = d.Admin ?? false,
                            UserView = d.UserView ?? false,
                            Enstaseis = d.Enstaseis ?? false
                        }).ToList();

            return data;
        }

        #endregion


        #region ΠΡΟΚΗΡΥΣΣΟΜΕΝΕΣ ΕΙΔΙΚΟΤΗΤΕΣ ΚΑΤΑΧΩΡΗΣΗ

        public ActionResult EidikotitesProkirixisSetup(string notify = null)
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
                this.AddNotification(notify, NotificationType.INFO);

            int schoolYearId = (int)c.GetAdminProkirixi().SchoolYear;
            ViewData["prokirixiProtocol"] = c.GetAdminProkirixi().Protocol;
            ViewData["schoolYearText"] = c.GetSchoolYearText(schoolYearId);

            populateEidikotites();
            return View();
        }

        public ActionResult EidikotitesProkirixiRead([DataSourceRequest] DataSourceRequest request, int prokirixiId, int schoolId)
        {
            var data = ProkirixiEidikotitesFromDB(prokirixiId, schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EidikotitesProkirixiCreate([DataSourceRequest] DataSourceRequest request, 
                [Bind(Prefix = "models")]IEnumerable<ProkirixisEidikotitesViewModel> data, int prokirixiId, int schoolId)
        {
            var results = new List<ProkirixisEidikotitesViewModel>();

            if (prokirixiId > 0 && schoolId > 0)
            {
                foreach (var d in data)
                {
                    if (d != null && ModelState.IsValid)
                    {
                        ProkirixisEidikotites newData = new ProkirixisEidikotites()
                        {
                            ProkirixiID = prokirixiId,
                            SchoolID = schoolId,
                            EidikotitaID = d.EidikotitaID
                        };
                        if (EidikotitaSchoolExists(newData))
                        {
                            ModelState.AddModelError("", "Έγινε απόπειρα δημιουργίας διπλοεγγραφής. Η διπλή καταχώρηση ακυρώθηκε.");
                            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
                        }
                        db.ProkirixisEidikotites.Add(newData);
                        results.Add(d);
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Δεν έχει γίνει επιλογή προκήρυξης και σχολείου. Η αποθήκευση ακυρώθηκε.");
            }
            return Json(results.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EidikotitesProkirixiUpdate([DataSourceRequest] DataSourceRequest request, 
                [Bind(Prefix = "models")]IEnumerable<ProkirixisEidikotitesViewModel> data, int prokirixiId, int schoolId)
        {
            if (prokirixiId > 0 && schoolId > 0)
            {
                foreach (var d in data)
                {
                    if (d != null && ModelState.IsValid)
                    {
                        ProkirixisEidikotites entity = db.ProkirixisEidikotites.Find(d.PSE_ID);

                        entity.ProkirixiID = prokirixiId;
                        entity.SchoolID = schoolId;
                        entity.EidikotitaID = d.EidikotitaID;

                        db.Entry(entity).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Δεν έχει γίνει επιλογή προκήρυξης και σχολείου. Η αποθήκευση ακυρώθηκε.");
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EidikotitesProkirixiDestroy([DataSourceRequest] DataSourceRequest request, 
                [Bind(Prefix = "models")]IEnumerable<ProkirixisEidikotitesViewModel> data)
        {
            if (data.Any())
            {
                foreach (var row in data)
                {
                    if (row != null)
                    {
                        if (CanDeleteEidikotitaInProkirixi(row))
                        {
                            ProkirixisEidikotites entity = db.ProkirixisEidikotites.Find(row.PSE_ID);

                            db.Entry(entity).State = EntityState.Deleted;
                            db.ProkirixisEidikotites.Remove(entity);
                            db.SaveChanges();
                        }
                        else
                        {
                            ModelState.AddModelError("", "Δεν μπορεί να διαγραφεί ειδικότητα προηγούμενης προκήρυξης.");
                        }
                    }
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public List<ProkirixisEidikotitesViewModel> ProkirixiEidikotitesFromDB(int prokirixiId, int schoolId)
        {
            var data = (from d in db.ProkirixisEidikotites
                        where d.ProkirixiID == prokirixiId && d.SchoolID == schoolId
                        orderby d.SysEidikotites.EidikotitaKladosID, d.SysEidikotites.EidikotitaCode, d.SysEidikotites.EidikotitaName
                        select new ProkirixisEidikotitesViewModel
                        {
                            PSE_ID = d.PSE_ID,
                            ProkirixiID = d.ProkirixiID,
                            SchoolID = d.SchoolID,
                            EidikotitaID = d.EidikotitaID
                        }).ToList();
            return data;
        }

        public bool CanDeleteEidikotitaInProkirixi(ProkirixisEidikotitesViewModel data)
        {
            int currentProkirixi = c.GetAdminProkirixiID();
            if (data.ProkirixiID != currentProkirixi) return false;

            return true;
        }

        public bool EidikotitaSchoolExists(ProkirixisEidikotites item)
        {
            var data = (from d in db.ProkirixisEidikotites
                        where d.ProkirixiID == item.ProkirixiID && d.SchoolID == item.SchoolID && d.EidikotitaID == item.EidikotitaID
                        select d).Count();
            if (data > 0)
                return true;
            else
                return false;
        }

        public ActionResult EidikotitesProkirixiPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "UserAdmins");
            }
            else
            {
                return View();
            }
        }

        #endregion


        #region ΠΡΟΚΗΡΥΣΣΟΜΕΝΕΣ ΕΙΔΙΚΟΤΗΤΕΣ ΠΡΟΒΟΛΗ

        public ActionResult EidikotitesProkirixisBrowse(string notify = null)
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
            if (notify != null) this.AddNotification(notify, NotificationType.INFO);

            int schoolYearId = (int)c.GetAdminProkirixi().SchoolYear;
            ViewData["prokirixiProtocol"] = c.GetAdminProkirixi().Protocol;
            ViewData["schoolYearText"] = c.GetSchoolYearText(schoolYearId);

            populatePeriferies();
            populateEidikotites();
            return View();
        }

        public ActionResult EidikotitesRead([DataSourceRequest] DataSourceRequest request, int schoolId)
        {
            prokirixiId = c.GetAdminProkirixiID();
            if (prokirixiId == 0)
            {
                this.AddNotification("Δεν βρέθηκε διαχειριστική Προκήρυξη.", NotificationType.WARNING);
                return RedirectToAction("Index", "Admin");
            }

            var data = ProkirixiEidikotitesFromDB(prokirixiId, schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EidikotitesCreate([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<ProkirixisEidikotitesViewModel> data, int schoolId)
        {
            var results = new List<ProkirixisEidikotitesViewModel>();
            prokirixiId = c.GetAdminProkirixiID();

            if (schoolId > 0)
            {
                foreach (var d in data)
                {
                    var existingData = db.ProkirixisEidikotites.Where(s => s.ProkirixiID == prokirixiId && s.SchoolID == schoolId && s.EidikotitaID == d.EidikotitaID).Count();
                    if (existingData > 0)
                    {
                        ModelState.AddModelError("", "Υπάρχει ήδη η καταχώρηση αυτή. Η αποθήκευση ακυρώθηκε.");
                    }
                    if (d != null && ModelState.IsValid)
                    {
                        ProkirixisEidikotites newData = new ProkirixisEidikotites()
                        {
                            ProkirixiID = prokirixiId,
                            SchoolID = schoolId,
                            EidikotitaID = d.EidikotitaID
                        };
                        db.ProkirixisEidikotites.Add(newData);
                        results.Add(d);
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Δεν έχει γίνει επιλογή σχολείου. Η αποθήκευση ακυρώθηκε.");
            }
            return Json(results.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EidikotitesUpdate([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<ProkirixisEidikotitesViewModel> data, int schoolId)
        {
            prokirixiId = c.GetAdminProkirixiID();
            if (schoolId > 0)
            {
                foreach (var d in data)
                {
                    if (d != null && ModelState.IsValid)
                    {
                        ProkirixisEidikotites entity = db.ProkirixisEidikotites.Find(d.PSE_ID);

                        entity.ProkirixiID = prokirixiId;
                        entity.SchoolID = schoolId;
                        entity.EidikotitaID = d.EidikotitaID;

                        db.Entry(entity).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Δεν έχει γίνει επιλογή σχολείου. Η αποθήκευση ακυρώθηκε.");
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EidikotitesDestroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<ProkirixisEidikotitesViewModel> data)
        {
            if (data.Any())
            {
                foreach (var row in data)
                {
                    if (row != null)
                    {
                        ProkirixisEidikotites entity = db.ProkirixisEidikotites.Find(row.PSE_ID);

                        db.Entry(entity).State = EntityState.Deleted;
                        db.ProkirixisEidikotites.Remove(entity);
                        db.SaveChanges();
                    }
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SchoolsRead([DataSourceRequest] DataSourceRequest request)
        {
            var schools = db.SysSchools.Select(p => new SchoolsViewModel()
            {
                SchoolID = p.SchoolID,
                SchoolName = p.SchoolName,
                SchoolPeriferiaID = p.SchoolPeriferiaID
            });

            return Json(schools.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region GETTERS AND POPULATORS

        public void populateEidikotites()
        {
            var data = (from d in db.sqlEidikotitesSelector
                         orderby d.EidikotitaKladosID, d.EidikotitaDesc
                         select d).ToList();

            ViewData["Eidikotites"] = data;
            ViewData["defaultEidikotita"] = data.First().EidikotitaID;
        }

        public void populateSchoolYears()
        {
            var syears = (from d in db.SysSchoolYears
                          orderby d.SchoolYearText descending
                          select d).ToList();

            ViewData["SchoolYears"] = syears;
            ViewData["DefaultSchoolYear"] = syears.First().SchoolYearID;
        }

        public void populateStatus()
        {
            var status = (from s in db.SysProkirixiStatus
                          select s).ToList();

            ViewData["Status"] = status;
        }

        public void populateKlados()
        {
            var data = (from d in db.SysKlados orderby d.KladosID select d).ToList();

            ViewData["kladoi"] = data;
            ViewData["kladosDefault"] = data.First().KladosID;
        }

        public void populatePeriferies()
        {
            var Periferies = (from k in db.SysPeriferies
                              select k).ToList();

            ViewData["periferies"] = Periferies;
        }

        public ActionResult GetProkirixeis()
        {
            var data = (from d in db.Prokirixis
                        orderby d.SchoolYear descending, d.DateStart descending
                        select new ProkirixisViewModel
                        {
                            ProkirixiID = d.ProkirixiID,
                            Protocol = d.Protocol
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSchoolsIek()
        {
            var data = (from d in db.SysSchools
                        orderby d.SchoolName
                        select new SchoolsViewModel
                        {
                            SchoolID = d.SchoolID,
                            SchoolName = d.SchoolName,
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public UserAdmins GetLoginAdmin()
        {
            loggedAdmin = db.UserAdmins.Where(u => u.Username == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
            ViewBag.loggedAdmin = loggedAdmin;
            ViewBag.loggedUser = loggedAdmin.FullName;
            return loggedAdmin;
        }

        #endregion


        #region ADDITIONAL TOOLS

        public ActionResult CopyEidikotitesFromProkirixi(int sourceProkirixiID)
        {
            int currentProkirixiID = c.GetAdminProkirixiID();
            string msg = "";

            if (!(sourceProkirixiID > 0))
            {
                msg = "Δεν έχει γίνει επιλογή προηγούμενης προκήρυξης για μεταφορά!";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }

            var source = (from s in db.ProkirixisEidikotites where s.ProkirixiID == sourceProkirixiID select s).ToList();
            var target = (from t in db.ProkirixisEidikotites where t.ProkirixiID == currentProkirixiID select t).ToList();
            if (target.Count > 0)
            {
                msg = "Στην τρέχουσα προκήρυξη έχουν ήδη καταχωρηθεί ειδικότητες!";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            if (source.Count == 0)
            {
                msg = "Δεν βρέθηκαν ειδικότητες της προηγούμενης προκήρυξης!";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }

            foreach (var p in source)
            {
                ProkirixisEidikotites newdata = new ProkirixisEidikotites();

                newdata.ProkirixiID = currentProkirixiID;
                newdata.SchoolID = p.SchoolID;
                newdata.EidikotitaID = p.EidikotitaID;

                db.ProkirixisEidikotites.Add(newdata);
                db.SaveChanges();
            }
            msg = "Η διαδικασία αντιγραφής ειδικοτήτων από προηγούμενη προκήρυξη ολοκληρώθηκε.";
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        #endregion
    }
}