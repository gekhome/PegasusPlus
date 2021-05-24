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
using System.IO;

namespace PegasusPlus.Controllers.DataControllers
{
    public class SchoolController : Controller
    {
        private PegasusPlusDBEntities db = new PegasusPlusDBEntities();
        private UserSchools loggedSchool;
        private int prokirixiId;

        private const string UPLOAD_PATH = "~/Uploads/FilesPersonal/";
        private const string EPIMORFOSI_PATH = "~/Uploads/FilesEpimorfosi/";

        Common c = new Common();
        Kerberos k = new Kerberos();

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


        #region ΕΛΕΓΧΟΣ ΑΙΤΗΣΕΩΝ

        public ActionResult AitiseisAudit(string notify = null)
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
            if (notify != null)
            {
                this.AddNotification(notify, NotificationType.WARNING);
            }

            prokirixiId = c.GetAdminProkirixiID();
            if (prokirixiId == 0)
            {
                string msg = "Δεν βρέθηκε ενεργοποιημένη προκήρυξη για διαχείριση από τα σχολεία.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }

            int schoolYearId = (int)c.GetActiveProkirixi().SchoolYear;
            ViewData["prokirixiProtocol"] = c.GetActiveProkirixi().Protocol;
            ViewData["schoolYearText"] = c.GetSchoolYearText(schoolYearId);

            populateSchoolYears();
            populateTeachTypes();
            populateIncomeYears();

            return View();
        }

        public ActionResult AitiseisAudit_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetAitiseisAllFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<sqlTeacherAitiseis> GetAitiseisAllFromDB()
        {
            List<sqlTeacherAitiseis> data = new List<sqlTeacherAitiseis>();

            loggedSchool = GetLoginSchool();
            prokirixiId = c.GetActiveProkirixiID();

            if (prokirixiId > 0)
            {
                data = (from d in db.sqlTeacherAitiseis
                        where d.ProkirixisID == prokirixiId && d.SchoolID == loggedSchool.UserSchoolID
                        orderby d.FullName, d.SchoolName
                        select d).ToList();
            }
            return (data);
        }

        public bool AitiseisExist(int prokirixiId)
        {
            loggedSchool = GetLoginSchool();

            var data = (from d in db.sqlTeacherAitiseis
                        where d.ProkirixisID == prokirixiId && d.SchoolID == loggedSchool.UserSchoolID
                        select d).Count();

            if (data == 0) return false;
            else return true;
        }

        public ActionResult AitisiPrint(int aitisiId = 0)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "UserSchools");
            }
            else
            {
                loggedSchool = GetLoginSchool();
                AitisisParameters parameters = new AitisisParameters();
                parameters.AitisiID = aitisiId;

                return View(parameters);
            }
        }


        #region ATISIS SCHOOLS

        public ActionResult AitisiSchools(int aitisiId = 0)
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

            var selectedAitisi = (from a in db.Aitisis
                                  where a.AitisisID == aitisiId
                                  select new AitisisViewModel
                                  {
                                      AitisisID = a.AitisisID,
                                      AitisisProtocol = a.AitisisProtocol,
                                      TeacherAFM = a.TeacherAFM,
                                      AitisisDate = a.AitisisDate,
                                      Periferia = a.Periferia,
                                      Eidikotita = a.Eidikotita,
                                      Klados = a.Klados
                                  }).FirstOrDefault();

            populateViewBagWithAitisi(selectedAitisi);
            populateSchools();

            return View(selectedAitisi);
        }

        #region SCHOOLS CHILD GRID

        public ActionResult Schools_Read([DataSourceRequest] DataSourceRequest request, int aitisiID = 0)
        {
            var data = GetAitisiSchoolsFromDB(aitisiID);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Schools_Create([DataSourceRequest] DataSourceRequest request, AitisiSchoolsViewModel data, int aitisiID = 0)
        {
            var newdata = new AitisiSchoolsViewModel();

            if (aitisiID == 0)
                ModelState.AddModelError("", "Πρέπει να επιλέξετε πρώτα μια αίτηση με κλικ επάνω στη γραμμή του πλέγματος.");

            if (data != null && ModelState.IsValid)
            {
                var savedSchools = db.AitisiSchools.Where(x => x.AitisiID == aitisiID && x.SchoolID == data.SchoolID).Count();
                if (savedSchools == 0)
                {
                    AitisiSchools entity = new AitisiSchools()
                    {
                        AitisiID = aitisiID,
                        PeriferiaID = (from d in db.Aitisis where d.AitisisID == aitisiID select d.Periferia).FirstOrDefault() ?? 0,
                        SchoolID = data.SchoolID
                    };
                    db.Entry(entity).State = EntityState.Added;
                    db.AitisiSchools.Add(entity);
                    db.SaveChanges();

                    data.RowID = entity.RowID;
                    newdata = RefreshAitisiSchoolsFromDB(data.RowID);
                }
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Schools_Update([DataSourceRequest] DataSourceRequest request, AitisiSchoolsViewModel data, int aitisiID = 0)
        {
            var newdata = new AitisiSchoolsViewModel();

            if (aitisiID == 0)
                ModelState.AddModelError("", "Πρέπει να επιλέξετε πρώτα μια αίτηση με κλικ επάνω στη γραμμή του πλέγματος.");

            if (data != null && ModelState.IsValid)
            {
                AitisiSchools entity = db.AitisiSchools.Find(data.RowID);

                entity.AitisiID = data.AitisiID;
                entity.SchoolID = data.SchoolID;
                entity.PeriferiaID = (from d in db.Aitisis where d.AitisisID == aitisiID select d.Periferia).FirstOrDefault() ?? 0;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();

                newdata = RefreshAitisiSchoolsFromDB(entity.RowID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Schools_Destroy([DataSourceRequest] DataSourceRequest request, AitisiSchoolsViewModel data)
        {
            if (data != null)
            {
                AitisiSchools entitySchool = db.AitisiSchools.Find(data.RowID);
                db.Entry(entitySchool).State = EntityState.Deleted;
                db.AitisiSchools.Remove(entitySchool);
                db.SaveChanges();
            }

            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public AitisiSchoolsViewModel RefreshAitisiSchoolsFromDB(int recordId)
        {
            var data = (from d in db.AitisiSchools
                        where d.RowID == recordId
                        select new AitisiSchoolsViewModel
                        {
                            RowID = d.RowID,
                            AitisiID = d.AitisiID,
                            SchoolID = d.SchoolID,
                            PeriferiaID = d.PeriferiaID
                        }).FirstOrDefault();

            return (data);
        }

        public List<AitisiSchoolsViewModel> GetAitisiSchoolsFromDB(int aitisiID)
        {
            var data = (from d in db.AitisiSchools
                        where d.AitisiID == aitisiID
                        orderby d.SchoolID
                        select new AitisiSchoolsViewModel
                        {
                            RowID = d.RowID,
                            AitisiID = d.AitisiID,
                            SchoolID = d.SchoolID,
                            PeriferiaID = d.PeriferiaID
                        }).ToList();

            return (data);
        }

        #endregion

        public void populateViewBagWithAitisi(AitisisViewModel selectedAitisi)
        {
            ViewBag.SelectedAitisiData = selectedAitisi;

            ViewBag.SelectedAitisiTeacher = (from d in db.Teachers
                                             where d.AFM == selectedAitisi.TeacherAFM
                                             select new TeacherViewModel
                                             {
                                                 AFM = d.AFM,
                                                 LastName = d.LastName,
                                                 FirstName = d.FirstName
                                             }).FirstOrDefault();

            ViewBag.SelectedAitisiEidikotita = (from e in db.sqlEidikotitesSelector
                                                where e.EidikotitaID == selectedAitisi.Eidikotita
                                                select new EidikotitesSelectorViewModel
                                                {
                                                    EidikotitaID = e.EidikotitaID,
                                                    EidikotitaDesc = e.EidikotitaDesc,
                                                }).FirstOrDefault();
            return;
        }

        #endregion


        #region TEACHING GRID (CHILD 1)

        public ActionResult Teaching_Read([DataSourceRequest] DataSourceRequest request, int aitisiId)
        {
            var data = GetTeachingModelFromDB(aitisiId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Teaching_Update([DataSourceRequest] DataSourceRequest request, WorkTeachingViewModel data, int aitisiId)
        {
            var newdata = new WorkTeachingViewModel();

            if (aitisiId > 0)
            {
                if (data != null & ModelState.IsValid)
                {
                    WorkTeaching entity = db.WorkTeaching.Find(data.ExperienceID);

                    entity.AitisiID = aitisiId;
                    entity.ProkirixiID = c.GetOpenProkirixiID();
                    entity.SchoolYear = data.SchoolYear;
                    entity.TeachType = data.TeachType;
                    entity.DateStart = data.DateStart;
                    entity.DateFinal = data.DateFinal;
                    entity.HoursTotal = data.HoursTotal;
                    entity.HoursWeek = data.HoursWeek;
                    entity.DocumentProtocol = data.DocumentProtocol;
                    entity.DocumentForeas = data.DocumentForeas;
                    entity.Moria = k.MoriaTeaching(data);
                    entity.Valid = data.Valid;
                    entity.SchoolYearText = c.GetSchoolYearText((int)data.SchoolYear);

                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                    newdata = RefreshTeachingFromDB(entity.ExperienceID);
                }
            }
            else
            {
                ModelState.AddModelError("", "Δεν έχει γίνει επιλογή αίτησης. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public WorkTeachingViewModel RefreshTeachingFromDB(int recordId = 0)
        {
            var data = (from d in db.WorkTeaching
                        where d.ExperienceID == recordId
                        select new WorkTeachingViewModel
                        {
                            ExperienceID = d.ExperienceID,
                            AitisiID = d.AitisiID,
                            ProkirixiID = d.ProkirixiID,
                            SchoolYear = d.SchoolYear,
                            TeachType = d.TeachType,
                            DateStart = d.DateStart,
                            DateFinal = d.DateFinal,
                            HoursTotal = d.HoursTotal,
                            HoursWeek = d.HoursWeek,
                            Moria = d.Moria,
                            DocumentProtocol = d.DocumentProtocol,
                            DocumentForeas = d.DocumentForeas,
                            Valid = d.Valid ?? true
                        }).FirstOrDefault();

            return data;
        }

        public List<WorkTeachingViewModel> GetTeachingModelFromDB(int aitisiId)
        {
            var data = (from d in db.WorkTeaching
                        where d.AitisiID == aitisiId
                        orderby d.SchoolYearText descending, d.TeachType
                        select new WorkTeachingViewModel
                        {
                            ExperienceID = d.ExperienceID,
                            AitisiID = d.AitisiID,
                            ProkirixiID = d.ProkirixiID,
                            SchoolYear = d.SchoolYear,
                            TeachType = d.TeachType,
                            DateStart = d.DateStart,
                            DateFinal = d.DateFinal,
                            HoursTotal = d.HoursTotal,
                            HoursWeek = d.HoursWeek,
                            Moria = d.Moria,
                            DocumentProtocol = d.DocumentProtocol,
                            DocumentForeas = d.DocumentForeas,
                            Valid = d.Valid ?? true,
                            SchoolYearText = d.SchoolYearText,
                            TeacherAFM = d.TeacherAFM
                        }).ToList();

            return data;
        }

        #endregion

        #region VOCATION GRID (CHILD 2)

        public ActionResult Vocation_Read([DataSourceRequest] DataSourceRequest request, int aitisiId)
        {
            var data = GetVocationModelFromDB(aitisiId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Vocation_Update([DataSourceRequest] DataSourceRequest request, WorkVocationViewModel data, int aitisiId)
        {
            var newdata = new WorkVocationViewModel();

            if (aitisiId > 0)
            {
                if (data != null & ModelState.IsValid)
                {
                    WorkVocation entity = db.WorkVocation.Find(data.ExperienceID);

                    entity.AitisiID = aitisiId;
                    entity.ProkirixiID = c.GetOpenProkirixiID();
                    entity.DateStart = data.DateStart;
                    entity.DateFinal = data.DateFinal;
                    entity.DaysAuto = k.SetDaysAutoVocation(data);
                    entity.DaysManual = data.DaysManual;
                    entity.DocumentProtocol = data.DocumentProtocol;
                    entity.DocumentForeas = data.DocumentForeas;
                    entity.Position = data.Position;
                    entity.Subject = data.Subject;
                    entity.Moria = k.MoriaVocation(data);
                    entity.Valid = data.Valid;

                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                    newdata = RefreshVocationModelFromDB(entity.ExperienceID);
                }
            }
            else
            {
                ModelState.AddModelError("", "Δεν έχει γίνει επιλογή αίτησης. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public WorkVocationViewModel RefreshVocationModelFromDB(int recordId)
        {
            var data = (from d in db.WorkVocation
                        where d.ExperienceID == recordId
                        select new WorkVocationViewModel
                        {
                            ExperienceID = d.ExperienceID,
                            AitisiID = d.AitisiID,
                            ProkirixiID = d.ProkirixiID,
                            DateStart = d.DateStart,
                            DateFinal = d.DateFinal,
                            DaysAuto = d.DaysAuto,
                            DaysManual = d.DaysManual,
                            Moria = d.Moria,
                            DocumentProtocol = d.DocumentProtocol,
                            DocumentForeas = d.DocumentForeas,
                            Position = d.Position,
                            Subject = d.Subject,
                            Valid = d.Valid ?? true
                        }).FirstOrDefault();

            return data;
        }

        public List<WorkVocationViewModel> GetVocationModelFromDB(int aitisiId)
        {
            var data = (from d in db.WorkVocation
                        where d.AitisiID == aitisiId
                        orderby d.DateStart descending
                        select new WorkVocationViewModel
                        {
                            ExperienceID = d.ExperienceID,
                            AitisiID = d.AitisiID,
                            ProkirixiID = d.ProkirixiID,
                            DateStart = d.DateStart,
                            DateFinal = d.DateFinal,
                            DaysAuto = d.DaysAuto,
                            DaysManual = d.DaysManual,
                            Moria = d.Moria,
                            DocumentProtocol = d.DocumentProtocol,
                            DocumentForeas = d.DocumentForeas,
                            Position = d.Position,
                            Subject = d.Subject,
                            Valid = d.Valid ?? true,
                            TeacherAFM = d.TeacherAFM
                        }).ToList();

            return data;
        }

        #endregion

        #region FREELANCE GRID (CHILD 3)

        public ActionResult Freelance_Read([DataSourceRequest] DataSourceRequest request, int aitisiId)
        {
            var data = GetFreelanceModelFromDB(aitisiId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Freelance_Update([DataSourceRequest] DataSourceRequest request, WorkFreelanceViewModel data, int aitisiId)
        {
            var newdata = new WorkFreelanceViewModel();
            float _taxfree = 0;
            string _nomisma = "";

            if (aitisiId > 0)
            {
                if (data != null & ModelState.IsValid)
                {
                    WorkFreelance entity = db.WorkFreelance.Find(data.ExperienceID);

                    _taxfree = setIncomeTaxfree((int)data.IncomeYear);
                    _nomisma = setIncomeNomisma((int)data.IncomeYear);

                    entity.AitisiID = aitisiId;
                    entity.ProkirixiID = c.GetOpenProkirixiID();
                    entity.DateStart = data.DateStart;
                    entity.DateFinal = data.DateFinal;
                    entity.DaysAuto = k.SetDaysAutoFreelance(data);
                    entity.DaysManual = data.DaysManual;
                    entity.IncomeYear = data.IncomeYear;
                    entity.Income = data.Income;
                    entity.IncomeTaxFree = _taxfree;
                    entity.IncomeCurrency = _nomisma;
                    entity.WorkEvidence = data.WorkEvidence;
                    entity.Subject = data.Subject;
                    entity.Moria = k.MoriaFreelance(data);
                    entity.Valid = data.Valid;

                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                    newdata = RefreshFreelanceModelFromDB(entity.ExperienceID);
                }
            }
            else
            {
                ModelState.AddModelError("", "Δεν έχει γίνει επιλογή αίτησης. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public WorkFreelanceViewModel RefreshFreelanceModelFromDB(int recordId)
        {
            var data = (from d in db.WorkFreelance
                        where d.ExperienceID == recordId
                        select new WorkFreelanceViewModel
                        {
                            ExperienceID = d.ExperienceID,
                            AitisiID = d.AitisiID,
                            ProkirixiID = d.ProkirixiID,
                            DateStart = d.DateStart,
                            DateFinal = d.DateFinal,
                            DaysAuto = d.DaysAuto,
                            DaysManual = d.DaysManual,
                            Income = d.Income,
                            IncomeYear = d.IncomeYear,
                            IncomeTaxFree = d.IncomeTaxFree,
                            IncomeCurrency = d.IncomeCurrency,
                            Moria = d.Moria,
                            WorkEvidence = d.WorkEvidence,
                            Subject = d.Subject,
                            Valid = d.Valid ?? true
                        }).FirstOrDefault();

            return data;
        }

        public List<WorkFreelanceViewModel> GetFreelanceModelFromDB(int aitisiId)
        {
            var data = (from d in db.WorkFreelance
                        where d.AitisiID == aitisiId
                        orderby d.DateStart descending
                        select new WorkFreelanceViewModel
                        {
                            ExperienceID = d.ExperienceID,
                            AitisiID = d.AitisiID,
                            ProkirixiID = d.ProkirixiID,
                            DateStart = d.DateStart,
                            DateFinal = d.DateFinal,
                            DaysAuto = d.DaysAuto,
                            DaysManual = d.DaysManual,
                            Income = d.Income,
                            IncomeYear = d.IncomeYear,
                            IncomeTaxFree = d.IncomeTaxFree,
                            IncomeCurrency = d.IncomeCurrency,
                            Moria = d.Moria,
                            WorkEvidence = d.WorkEvidence,
                            Subject = d.Subject,
                            Valid = d.Valid ?? true,
                            TeacherAFM = d.TeacherAFM
                        }).ToList();

            return data;
        }

        #endregion


        #region AITISI EDIT FORM

        public ActionResult EditAitisi(int? aitisiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
                return RedirectToAction("Login", "UserSchools");
            else
                loggedSchool = GetLoginSchool();

            AitisisViewModel aitisi = GetAitisiModelFromDB((int)aitisiId);

            if (aitisi == null)
            {
                string msg = "Προέκυψε αδυναμία εύρεσης της επιλεγμένης αίτησης.";
                return RedirectToAction("ErrorData", "School", new { notify = msg });
            }

            Teachers data = (from d in db.Teachers where d.AFM == aitisi.TeacherAFM select d).FirstOrDefault();
            ViewBag.TeacherInfo = data;

            return View(aitisi);
        }

        [HttpPost]
        public ActionResult EditAitisi(AitisisViewModel model)
        {
            int aitisiId = model.AitisisID;
            var ap = (from d in db.sqlAitisiProsonta where d.AitisisID == aitisiId select d).FirstOrDefault();

            if (ModelState.IsValid)
            {
                Aitisis entity = db.Aitisis.Find(aitisiId);

                entity.AitisisDate = DateTime.Now.Date;
                entity.Klados = model.Klados;
                entity.Eidikotita = model.Eidikotita;
                entity.EidikotitaGroup = model.Eidikotita.HasValue ? c.GetEidikotitaGroupId((int)model.Eidikotita) : null;
                entity.EpagelmaCategory = model.EpagelmaCategory;
                entity.BasicEducation = model.BasicEducation;
                entity.PtyxioType = model.PtyxioType;
                entity.PtyxioTitlos = model.PtyxioTitlos;
                entity.PtyxioInstitution = model.PtyxioInstitution;
                entity.PtyxioSchool = model.PtyxioSchool;
                entity.PtyxioTmima = model.PtyxioTmima;
                entity.PtyxioGrade = model.PtyxioGrade;
                entity.PtyxioYear = model.PtyxioYear;
                entity.PtyxioForeign = model.PtyxioForeign;
                entity.PtyxioAnagnorisi = model.PtyxioAnagnorisi;
                entity.MscExists = model.MscExists;
                entity.MscTitlos = model.MscTitlos;
                entity.MscInstitution = model.MscInstitution;
                entity.MscYear = model.MscYear;
                entity.MscSynafeia = model.MscSynafeia;
                entity.MscForeign = model.MscForeign;
                entity.MscAnagnorisi = model.MscAnagnorisi;
                entity.PhdExists = model.PhdExists;
                entity.PhdTitlos = model.PhdTitlos;
                entity.PhdInstitution = model.PhdInstitution;
                entity.PhdYear = model.PhdYear;
                entity.PhdSynafeia = model.PhdSynafeia;
                entity.PhdForeign = model.PhdForeign;
                entity.PhdAnagnorisi = model.PhdAnagnorisi;
                entity.EpagelmaCategory = model.EpagelmaCategory;
                entity.Periferia = model.Periferia;
                entity.School = model.School;
                entity.OaedPtyxioConfirm = model.OaedPtyxioConfirm;
                entity.OaedMscConfirm = model.OaedMscConfirm;
                entity.OaedPhdConfirm = model.OaedPhdConfirm;
                entity.OaedMoriodotisiDate = DateTime.Now.Date;
                entity.OaedApoklismos = model.OaedApoklismos;
                entity.OaedApoklismosAitia = model.OaedApoklismosAitia;
                entity.OaedCheckStatus = model.OaedCheckStatus;
                entity.OaedEnstasi = model.OaedEnstasi;
                entity.OaedEpitropi1Text = model.OaedEpitropi1Text;
                entity.OaedEpitropi2Text = model.OaedEpitropi2Text;
                // Moria calculations
                if (ap != null)
                {
                    entity.MoriaCertified = k.MoriaCertifiedTrainer(ap);
                    entity.MoriaComputer = k.MoriaComputer(ap);
                    entity.MoriaEpimorfosi = k.MoriaEpimorfosi(ap);
                    entity.MoriaLanguage = k.MoriaLanguages(ap);
                    entity.MoriaMsc = k.MoriaMsc(ap);
                    entity.MoriaPhd = k.MoriaPhd(ap);
                    entity.MoriaPtyxio = k.MoriaPtyxio(ap);
                    entity.MoriaTeach = k.MoriaTeach(ap);
                    entity.MoriaWork = k.MoriaWork(ap);
                    entity.MoriaAnergia = k.MoriaAnergia(ap);
                    entity.MoriaSocial = k.MoriaSocial(ap);
                    entity.MoriaTotal = k.MoriaTotal(ap);
                }

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                this.AddNotification("Η αποθήκευση της αίτησης ολοκληρώθηκε με επιτυχία.", NotificationType.SUCCESS);
                AitisisViewModel newData = GetAitisiModelFromDB(entity.AitisisID);

                Teachers data = (from d in db.Teachers where d.AFM == entity.TeacherAFM select d).FirstOrDefault();
                ViewBag.TeacherInfo = data;

                return View(newData);
            }
            else
            {
                Teachers data = (from d in db.Teachers where d.AFM == model.TeacherAFM select d).FirstOrDefault();
                ViewBag.TeacherInfo = data;

                this.AddNotification("Η αποθήκευση απέτυχε λόγω σφαλμάτων καταχώρησης. Δείτε τη σύνοψη στο κάτω μέρος της σελίδας.", NotificationType.ERROR);
                return View(model);
            }
        }

        public AitisisViewModel GetAitisiModelFromDB(int aitisiId)
        {
            var data = (from d in db.Aitisis
                        where d.AitisisID == aitisiId
                        select new AitisisViewModel
                        {
                            AitisisID = d.AitisisID,
                            AitisisDate = d.AitisisDate,
                            AitisisProtocol = d.AitisisProtocol,
                            ProkirixisID = d.ProkirixisID,
                            TeacherAFM = d.TeacherAFM,
                            Klados = d.Klados,
                            Eidikotita = d.Eidikotita,
                            BasicEducation = d.BasicEducation,
                            PtyxioType = d.PtyxioType,
                            PtyxioTitlos = d.PtyxioTitlos,
                            PtyxioInstitution = d.PtyxioInstitution,
                            PtyxioSchool = d.PtyxioSchool,
                            PtyxioTmima = d.PtyxioTmima,
                            PtyxioGrade = d.PtyxioGrade,
                            PtyxioYear = d.PtyxioYear,
                            PtyxioForeign = d.PtyxioForeign ?? false,
                            PtyxioAnagnorisi = d.PtyxioAnagnorisi ?? false,
                            PtyxioFilename = d.PtyxioFilename,
                            PtyxioAnagnorisiFilename = d.PtyxioAnagnorisiFilename,
                            MscTitlos = d.MscTitlos,
                            MscInstitution = d.MscInstitution,
                            MscYear = d.MscYear,
                            MscSynafeia = d.MscSynafeia,
                            MscForeign = d.MscForeign ?? false,
                            MscAnagnorisi = d.MscAnagnorisi ?? false,
                            MscFilename = d.MscFilename,
                            MscAnagnorisiFilename = d.MscAnagnorisiFilename,
                            PhdTitlos = d.PhdTitlos,
                            PhdInstitution = d.PhdInstitution,
                            PhdYear = d.PhdYear,
                            PhdSynafeia = d.PhdSynafeia,
                            PhdForeign = d.PhdForeign ?? false,
                            PhdAnagnorisi = d.PhdAnagnorisi ?? false,
                            PhdFilename = d.PhdFilename,
                            PhdAnagnorisiFilename = d.PhdAnagnorisiFilename,
                            EpagelmaCategory = d.EpagelmaCategory,
                            Periferia = d.Periferia,
                            School = d.School,
                            MscExists = d.MscExists ?? false,
                            PhdExists = d.PhdExists ?? false,
                            OaedApoklismos = d.OaedApoklismos ?? false,
                            OaedApoklismosAitia = d.OaedApoklismosAitia,
                            OaedCheckStatus = d.OaedCheckStatus ?? false,
                            OaedEnstasi = d.OaedEnstasi ?? false,
                            OaedEpitropi1Text = d.OaedEpitropi1Text,
                            OaedEpitropi2Text = d.OaedEpitropi2Text,
                            OaedMoriodotisiDate = d.OaedMoriodotisiDate,
                            OaedMscConfirm = d.OaedMscConfirm ?? false,
                            OaedPhdConfirm = d.OaedPhdConfirm ?? false,
                            OaedPtyxioConfirm = d.OaedPtyxioConfirm ?? false,
                            MoriaCertified = d.MoriaCertified ?? 0.0m,
                            MoriaComputer = d.MoriaComputer ?? 0.0m,
                            MoriaEpimorfosi = d.MoriaEpimorfosi ?? 0.0m,
                            MoriaLanguage = d.MoriaLanguage ?? 0.0m,
                            MoriaPtyxio = d.MoriaPtyxio ?? 0.0m,
                            MoriaMsc = d.MoriaMsc ?? 0.0m,
                            MoriaPhd = d.MoriaPhd ?? 0.0m,
                            MoriaTeach = d.MoriaTeach ?? 0.0m,
                            MoriaWork = d.MoriaWork ?? 0.0m,
                            MoriaAnergia = d.MoriaAnergia ?? 0.0m,
                            MoriaSocial = d.MoriaSocial ?? 0.0m,
                            MoriaTotal = d.MoriaTotal ?? 0.0m
                        }).FirstOrDefault();

            return data;
        }

        #endregion

        #region PERSONAL INFO EDIT FORM

        public ActionResult EditPersonal(int aitisiId)
        {
            //check if user is unauthenticated to redirect him
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
                return RedirectToAction("Login", "UserSchools");
            else
                loggedSchool = GetLoginSchool();

            prokirixiId = c.GetActiveProkirixiID();
            if (prokirixiId == 0)
            {
                return RedirectToAction("Index", "School", new { notify = "Δεν βρέθηκε ενεργοποιημένη προκήρυξη για διαχείριση από τα σχολεία." });
            }

            string afm = (from d in db.Aitisis where d.AitisisID == aitisiId select d).FirstOrDefault().TeacherAFM;

            TeacherViewModel model = GetTeacherDataFromDB(afm);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditPersonal(TeacherViewModel model)
        {
            if (ModelState.IsValid)
            {
                Teachers entity = db.Teachers.Find(model.AFM);

                entity.AFM = model.AFM;
                entity.ADT = model.ADT;
                entity.AMKA = model.AMKA;
                entity.AMA = model.AMA;
                entity.DOY = model.DOY;
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.FatherName = model.FatherName;
                entity.MotherName = model.MotherName;
                entity.Gender = model.Gender;
                entity.FamilyStatus = model.FamilyStatus;
                entity.Children = model.Children;
                entity.Birthdate = model.Birthdate;
                entity.Nomos = model.Nomos;
                entity.City = model.City;
                entity.Address = model.Address;
                entity.PostCode = model.PostCode;
                entity.Telephone = model.Telephone;
                entity.CellPhone = model.CellPhone;
                entity.Email = model.Email;
                entity.Epagelma = model.Epagelma;
                entity.Idiotita = model.Idiotita;
                entity.SocialTriteknos = model.SocialTriteknos;
                entity.SocialPolyteknos = model.SocialPolyteknos;
                entity.SocialSingleParent = model.SocialSingleParent;
                entity.SocialAmea = model.SocialAmea;
                entity.SocialAnergos = model.SocialAnergos;

                db.Entry(entity).State = EntityState.Modified;
                if (k.ValidateBirthdate(entity))
                {
                    db.SaveChanges();
                    this.AddNotification("Η αποθήκευση ολοκληρώθηκε με επιτυχία.", NotificationType.SUCCESS);
                    TeacherViewModel newData = GetTeacherDataFromDB(model.AFM);
                    return View(newData);
                }
                else
                {
                    this.AddNotification("Η αποθήκευση απέτυχε λόγω σφαλμάτων καταχώρησης.", NotificationType.ERROR);
                    ModelState.AddModelError("BirthDate", "Η ημερομηνία γέννησης είναι εκτός λογικών ορίων.");
                    return View(model);
                }
            }
            this.AddNotification("Η αποθήκευση απέτυχε λόγω σφαλμάτων καταχώρησης. Δείτε τη σύνοψη στο κάτω μέρος της σελίδας.", NotificationType.ERROR);
            return View(model);
        }

        public TeacherViewModel GetTeacherDataFromDB(string afm)
        {
            var data = (from d in db.Teachers
                        where d.AFM == afm
                        select new TeacherViewModel
                        {
                            AFM = d.AFM,
                            ADT = d.ADT,
                            AMKA = d.AMKA,
                            AMA = d.AMA,
                            DOY = d.DOY,
                            FirstName = d.FirstName,
                            LastName = d.LastName,
                            FatherName = d.FatherName,
                            MotherName = d.MotherName,
                            Gender = d.Gender,
                            FamilyStatus = d.FamilyStatus,
                            Children = d.Children,
                            Birthdate = d.Birthdate,
                            Nomos = d.Nomos,
                            City = d.City,
                            Address = d.Address,
                            PostCode = d.PostCode,
                            Telephone = d.Telephone,
                            CellPhone = d.CellPhone,
                            Email = d.Email,
                            Epagelma = d.Epagelma,
                            Idiotita = d.Idiotita,
                            SocialTriteknos = d.SocialTriteknos ?? false,
                            SocialPolyteknos = d.SocialPolyteknos ?? false,
                            SocialSingleParent = d.SocialSingleParent ?? false,
                            SocialAmea = d.SocialAmea ?? false,
                            SocialAnergos = d.SocialAnergos,
                            ADT_FILENAME = d.ADT_FILENAME,
                            AFM_FILENAME = d.AFM_FILENAME,
                            AMA_FILENAME = d.AMA_FILENAME,
                            AMKA_FILENAME = d.AMKA_FILENAME,
                            SocialTriteknosFilename = d.SocialTriteknosFilename,
                            SocialPolyteknosFilename = d.SocialPolyteknosFilename,
                            SocialSingleParentFilename = d.SocialSingleParentFilename,
                            SocialAmeaFilename = d.SocialAmeaFilename,
                            SocialAnergosFilename = d.SocialAnergosFilename
                        }).FirstOrDefault();
            return data;
        }

        #endregion

        #region SKILLS EDIT FORM

        public ActionResult EditSkills(int aitisiId = 0)
        {
            //check if user is unauthenticated to redirect him
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
                return RedirectToAction("Login", "UserSchools");
            else
                loggedSchool = GetLoginSchool();

            if (c.GetActiveProkirixiID() == 0)
            {
                return RedirectToAction("Index", "School", new { notify = "Δεν βρέθηκε ενεργοποιημένη προκήρυξη για επεξεργασία από τα σχολεία." });
            }

            string afm = (from d in db.Aitisis where d.AitisisID == aitisiId select d).FirstOrDefault().TeacherAFM;
            Teachers data = (from d in db.Teachers where d.AFM == afm select d).FirstOrDefault();
            ViewBag.TeacherInfo = data;

            TeacherSkillsViewModel model = GetSkillsModelFromDB(data.AFM);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditSkills(TeacherSkillsViewModel model)
        {
            if (ModelState.IsValid)
            {
                TeacherSkills entity = db.TeacherSkills.Where(d => d.TeacherAFM == model.TeacherAFM).FirstOrDefault();

                entity.TeacherAFM = model.TeacherAFM;
                entity.Ptyxio2Anagnorisi = model.Ptyxio2Anagnorisi;
                entity.Ptyxio2Foreign = model.Ptyxio2Foreign;
                entity.Ptyxio2Institution = model.Ptyxio2Institution;
                entity.Ptyxio2Titlos = model.Ptyxio2Titlos;
                entity.Ptyxio2Type = model.Ptyxio2Type;
                entity.Ptyxio2Year = model.Ptyxio2Year;
                entity.Language1 = model.Language1;
                entity.Language1Level = model.Language1Level;
                entity.Language1Titlos = model.Language1Titlos;
                entity.Language2 = model.Language2;
                entity.Language2Level = model.Language2Level;
                entity.Language2Titlos = model.Language2Titlos;
                entity.ComputerCertificate = model.ComputerCertificate;
                entity.ComputerTitlos = model.ComputerTitlos;
                entity.ComputerYear = model.ComputerYear;
                entity.CertifiedTrainer = model.CertifiedTrainer;
                entity.CertifiedTrainerAM = model.CertifiedTrainerAM;
                entity.Epimorfosi = model.Epimorfosi;
                entity.EpimorfosiTotalHours = model.EpimorfosiTotalHours;
                entity.N2190Protect = model.N2190Protect;
                entity.OaedPtyxio2Confirm = model.OaedPtyxio2Confirm;
                entity.OaedCertifiedConfirm = model.OaedCertifiedConfirm;
                entity.OaedComputerConfirm = model.OaedComputerConfirm;
                entity.OaedEpimorfosiConfirm = model.OaedEpimorfosiConfirm;
                entity.OaedLanguage1Confirm = model.OaedLanguage1Confirm;
                entity.OaedLanguage2Confirm = model.OaedLanguage2Confirm;
                entity.OaedN2190Confirm = model.OaedN2190Confirm;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                this.AddNotification("Η αποθήκευση ολοκληρώθηκε με επιτυχία.", NotificationType.SUCCESS);
                TeacherSkillsViewModel newData = GetSkillsModelFromDB(model.TeacherAFM);

                Teachers data = (from d in db.Teachers where d.AFM == model.TeacherAFM select d).FirstOrDefault();
                ViewBag.TeacherInfo = data;
                return View(newData);
            }
            else
            {
                Teachers data = (from d in db.Teachers where d.AFM == model.TeacherAFM select d).FirstOrDefault();
                ViewBag.TeacherInfo = data;
                this.AddNotification("Η αποθήκευση απέτυχε λόγω σφαλμάτων καταχώρησης. Δείτε τη σύνοψη στο κάτω μέρος της σελίδας.", NotificationType.ERROR);
                return View(model);
            }
        }

        public TeacherSkillsViewModel GetSkillsModelFromDB(string afm)
        {
            var data = (from d in db.TeacherSkills
                        where d.TeacherAFM == afm
                        select new TeacherSkillsViewModel
                        {
                            SkillsID = d.SkillsID,
                            TeacherAFM = d.TeacherAFM,
                            CertifiedTrainer = d.CertifiedTrainer ?? false,
                            CertifiedTrainerFilename = d.CertifiedTrainerFilename,
                            CertifiedTrainerAM = d.CertifiedTrainerAM,
                            ComputerCertificate = d.ComputerCertificate,
                            ComputerFilename = d.ComputerFilename,
                            ComputerTitlos = d.ComputerTitlos,
                            ComputerYear = d.ComputerYear,
                            Epimorfosi = d.Epimorfosi ?? false,
                            EpimorfosiTotalHours = d.EpimorfosiTotalHours,
                            Language1 = d.Language1,
                            Language1Filename = d.Language1Filename,
                            Language1Level = d.Language1Level,
                            Language1Titlos = d.Language1Titlos,
                            Language2 = d.Language2,
                            Language2Filename = d.Language2Filename,
                            Language2Level = d.Language2Level,
                            Language2Titlos = d.Language2Titlos,
                            N2190Protect = d.N2190Protect ?? false,
                            N2190ProtectFilename = d.N2190ProtectFilename,
                            Ptyxio2Anagnorisi = d.Ptyxio2Anagnorisi ?? false,
                            Ptyxio2AnagnorisiFilename = d.Ptyxio2AnagnorisiFilename,
                            Ptyxio2Filename = d.Ptyxio2Filename,
                            Ptyxio2Foreign = d.Ptyxio2Foreign ?? false,
                            Ptyxio2Institution = d.Ptyxio2Institution,
                            Ptyxio2Titlos = d.Ptyxio2Titlos,
                            Ptyxio2Type = d.Ptyxio2Type,
                            Ptyxio2Year = d.Ptyxio2Year,
                            OaedCertifiedConfirm = d.OaedCertifiedConfirm ?? false,
                            OaedComputerConfirm = d.OaedComputerConfirm ?? false,
                            OaedEpimorfosiConfirm = d.OaedEpimorfosiConfirm ?? false,
                            OaedLanguage1Confirm = d.OaedLanguage1Confirm ?? false,
                            OaedLanguage2Confirm = d.OaedLanguage2Confirm ?? false,
                            OaedN2190Confirm = d.OaedN2190Confirm ?? false,
                            OaedPtyxio2Confirm = d.OaedPtyxio2Confirm ?? false
                        }).FirstOrDefault();

            return data;
        }

        #endregion

        #region UPLOADED FILES PAGE

        public ActionResult TeacherUploadsSchool(string afm = null)
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

            ViewData["teacherAFM"] = afm;

            return View();
        }

        public ActionResult UploadFiles_Read([DataSourceRequest] DataSourceRequest request, string afm = null)
        {
            var data = GetTeacherUploadsFromDB(afm);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<TeacherUploadViewModel> GetTeacherUploadsFromDB(string afm = null)
        {
            var data = (from d in db.TeacherUploads
                        where d.TeacherAFM == afm
                        orderby d.FileName
                        select new TeacherUploadViewModel
                        {
                            UploadFileID = d.UploadFileID,
                            FileName = d.FileName,
                            Description = d.Description,
                            SchoolYearText = d.SchoolYearText,
                            TeacherAFM = d.TeacherAFM,
                        }).ToList();

            return data;
        }

        public FileResult Download(int file_id)
        {
            string filename = "";
            string physicalPath = "";

            var fileinfo = (from d in db.TeacherUploads where d.UploadFileID == file_id select d).FirstOrDefault();
            if (fileinfo != null)
            {
                filename = fileinfo.FileName;
                physicalPath = UPLOAD_PATH + fileinfo.TeacherAFM + "/";
            }

            return File(Path.Combine(Server.MapPath(physicalPath), filename), System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }

        #endregion

        #region EPIMORFOSEIS PAGE

        public ActionResult EpimorfoseisSchool(string afm = null)
        {
            //check if user is unauthenticated to redirect him
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
                return RedirectToAction("Login", "UserSchools");
            else
                loggedSchool = GetLoginSchool();

            ViewData["teacherAfm"] = afm;

            return View();
        }

        public ActionResult Epimorfosi_Read([DataSourceRequest] DataSourceRequest request, string afm = null)
        {
            var data = GetEpimorfoseisFromDB(afm);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Epimorfosi_Update([DataSourceRequest] DataSourceRequest request, EpimorfosiViewModel data)
        {
            var newdata = new EpimorfosiViewModel();

            if (data != null && ModelState.IsValid)
            {
                Epimorfosis entity = db.Epimorfosis.Find(data.EpimorfosiID);

                entity.EpimorfosiTitlos = data.EpimorfosiTitlos;
                entity.EpimorfosiForeas = data.EpimorfosiForeas;
                entity.EpimorfosiDateStart = data.EpimorfosiDateStart;
                entity.EpimorfosiDateFinal = data.EpimorfosiDateFinal;
                entity.EpimorfosiHours = data.EpimorfosiHours;
                entity.EpimorfosiType = data.EpimorfosiType;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                newdata = RefreshEpimorfosiFromDB(entity.EpimorfosiID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public EpimorfosiViewModel RefreshEpimorfosiFromDB(int recordId)
        {
            var data = (from d in db.Epimorfosis
                        where d.EpimorfosiID == recordId
                        select new EpimorfosiViewModel
                        {
                            EpimorfosiID = d.EpimorfosiID,
                            EpimorfosiTitlos = d.EpimorfosiTitlos,
                            EpimorfosiForeas = d.EpimorfosiForeas,
                            EpimorfosiHours = d.EpimorfosiHours,
                            EpimorfosiDateStart = d.EpimorfosiDateStart,
                            EpimorfosiDateFinal = d.EpimorfosiDateFinal,
                            EpimorfosiType = d.EpimorfosiType
                        }).FirstOrDefault();

            return data;
        }

        public List<EpimorfosiViewModel> GetEpimorfoseisFromDB(string afm = null)
        {
            var data = (from d in db.Epimorfosis
                        where d.TeacherAfm == afm
                        orderby d.EpimorfosiDateStart descending
                        select new EpimorfosiViewModel
                        {
                            EpimorfosiID = d.EpimorfosiID,
                            EpimorfosiTitlos = d.EpimorfosiTitlos,
                            EpimorfosiForeas = d.EpimorfosiForeas,
                            EpimorfosiHours = d.EpimorfosiHours,
                            EpimorfosiDateStart = d.EpimorfosiDateStart,
                            EpimorfosiDateFinal = d.EpimorfosiDateFinal,
                            EpimorfosiType = d.EpimorfosiType
                        }).ToList();

            return data;
        }

        // CHILD GRID WITH FILES
        public ActionResult EpimorfosiFiles_Read([DataSourceRequest] DataSourceRequest request, int epimorfosiId = 0)
        {
            var data = GetEpimorfosiFilesFromDB(epimorfosiId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<EpimorfosiFileViewModel> GetEpimorfosiFilesFromDB(int epimorfosiId = 0)
        {
            var data = (from d in db.EpimorfosiFiles
                        where d.EpimorfosiID == epimorfosiId
                        orderby d.Filename
                        select new EpimorfosiFileViewModel
                        {
                            UploadFileID = d.UploadFileID,
                            Description = d.Description,
                            Filename = d.Filename,
                            EpimorfosiID = d.EpimorfosiID,
                            TeacherAFM = d.TeacherAFM
                        }).ToList();

            return (data);
        }

        public FileResult DownloadEpimorfosiFile(int file_id)
        {
            string f = "";
            string uploadPath = "";
            var fileinfo = (from d in db.EpimorfosiFiles where d.UploadFileID == file_id select d).FirstOrDefault();

            if (fileinfo != null)
            {
                f = fileinfo.Filename;
                uploadPath = EPIMORFOSI_PATH + fileinfo.Epimorfosis.TeacherAfm + "/";
            }
            return File(Path.Combine(Server.MapPath(uploadPath), f), System.Net.Mime.MediaTypeNames.Application.Octet, f);
        }

        #endregion

        #region UPLOADED FILES GRIDS

        public ActionResult TeachingFiles_Read([DataSourceRequest] DataSourceRequest request, int expId = 0)
        {
            var data = GetUploadsTeachingFromDB(expId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<UploadsTeachingViewModel> GetUploadsTeachingFromDB(int expId = 0)
        {
            var data = (from d in db.UploadsTeaching
                        where d.ExperienceID == expId
                        orderby d.FileName
                        select new UploadsTeachingViewModel
                        {
                            UploadFileID = d.UploadFileID,
                            FileName = d.FileName,
                            Description = d.Description,
                            Category = d.Category,
                            ExperienceID = d.ExperienceID,
                            TeacherAFM = d.TeacherAFM
                        }).ToList();

            return data;
        }

        public ActionResult VocationFiles_Read([DataSourceRequest] DataSourceRequest request, int expId = 0)
        {
            var data = GetUploadsVocationFromDB(expId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<UploadsVocationViewModel> GetUploadsVocationFromDB(int expId = 0)
        {
            var data = (from d in db.UploadsVocation
                        where d.ExperienceID == expId
                        orderby d.FileName
                        select new UploadsVocationViewModel
                        {
                            UploadFileID = d.UploadFileID,
                            FileName = d.FileName,
                            Description = d.Description,
                            Category = d.Category,
                            ExperienceID = d.ExperienceID,
                            TeacherAFM = d.TeacherAFM
                        }).ToList();

            return data;
        }

        public ActionResult FreelanceFiles_Read([DataSourceRequest] DataSourceRequest request, int expId = 0)
        {
            var data = GetUploadsFreelanceFromDB(expId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<UploadsFreelanceViewModel> GetUploadsFreelanceFromDB(int expId = 0)
        {
            var data = (from d in db.UploadsFreelance
                        where d.ExperienceID == expId
                        orderby d.FileName
                        select new UploadsFreelanceViewModel
                        {
                            UploadFileID = d.UploadFileID,
                            FileName = d.FileName,
                            Description = d.Description,
                            Category = d.Category,
                            ExperienceID = d.ExperienceID,
                            TeacherAFM = d.TeacherAFM
                        }).ToList();

            return data;
        }

        #endregion

        #endregion


        #region ΜΟΡΙΟΔΟΤΗΣΗ ΑΙΤΗΣΕΩΝ

        public ActionResult AitiseisMoria()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "UserSchools");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }

            int prokirixiId = c.GetAdminProkirixiID();
            if (prokirixiId == 0)
            {
                return RedirectToAction("Index", "School", new { notify = "Δεν βρέθηκε ενεργοποιημένη προκήρυξη για διαχείριση." });
            }

            int schoolYearId = (int)c.GetAdminProkirixi().SchoolYear;
            ViewData["prokirixiProtocol"] = c.GetAdminProkirixi().Protocol;
            ViewData["schoolYearText"] = c.GetSchoolYearText(schoolYearId);

            return View();
        }

        public ActionResult AitiseisMoria_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetTeacherAitiseisFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<TeacherAitiseisViewModel> GetTeacherAitiseisFromDB()
        {
            int prokirixiId = c.GetAdminProkirixiID();
            loggedSchool = GetLoginSchool();

            var data = (from d in db.sqlTeacherAitiseis
                        where d.ProkirixisID == prokirixiId && d.SchoolID == loggedSchool.UserSchoolID
                        orderby d.FullName, d.AitisisProtocol
                        select new TeacherAitiseisViewModel
                        {
                            AitisisID = d.AitisisID,
                            AitisisProtocol = d.AitisisProtocol,
                            TeacherAFM = d.TeacherAFM,
                            FullName = d.FullName,
                            EidikotitaText = d.EidikotitaText,
                            OaedCheckStatus = d.OaedCheckStatus ?? false,
                            OaedEnstasi = d.OaedEnstasi ?? false,
                            PeriferiaID = d.PeriferiaID,
                            PeriferiakiID = d.PeriferiakiID,
                            PeriferiakiName = d.PeriferiakiName,
                            PeriferiaName = d.PeriferiaName,
                            ProkirixisID = d.ProkirixisID,
                            Protocol = d.Protocol,
                            SchoolID = d.SchoolID,
                            SchoolName = d.SchoolName
                        }).ToList();

            return data;
        }

        public ActionResult BatchMoriodotisi()
        {
            prokirixiId = c.GetActiveProkirixiID();
            loggedSchool = GetLoginSchool();

            if (prokirixiId == 0)
            {
                string msg = "Δεν βρέθηκε ενεργοποιημένη προκήρυξη για τα σχολεία.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }

            var aitiseis = (from d in db.Aitisis where d.ProkirixisID == prokirixiId && d.School == loggedSchool.UserSchoolID select d).ToList();
            if (aitiseis.Count == 0)
            {
                string msg = "Δεν βρέθηκαν αιτήσεις σχολείου γι' αυτή την Προκήρυξη. Η μοριοδότηση ακυρώθηκε.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }

            foreach (var item in aitiseis)
            {
                var ap = (from d in db.sqlAitisiProsonta where d.AitisisID == item.AitisisID select d).FirstOrDefault();

                Aitisis entity = db.Aitisis.Find(item.AitisisID);

                entity.MoriaPtyxio = k.MoriaPtyxio(ap);
                entity.MoriaMsc = k.MoriaMsc(ap);
                entity.MoriaPhd = k.MoriaPhd(ap);
                entity.MoriaLanguage = k.MoriaLanguages(ap);
                entity.MoriaComputer = k.MoriaComputer(ap);
                entity.MoriaCertified = k.MoriaCertifiedTrainer(ap);
                entity.MoriaEpimorfosi = k.MoriaEpimorfosi(ap);
                entity.MoriaTeach = k.MoriaTeach(ap);
                entity.MoriaWork = k.MoriaWork(ap);
                entity.MoriaAnergia = k.MoriaAnergia(ap);
                entity.MoriaSocial = k.MoriaSocial(ap);
                entity.MoriaTotal = k.MoriaTotal(ap);

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
            string message = "Η μοριοδότηση όλων των αιτήσεων του σχολείου ολοκληρώθηκε.";
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AitisiMoriaView(int aitisiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
                return RedirectToAction("Login", "UserSchools");
            else
                loggedSchool = GetLoginSchool();

            AitisiDataViewModel model = GetAitisiDataFromDB(aitisiId);
            if (model == null)
            {
                model = new AitisiDataViewModel();
                this.AddNotification("Δεν βρέθηκαν δεδομένα για προβολή της αίτησης αυτής.", NotificationType.WARNING);
            }

            return View(model);
        }

        #endregion


        #region MORIA CALCULATION AND REPORT

        public AitisiDataViewModel CalculateMoriaAitisi(AitisiDataViewModel a)
        {
            var ap = (from d in db.sqlAitisiProsonta where d.AitisisID == a.AitisisID select d).FirstOrDefault();
            if (ap == null) return a;

            Aitisis entity = db.Aitisis.Find(a.AitisisID);

            entity.MoriaPtyxio = k.MoriaPtyxio(ap);
            entity.MoriaMsc = k.MoriaMsc(ap);
            entity.MoriaPhd = k.MoriaPhd(ap);
            entity.MoriaLanguage = k.MoriaLanguages(ap);
            entity.MoriaComputer = k.MoriaComputer(ap);
            entity.MoriaCertified = k.MoriaCertifiedTrainer(ap);
            entity.MoriaEpimorfosi = k.MoriaEpimorfosi(ap);
            entity.MoriaTeach = k.MoriaTeach(ap);
            entity.MoriaWork = k.MoriaWork(ap);
            entity.MoriaAnergia = k.MoriaAnergia(ap);
            entity.MoriaSocial = k.MoriaSocial(ap);
            entity.MoriaTotal = k.MoriaTotal(ap);

            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();

            AitisiDataViewModel model = GetAitisiDataFromDB(a.AitisisID);
            return model;
        }

        public AitisiDataViewModel GetAitisiDataFromDB(int aitisiId)
        {
            var data = (from d in db.sqlAitisiData
                        where d.AitisisID == aitisiId
                        select new AitisiDataViewModel
                        {
                            AitisisID = d.AitisisID,
                            ProkirixisID = d.ProkirixisID,
                            AitisisProtocol = d.AitisisProtocol,
                            TeacherAFM = d.TeacherAFM,
                            FullName = d.FullName,
                            KladosEidikotita = d.KladosEidikotita,
                            PeriferiaName = d.PeriferiaName,
                            SchoolName = d.SchoolName,
                            PtyxioTitlos = d.PtyxioTitlos,
                            PtyxioTypeText = d.PtyxioTypeText,
                            MscTitlos = d.MscTitlos,
                            PhdTitlos = d.PhdTitlos,
                            Ptyxio2Titlos = d.Ptyxio2Titlos,
                            Language1Epipedo = d.Language1Epipedo,
                            Language2Epipedo = d.Language2Epipedo,
                            ComputerTitle = d.ComputerTitle,
                            EpimorfosiHours = d.EpimorfosiHours,
                            CertifiedTrainerAM = d.CertifiedTrainerAM,
                            SocialAmeaText = d.SocialAmeaText,
                            SocialPolyteknosText = d.SocialPolyteknosText,
                            SocialSingleParentText = d.SocialSingleParentText,
                            SocialTriteknosText = d.SocialTriteknosText,
                            AnergiaDiarkeiaText = d.AnergiaDiarkeiaText,
                            EpagelmaText = d.EpagelmaText,
                            MoriaPtyxio = d.MoriaPtyxio,
                            MoriaMsc = d.MoriaMsc,
                            MoriaPhd = d.MoriaPhd,
                            MoriaLanguages = d.MoriaLanguages,
                            MoriaComputer = d.MoriaComputer,
                            MoriaCertified = d.MoriaCertified,
                            MoriaEpimorfosi = d.MoriaEpimorfosi,
                            MoriaSocial = d.MoriaSocial,
                            MoriaAnergia = d.MoriaAnergia,
                            MoriaTeach = d.MoriaTeach,
                            MoriaWork = d.MoriaWork,
                            MoriaTotal = d.MoriaTotal
                        }).FirstOrDefault();

            return data;
        }

        public ActionResult AitisiMoriaPrint(int aitisiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
                return RedirectToAction("Login", "UserSchools");
            else
                loggedSchool = GetLoginSchool();

            AitisisParameters model = new AitisisParameters();
            model.AitisiID = aitisiId;

            return View(model);
        }

        #endregion


        #region ΣΥΝΟΛΙΚΟ ΜΗΤΡΩΟ ΕΚΠΑΙΔΕΥΤΙΚΩΝ

        public ActionResult TeachersRegistry()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "UserSchools");
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

        public ActionResult AitiseisRegistry()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "UserSchools");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }

            populateTeachTypes();
            populateSchoolYears();
            populateIncomeYears();

            return View();
        }

        public ActionResult Aitiseis_ReadGlobal([DataSourceRequest] DataSourceRequest request, int prokirixiId = 0)
        {
            List<sqlTeacherAitiseis> data = GetAitiseisListGlobal(prokirixiId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<sqlTeacherAitiseis> GetAitiseisListGlobal(int prokirixiId = 0)
        {
            List<sqlTeacherAitiseis> results = new List<sqlTeacherAitiseis>();

            if (prokirixiId > 0)
            {
                var data = (from a in db.sqlTeacherAitiseis
                            where a.ProkirixisID == prokirixiId
                            orderby a.FullName
                            select a).ToList();
                results = data;
            }
            else
            {
                var data = (from a in db.sqlTeacherAitiseis
                            orderby a.ProkirixisID descending, a.FullName
                            select a).ToList();
                results = data;
            }

            return results;
        }

        public ActionResult AitisiExperiencePrint(int aitisiId = 0)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "UserSchools");
            }
            else
            {
                loggedSchool = GetLoginSchool();
                AitisisParameters parameters = new AitisisParameters();
                parameters.AitisiID = aitisiId;

                return View(parameters);
            }
        }

        public ActionResult AitiseisRegistryPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "UserSchools");
            }
            else
            {
                loggedSchool = GetLoginSchool();
                AitisisParameters parameters = new AitisisParameters();
                parameters.ProkirixiID = c.GetAdminProkirixiID();

                return View(parameters);
            }
        }

        #endregion


        #region ΕΚΤΥΠΩΣΕΙΣ

        public ActionResult MultipleAitiseisPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "UserSchools");
            }
            else
            {
                loggedSchool = GetLoginSchool();
                ReportParameters parameters = new ReportParameters();
                parameters.ProkirixiID = c.GetAdminProkirixiID();

                return View(parameters);
            }
        }

        public ActionResult EnstaseisDetailPost()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "UserSchools");
            }
            else
            {
                loggedSchool = GetLoginSchool();
                ReportParameters parameters = new ReportParameters();
                parameters.ProkirixiID = c.GetAdminProkirixiID();
                parameters.PeriferiakiID = (from d in db.SysSchools where d.SchoolID == loggedSchool.UserSchoolID select d).FirstOrDefault().SchoolPeriferiakiID;
                parameters.SchoolID = loggedSchool.UserSchoolID;

                return View(parameters);
            }
        }

        public ActionResult EnstaseisDetailPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "UserSchools");
            }
            else
            {
                loggedSchool = GetLoginSchool();
                ReportParameters parameters = new ReportParameters();
                parameters.ProkirixiID = c.GetAdminProkirixiID();
                parameters.PeriferiakiID = (from d in db.SysSchools where d.SchoolID == loggedSchool.UserSchoolID select d).FirstOrDefault().SchoolPeriferiakiID;
                parameters.SchoolID = loggedSchool.UserSchoolID;

                return View(parameters);
            }
        }

        public ActionResult EnstaseisSummary()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "UserSchools");
            }
            else
            {
                loggedSchool = GetLoginSchool();
                ReportParameters parameters = new ReportParameters();
                parameters.ProkirixiID = c.GetAdminProkirixiID();

                return View(parameters);
            }
        }

        #region ΣΤΑΤΙΣΤΙΚΕΣ ΕΚΘΕΣΕΙΣ

        public ActionResult statGendersPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "UserSchools");
            }
            else
            {
                loggedSchool = GetLoginSchool();
                ReportParameters parameters = new ReportParameters();
                parameters.ProkirixiID = c.GetAdminProkirixiID();

                return View(parameters);
            }
        }

        public ActionResult statAnergiaPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "UserSchools");
            }
            else
            {
                loggedSchool = GetLoginSchool();
                ReportParameters parameters = new ReportParameters();
                parameters.ProkirixiID = c.GetAdminProkirixiID();

                return View(parameters);
            }
        }

        public ActionResult statCertifiedPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "UserSchools");
            }
            else
            {
                loggedSchool = GetLoginSchool();
                ReportParameters parameters = new ReportParameters();
                parameters.ProkirixiID = c.GetAdminProkirixiID();

                return View(parameters);
            }
        }

        public ActionResult statSocialGroupsPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "UserSchools");
            }
            else
            {
                loggedSchool = GetLoginSchool();
                ReportParameters parameters = new ReportParameters();
                parameters.ProkirixiID = c.GetAdminProkirixiID();

                return View(parameters);
            }
        }

        public ActionResult statPtyxioLevelPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "UserSchools");
            }
            else
            {
                loggedSchool = GetLoginSchool();
                ReportParameters parameters = new ReportParameters();
                parameters.ProkirixiID = c.GetAdminProkirixiID();

                return View(parameters);
            }
        }

        #endregion

        #endregion


        #region GETTERS AND POPULATORS

        public void populateSchools()
        {
            var data = (from d in db.SysSchools
                        orderby d.SchoolName
                        select new SchoolsViewModel
                        {
                            SchoolID = d.SchoolID,
                            SchoolName = d.SchoolName,
                            SchoolPeriferiaID = d.SchoolPeriferiaID
                        }).ToList();

            ViewData["schools"] = data;
            ViewData["defaultSchool"] = data.First().SchoolID;
        }

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

        public JsonResult GetProkirixeis()
        {
            var data = (from d in db.Prokirixis
                        orderby d.DateStart descending
                        select new ProkirixisViewModel
                        {
                            ProkirixiID = d.ProkirixiID,
                            Protocol = d.Protocol
                        }).ToList();

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


        #region ERROR PAGES

        public ActionResult Error(string notify = null)
        {
            if (notify != null)
                this.AddNotification(notify, NotificationType.WARNING);

            return View();
        }

        public ActionResult ErrorData(string notify = null)
        {
            if (notify != null)
                this.AddNotification(notify, NotificationType.WARNING);

            return View();
        }

        #endregion

    }
}