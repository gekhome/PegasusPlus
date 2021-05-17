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


        #region ΣΥΝΟΛΙΚΟ ΜΗΤΡΩΟ ΕΚΠΑΙΔΕΥΤΙΚΩΝ

        public ActionResult TeachersRegistry()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "UserAdmins");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            prokirixiId = c.GetAdminProkirixiID();
            if (!(prokirixiId > 0))
            {
                string msg = "Δεν βρέθηκε ενεργοποιημένη προκήρυξη για διαχείριση.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }

            populateIdiotita();
            return View();
        }

        public ActionResult TeachersRegistry_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetTeachersRegistry();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<TeachersRegistryViewModel> GetTeachersRegistry()
        {
            var data = (from d in db.sqlTeachersRegistry
                        orderby d.FullName
                        select new TeachersRegistryViewModel
                        {
                            AFM = d.AFM,
                            FullName = d.FullName,
                            Telephone = d.Telephone,
                            CellPhone = d.CellPhone,
                            Email = d.Email,
                            FullAddress = d.FullAddress,
                            Age = d.Age,
                            Idiotita = d.Idiotita
                        }).ToList();

            return data;
        }

        public ActionResult TeacherDataPrint()
        {
            prokirixiId = c.GetAdminProkirixiID();

            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "UserSchools");
            }
            else
            {
                loggedSchool = GetLoginSchool();
                prokirixiId = c.GetAdminProkirixiID();

                TeacherRegistryParameters parameters = new TeacherRegistryParameters();
                parameters.SchoolID = (from d in db.SysSchools orderby d.SchoolName select d).First().SchoolID;
                parameters.ProkirixiID = prokirixiId;

                return View(parameters);
            }
        }

        public ActionResult TeachersEidikotitaPrint()
        {
            prokirixiId = c.GetAdminProkirixiID();

            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "UserSchools");
            }
            else
            {
                loggedSchool = GetLoginSchool();
                prokirixiId = c.GetAdminProkirixiID();

                TeacherRegistryParameters parameters = new TeacherRegistryParameters();
                parameters.SchoolID = (from d in db.SysSchools orderby d.SchoolName select d).First().SchoolID;
                parameters.ProkirixiID = prokirixiId;

                return View(parameters);
            }
        }

        #endregion


        #region ΜΗΤΡΩΟ ΑΙΤΗΣΕΩΝ




        #endregion


        #region GETTERS

        public void populateIdiotita()
        {
            var data = (from d in db.SysIdiotita
                        orderby d.IdiotitaText
                        select d).ToList();

            ViewData["idiotites"] = data;
            ViewData["defaultIdiotita"] = data.First().IdiotitaID;
        }

        public void populateSchoolYears()
        {
            var syears = (from s in db.SysSchoolYears
                          orderby s.SchoolYearText
                          select s).ToList();

            ViewData["school_years"] = syears;
            ViewData["defaultSchoolYear"] = syears.First().SchoolYearID;
        }

        public void populateTeachTypes()
        {
            var data = (from s in db.SysTeachTypes
                        orderby s.TypeID
                        select s).ToList();

            ViewData["teach_types"] = data;
            ViewData["defaultTeachType"] = data.First().TypeID;
        }

        public void populateIncomeYears()
        {
            var incomeYears = (from d in db.SysTaxFree
                               orderby d.YearText
                               select d).ToList();

            ViewData["income_years"] = incomeYears;
        }

        public float setIncomeTaxfree(int taxyear)
        {
            float IncomeTaxFree = 0;
            if (!(taxyear > 0))
                return IncomeTaxFree;

            var data = (from d in db.SysTaxFree
                        where d.YearID == taxyear
                        select new { d.TaxFree }).FirstOrDefault();

            IncomeTaxFree = (float)data.TaxFree;
            return IncomeTaxFree;
        }

        public string setIncomeNomisma(int taxyear)
        {
            string IncomeNomisma = "";
            if (!(taxyear > 0))
                return IncomeNomisma;

            var data = (from d in db.SysTaxFree
                        where d.YearID == taxyear
                        select new { d.Nomisma }).FirstOrDefault();

            IncomeNomisma = data.Nomisma;
            return IncomeNomisma;
        }

        public JsonResult GetIncomeYears()
        {
            var data = db.SysTaxFree.Select(p => new TaxFreeViewModel
            {
                YearID = p.YearID,
                YearText = p.YearText
            }).OrderBy(x => x.YearText);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetApoklismos()
        {
            var data = db.SysApoklismoi.Select(p => new ApoklismoiViewModel
            {
                ApoklismosID = p.ApoklismosID,
                ApoklismosText = p.ApoklismosText
            }).OrderBy(x => x.ApoklismosText);

            return Json(data, JsonRequestBehavior.AllowGet);
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

        #endregion
    }
}