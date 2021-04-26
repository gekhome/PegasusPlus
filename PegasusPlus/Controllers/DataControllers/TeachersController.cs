using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

using PegasusPlus.DAL;
using PegasusPlus.Models;
using PegasusPlus.BPM;
using PegasusPlus.Extensions;
using System;
using System.IO;

namespace PegasusPlus.Controllers.DataControllers
{
    public class TeachersController : Controller
    {
        private PegasusPlusDBEntities db = new PegasusPlusDBEntities();
        private string Msg;
        private int prokirixiId;
        //private bool ViewAllowed;
        private const string UPLOAD_PATH = "~/App_Data/UploadFiles/Personal/";

        Common c = new Common();
        Kerberos k = new Kerberos();

        private UserTeachers loggedTeacher;
        private Teachers loggedTeacherData;

        public ActionResult Index(string notify = null)
        {
            //check if user is unauthenticated to redirect him
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
                return RedirectToAction("TaxisNetLogin", "UserTeachers");
            else
                loggedTeacher = GetLoginTeacher();

            Msg = "Δεν βρέθηκε ανοικτή προκήρυξη. Όλες οι ενέργειες δημιουργίας και ";
            Msg += "επεξεργασίας δεδομένων είναι απενεργοποιημένες.";

            prokirixiId = c.GetOpenProkirixiID();
            if (prokirixiId == 0)
            {
                this.AddNotification(Msg, NotificationType.WARNING);
            }
            if (notify != null)
            {
                this.AddNotification(notify, NotificationType.WARNING);
            }
            return View();
        }

        #region TEACHER DATA FORM

        public ActionResult TeacherCreate()
        {
            //check if user is unauthenticated to redirect him
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
                return RedirectToAction("TaxisNetLogin", "UserTeachers");
            else
                loggedTeacher = GetLoginTeacher();

            prokirixiId = c.GetOpenProkirixiID();
            if (prokirixiId == 0)
            {
                return RedirectToAction("Index", "Teachers");
            }

            if (db.Teachers.Find(loggedTeacher.UserAfm) == null)
            {
                return View(new TeacherViewModel() { AFM = loggedTeacher.UserAfm });
            }
            else
            {
                return RedirectToAction("TeacherEdit", "Teachers");
            }
        }

        public ActionResult TeacherEdit()
        {
            //check if user is unauthenticated to redirect him
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
                return RedirectToAction("TaxisNetLogin", "UserTeachers");
            else
                loggedTeacher = GetLoginTeacher();

            prokirixiId = c.GetOpenProkirixiID();
            if (prokirixiId == 0)
            {
                return RedirectToAction("Index", "Teachers");
            }

            Teachers data = db.Teachers.Find(loggedTeacher.UserAfm);
            if (data == null)
            {
                return RedirectToAction("TeacherCreate", "Teachers");
            }
            TeacherViewModel teacherEditModel = new TeacherViewModel(data);
            return View(teacherEditModel);
        }

        [HttpPost]
        public ActionResult TeacherCreate(TeacherViewModel model)
        {
            loggedTeacher = GetLoginTeacher();

            int prokirixiId = c.GetOpenProkirixiID();
            if (prokirixiId == 0)
            {
                return RedirectToAction("Index", "Teachers");
            }

            if (!SaveUploadedFiles(model))
                ModelState.AddModelError("", "Ένα ή περισσότερα ανεβασμένα αρχεία δεν έχουν αποδεκτή επέκταση (PDF, DOC, DOCX, ODT)");

            if (ModelState.IsValid)
            {
                Teachers entity = new Teachers()
                {
                    AFM = model.AFM,
                    DOY = model.DOY,
                    ADT = model.ADT,
                    FirstName = model.FirstName.HasValue() ? model.FirstName.Trim() : model.FirstName,
                    LastName = model.LastName.HasValue() ? model.LastName.Trim() : model.LastName,
                    FatherName = model.FatherName.HasValue() ? model.FatherName.Trim() : model.FatherName,
                    MotherName = model.MotherName.HasValue() ? model.MotherName.Trim() : model.MotherName,
                    Gender = model.Gender,
                    FamilyStatus = model.FamilyStatus,
                    Children = model.Children,
                    Birthdate = model.Birthdate,
                    Nomos = model.Nomos,
                    City = model.City,
                    Address = model.Address,
                    PostCode = model.PostCode,
                    Telephone = model.Telephone,
                    CellPhone = model.CellPhone,
                    Email = model.Email,
                    Epagelma = model.Epagelma,
                    Idiotita = model.Idiotita,
                    AMKA = model.AMKA,
                    AMA = model.AMA,
                    InsuranceMain = model.InsuranceMain,
                    InsuranceFirstYear = model.InsuranceFirstYear,
                    SocialTriteknos = model.SocialTriteknos,
                    SocialPolyteknos = model.SocialPolyteknos,
                    SocialSingleParent = model.SocialSingleParent,
                    SocialAmea = model.SocialAmea,
                    SocialAnergos = model.SocialAnergos,
                    AnergiaCardExpireDate = model.AnergiaCardExpireDate,
                    // Filenames of uploaed files, some may be null
                    ADT_FILENAME = Path.GetFileName(model.FileADT.FileName),
                    AFM_FILENAME = Path.GetFileName(model.FileAFM.FileName),
                    AMA_FILENAME = Path.GetFileName(model.FileAMA.FileName),
                    AMKA_FILENAME = Path.GetFileName(model.FileAMKA.FileName),
                    SocialAmeaFilename = Path.GetFileName(model.FileAMEA.FileName),
                    SocialAnergosFilename = Path.GetFileName(model.FileAnergia.FileName),
                    SocialPolyteknosFilename = Path.GetFileName(model.FilePolyteknos.FileName),
                    SocialSingleParentFilename = Path.GetFileName(model.FileSingleParent.FileName),
                    SocialTriteknosFilename = Path.GetFileName(model.FileTriteknos.FileName)

                };
                if (k.ValidateBirthdate(entity))
                {
                    db.Teachers.Add(entity);
                    db.SaveChanges();
                    // Notify here
                    this.AddNotification("Η αποθήκευση ολοκληρώθηκε με επιτυχία.", NotificationType.SUCCESS);
                }
                else
                {
                    this.AddNotification("Η αποθήκευση απέτυχε λόγω σφαλμάτων καταχώρησης.", NotificationType.ERROR);
                    ModelState.AddModelError("Birthdate", "Η ημερομηνία γέννησης είναι εκτός λογικών ορίων.");
                }
                model.AFM = loggedTeacher.UserAfm;
                return View(model);
            }
            this.AddNotification("Η αποθήκευση απέτυχε λόγω σφαλμάτων καταχώρησης.", NotificationType.ERROR);
            model.AFM = loggedTeacher.UserAfm;
            return View(model);
        }

        [HttpPost]
        public ActionResult TeacherEdit(TeacherViewModel model)
        {
            loggedTeacher = GetLoginTeacher();
            prokirixiId = c.GetOpenProkirixiID();
            if (prokirixiId == 0)
            {
                return RedirectToAction("Index", "Teachers");
            }

            if (!SaveUploadedFiles(model))
                ModelState.AddModelError("", "Ένα ή περισσότερα ανεβασμένα αρχεία δεν έχουν αποδεκτή επέκταση (PDF, DOC, DOCX, ODT)");

            if (ModelState.IsValid)
            {
                Teachers entity = db.Teachers.Find(loggedTeacher.UserAfm);

                entity.AFM = model.AFM;
                entity.DOY = model.DOY;
                entity.ADT = model.ADT;
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
                entity.AMKA = model.AMKA;
                entity.AMA = model.AMA;
                entity.InsuranceMain = model.InsuranceMain;
                entity.InsuranceFirstYear = model.InsuranceFirstYear;
                entity.SocialTriteknos = model.SocialTriteknos;
                entity.SocialPolyteknos = model.SocialPolyteknos;
                entity.SocialSingleParent = model.SocialSingleParent;
                entity.SocialAmea = model.SocialAmea;
                entity.SocialAnergos = model.SocialAnergos;
                entity.AnergiaCardExpireDate = model.AnergiaCardExpireDate;
                // Filenames of uploaed files, some may be null
                entity.ADT_FILENAME = Path.GetFileName(model.FileADT.FileName);
                entity.AFM_FILENAME = Path.GetFileName(model.FileAFM.FileName);
                entity.AMA_FILENAME = Path.GetFileName(model.FileAMA.FileName);
                entity.AMKA_FILENAME = Path.GetFileName(model.FileAMKA.FileName);
                entity.SocialAmeaFilename = Path.GetFileName(model.FileAMEA.FileName);
                entity.SocialAnergosFilename = Path.GetFileName(model.FileAnergia.FileName);
                entity.SocialPolyteknosFilename = Path.GetFileName(model.FilePolyteknos.FileName);
                entity.SocialSingleParentFilename = Path.GetFileName(model.FileSingleParent.FileName);
                entity.SocialTriteknosFilename = Path.GetFileName(model.FileTriteknos.FileName);

                db.Entry(entity).State = EntityState.Modified;
                if (k.ValidateBirthdate(entity))
                {
                    db.SaveChanges();
                    this.AddNotification("Η αποθήκευση ολοκληρώθηκε με επιτυχία.", NotificationType.SUCCESS);
                    model.AFM = loggedTeacher.UserAfm;
                    return View(model);
                }
                else
                {
                    this.AddNotification("Η αποθήκευση απέτυχε λόγω σφαλμάτων καταχώρησης.", NotificationType.ERROR);
                    ModelState.AddModelError("BIRTHDATE", "Η ημερομηνία γέννησης είναι εκτός λογικών ορίων.");
                    model.AFM = loggedTeacher.UserAfm;
                    return View(model);
                }
            }
            this.AddNotification("Η αποθήκευση απέτυχε λόγω σφαλμάτων καταχώρησης.", NotificationType.ERROR);
            model.AFM = loggedTeacher.UserAfm;
            return View(model);
        }

        public bool SaveUploadedFiles(TeacherViewModel model)
        {
            loggedTeacher = GetLoginTeacher();
            string prefix_afm = loggedTeacher.UserAfm;

            string FileName1 = Path.GetFileName(model.FileAFM.FileName);
            string Extension1 = Path.GetExtension(model.FileAFM.FileName);
            if (!string.IsNullOrEmpty(FileName1) && ValidFileExtension(Extension1))
            {
                var physicalPath1 = Path.Combine(Server.MapPath(UPLOAD_PATH), prefix_afm + '_' + FileName1);
                model.FileAFM.SaveAs(physicalPath1);
                StoreUploadedFile(FileName1, Extension1, "ΒΕΒΑΙΩΣΗ ΑΦΜ");
            }
            else return false;

            string FileName2 = Path.GetFileName(model.FileADT.FileName);
            string Extension2 = Path.GetExtension(model.FileADT.FileName);
            if (!string.IsNullOrEmpty(FileName2) && ValidFileExtension(Extension2))
            {
                var physicalPath2 = Path.Combine(Server.MapPath(UPLOAD_PATH), prefix_afm + '_' + FileName2);
                model.FileADT.SaveAs(physicalPath2);
                StoreUploadedFile(FileName2, Extension2, "ΒΕΒΑΙΩΣΗ ΑΔΤ");
            }
            else return false;

            string FileName3 = Path.GetFileName(model.FileAMKA.FileName);
            string Extension3 = Path.GetExtension(model.FileAMKA.FileName);
            if (!string.IsNullOrEmpty(FileName3) && ValidFileExtension(Extension3))
            {
                var physicalPath3 = Path.Combine(Server.MapPath(UPLOAD_PATH), prefix_afm + '_' + FileName3);
                model.FileAMKA.SaveAs(physicalPath3);
                StoreUploadedFile(FileName3, Extension3, "ΒΕΒΑΙΩΣΗ ΑΜΚΑ");
            }
            else return false;

            string FileName4 = Path.GetFileName(model.FileAMA.FileName);
            string Extension4 = Path.GetExtension(model.FileAMA.FileName);
            if (!string.IsNullOrEmpty(FileName4) && ValidFileExtension(Extension4))
            {
                var physicalPath4 = Path.Combine(Server.MapPath(UPLOAD_PATH), prefix_afm + '_' + FileName4);
                model.FileAMA.SaveAs(physicalPath4);
                StoreUploadedFile(FileName4, Extension4, "ΒΕΒΑΙΩΣΗ ΑΜΑ");
            }
            else return false;

            string FileName5 = Path.GetFileName(model.FileTriteknos.FileName);
            string Extension5 = Path.GetExtension(model.FileTriteknos.FileName);
            if (!string.IsNullOrEmpty(FileName5) && ValidFileExtension(Extension5))
            {
                var physicalPath5 = Path.Combine(Server.MapPath(UPLOAD_PATH), prefix_afm + '_' + FileName5);
                model.FileTriteknos.SaveAs(physicalPath5);
                StoreUploadedFile(FileName5, Extension5, "ΒΕΒΑΙΩΣΗ ΤΡΙΤΕΚΝΗΣ ΟΙΚΟΓΕΝΕΙΑΣ");
            }
            else return false;

            string FileName6 = Path.GetFileName(model.FilePolyteknos.FileName);
            string Extension6 = Path.GetExtension(model.FilePolyteknos.FileName);
            if (!string.IsNullOrEmpty(FileName6) && ValidFileExtension(Extension6))
            {
                var physicalPath6 = Path.Combine(Server.MapPath(UPLOAD_PATH), prefix_afm + '_' + FileName6);
                model.FilePolyteknos.SaveAs(physicalPath6);
                StoreUploadedFile(FileName5, Extension5, "ΒΕΒΑΙΩΣΗ ΠΟΛΥΤΕΚΝΗΣ ΟΙΚΟΓΕΝΕΙΑΣ");
            }
            else return false;

            string FileName7 = Path.GetFileName(model.FileSingleParent.FileName);
            string Extension7 = Path.GetExtension(model.FileSingleParent.FileName);
            if (!string.IsNullOrEmpty(FileName7) && ValidFileExtension(Extension7))
            {
                var physicalPath7 = Path.Combine(Server.MapPath(UPLOAD_PATH), prefix_afm + '_' + FileName7);
                model.FileSingleParent.SaveAs(physicalPath7);
                StoreUploadedFile(FileName5, Extension5, "ΒΕΒΑΙΩΣΗ ΜΟΝΟΓΟΝΕΪΚΗΣ ΟΙΚΟΓΕΝΕΙΑΣ");
            }
            else return false;

            string FileName8 = Path.GetFileName(model.FileAMEA.FileName);
            string Extension8 = Path.GetExtension(model.FileAMEA.FileName);
            if (!string.IsNullOrEmpty(FileName8) && ValidFileExtension(Extension8))
            {
                var physicalPath8 = Path.Combine(Server.MapPath(UPLOAD_PATH), prefix_afm + '_' + FileName8);
                model.FileAMEA.SaveAs(physicalPath8);
                StoreUploadedFile(FileName5, Extension5, "ΒΕΒΑΙΩΣΗ ΟΙΚΟΓΕΝΕΙΑΣ ΑΜΕΑ");
            }
            else return false;

            string FileName9 = Path.GetFileName(model.FileAnergia.FileName);
            string Extension9 = Path.GetExtension(model.FileAnergia.FileName);
            if (!string.IsNullOrEmpty(FileName9) && ValidFileExtension(Extension9))
            {
                var physicalPath9 = Path.Combine(Server.MapPath(UPLOAD_PATH), prefix_afm + '_' + FileName9);
                model.FileAnergia.SaveAs(physicalPath9);
                StoreUploadedFile(FileName5, Extension5, "ΒΕΒΑΙΩΣΗ ΔΙΑΡΚΕΙΑΣ ΑΝΕΡΓΙΑΣ");
            }
            else return false;

            return true;
        }

        public bool ValidFileExtension(string extension)
        {
            string[] extensions = { ".PDF", ".DOC", ".DOCX", ".ODT" };

            List<string> allowed_extensions = new List<string>(extensions);

            if (allowed_extensions.Contains(extension.ToUpper()))
                return true;
            return false;
        }

        public void StoreUploadedFile(string filename, string extension, string description)
        {
            loggedTeacher = GetLoginTeacher();
            prokirixiId = c.GetOpenProkirixiID();
            int schoolyearId = (int)db.Prokirixis.Find(prokirixiId).SchoolYear;

            var data = (from d in db.TeachersUploads where d.FileName == filename select d).Count();
            if (data == 0)
            {
                TeachersUploads entity = new TeachersUploads()
                {
                    FileName = filename,
                    Extension = extension,
                    Description = description,
                    TeacherAFM = loggedTeacher.UserAfm,
                    SchoolYearText = c.GetSchoolYearText(schoolyearId)
                };
                db.TeachersUploads.Add(entity);
                db.SaveChanges();
            }
        }


        #endregion

        #region GETTERS

        public JsonResult GetGenders()
        {
            var data = db.SysGenders.Select(p => new GendersViewModel
            {
                GenderID = p.GenderID,
                GenderText = p.GenderText
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFamilyStatus()
        {
            var data = db.SysFamilyStatus.Select(p => new FamilyStatusViewModel
            {
                FamilyStatusID = p.FamilyStatusID,
                FamilyStatusText = p.FamilyStatusText
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNomoi()
        {
            var data = db.SysNomoi.Select(p => new NomosViewModel
            {
                NomosID = p.NomosID,
                NomosText = p.NomosText
            }).OrderBy(m => m.NomosText);

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public UserTeachers GetLoginTeacher()
        {
            loggedTeacher = db.UserTeachers.Where(m => m.UserAfm == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
            ViewBag.loggedUser = loggedTeacher.Username;

            loggedTeacherData = db.Teachers.Find(loggedTeacher.UserAfm);
            ViewBag.loggedTeacher = loggedTeacherData;

            if (loggedTeacherData != null)
            {
                if (!string.IsNullOrEmpty(loggedTeacherData.FirstName) && !string.IsNullOrEmpty(loggedTeacherData.LastName))
                {
                    ViewBag.loggedUser = loggedTeacherData.FirstName + " " + loggedTeacherData.LastName;
                }
            }

            return loggedTeacher;
        }


        #endregion
    }
}