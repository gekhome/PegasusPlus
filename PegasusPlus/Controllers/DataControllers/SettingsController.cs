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
    public class SettingsController : Controller
    {
        private PegasusPlusDBEntities db = new PegasusPlusDBEntities();
        private UserSchools loggedSchool;
        int prokirixiId = 0;

        Common c = new Common();


        #region ΣΧΟΛΙΚΑ ΕΤΗ

        public ActionResult SchoolYearsList()
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
            return View();
        }

        public ActionResult SchoolYear_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetSchoolYearsFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
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
                return RedirectToAction("Login", "UserSchools");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }

            return View();
        }

        [HttpPost]
        public ActionResult Apokleismos_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetApokleismoiFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
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
                return RedirectToAction("Login", "UserSchools");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            return View();
        }

        public ActionResult Taxfree_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetTaxfreeFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
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
                return RedirectToAction("Login", "UserSchools");
            }
            else
            {
                loggedSchool = GetLoginSchool();
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
                return RedirectToAction("Login", "UserSchools");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }

            return View();
        }


        #region ΠΕΡΙΦΕΡΕΙΕΣ ΠΛΕΓΜΑ

        public ActionResult PeriferiaMajor_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetPeriferiaMajorFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
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

        #endregion

        #region ΝΟΜΟΙ ΠΛΕΓΜΑ

        public ActionResult Nomos_Read([DataSourceRequest] DataSourceRequest request, int periferiaId = 0)
        {
            var data = GetNomosFromDB(periferiaId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<NomosViewModel> GetNomosFromDB(int periferiaId = 0)
        {
            List<NomosViewModel> data = new List<NomosViewModel>();

            if (periferiaId > 0)
            {
                data = (from d in db.SysNomoi
                        where d.PeriferiaMajorID == periferiaId
                        orderby d.NomosText
                        select new NomosViewModel
                        {
                            NomosID = d.NomosID,
                            NomosText = d.NomosText,
                            PeriferiaMajorID = d.PeriferiaMajorID
                        }).ToList();
            }
            else
            {
                data = (from d in db.SysNomoi
                        orderby d.NomosText
                        select new NomosViewModel
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


        #region ΕΙΔΙΚΟΤΗΤΕΣ ΠΛΕΓΜΑ ΠΡΟΒΟΛΗΣ

        public ActionResult EidikotitesList()
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
            populateKladoi();
            populateKladoiUnified();

            return View();
        }

        public ActionResult Eidikotita_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetEidikotitesFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
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
                return RedirectToAction("Login", "UserSchools");
            }
            else
            {
                loggedSchool = GetLoginSchool();
                return View();
            }
        }

        #endregion


        #region ΕΝΙΑΙΟΙ ΚΛΑΔΟΙ ΚΑΙ ΕΝΤΑΓΜΕΝΕΣ ΕΙΔΙΚΟΤΗΤΕΣ

        public ActionResult KladosUnifiedList()
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

            populateKlados();
            populateEidikotitesKladosEniaios();
            return View();
        }

        public ActionResult KladosUnified_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetKladosUnifiedFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
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

        public ActionResult KladosUnifiedPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "UserSchools");
            }
            else
            {
                loggedSchool = GetLoginSchool();
                return View();
            }
        }


        #region ΠΛΕΓΜΑ ΕΝΤΑΓΜΕΝΩΝ ΕΙΔΙΚΟΤΗΤΩΝ ΣΕ ΕΝΙΑΙΟ ΚΛΑΔΟ

        public ActionResult EidikotitaKladosEniaios_Read([DataSourceRequest] DataSourceRequest request, int kladosunifiedId = 0)
        {
            var data = GetEidikotitesKladosEniaios(kladosunifiedId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
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

        #endregion


        #region ΟΜΑΔΕΣ ΕΙΔΙΚΟΤΗΤΩΝ ΚΑΙ ΕΝΤΑΞΗ ΕΙΔΙΚΟΤΗΤΩΝ

        public ActionResult EidikotitesGroupsList()
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

            populateSqlEidikotites();
            populateKladoi();
            return View();
        }

        public ActionResult EidikotitaGroup_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetEidikotitesGroupsFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<EidikotitesGroupsViewModel> GetEidikotitesGroupsFromDB()
        {

            var data = (from d in db.SysEidikotitesGroups
                        orderby d.Klados, d.GroupText
                        where d.GroupText != null
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
                return RedirectToAction("Login", "UserSchools");
            }
            else
            {
                loggedSchool = GetLoginSchool();
                return View();
            }
        }


        #region ΠΛΕΓΜΑ ΕΝΤΑΓΜΕΝΩΝ ΕΙΔΙΚΟΤΗΤΩΝ ΣΕ ΟΜΑΔΑ

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
                return RedirectToAction("Login", "UserSchools");
            }
            else
            {
                loggedSchool = GetLoginSchool();
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


        #region ΠΡΟΚΗΡΥΣΣΟΜΕΝΕΣ ΕΙΔΙΚΟΤΗΤΕΣ ΠΡΟΒΟΛΗ

        public ActionResult EidikotitesProkirixis(string notify = null)
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

        public ActionResult EidikotitesProkirixiPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "UserSchools");
            }
            else
            {
                loggedSchool = GetLoginSchool();
                return View();
            }
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

        public void populatePeriferies()
        {
            var Periferies = (from k in db.SysPeriferies
                              select k).ToList();

            ViewData["periferies"] = Periferies;
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