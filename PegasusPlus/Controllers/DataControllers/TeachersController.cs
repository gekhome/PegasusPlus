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
        private const string UPLOAD_PATH = "~/App_Data/FilesPersonal/";

        private const string UPLOAD_AFM_TEXT = "ΒΕΒΑΙΩΣΗ ΑΦΜ";
        private const string UPLOAD_ADT_TEXT = "ΒΕΒΑΙΩΣΗ ΑΔΤ";
        private const string UPLOAD_AMA_TEXT = "ΒΕΒΑΙΩΣΗ ΑΜΑ";
        private const string UPLOAD_AMKA_TEXT = "ΒΕΒΑΙΩΣΗ ΑΜΚΑ";
        private const string UPLOAD_AMEA_TEXT = "ΒΕΒΑΙΩΣΗ ΑΜΕΑ ΟΙΚΟΓΕΝΕΙΑΣ";
        private const string UPLOAD_ANERGIA_TEXT = "ΒΕΒΑΙΩΣΗ ΔΙΑΡΚΕΙΑΣ ΑΝΕΡΓΙΑΣ";
        private const string UPLOAD_TRITEKNOS_TEXT = "ΒΕΒΑΙΩΣΗ ΤΡΙΤΕΚΝΗΣ ΟΙΚΟΓΕΝΕΙΑΣ";
        private const string UPLOAD_POLYTEKNOS_TEXT = "ΒΕΒΑΙΩΣΗ ΠΟΛΥΤΕΚΝΗΣ ΟΙΚΟΓΕΝΕΙΑΣ";
        private const string UPLOAD_SINGLEPARENT_TEXT = "ΒΕΒΑΙΩΣΗ ΜΟΝΟΓΟΝΕΪΚΗΣ ΟΙΚΟΓΕΝΕΙΑΣ";

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
                return RedirectToAction("Index", "Teachers", new { notify = "Δεν βρέθηκε ανοικτή προκήρυξη για επεξεργασία." });
            }

            Teachers data = db.Teachers.Find(loggedTeacher.UserAfm);
            if (data == null)
            {
                return RedirectToAction("TeacherCreate", "Teachers");
            }
            TeacherViewModel model = GetTeacherDataFromDB(loggedTeacher.UserAfm);
            return View(model);
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
                    AFM = loggedTeacher.UserAfm,
                    ADT = model.ADT,
                    AMKA = model.AMKA,
                    AMA = model.AMA,
                    DOY = model.DOY,
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
                    //InsuranceMain = model.InsuranceMain,
                    //InsuranceFirstYear = model.InsuranceFirstYear,
                    //AnergiaCardExpireDate = model.AnergiaCardExpireDate,
                    SocialTriteknos = model.SocialTriteknos,
                    SocialPolyteknos = model.SocialPolyteknos,
                    SocialSingleParent = model.SocialSingleParent,
                    SocialAmea = model.SocialAmea,
                    SocialAnergos = model.SocialAnergos,
                    // Filenames of uploaed files, some may be null
                    ADT_FILENAME = model.FileADT != null ? Path.GetFileName(model.FileADT.FileName) : "",
                    AFM_FILENAME = model.FileAFM != null ? Path.GetFileName(model.FileAFM.FileName) : "",
                    AMA_FILENAME = model.FileAMA != null ? Path.GetFileName(model.FileAMA.FileName) : "",
                    AMKA_FILENAME = model.FileAMKA != null ? Path.GetFileName(model.FileAMKA.FileName) : "",
                    SocialTriteknosFilename = model.FileTriteknos != null ? Path.GetFileName(model.FileTriteknos.FileName) : "",
                    SocialPolyteknosFilename = model.FilePolyteknos != null ? Path.GetFileName(model.FilePolyteknos.FileName) : "",
                    SocialSingleParentFilename = model.FileSingleParent != null ? Path.GetFileName(model.FileSingleParent.FileName) : "",
                    SocialAmeaFilename = model.FileAMEA != null ? Path.GetFileName(model.FileAMEA.FileName) : "",
                    SocialAnergosFilename = model.FileAnergia != null ? Path.GetFileName(model.FileAnergia.FileName) : ""

                };
                if (k.ValidateBirthdate(entity))
                {
                    db.Teachers.Add(entity);
                    db.SaveChanges();
                    this.AddNotification("Η αποθήκευση ολοκληρώθηκε με επιτυχία.", NotificationType.SUCCESS);
                    TeacherViewModel newData = GetTeacherDataFromDB(loggedTeacher.UserAfm);
                    return View(newData);
                }
                else
                {
                    this.AddNotification("Η αποθήκευση απέτυχε λόγω σφαλμάτων καταχώρησης.", NotificationType.ERROR);
                    ModelState.AddModelError("Birthdate", "Η ημερομηνία γέννησης είναι εκτός λογικών ορίων.");
                    model.AFM = loggedTeacher.UserAfm;
                    return View(model);
                }
            }
            this.AddNotification("Η αποθήκευση απέτυχε λόγω σφαλμάτων καταχώρησης. Δείτε τη σύνοψη στο κάτω μέρος της σελίδας.", NotificationType.ERROR);
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

                entity.AFM = loggedTeacher.UserAfm;
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
                //entity.InsuranceMain = model.InsuranceMain;
                //entity.InsuranceFirstYear = model.InsuranceFirstYear;
                //entity.AnergiaCardExpireDate = model.AnergiaCardExpireDate;
                entity.SocialTriteknos = model.SocialTriteknos;
                entity.SocialPolyteknos = model.SocialPolyteknos;
                entity.SocialSingleParent = model.SocialSingleParent;
                entity.SocialAmea = model.SocialAmea;
                entity.SocialAnergos = model.SocialAnergos;
                // Filenames of uploaed files, some may be null
                entity.ADT_FILENAME = model.FileADT != null ? Path.GetFileName(model.FileADT.FileName) : GetAdtFilename(loggedTeacher.UserAfm);
                entity.AFM_FILENAME = model.FileAFM != null ? Path.GetFileName(model.FileAFM.FileName) : GetAfmFilename(loggedTeacher.UserAfm);
                entity.AMA_FILENAME = model.FileAMA != null ? Path.GetFileName(model.FileAMA.FileName) : GetAmaFilename(loggedTeacher.UserAfm);
                entity.AMKA_FILENAME = model.FileAMKA != null ? Path.GetFileName(model.FileAMKA.FileName) : GetAmkaFilename(loggedTeacher.UserAfm);
                entity.SocialAmeaFilename = model.FileAMEA != null ? Path.GetFileName(model.FileAMEA.FileName) : GetAmeaFilename(loggedTeacher.UserAfm);
                entity.SocialAnergosFilename = model.FileAnergia != null ? Path.GetFileName(model.FileAnergia.FileName) : GetAnergosFilename(loggedTeacher.UserAfm);
                entity.SocialPolyteknosFilename = model.FilePolyteknos != null ? Path.GetFileName(model.FilePolyteknos.FileName) : GetPolyteknosFilename(loggedTeacher.UserAfm);
                entity.SocialSingleParentFilename = model.FileSingleParent != null ? Path.GetFileName(model.FileSingleParent.FileName) : GetSingleParentFilename(loggedTeacher.UserAfm);
                entity.SocialTriteknosFilename = model.FileTriteknos != null ? Path.GetFileName(model.FileTriteknos.FileName) : GetTriteknosFilename(loggedTeacher.UserAfm);

                db.Entry(entity).State = EntityState.Modified;
                if (k.ValidateBirthdate(entity))
                {
                    db.SaveChanges();
                    this.AddNotification("Η αποθήκευση ολοκληρώθηκε με επιτυχία.", NotificationType.SUCCESS);
                    TeacherViewModel newData = GetTeacherDataFromDB(loggedTeacher.UserAfm);
                    return View(newData);
                }
                else
                {
                    this.AddNotification("Η αποθήκευση απέτυχε λόγω σφαλμάτων καταχώρησης.", NotificationType.ERROR);
                    ModelState.AddModelError("BirthDate", "Η ημερομηνία γέννησης είναι εκτός λογικών ορίων.");
                    model.AFM = loggedTeacher.UserAfm;
                    return View(model);
                }
            }
            this.AddNotification("Η αποθήκευση απέτυχε λόγω σφαλμάτων καταχώρησης. Δείτε τη σύνοψη στο κάτω μέρος της σελίδας.", NotificationType.ERROR);
            model.AFM = loggedTeacher.UserAfm;
            return View(model);
        }

        public bool ValidFileExtension(string extension)
        {
            string[] extensions = { ".PDF", ".DOC", ".DOCX", ".ODT" };

            List<string> allowed_extensions = new List<string>(extensions);

            if (allowed_extensions.Contains(extension.ToUpper()))
                return true;
            return false;
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
                            InsuranceMain = d.InsuranceMain,
                            InsuranceFirstYear = d.InsuranceFirstYear,
                            SocialTriteknos = d.SocialTriteknos ?? false,
                            SocialPolyteknos = d.SocialPolyteknos ?? false,
                            SocialSingleParent = d.SocialSingleParent ?? false,
                            SocialAmea = d.SocialAmea ?? false,
                            SocialAnergos = d.SocialAnergos,
                            AnergiaCardExpireDate = d.AnergiaCardExpireDate,
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

        #region SAVE UPLOAD FILES

        public bool SaveUploadedFiles(TeacherViewModel model)
        {
            loggedTeacher = GetLoginTeacher();
            string prefix_afm = loggedTeacher.UserAfm;
            string upload_path = UPLOAD_PATH + prefix_afm + "/";

            bool exists = Directory.Exists(Server.MapPath(upload_path));
            if (!exists)
                Directory.CreateDirectory(Server.MapPath(upload_path));

            if (model.FileAFM != null)
            {
                string FileName1 = Path.GetFileName(model.FileAFM.FileName);
                string Extension1 = Path.GetExtension(model.FileAFM.FileName);
                if (!string.IsNullOrEmpty(FileName1) && ValidFileExtension(Extension1))
                {
                    var physicalPath1 = Path.Combine(Server.MapPath(upload_path), FileName1);
                    model.FileAFM.SaveAs(physicalPath1);
                    StoreUploadedFile(FileName1, physicalPath1, UPLOAD_AFM_TEXT);
                }
                else return false;
            }

            if (model.FileADT != null)
            {
                string FileName2 = Path.GetFileName(model.FileADT.FileName);
                string Extension2 = Path.GetExtension(model.FileADT.FileName);
                if (!string.IsNullOrEmpty(FileName2) && ValidFileExtension(Extension2))
                {
                    var physicalPath2 = Path.Combine(Server.MapPath(upload_path), FileName2);
                    model.FileADT.SaveAs(physicalPath2);
                    StoreUploadedFile(FileName2, physicalPath2, UPLOAD_ADT_TEXT);
                }
                else return false;
            }

            if (model.FileAMKA != null)
            {
                string FileName3 = Path.GetFileName(model.FileAMKA.FileName);
                string Extension3 = Path.GetExtension(model.FileAMKA.FileName);
                if (!string.IsNullOrEmpty(FileName3) && ValidFileExtension(Extension3))
                {
                    var physicalPath3 = Path.Combine(Server.MapPath(upload_path), FileName3);
                    model.FileAMKA.SaveAs(physicalPath3);
                    StoreUploadedFile(FileName3, physicalPath3, UPLOAD_AMKA_TEXT);
                }
                else return false;
            }

            if (model.FileAMA != null)
            {
                string FileName4 = Path.GetFileName(model.FileAMA.FileName);
                string Extension4 = Path.GetExtension(model.FileAMA.FileName);
                if (!string.IsNullOrEmpty(FileName4) && ValidFileExtension(Extension4))
                {
                    var physicalPath4 = Path.Combine(Server.MapPath(upload_path), FileName4);
                    model.FileAMA.SaveAs(physicalPath4);
                    StoreUploadedFile(FileName4, physicalPath4, UPLOAD_AMA_TEXT);
                }
                else return false;
            }

            if (model.FileTriteknos != null)
            {
                string FileName5 = Path.GetFileName(model.FileTriteknos.FileName);
                string Extension5 = Path.GetExtension(model.FileTriteknos.FileName);
                if (!string.IsNullOrEmpty(FileName5) && ValidFileExtension(Extension5))
                {
                    var physicalPath5 = Path.Combine(Server.MapPath(upload_path), FileName5);
                    model.FileTriteknos.SaveAs(physicalPath5);
                    StoreUploadedFile(FileName5, physicalPath5, UPLOAD_TRITEKNOS_TEXT);
                }
                else return false;
            }

            if (model.FilePolyteknos != null)
            {
                string FileName6 = Path.GetFileName(model.FilePolyteknos.FileName);
                string Extension6 = Path.GetExtension(model.FilePolyteknos.FileName);
                if (!string.IsNullOrEmpty(FileName6) && ValidFileExtension(Extension6))
                {
                    var physicalPath6 = Path.Combine(Server.MapPath(upload_path), FileName6);
                    model.FilePolyteknos.SaveAs(physicalPath6);
                    StoreUploadedFile(FileName6, physicalPath6, UPLOAD_POLYTEKNOS_TEXT);
                }
                else return false;
            }

            if (model.FileSingleParent != null)
            {
                string FileName7 = Path.GetFileName(model.FileSingleParent.FileName);
                string Extension7 = Path.GetExtension(model.FileSingleParent.FileName);
                if (!string.IsNullOrEmpty(FileName7) && ValidFileExtension(Extension7))
                {
                    var physicalPath7 = Path.Combine(Server.MapPath(upload_path), FileName7);
                    model.FileSingleParent.SaveAs(physicalPath7);
                    StoreUploadedFile(FileName7, physicalPath7, UPLOAD_SINGLEPARENT_TEXT);
                }
                else return false;
            }

            if (model.FileAMEA != null)
            {
                string FileName8 = Path.GetFileName(model.FileAMEA.FileName);
                string Extension8 = Path.GetExtension(model.FileAMEA.FileName);
                if (!string.IsNullOrEmpty(FileName8) && ValidFileExtension(Extension8))
                {
                    var physicalPath8 = Path.Combine(Server.MapPath(upload_path), FileName8);
                    model.FileAMEA.SaveAs(physicalPath8);
                    StoreUploadedFile(FileName8, physicalPath8, UPLOAD_AMEA_TEXT);
                }
                else return false;
            }

            if (model.FileAnergia != null)
            {
                string FileName9 = Path.GetFileName(model.FileAnergia.FileName);
                string Extension9 = Path.GetExtension(model.FileAnergia.FileName);
                if (!string.IsNullOrEmpty(FileName9) && ValidFileExtension(Extension9))
                {
                    var physicalPath9 = Path.Combine(Server.MapPath(upload_path), FileName9);
                    model.FileAnergia.SaveAs(physicalPath9);
                    StoreUploadedFile(FileName9, physicalPath9, UPLOAD_ANERGIA_TEXT);
                }
                else return false;
            }

            return true;
        }

        public void StoreUploadedFile(string filename, string filepath, string description)
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
                    FilePath = filepath,
                    Description = description,
                    TeacherAFM = loggedTeacher.UserAfm,
                    SchoolYearText = c.GetSchoolYearText(schoolyearId)
                };
                db.TeachersUploads.Add(entity);
                db.SaveChanges();
            }
        }

        #endregion

        #region GET FILENAME FIELDS FROM DATABASE

        public string GetAdtFilename(string afm)
        {
            var data = (from d in db.Teachers where d.AFM == afm select new { d.ADT_FILENAME }).FirstOrDefault();
            if (data != null)
                return data.ADT_FILENAME;
            return "";
        }

        public string GetAfmFilename(string afm)
        {
            var data = (from d in db.Teachers where d.AFM == afm select new { d.AFM_FILENAME }).FirstOrDefault();
            if (data != null)
                return data.AFM_FILENAME;
            return "";
        }

        public string GetAmaFilename(string afm)
        {
            var data = (from d in db.Teachers where d.AFM == afm select new { d.AMA_FILENAME }).FirstOrDefault();
            if (data != null)
                return data.AMA_FILENAME;
            return "";
        }

        public string GetAmkaFilename(string afm)
        {
            var data = (from d in db.Teachers where d.AFM == afm select new { d.AMKA_FILENAME }).FirstOrDefault();
            if (data != null)
                return data.AMKA_FILENAME;
            return "";
        }

        public string GetAmeaFilename(string afm)
        {
            var data = (from d in db.Teachers where d.AFM == afm select new { d.SocialAmeaFilename }).FirstOrDefault();
            if (data != null)
                return data.SocialAmeaFilename;
            return "";
        }

        public string GetAnergosFilename(string afm)
        {
            var data = (from d in db.Teachers where d.AFM == afm select new { d.SocialAnergosFilename }).FirstOrDefault();
            if (data != null)
                return data.SocialAnergosFilename;
            return "";
        }

        public string GetSingleParentFilename(string afm)
        {
            var data = (from d in db.Teachers where d.AFM == afm select new { d.SocialSingleParentFilename }).FirstOrDefault();
            if (data != null)
                return data.SocialSingleParentFilename;
            return "";
        }

        public string GetPolyteknosFilename(string afm)
        {
            var data = (from d in db.Teachers where d.AFM == afm select new { d.SocialPolyteknosFilename }).FirstOrDefault();
            if (data != null)
                return data.SocialPolyteknosFilename;
            return "";
        }

        public string GetTriteknosFilename(string afm)
        {
            var data = (from d in db.Teachers where d.AFM == afm select new { d.SocialTriteknosFilename }).FirstOrDefault();
            if (data != null)
                return data.SocialTriteknosFilename;
            return "";
        }

        #endregion


        #region ΠΛΕΓΜΑ ΑΝΕΒΑΣΜΕΝΩΝ ΑΡΧΕΙΩΝ

        public ActionResult TeacherUploads(string afm = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("TaxisNetLogin", "UserTeachers");
            }
            else
            {
                loggedTeacher = GetLoginTeacher();
            }
            return View();
        }

        public ActionResult UploadFiles_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetTeacherUploadsFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UploadFiles_Destroy([DataSourceRequest] DataSourceRequest request, TeacherUploadViewModel data)
        {
            // TODO: ALSO REMOVE UPLOADED FILES FROM SERVER
            if (data != null)
            {
                TeachersUploads entity = db.TeachersUploads.Find(data.UploadFileID);
                if (entity != null)
                {
                    // First delete the physical file and then the info record. Important!
                    DeleteUploadedFile(data.UploadFileID);

                    db.Entry(entity).State = EntityState.Deleted;
                    db.TeachersUploads.Remove(entity);
                    db.SaveChanges();
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        // Custom Destroy action
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UploadFile_Delete(int fileId = 0)
        {
            string msg = "";
            TeachersUploads entity = db.TeachersUploads.Find(fileId);
            if (entity != null)
            {
                // First delete the physical file and then the info record. Important!
                DeleteUploadedFile(fileId);
                // Clear the filename field in Teachers data and then the record. Important!
                ClearFilenameField(fileId);

                db.Entry(entity).State = EntityState.Deleted;
                db.TeachersUploads.Remove(entity);
                db.SaveChanges();
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public List<TeacherUploadViewModel> GetTeacherUploadsFromDB()
        {
            loggedTeacher = GetLoginTeacher();

            var data = (from d in db.TeachersUploads
                        where d.TeacherAFM == loggedTeacher.UserAfm
                        orderby d.FileName
                        select new TeacherUploadViewModel
                        {
                            UploadFileID = d.UploadFileID,
                            FileName = d.FileName,
                            FilePath = d.FilePath,
                            Description = d.Description,
                            SchoolYearText = d.SchoolYearText,
                            TeacherAFM = d.TeacherAFM
                        }).ToList();

            return data;
        }

        public void DeleteUploadedFile(int fileId)
        {
            loggedTeacher = GetLoginTeacher();
            string uploadPath = UPLOAD_PATH + loggedTeacher.UserAfm + "/";
            string filename = "";

            var data = (from d in db.TeachersUploads where d.UploadFileID == fileId select d).FirstOrDefault();
            if (data != null)
            {
                filename = data.FileName;
                var physicalPath = Path.Combine(Server.MapPath(uploadPath), filename);

                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
            }
        }

        public void ClearFilenameField(int fileId)
        {
            var data = (from d in db.TeachersUploads where d.UploadFileID == fileId select new { d.Description, d.TeacherAFM }).FirstOrDefault();
            if (data == null) return;

            if (data.Description == UPLOAD_ADT_TEXT)
            {
                Teachers entity = db.Teachers.Find(data.TeacherAFM);
                if (entity != null)
                {
                    entity.ADT_FILENAME = "";
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else if (data.Description == UPLOAD_AFM_TEXT)
            {
                Teachers entity = db.Teachers.Find(data.TeacherAFM);
                if (entity != null)
                {
                    entity.AFM_FILENAME = "";
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else if (data.Description == UPLOAD_AMA_TEXT)
            {
                Teachers entity = db.Teachers.Find(data.TeacherAFM);
                if (entity != null)
                {
                    entity.AMA_FILENAME = "";
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else if (data.Description == UPLOAD_AMKA_TEXT)
            {
                Teachers entity = db.Teachers.Find(data.TeacherAFM);
                if (entity != null)
                {
                    entity.AMKA_FILENAME = "";
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else if (data.Description == UPLOAD_TRITEKNOS_TEXT)
            {
                Teachers entity = db.Teachers.Find(data.TeacherAFM);
                if (entity != null)
                {
                    entity.SocialTriteknosFilename = "";
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else if (data.Description == UPLOAD_POLYTEKNOS_TEXT)
            {
                Teachers entity = db.Teachers.Find(data.TeacherAFM);
                if (entity != null)
                {
                    entity.SocialPolyteknosFilename = "";
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else if (data.Description == UPLOAD_SINGLEPARENT_TEXT)
            {
                Teachers entity = db.Teachers.Find(data.TeacherAFM);
                if (entity != null)
                {
                    entity.SocialSingleParentFilename = "";
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else if (data.Description == UPLOAD_AMEA_TEXT)
            {
                Teachers entity = db.Teachers.Find(data.TeacherAFM);
                if (entity != null)
                {
                    entity.SocialAmeaFilename = "";
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else if (data.Description == UPLOAD_ANERGIA_TEXT)
            {
                Teachers entity = db.Teachers.Find(data.TeacherAFM);
                if (entity != null)
                {
                    entity.SocialAnergosFilename = "";
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        public FileResult Download(int file_id)
        {
            loggedTeacher = GetLoginTeacher();
            string physicalPath = UPLOAD_PATH + loggedTeacher.UserAfm + "/";
            string filename = "";

            var fileinfo = (from d in db.TeachersUploads where d.UploadFileID == file_id select d).FirstOrDefault();
            if (fileinfo != null)
            {
                filename = fileinfo.FileName;
            }

            return File(Path.Combine(Server.MapPath(physicalPath), filename), System.Net.Mime.MediaTypeNames.Application.Octet, filename);
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

        public JsonResult GetIdiotites()
        {
            var data = db.SysIdiotita.Select(p => new IdiotitaViewModel
            {
                IdiotitaID = p.IdiotitaID,
                IdiotitaText = p.IdiotitaText
            }).OrderBy(m => m.IdiotitaText);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAnergia()
        {
            var data = db.SysAnergia.Select(p => new AnergiaViewModel
            {
                AnergiaID = p.AnergiaID,
                AnergiaText = p.AnergiaText
            });
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