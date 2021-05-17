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
        private const string UPLOAD_PATH = "~/Uploads/FilesPersonal/";
        private const string EPIMORFOSI_PATH = "~/Uploads/FilesEpimorfosi/";

        private const string UPLOAD_AFM_TEXT = "ΒΕΒΑΙΩΣΗ ΑΦΜ";
        private const string UPLOAD_ADT_TEXT = "ΒΕΒΑΙΩΣΗ ΑΔΤ";
        private const string UPLOAD_AMA_TEXT = "ΒΕΒΑΙΩΣΗ ΑΜΑ";
        private const string UPLOAD_AMKA_TEXT = "ΒΕΒΑΙΩΣΗ ΑΜΚΑ";
        private const string UPLOAD_AMEA_TEXT = "ΒΕΒΑΙΩΣΗ ΑΜΕΑ ΟΙΚΟΓΕΝΕΙΑΣ";
        private const string UPLOAD_ANERGIA_TEXT = "ΒΕΒΑΙΩΣΗ ΔΙΑΡΚΕΙΑΣ ΑΝΕΡΓΙΑΣ";
        private const string UPLOAD_TRITEKNOS_TEXT = "ΒΕΒΑΙΩΣΗ ΤΡΙΤΕΚΝΗΣ ΟΙΚΟΓΕΝΕΙΑΣ";
        private const string UPLOAD_POLYTEKNOS_TEXT = "ΒΕΒΑΙΩΣΗ ΠΟΛΥΤΕΚΝΗΣ ΟΙΚΟΓΕΝΕΙΑΣ";
        private const string UPLOAD_SINGLEPARENT_TEXT = "ΒΕΒΑΙΩΣΗ ΜΟΝΟΓΟΝΕΪΚΗΣ ΟΙΚΟΓΕΝΕΙΑΣ";

        private const string UPLOAD_PTYXIO2_TEXT = "ΔΕΥΤΕΡΟ ΠΤΥΧΙΟ/ΜΕΤΑΠΤΥΧΙΑΚΟ/ΔΙΔΑΚΤΟΡΙΚΟ";
        private const string UPLOAD_ANAGNORISI_TEXT = "ΑΝΑΓΝΩΡΙΣΗ ΔΕΥΤΕΡΟΥ ΠΤΥΧΙΟΥ";
        private const string UPLOAD_LANGUAGE1_TEXT = "ΠΙΣΤΟΠΟΙΗΤΙΚΟ ΠΡΩΤΗΣ ΞΕΝΗΣ ΓΛΩΣΣΑΣ";
        private const string UPLOAD_LANGUAGE2_TEXT = "ΠΙΣΤΟΠΟΙΗΤΙΚΟ ΔΕΥΤΕΡΗΣ ΞΕΝΗΣ ΓΛΩΣΣΑΣ";
        private const string UPLOAD_COMPUTER_TEXT = "ΠΙΣΤΟΠΟΙΗΤΙΚΟ ΓΝΩΣΗΣ Η/Υ";
        private const string UPLOAD_CERTIFIED_TEXT = "ΠΙΣΤΟΠΟΙΗΣΗ ΕΚΠΑΙΔΕΥΤΗ ΕΝΗΛΙΚΩΝ";
        private const string UPLOAD_N2190_TEXT = "ΒΕΒΑΙΩΣΗ ΠΡΟΣΤΑΤΕΥΟΜΕΝΟΥ Ν.2190";

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
                return RedirectToAction("Index", "Teachers", new { notify = "Δεν βρέθηκε ανοικτή προκήρυξη για επεξεργασία." });
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

            if (!SaveUploadedFiles(model))
                ModelState.AddModelError("", "Ένα ή περισσότερα ανεβασμένα αρχεία δεν έχουν αποδεκτό τύπο (PDF, DOC, DOCX, ODT)");

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
                    //TeacherViewModel newData = GetTeacherDataFromDB(loggedTeacher.UserAfm);
                    return RedirectToAction("TeacherEdit", "Teachers");
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

            if (!SaveUploadedFiles(model))
                ModelState.AddModelError("", "Ένα ή περισσότερα ανεβασμένα αρχεία δεν έχουν αποδεκτό τύπο (PDF, DOC, DOCX, ODT)");

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


        #region SAVE TEACHER DATA FILES

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
                if (!string.IsNullOrEmpty(FileName1) && k.ValidFileExtension(Extension1))
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
                if (!string.IsNullOrEmpty(FileName2) && k.ValidFileExtension(Extension2))
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
                if (!string.IsNullOrEmpty(FileName3) && k.ValidFileExtension(Extension3))
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
                if (!string.IsNullOrEmpty(FileName4) && k.ValidFileExtension(Extension4))
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
                if (!string.IsNullOrEmpty(FileName5) && k.ValidFileExtension(Extension5))
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
                if (!string.IsNullOrEmpty(FileName6) && k.ValidFileExtension(Extension6))
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
                if (!string.IsNullOrEmpty(FileName7) && k.ValidFileExtension(Extension7))
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
                if (!string.IsNullOrEmpty(FileName8) && k.ValidFileExtension(Extension8))
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
                if (!string.IsNullOrEmpty(FileName9) && k.ValidFileExtension(Extension9))
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

            var data = (from d in db.TeacherUploads where d.FileName == filename && d.TeacherAFM == loggedTeacher.UserAfm select d).Count();
            if (data == 0)
            {
                TeacherUploads entity = new TeacherUploads()
                {
                    FileName = filename,
                    Description = description,
                    TeacherAFM = loggedTeacher.UserAfm,
                    SchoolYearText = c.GetSchoolYearText(schoolyearId)
                };
                db.TeacherUploads.Add(entity);
                db.SaveChanges();
            }
        }

        #endregion


        #region TEACHER SKILLS FORM

        public ActionResult SkillsCreate()
        {
            //check if user is unauthenticated to redirect him
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
                return RedirectToAction("TaxisNetLogin", "UserTeachers");
            else
                loggedTeacher = GetLoginTeacher();

            if (c.GetOpenProkirixiID() == 0)
            {
                return RedirectToAction("Index", "Teachers", new { notify = "Δεν βρέθηκε ανοικτή προκήρυξη για επεξεργασία." });
            }

            if (db.Teachers.Find(loggedTeacher.UserAfm) == null)
            {
                string msg = "Δεν βρέθηκαν τα στοιχεία εκπαιδευτικού. Πρέπει πρώτα να καταχωρήσετε προσωπικά στοιχεία.";
                return RedirectToAction("Index", "Teachers", new { notify = msg });
            }
            return View(new TeacherSkillsViewModel());
        }

        public ActionResult SkillsEdit()
        {
            //check if user is unauthenticated to redirect him
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
                return RedirectToAction("TaxisNetLogin", "UserTeachers");
            else
                loggedTeacher = GetLoginTeacher();

            if (c.GetOpenProkirixiID() == 0)
            {
                return RedirectToAction("Index", "Teachers", new { notify = "Δεν βρέθηκε ανοικτή προκήρυξη για επεξεργασία." });
            }

            if (db.Teachers.Find(loggedTeacher.UserAfm) == null)
            {
                string msg = "Δεν βρέθηκαν τα στοιχεία εκπαιδευτικού. Πρέπει πρώτα να καταχωρήσετε προσωπικά στοιχεία.";
                return RedirectToAction("Index", "Teachers", new { notify = msg });
            }

            if (db.TeacherSkills.Where(d => d.TeacherAFM == loggedTeacher.UserAfm).FirstOrDefault() == null)
                return RedirectToAction("SkillsCreate", "Teachers");

            TeacherSkillsViewModel model = GetSkillsModelFromDB(loggedTeacher.UserAfm);
            return View(model);
        }

        [HttpPost]
        public ActionResult SkillsCreate(TeacherSkillsViewModel model)
        {
            loggedTeacher = GetLoginTeacher();

            if (!SaveSkillsUploadedFiles(model))
                ModelState.AddModelError("", "Ένα ή περισσότερα ανεβασμένα αρχεία δεν έχουν αποδεκτό τύπο (PDF, DOC, DOCX, ODT)");

            if (ModelState.IsValid)
            {
                TeacherSkills entity = new TeacherSkills()
                {
                    TeacherAFM = loggedTeacher.UserAfm,
                    Ptyxio2Anagnorisi = model.Ptyxio2Anagnorisi,
                    Ptyxio2Foreign = model.Ptyxio2Foreign,
                    Ptyxio2Institution = model.Ptyxio2Institution,
                    Ptyxio2Titlos = model.Ptyxio2Titlos,
                    Ptyxio2Type = model.Ptyxio2Type,
                    Ptyxio2Year = model.Ptyxio2Year,
                    Language1 = model.Language1,
                    Language1Level = model.Language1Level,
                    Language1Titlos = model.Language1Titlos,
                    Language2 = model.Language2,
                    Language2Level = model.Language2Level,
                    Language2Titlos = model.Language2Titlos,
                    ComputerCertificate = model.ComputerCertificate,
                    ComputerTitlos = model.ComputerTitlos,
                    ComputerYear = model.ComputerYear,
                    CertifiedTrainer = model.CertifiedTrainer,
                    CertifiedTrainerAM = model.CertifiedTrainerAM,
                    Epimorfosi = model.Epimorfosi,
                    EpimorfosiTotalHours = model.EpimorfosiTotalHours,
                    N2190Protect = model.N2190Protect,
                    Ptyxio2Filename = model.FilePtyxio2 != null ? Path.GetFileName(model.FilePtyxio2.FileName) : "",
                    Ptyxio2AnagnorisiFilename = model.FileAnagnorisi != null ? Path.GetFileName(model.FileAnagnorisi.FileName) : "",
                    Language1Filename = model.FileLanguage1 != null ? Path.GetFileName(model.FileLanguage1.FileName) : "",
                    Language2Filename = model.FileLanguage2 != null ? Path.GetFileName(model.FileLanguage2.FileName) : "",
                    ComputerFilename = model.FileComputer != null ? Path.GetFileName(model.FileComputer.FileName) : "",
                    CertifiedTrainerFilename = model.FileCertified != null ? Path.GetFileName(model.FileCertified.FileName) : "",
                    N2190ProtectFilename = model.FileN2190 != null ? Path.GetFileName(model.FileN2190.FileName) : "",
                    OaedPtyxio2Confirm = true,
                    OaedCertifiedConfirm = true,
                    OaedComputerConfirm = true,
                    OaedEpimorfosiConfirm = true,
                    OaedLanguage1Confirm = true,
                    OaedLanguage2Confirm = true,
                    OaedN2190Confirm = model.N2190Protect == true ? true : false
                };
                db.TeacherSkills.Add(entity);
                db.SaveChanges();
                this.AddNotification("Η αποθήκευση ολοκληρώθηκε με επιτυχία.", NotificationType.SUCCESS);
                //TeacherSkillsViewModel newData = GetSkillsModelFromDB(loggedTeacher.UserAfm);
                // do this, otherwise each edit creates new record!!!
                return RedirectToAction("SkillsEdit", "Teachers");
            }
            else
            {
                this.AddNotification("Η αποθήκευση απέτυχε λόγω σφαλμάτων καταχώρησης. Δείτε τη σύνοψη στο κάτω μέρος της σελίδας.", NotificationType.ERROR);
                model.TeacherAFM = loggedTeacher.UserAfm;
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult SkillsEdit(TeacherSkillsViewModel model)
        {
            loggedTeacher = GetLoginTeacher();

            if (!SaveSkillsUploadedFiles(model))
                ModelState.AddModelError("", "Ένα ή περισσότερα ανεβασμένα αρχεία δεν έχουν αποδεκτό τύπο (PDF, DOC, DOCX, ODT)");

            if (ModelState.IsValid)
            {
                TeacherSkills entity = db.TeacherSkills.Where(d => d.TeacherAFM == loggedTeacher.UserAfm).FirstOrDefault();

                entity.TeacherAFM = loggedTeacher.UserAfm;
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
                entity.Ptyxio2Filename = model.FilePtyxio2 != null ? Path.GetFileName(model.FilePtyxio2.FileName) : GetPtyxio2Filename(loggedTeacher.UserAfm);
                entity.Ptyxio2AnagnorisiFilename = model.FileAnagnorisi != null ? Path.GetFileName(model.FileAnagnorisi.FileName) : GetPtyxio2AnagnorisiFilename(loggedTeacher.UserAfm);
                entity.Language1Filename = model.FileLanguage1 != null ? Path.GetFileName(model.FileLanguage1.FileName) : GetLanguage1Filename(loggedTeacher.UserAfm);
                entity.Language2Filename = model.FileLanguage2 != null ? Path.GetFileName(model.FileLanguage2.FileName) : GetLanguage2Filename(loggedTeacher.UserAfm); ;
                entity.ComputerFilename = model.FileComputer != null ? Path.GetFileName(model.FileComputer.FileName) : GetComputerFilename(loggedTeacher.UserAfm);
                entity.CertifiedTrainerFilename = model.FileCertified != null ? Path.GetFileName(model.FileCertified.FileName) : GetCertifiedFilename(loggedTeacher.UserAfm);
                entity.N2190ProtectFilename = model.FileN2190 != null ? Path.GetFileName(model.FileN2190.FileName) : GetN2190Filename(loggedTeacher.UserAfm);
                entity.OaedPtyxio2Confirm = true;
                entity.OaedCertifiedConfirm = true;
                entity.OaedComputerConfirm = true;
                entity.OaedEpimorfosiConfirm = true;
                entity.OaedLanguage1Confirm = true;
                entity.OaedLanguage2Confirm = true;
                entity.OaedN2190Confirm = model.N2190Protect == true ? true : false;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                this.AddNotification("Η αποθήκευση ολοκληρώθηκε με επιτυχία.", NotificationType.SUCCESS);
                TeacherSkillsViewModel newData = GetSkillsModelFromDB(loggedTeacher.UserAfm);
                return View(newData);
            }
            else
            {
                this.AddNotification("Η αποθήκευση απέτυχε λόγω σφαλμάτων καταχώρησης. Δείτε τη σύνοψη στο κάτω μέρος της σελίδας.", NotificationType.ERROR);
                model.TeacherAFM = loggedTeacher.UserAfm;
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


        #region SAVE TEACHER SKILLS FILES

        public bool SaveSkillsUploadedFiles(TeacherSkillsViewModel model)
        {
            loggedTeacher = GetLoginTeacher();
            string prefix_afm = loggedTeacher.UserAfm;
            string upload_path = UPLOAD_PATH + prefix_afm + "/";

            bool exists = Directory.Exists(Server.MapPath(upload_path));
            if (!exists)
                Directory.CreateDirectory(Server.MapPath(upload_path));

            if (model.FilePtyxio2 != null)
            {
                string FileName1 = Path.GetFileName(model.FilePtyxio2.FileName);
                string Extension1 = Path.GetExtension(model.FilePtyxio2.FileName);
                if (!string.IsNullOrEmpty(FileName1) && k.ValidFileExtension(Extension1))
                {
                    var physicalPath1 = Path.Combine(Server.MapPath(upload_path), FileName1);
                    model.FilePtyxio2.SaveAs(physicalPath1);
                    StoreUploadedFile(FileName1, physicalPath1, UPLOAD_PTYXIO2_TEXT);
                }
                else return false;
            }

            if (model.FileAnagnorisi != null)
            {
                string FileName2 = Path.GetFileName(model.FileAnagnorisi.FileName);
                string Extension2 = Path.GetExtension(model.FileAnagnorisi.FileName);
                if (!string.IsNullOrEmpty(FileName2) && k.ValidFileExtension(Extension2))
                {
                    var physicalPath2 = Path.Combine(Server.MapPath(upload_path), FileName2);
                    model.FileAnagnorisi.SaveAs(physicalPath2);
                    StoreUploadedFile(FileName2, physicalPath2, UPLOAD_ANAGNORISI_TEXT);
                }
                else return false;
            }

            if (model.FileLanguage1 != null)
            {
                string FileName3 = Path.GetFileName(model.FileLanguage1.FileName);
                string Extension3 = Path.GetExtension(model.FileLanguage1.FileName);
                if (!string.IsNullOrEmpty(FileName3) && k.ValidFileExtension(Extension3))
                {
                    var physicalPath3 = Path.Combine(Server.MapPath(upload_path), FileName3);
                    model.FileLanguage1.SaveAs(physicalPath3);
                    StoreUploadedFile(FileName3, physicalPath3, UPLOAD_LANGUAGE1_TEXT);
                }
                else return false;
            }

            if (model.FileLanguage2 != null)
            {
                string FileName4 = Path.GetFileName(model.FileLanguage2.FileName);
                string Extension4 = Path.GetExtension(model.FileLanguage2.FileName);
                if (!string.IsNullOrEmpty(FileName4) && k.ValidFileExtension(Extension4))
                {
                    var physicalPath4 = Path.Combine(Server.MapPath(upload_path), FileName4);
                    model.FileLanguage2.SaveAs(physicalPath4);
                    StoreUploadedFile(FileName4, physicalPath4, UPLOAD_LANGUAGE2_TEXT);
                }
                else return false;
            }

            if (model.FileComputer != null)
            {
                string FileName5 = Path.GetFileName(model.FileComputer.FileName);
                string Extension5 = Path.GetExtension(model.FileComputer.FileName);
                if (!string.IsNullOrEmpty(FileName5) && k.ValidFileExtension(Extension5))
                {
                    var physicalPath5 = Path.Combine(Server.MapPath(upload_path), FileName5);
                    model.FileComputer.SaveAs(physicalPath5);
                    StoreUploadedFile(FileName5, physicalPath5, UPLOAD_COMPUTER_TEXT);
                }
                else return false;
            }

            if (model.FileCertified != null)
            {
                string FileName6 = Path.GetFileName(model.FileCertified.FileName);
                string Extension6 = Path.GetExtension(model.FileCertified.FileName);
                if (!string.IsNullOrEmpty(FileName6) && k.ValidFileExtension(Extension6))
                {
                    var physicalPath6 = Path.Combine(Server.MapPath(upload_path), FileName6);
                    model.FileCertified.SaveAs(physicalPath6);
                    StoreUploadedFile(FileName6, physicalPath6, UPLOAD_CERTIFIED_TEXT);
                }
                else return false;
            }

            if (model.FileN2190 != null)
            {
                string FileName7 = Path.GetFileName(model.FileN2190.FileName);
                string Extension7 = Path.GetExtension(model.FileN2190.FileName);
                if (!string.IsNullOrEmpty(FileName7) && k.ValidFileExtension(Extension7))
                {
                    var physicalPath7 = Path.Combine(Server.MapPath(upload_path), FileName7);
                    model.FileN2190.SaveAs(physicalPath7);
                    StoreUploadedFile(FileName7, physicalPath7, UPLOAD_N2190_TEXT);
                }
                else return false;
            }

            return true;
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

        public string GetPtyxio2Filename(string afm)
        {
            var data = (from d in db.TeacherSkills where d.TeacherAFM == afm select new { d.Ptyxio2Filename }).FirstOrDefault();
            if (data != null)
                return data.Ptyxio2Filename;
            return "";
        }

        public string GetPtyxio2AnagnorisiFilename(string afm)
        {
            var data = (from d in db.TeacherSkills where d.TeacherAFM == afm select new { d.Ptyxio2AnagnorisiFilename }).FirstOrDefault();
            if (data != null)
                return data.Ptyxio2AnagnorisiFilename;
            return "";
        }

        public string GetLanguage1Filename(string afm)
        {
            var data = (from d in db.TeacherSkills where d.TeacherAFM == afm select new { d.Language1Filename }).FirstOrDefault();
            if (data != null)
                return data.Language1Filename;
            return "";
        }

        public string GetLanguage2Filename(string afm)
        {
            var data = (from d in db.TeacherSkills where d.TeacherAFM == afm select new { d.Language2Filename }).FirstOrDefault();
            if (data != null)
                return data.Language2Filename;
            return "";
        }

        public string GetComputerFilename(string afm)
        {
            var data = (from d in db.TeacherSkills where d.TeacherAFM == afm select new { d.ComputerFilename }).FirstOrDefault();
            if (data != null)
                return data.ComputerFilename;
            return "";
        }

        public string GetCertifiedFilename(string afm)
        {
            var data = (from d in db.TeacherSkills where d.TeacherAFM == afm select new { d.CertifiedTrainerFilename }).FirstOrDefault();
            if (data != null)
                return data.CertifiedTrainerFilename;
            return "";
        }

        public string GetN2190Filename(string afm)
        {
            var data = (from d in db.TeacherSkills where d.TeacherAFM == afm select new { d.N2190ProtectFilename }).FirstOrDefault();
            if (data != null)
                return data.N2190ProtectFilename;
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
                TeacherUploads entity = db.TeacherUploads.Find(data.UploadFileID);
                if (entity != null)
                {
                    // First delete the physical file and then the info record. Important!
                    DeleteUploadedFile(data.UploadFileID);

                    db.Entry(entity).State = EntityState.Deleted;
                    db.TeacherUploads.Remove(entity);
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
            TeacherUploads entity = db.TeacherUploads.Find(fileId);
            if (entity != null)
            {
                // First delete the physical file and then the info record. Important!
                DeleteUploadedFile(fileId);
                // Clear the filename field in Teachers data and then the record. Important!
                ClearFilenameField(fileId);

                db.Entry(entity).State = EntityState.Deleted;
                db.TeacherUploads.Remove(entity);
                db.SaveChanges();
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public List<TeacherUploadViewModel> GetTeacherUploadsFromDB()
        {
            loggedTeacher = GetLoginTeacher();

            var data = (from d in db.TeacherUploads
                        where d.TeacherAFM == loggedTeacher.UserAfm
                        orderby d.FileName
                        select new TeacherUploadViewModel
                        {
                            UploadFileID = d.UploadFileID,
                            FileName = d.FileName,
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

            var data = (from d in db.TeacherUploads where d.UploadFileID == fileId select d).FirstOrDefault();
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
            var data = (from d in db.TeacherUploads where d.UploadFileID == fileId select new { d.Description, d.TeacherAFM }).FirstOrDefault();
            if (data == null) return;

            // 1. FIELDS FROM TEACHERS TABLE
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
            // 2. FIELDS FROM SKILLS TABLE
            else if (data.Description == UPLOAD_PTYXIO2_TEXT)
            {
                TeacherSkills entity = db.TeacherSkills.Where(d => d.TeacherAFM == data.TeacherAFM).FirstOrDefault();
                if (entity != null)
                {
                    entity.Ptyxio2Filename = "";
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else if (data.Description == UPLOAD_ANAGNORISI_TEXT)
            {
                TeacherSkills entity = db.TeacherSkills.Where(d => d.TeacherAFM == data.TeacherAFM).FirstOrDefault();
                if (entity != null)
                {
                    entity.Ptyxio2AnagnorisiFilename = "";
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else if (data.Description == UPLOAD_LANGUAGE1_TEXT)
            {
                TeacherSkills entity = db.TeacherSkills.Where(d => d.TeacherAFM == data.TeacherAFM).FirstOrDefault();
                if (entity != null)
                {
                    entity.Language1Filename = "";
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else if (data.Description == UPLOAD_LANGUAGE2_TEXT)
            {
                TeacherSkills entity = db.TeacherSkills.Where(d => d.TeacherAFM == data.TeacherAFM).FirstOrDefault();
                if (entity != null)
                {
                    entity.Language2Filename = "";
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else if (data.Description == UPLOAD_COMPUTER_TEXT)
            {
                TeacherSkills entity = db.TeacherSkills.Where(d => d.TeacherAFM == data.TeacherAFM).FirstOrDefault();
                if (entity != null)
                {
                    entity.ComputerFilename = "";
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else if (data.Description == UPLOAD_CERTIFIED_TEXT)
            {
                TeacherSkills entity = db.TeacherSkills.Where(d => d.TeacherAFM == data.TeacherAFM).FirstOrDefault();
                if (entity != null)
                {
                    entity.CertifiedTrainerFilename = "";
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else if (data.Description == UPLOAD_N2190_TEXT)
            {
                TeacherSkills entity = db.TeacherSkills.Where(d => d.TeacherAFM == data.TeacherAFM).FirstOrDefault();
                if (entity != null)
                {
                    entity.N2190ProtectFilename = "";
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

            var fileinfo = (from d in db.TeacherUploads where d.UploadFileID == file_id select d).FirstOrDefault();
            if (fileinfo != null)
            {
                filename = fileinfo.FileName;
            }

            return File(Path.Combine(Server.MapPath(physicalPath), filename), System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }

        #endregion


        #region ΣΕΛΙΔΑ ΕΠΙΜΟΡΦΩΣΕΩΝ ΜΕ ΔΥΟ ΠΛΕΓΜΑΤΑ

        public ActionResult Epimorfoseis(string notify = null)
        {
            //check if user is unauthenticated to redirect him
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
                return RedirectToAction("TaxisNetLogin", "UserTeachers");
            else
                loggedTeacher = GetLoginTeacher();

            if (notify != null)
                this.AddNotification(notify, NotificationType.WARNING);

            return View();
        }

        public ActionResult Epimorfosi_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetEpimorfoseisFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Epimorfosi_Create([DataSourceRequest] DataSourceRequest request, EpimorfosiViewModel data)
        {
            loggedTeacher = GetLoginTeacher();
            var newdata = new EpimorfosiViewModel();

            if (data != null && ModelState.IsValid)
            {
                Epimorfosis entity = new Epimorfosis()
                {
                    TeacherAfm = loggedTeacher.UserAfm,
                    ProkirixiID = c.GetOpenProkirixiID(),
                    EpimorfosiTitlos = data.EpimorfosiTitlos,
                    EpimorfosiForeas = data.EpimorfosiForeas,
                    EpimorfosiDateStart = data.EpimorfosiDateStart,
                    EpimorfosiDateFinal = data.EpimorfosiDateFinal,
                    EpimorfosiHours = data.EpimorfosiHours,
                    EpimorfosiType = data.EpimorfosiType
                };
                db.Epimorfosis.Add(entity);
                db.SaveChanges();
                data.EpimorfosiID = entity.EpimorfosiID;
                newdata = RefreshEpimorfosiFromDB(data.EpimorfosiID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Epimorfosi_Update([DataSourceRequest] DataSourceRequest request, EpimorfosiViewModel data)
        {
            loggedTeacher = GetLoginTeacher();
            var newdata = new EpimorfosiViewModel();

            if (data != null && ModelState.IsValid)
            {
                Epimorfosis entity = db.Epimorfosis.Find(data.EpimorfosiID);

                entity.TeacherAfm = loggedTeacher.UserAfm;
                entity.ProkirixiID = c.GetOpenProkirixiID();

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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Epimorfosi_Destroy([DataSourceRequest] DataSourceRequest request, EpimorfosiViewModel data)
        {
            if (data != null)
            {
                Epimorfosis entity = db.Epimorfosis.Find(data.EpimorfosiID);
                if (entity != null)
                {
                    if (k.CanDeleteEpimorfosi(data.EpimorfosiID))
                    {
                        db.Entry(entity).State = EntityState.Deleted;
                        db.Epimorfosis.Remove(entity);
                        db.SaveChanges();
                    }
                    else
                        ModelState.AddModelError("", "Δεν μπορεί να διαγραφεί η εγγραφή διότι έχει συσχετισμένα αρχεία. Διαγράψτε πρώτα τα αρχεία.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public EpimorfosiViewModel RefreshEpimorfosiFromDB(int recordId)
        {
            loggedTeacher = GetLoginTeacher();
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

        public List<EpimorfosiViewModel> GetEpimorfoseisFromDB()
        {
            loggedTeacher = GetLoginTeacher();
            var data = (from d in db.Epimorfosis
                        where d.TeacherAfm == loggedTeacher.UserAfm
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

        #endregion


        #region CHILD GRID WITH EPIMORFOSI FILES

        public ActionResult EpimorfosiFiles_Read([DataSourceRequest] DataSourceRequest request, int epimorfosiId = 0)
        {
            var data = GetEpimorfosiFilesFromDB(epimorfosiId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EpimorfosiFiles_Destroy([DataSourceRequest] DataSourceRequest request, EpimorfosiFileViewModel data)
        {
            if (data != null)
            {
                EpimorfosiFiles entity = db.EpimorfosiFiles.Find(data.UploadFileID);
                if (entity != null)
                {
                    // First delete the physical file and then the info record. Important!
                    DeleteEpimorfosiFile(data.UploadFileID);

                    db.Entry(entity).State = EntityState.Deleted;
                    db.EpimorfosiFiles.Remove(entity);
                    db.SaveChanges();
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
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

        public void DeleteEpimorfosiFile(int fileId)
        {
            loggedTeacher = GetLoginTeacher();
            string uploadPath = EPIMORFOSI_PATH + loggedTeacher.UserAfm + "/";
            string filename = "";

            var data = (from d in db.EpimorfosiFiles where d.UploadFileID == fileId select d).FirstOrDefault();
            if (data != null)
            {
                filename = data.Filename;
                var physicalPath = Path.Combine(Server.MapPath(uploadPath), filename);
                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
            }
        }

        #endregion


        #region EPIMOFROSI UPLOAD FORM WITH FUNCTIONS

        public ActionResult UploadEpimorfosi(int epimorfosiId, string notify = null)
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
                if (loggedTeacher == null)
                    return RedirectToAction("Error", "Teachers", new { notify = "Δεν βρέθηκε εξουσιοδοτημένος χρήστης για το αίτημα." });
            }
            if (notify != null)
                this.AddNotification(notify, NotificationType.WARNING);

            if (!(epimorfosiId > 0))
            {
                string msg = "Άκυρος κωδικός επιμόρφωσης. Πρέπει πρώτα να αποθηκεύσετε την εγγραφή επιμόρφωσης και μετά να ανεβάσετε αρχείο.";
                return RedirectToAction("ErrorData", "Teachers", new { notify = msg });
            }
            ViewData["epimorfosiId"] = epimorfosiId;

            return View();
        }

        public ActionResult Epimorfosi_Upload(IEnumerable<HttpPostedFileBase> files, int epimorfosiId = 0)
        {
            loggedTeacher = GetLoginTeacher();
            string uploadPath = EPIMORFOSI_PATH + loggedTeacher.UserAfm + "/";

            try
            {
                if (!Directory.Exists(Server.MapPath(uploadPath)))
                    Directory.CreateDirectory(Server.MapPath(uploadPath));

                if (files != null)
                {
                    foreach (var file in files)
                    {
                        // Some browsers send file names with full path. We are only interested in the file name.
                        if (file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var fileExtension = Path.GetExtension(fileName);
                            if (!k.ValidFileExtension(fileExtension))
                            {
                                string msg = "Μη επιτρεπόμενος τύπος αρχείου. Δοκιμάστε πάλι.";
                                return Content(msg);
                            }
                            var physicalPath = Path.Combine(Server.MapPath(uploadPath), fileName);
                            file.SaveAs(physicalPath);

                            EpimorfosiFiles fileDetail = new EpimorfosiFiles()
                            {
                                Filename = fileName.Length > 255 ? fileName.Substring(0, 255) : fileName,
                                TeacherAFM = loggedTeacher.UserAfm,
                                Description = "ΠΙΣΤΟΠΟΙΗΤΙΚΟ ΕΠΙΜΟΡΦΩΣΗΣ (" + loggedTeacher.UserAfm + ")",
                                EpimorfosiID = epimorfosiId
                            };
                            db.EpimorfosiFiles.Add(fileDetail);
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = "Παρουσιάστηκε σφάλμα στη μεταφόρτωση:<br/>" + ex.Message;
                return Content(msg);
            }
            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult Epimorfosi_Remove(string[] fileNames, int epimorfosiId)
        {
            // The parameter of the Remove action must be called "fileNames"
            loggedTeacher = GetLoginTeacher();
            string uploadPath = EPIMORFOSI_PATH + loggedTeacher.UserAfm + "/";

            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    var extension = Path.GetExtension(fileName);

                    var physicalPath = Path.Combine(Server.MapPath(uploadPath), fileName);
                    if (System.IO.File.Exists(physicalPath))
                    {
                        System.IO.File.Delete(physicalPath);
                        DeleteUploadFileRecord(fileName);
                    }
                }
            }
            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult DeleteUploadFileRecord(string filename)
        {
            EpimorfosiFiles entity = db.EpimorfosiFiles.Where(d => d.Filename == filename).FirstOrDefault();
            if (entity != null)
            {
                db.Entry(entity).State = EntityState.Deleted;
                db.EpimorfosiFiles.Remove(entity);
                db.SaveChanges();
            }
            return Content("");
        }

        public FileResult DownloadEpimorfosiFile(int file_id)
        {
            loggedTeacher = GetLoginTeacher();
            string f = "";
            string uploadPath = EPIMORFOSI_PATH + loggedTeacher.UserAfm + "/";

            var fileinfo = (from d in db.EpimorfosiFiles where d.UploadFileID == file_id select d).FirstOrDefault();
            if (fileinfo != null)
            {
                f = fileinfo.Filename;
            }
            return File(Path.Combine(Server.MapPath(uploadPath), f), System.Net.Mime.MediaTypeNames.Application.Octet, f);
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

        public JsonResult GetPtyxiaTypes()
        {
            var data = db.PtyxiaTypes.Select(p => new PtyxiaTypesViewModel
            {
                DegreeTypeID = p.DegreeTypeID,
                DegreeTypeText = p.DegreeTypeText
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLanguages(string text)
        {
            var data = db.SysLanguage.Select(p => new LanguageViewModel
            {
                LanguageID = p.LanguageID,
                LanguageText = p.LanguageText
            });

            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(p => p.LanguageText.Contains(text));
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLanguageLevel()
        {
            var data = db.SysLanguageLevel.Select(p => new LanguageLevelViewModel
            {
                LevelID = p.LevelID,
                LevelText = p.LevelText
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetComputerCertificates(string text)
        {
            var data = db.SysComputerAsep.Select(p => new ComputerAsepViewModel
            {
                CertificateID = p.CertificateID,
                CertificateText = p.CertificateText
            });

            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(p => p.CertificateText.Contains(text));
            }
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

    }
}