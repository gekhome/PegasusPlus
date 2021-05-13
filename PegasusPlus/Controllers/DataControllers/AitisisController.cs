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
using System.Net;

namespace PegasusPlus.Controllers.DataControllers
{
    public class AitisisController : Controller
    {
        private PegasusPlusDBEntities db = new PegasusPlusDBEntities();
        private string Msg;
        private int prokirixiId;
        private bool ViewAllowed;
        private const string UPLOAD_PATH = "~/Uploads/FilesPersonal/";
        private const string ENSTASEIS_PATH = "~/Uploads/Enstaseis/";

        private const string PTYXIO_TEXT = "ΒΑΣΙΚΟ ΠΤΥΧΙΟ";
        private const string PTYXIO_ANAGNORISI_TEXT = "ΑΝΑΓΝΩΡΙΣΗ ΒΑΣΙΚΟΥ ΠΤΥΧΙΟΥ";
        private const string MASTER_TEXT = "ΜΕΤΑΠΤΥΧΙΑΚΟ";
        private const string MASTER_ANAGNORISI_TEXT = "ΑΝΑΓΝΩΡΙΣΗ ΜΕΤΑΠΤΥΧΙΑΚΟΥ";
        private const string PHD_TEXT = "ΔΙΔΑΚΤΟΡΙΚΟ";
        private const string PHD_ANAGNORISI_TEXT = "ΑΝΑΓΝΩΡΙΣΗ ΔΙΔΑΚΤΟΡΙΚΟΥ";

        Common c = new Common();
        Kerberos k = new Kerberos();

        private UserTeachers loggedTeacher;
        private Teachers loggedTeacherData;


        #region AITISI DATA FORM

        public ActionResult AitisiCreate()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
                return RedirectToAction("TaxisNetLogin", "UserTeachers");
            else
                loggedTeacher = GetLoginTeacher();

            prokirixiId = c.GetOpenProkirixiID();
            if (prokirixiId == 0)
            {
                return RedirectToAction("Index", "Teachers", new { notify = "Δεν βρέθηκε ανοικτή Προκήρυξη. Η διαδικασία ακυρώθηκε." });
            }
            AitisisViewModel newAitisi = new AitisisViewModel()
            {
                TeacherAFM = loggedTeacher.UserAfm,
                AitisisDate = DateTime.Now.Date,
                ProkirixisID = prokirixiId
            };

            // try to select an existing teacher by AFM
            var data = (from d in db.Teachers where d.AFM == loggedTeacher.UserAfm select d).FirstOrDefault();
            if (data == null)
            {
                Msg = "Δεν βρέθηκε εκπαιδευτικός με αυτό το ΑΦΜ. Καταχωρήστε πρώτα τα προσωπικά στοιχεία.";
                return RedirectToAction("Index", "Teachers", new { notify = Msg });
            }
            return View(newAitisi);
        }

        public ActionResult AitisiEdit(int? aitisiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
                return RedirectToAction("TaxisNetLogin", "UserTeachers");
            else
                loggedTeacher = GetLoginTeacher();

            if (c.GetOpenProkirixiID() == 0)
            {
                return RedirectToAction("Index", "Teachers", new { notify = "Δεν βρέθηκε ανοικτή Προκήρυξη. Η διαδικασία ακυρώθηκε." });
            }
            if (aitisiId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            AitisisViewModel aitisi = GetAitisiModelFromDB((int)aitisiId);

            if (aitisi == null)
            {
                string msg = "Προέκυψε αδυναμία εύρεσης της επιλεγμένης αίτησης.";
                return RedirectToAction("ErrorData", "Aitisis", new { notify = msg });
            }
            return View(aitisi);
        }

        // Called from Aitisis Grid
        public ActionResult AitisiEdit2(int? aitisiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
                return RedirectToAction("TaxisNetLogin", "UserTeachers");
            else
                loggedTeacher = GetLoginTeacher();

            if (aitisiId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            AitisisViewModel aitisi = GetAitisiModelFromDB((int)aitisiId);

            if (aitisi == null)
            {
                string msg = "Προέκυψε αδυναμία εύρεσης της επιλεγμένης αίτησης.";
                return RedirectToAction("ErrorData", "Aitisis", new { notify = msg });
            }
            return View(aitisi);
        }

        [HttpPost]
        public ActionResult AitisiCreate(AitisisViewModel model)
        {
            loggedTeacher = GetLoginTeacher();

            if (!SaveUploadedFiles(model))
                ModelState.AddModelError("", "Ένα ή περισσότερα ανεβασμένα αρχεία δεν έχουν αποδεκτό τύπο (PDF, DOC, DOCX, ODT)");

            if (ModelState.IsValid)
            {
                Aitisis entity = new Aitisis()
                {
                    AitisisDate = DateTime.Now.Date,
                    AitisisProtocol = c.GenerateProtocol(),
                    TeacherAFM = loggedTeacher.UserAfm,
                    ProkirixisID = c.GetOpenProkirixiID(),
                    Klados = model.Klados,
                    Eidikotita = model.Eidikotita,
                    BasicEducation = model.BasicEducation,
                    PtyxioType = model.PtyxioType,
                    PtyxioTitlos = model.PtyxioTitlos,
                    PtyxioInstitution = model.PtyxioInstitution,
                    PtyxioSchool = model.PtyxioSchool,
                    PtyxioTmima = model.PtyxioTmima,
                    PtyxioGrade = model.PtyxioGrade,
                    PtyxioYear = model.PtyxioYear,
                    PtyxioForeign = model.PtyxioForeign,
                    PtyxioAnagnorisi = model.PtyxioAnagnorisi,
                    MscTitlos = model.MscTitlos,
                    MscInstitution = model.MscInstitution,
                    MscYear = model.MscYear,
                    MscSynafeia = model.MscSynafeia,
                    MscForeign = model.MscForeign,
                    MscAnagnorisi = model.MscAnagnorisi,
                    PhdTitlos = model.PhdTitlos,
                    PhdInstitution = model.PhdInstitution,
                    PhdYear = model.PhdYear,
                    PhdSynafeia = model.PhdSynafeia,
                    PhdForeign = model.PhdForeign,
                    PhdAnagnorisi = model.PhdAnagnorisi,
                    EpagelmaCategory = model.EpagelmaCategory,
                    Periferia = model.Periferia,
                    School = model.School,
                    PtyxioFilename = model.FilePtyxio != null ? Path.GetFileName(model.FilePtyxio.FileName) : "",
                    PtyxioAnagnorisiFilename = model.FilePtyxioAnagnorisi != null ? Path.GetFileName(model.FilePtyxioAnagnorisi.FileName) : "",
                    MscFilename = model.FileMsc != null ? Path.GetFileName(model.FileMsc.FileName) : "",
                    MscAnagnorisiFilename = model.FileMscAnagnorisi != null ? Path.GetFileName(model.FileMscAnagnorisi.FileName) : "",
                    PhdFilename = model.FilePhd != null ? Path.GetFileName(model.FilePhd.FileName) : "",
                    PhdAnagnorisiFilename = model.FilePhdAnagnorisi != null ? Path.GetFileName(model.FilePhdAnagnorisi.FileName) : "",
                    OaedPtyxioConfirm = true,
                    OaedMscConfirm = true,
                    OaedPhdConfirm = true
                };
                db.Entry(entity).State = EntityState.Added;
                db.Aitisis.Add(entity);
                db.SaveChanges();

                model.AitisisID = entity.AitisisID;
                this.AddNotification("Η αποθήκευση της αίτησης ολοκληρώθηκε με επιτυχία.", NotificationType.SUCCESS);
                AitisisViewModel newData = GetAitisiModelFromDB(entity.AitisisID);
                return RedirectToAction("AitisiEdit", "Aitisis", new { aitisiId = entity.AitisisID });
            }
            else
            {
                this.AddNotification("Η αποθήκευση απέτυχε λόγω σφαλμάτων καταχώρησης. Δείτε τη σύνοψη στο κάτω μέρος της σελίδας.", NotificationType.ERROR);
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult AitisiEdit(AitisisViewModel model)
        {
            loggedTeacher = GetLoginTeacher();

            int aitisiId = model.AitisisID;

            if (ModelState.IsValid)
            {
                Aitisis entity = db.Aitisis.Find(aitisiId);

                entity.AitisisDate = DateTime.Now.Date;
                entity.Klados = model.Klados;
                entity.Eidikotita = model.Eidikotita;
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
                entity.MscTitlos = model.MscTitlos;
                entity.MscInstitution = model.MscInstitution;
                entity.MscYear = model.MscYear;
                entity.MscSynafeia = model.MscSynafeia;
                entity.MscForeign = model.MscForeign;
                entity.MscAnagnorisi = model.MscAnagnorisi;
                entity.PhdTitlos = model.PhdTitlos;
                entity.PhdInstitution = model.PhdInstitution;
                entity.PhdYear = model.PhdYear;
                entity.PhdSynafeia = model.PhdSynafeia;
                entity.PhdForeign = model.PhdForeign;
                entity.PhdAnagnorisi = model.PhdAnagnorisi;
                entity.EpagelmaCategory = model.EpagelmaCategory;
                entity.Periferia = model.Periferia;
                entity.School = model.School;
                entity.PtyxioFilename = model.FilePtyxio != null ? Path.GetFileName(model.FilePtyxio.FileName) : GetPtyxioFilename(aitisiId);
                entity.PtyxioAnagnorisiFilename = model.FilePtyxioAnagnorisi != null ? Path.GetFileName(model.FilePtyxioAnagnorisi.FileName) : GetPtyxioAnagnorisiFilename(aitisiId);
                entity.MscFilename = model.FileMsc != null ? Path.GetFileName(model.FileMsc.FileName) : GetMscFilename(aitisiId);
                entity.MscAnagnorisiFilename = model.FileMscAnagnorisi != null ? Path.GetFileName(model.FileMscAnagnorisi.FileName) : GetMscAnagnorisiFilename(aitisiId);
                entity.PhdFilename = model.FilePhd != null ? Path.GetFileName(model.FilePhd.FileName) : GetPhdFilename(aitisiId);
                entity.PhdAnagnorisiFilename = model.FilePhdAnagnorisi != null ? Path.GetFileName(model.FilePhdAnagnorisi.FileName) : GetPhdAnagnorisiFilename(aitisiId);
                entity.OaedPtyxioConfirm = true;
                entity.OaedMscConfirm = true;
                entity.OaedPhdConfirm = true;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                this.AddNotification("Η αποθήκευση της αίτησης ολοκληρώθηκε με επιτυχία.", NotificationType.SUCCESS);
                AitisisViewModel newData = GetAitisiModelFromDB(entity.AitisisID);
                return View(newData);
            }
            else
            {
                this.AddNotification("Η αποθήκευση απέτυχε λόγω σφαλμάτων καταχώρησης. Δείτε τη σύνοψη στο κάτω μέρος της σελίδας.", NotificationType.ERROR);
                return View(model);
            }
        }

        // Called from Aitisis Grid
        [HttpPost]
        public ActionResult AitisiEdit2(AitisisViewModel model)
        {
            loggedTeacher = GetLoginTeacher();

            int aitisiId = model.AitisisID;

            if (ModelState.IsValid)
            {
                Aitisis entity = db.Aitisis.Find(aitisiId);

                entity.AitisisDate = DateTime.Now.Date;
                entity.Klados = model.Klados;
                entity.Eidikotita = model.Eidikotita;
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
                entity.MscTitlos = model.MscTitlos;
                entity.MscInstitution = model.MscInstitution;
                entity.MscYear = model.MscYear;
                entity.MscSynafeia = model.MscSynafeia;
                entity.MscForeign = model.MscForeign;
                entity.MscAnagnorisi = model.MscAnagnorisi;
                entity.PhdTitlos = model.PhdTitlos;
                entity.PhdInstitution = model.PhdInstitution;
                entity.PhdYear = model.PhdYear;
                entity.PhdSynafeia = model.PhdSynafeia;
                entity.PhdForeign = model.PhdForeign;
                entity.PhdAnagnorisi = model.PhdAnagnorisi;
                entity.EpagelmaCategory = model.EpagelmaCategory;
                entity.Periferia = model.Periferia;
                entity.School = model.School;
                entity.PtyxioFilename = model.FilePtyxio != null ? Path.GetFileName(model.FilePtyxio.FileName) : GetPtyxioFilename(aitisiId);
                entity.PtyxioAnagnorisiFilename = model.FilePtyxioAnagnorisi != null ? Path.GetFileName(model.FilePtyxioAnagnorisi.FileName) : GetPtyxioAnagnorisiFilename(aitisiId);
                entity.MscFilename = model.FileMsc != null ? Path.GetFileName(model.FileMsc.FileName) : GetMscFilename(aitisiId);
                entity.MscAnagnorisiFilename = model.FileMscAnagnorisi != null ? Path.GetFileName(model.FileMscAnagnorisi.FileName) : GetMscAnagnorisiFilename(aitisiId);
                entity.PhdFilename = model.FilePhd != null ? Path.GetFileName(model.FilePhd.FileName) : GetPhdFilename(aitisiId);
                entity.PhdAnagnorisiFilename = model.FilePhdAnagnorisi != null ? Path.GetFileName(model.FilePhdAnagnorisi.FileName) : GetPhdAnagnorisiFilename(aitisiId);
                entity.OaedPtyxioConfirm = true;
                entity.OaedMscConfirm = true;
                entity.OaedPhdConfirm = true;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                this.AddNotification("Η αποθήκευση της αίτησης ολοκληρώθηκε με επιτυχία.", NotificationType.SUCCESS);
                AitisisViewModel newData = GetAitisiModelFromDB(entity.AitisisID);
                return View(newData);
            }
            else
            {
                this.AddNotification("Η αποθήκευση απέτυχε λόγω σφαλμάτων καταχώρησης. Δείτε τη σύνοψη στο κάτω μέρος της σελίδας.", NotificationType.ERROR);
                return View(model);
            }
        }

        public AitisisViewModel GetAitisiModelFromDB(int aitisiId)
        {
            loggedTeacher = GetLoginTeacher();

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
                            School = d.School
                        }).FirstOrDefault();

            return data;
        }

        #endregion


        #region SAVE AITISI UPLOADED FILES

        public bool SaveUploadedFiles(AitisisViewModel model)
        {
            loggedTeacher = GetLoginTeacher();
            string upload_path = UPLOAD_PATH + loggedTeacher.UserAfm + "/";

            bool exists = Directory.Exists(Server.MapPath(upload_path));
            if (!exists)
                Directory.CreateDirectory(Server.MapPath(upload_path));

            if (model.FilePtyxio != null)
            {
                string FileName1 = Path.GetFileName(model.FilePtyxio.FileName);
                string Extension1 = Path.GetExtension(model.FilePtyxio.FileName);
                if (!string.IsNullOrEmpty(FileName1) && k.ValidFileExtension(Extension1))
                {
                    var physicalPath1 = Path.Combine(Server.MapPath(upload_path), FileName1);
                    model.FilePtyxio.SaveAs(physicalPath1);
                    StoreUploadedFile(FileName1, physicalPath1, PTYXIO_TEXT);
                }
                else return false;
            }

            if (model.FilePtyxioAnagnorisi != null)
            {
                string FileName2 = Path.GetFileName(model.FilePtyxioAnagnorisi.FileName);
                string Extension2 = Path.GetExtension(model.FilePtyxioAnagnorisi.FileName);
                if (!string.IsNullOrEmpty(FileName2) && k.ValidFileExtension(Extension2))
                {
                    var physicalPath2 = Path.Combine(Server.MapPath(upload_path), FileName2);
                    model.FilePtyxioAnagnorisi.SaveAs(physicalPath2);
                    StoreUploadedFile(FileName2, physicalPath2, PTYXIO_ANAGNORISI_TEXT);
                }
                else return false;
            }

            if (model.FileMsc != null)
            {
                string FileName3 = Path.GetFileName(model.FileMsc.FileName);
                string Extension3 = Path.GetExtension(model.FileMsc.FileName);
                if (!string.IsNullOrEmpty(FileName3) && k.ValidFileExtension(Extension3))
                {
                    var physicalPath3 = Path.Combine(Server.MapPath(upload_path), FileName3);
                    model.FileMsc.SaveAs(physicalPath3);
                    StoreUploadedFile(FileName3, physicalPath3, MASTER_TEXT);
                }
                else return false;
            }

            if (model.FileMscAnagnorisi != null)
            {
                string FileName4 = Path.GetFileName(model.FileMscAnagnorisi.FileName);
                string Extension4 = Path.GetExtension(model.FileMscAnagnorisi.FileName);
                if (!string.IsNullOrEmpty(FileName4) && k.ValidFileExtension(Extension4))
                {
                    var physicalPath4 = Path.Combine(Server.MapPath(upload_path), FileName4);
                    model.FileMscAnagnorisi.SaveAs(physicalPath4);
                    StoreUploadedFile(FileName4, physicalPath4, MASTER_ANAGNORISI_TEXT);
                }
                else return false;
            }

            if (model.FilePhd != null)
            {
                string FileName5 = Path.GetFileName(model.FilePhd.FileName);
                string Extension5 = Path.GetExtension(model.FilePhd.FileName);
                if (!string.IsNullOrEmpty(FileName5) && k.ValidFileExtension(Extension5))
                {
                    var physicalPath5 = Path.Combine(Server.MapPath(upload_path), FileName5);
                    model.FilePhd.SaveAs(physicalPath5);
                    StoreUploadedFile(FileName5, physicalPath5, PHD_TEXT);
                }
                else return false;
            }

            if (model.FilePhdAnagnorisi != null)
            {
                string FileName6 = Path.GetFileName(model.FilePhdAnagnorisi.FileName);
                string Extension6 = Path.GetExtension(model.FilePhdAnagnorisi.FileName);
                if (!string.IsNullOrEmpty(FileName6) && k.ValidFileExtension(Extension6))
                {
                    var physicalPath6 = Path.Combine(Server.MapPath(upload_path), FileName6);
                    model.FilePhdAnagnorisi.SaveAs(physicalPath6);
                    StoreUploadedFile(FileName6, physicalPath6, PHD_ANAGNORISI_TEXT);
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

            var data = (from d in db.TeacherUploads where d.FileName == filename select d).Count();
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


        #region GET FILENAME FIELDS FROM DATABASE

        public string GetPtyxioFilename(int aitisiId)
        {
            var data = (from d in db.Aitisis where d.AitisisID == aitisiId select new { d.PtyxioFilename }).FirstOrDefault();
            if (data != null)
                return data.PtyxioFilename;
            return "";
        }

        public string GetPtyxioAnagnorisiFilename(int aitisiId)
        {
            var data = (from d in db.Aitisis where d.AitisisID == aitisiId select new { d.PtyxioAnagnorisiFilename }).FirstOrDefault();
            if (data != null)
                return data.PtyxioAnagnorisiFilename;
            return "";
        }

        public string GetMscFilename(int aitisiId)
        {
            var data = (from d in db.Aitisis where d.AitisisID == aitisiId select new { d.MscFilename }).FirstOrDefault();
            if (data != null)
                return data.MscFilename;
            return "";
        }

        public string GetMscAnagnorisiFilename(int aitisiId)
        {
            var data = (from d in db.Aitisis where d.AitisisID == aitisiId select new { d.MscAnagnorisiFilename }).FirstOrDefault();
            if (data != null)
                return data.MscAnagnorisiFilename;
            return "";
        }

        public string GetPhdFilename(int aitisiId)
        {
            var data = (from d in db.Aitisis where d.AitisisID == aitisiId select new { d.PhdFilename }).FirstOrDefault();
            if (data != null)
                return data.PhdFilename;
            return "";
        }

        public string GetPhdAnagnorisiFilename(int aitisiId)
        {
            var data = (from d in db.Aitisis where d.AitisisID == aitisiId select new { d.PhdAnagnorisiFilename }).FirstOrDefault();
            if (data != null)
                return data.PhdAnagnorisiFilename;
            return "";
        }


        #endregion


        #region ΔΙΑΧΕΙΡΙΣΗ ΑΙΤΗΣΕΩΝ ΧΡΗΣΤΗ

        public ActionResult AitisisList()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("TaxisNetLogin", "UserTeachers");
            }
            else
            {
                loggedTeacher = GetLoginTeacher();
            }

            int prokirixiId = c.GetOpenProkirixiID();
            if (prokirixiId == 0)
            {
                return RedirectToAction("Index", "Teachers", new { notify = "Δεν βρέθηκε Ανοικτή Προκήρυξη." });
            }
            // first, find the teacher if he/she exists
            var data = (from d in db.Teachers where d.AFM == loggedTeacher.UserAfm select d).FirstOrDefault();
            if (data == null)
            {
                string msg = "Δεν βρέθηκε εκπαιδευτικός με αυτό το ΑΦΜ. Καταχωρήστε πρώτα τα προσωπικά στοιχεία.";
                return RedirectToAction("Index", "Teachers", new { notify = msg });
            }
            populatePeriferies();
            populateEidikotites();
            populateSchools();

            return View();
        }

        public ActionResult Aitisis_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetAitisiGridModelFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Aitisis_Destroy([DataSourceRequest] DataSourceRequest request, AitisisGridViewModel model)
        {
            Aitisis entity = db.Aitisis.Find(model.AitisisID);
            if (entity != null)
            {
                if (k.CanDeleteAitisi(model.AitisisID))
                {
                    db.Entry(entity).State = EntityState.Deleted;
                    db.Aitisis.Remove(entity);
                    db.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("", "Για διαγραφή αίτησης πρέπει πρώτα να διαγραφούν οι σχολικές μονάδες της αίτησης.");
                }
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        public List<AitisisGridViewModel> GetAitisiGridModelFromDB()
        {
            loggedTeacher = GetLoginTeacher();
            prokirixiId = c.GetOpenProkirixiID();

            var data = (from d in db.Aitisis
                        where d.TeacherAFM == loggedTeacher.UserAfm && d.ProkirixisID == prokirixiId
                        select new AitisisGridViewModel
                        {
                            AitisisID = d.AitisisID,
                            AitisisProtocol = d.AitisisProtocol,
                            Eidikotita = d.Eidikotita,
                            Periferia = d.Periferia,
                            ProkirixisID = d.ProkirixisID
                        }).ToList();

            return data;
        }

        public ActionResult FilteredSchools_Read(int? aitisiID)
        {
            //Εύρεση ειδικότητας της αίτησης
            var eidikotitaAitisis = (from d in db.Aitisis where d.AitisisID == aitisiID select d.Eidikotita).FirstOrDefault();
            //Εύρεση ενεργής προκύρηξης
            var openProkirixi = (from p in db.Prokirixis where p.Status == 1 select p.ProkirixiID).FirstOrDefault();
            //Εύρεση προκυρησσόμενων σχολείων με την ειδικότητα της αίτησης
            var prokirixiSchools = (from d in db.ProkirixisEidikotites where d.ProkirixiID == openProkirixi && d.EidikotitaID == eidikotitaAitisis select d.SchoolID).ToList();
            //Εύρεση περιφέριας της αίτησης
            var periferiaAitisis = (from d in db.Aitisis where d.AitisisID == aitisiID select d.Periferia);
            // aitisiID = 0, return all schools
            var schools = (from s in db.SysSchools select s).ToList();
            //Εύρεση σχολείων στην περιφέρεια τα οποία έχει προκυρηχθεί η ειδικότητα της αίτησης
            var filteredSchools = (from d in db.SysSchools
                                   where periferiaAitisis.Contains(d.SchoolPeriferiaID) && prokirixiSchools.Contains(d.SchoolID)
                                   orderby d.SchoolName
                                   select new SchoolsViewModel
                                   {
                                       SchoolID = d.SchoolID,
                                       SchoolName = d.SchoolName,
                                       SchoolPeriferiaID = d.SchoolPeriferiaID
                                   }).ToList();

            return Json(filteredSchools, JsonRequestBehavior.AllowGet);
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

        #endregion


        #region ΕΝΣΤΑΣΕΙΣ ΥΠΟΨΗΦΙΩΝ

        #region ΠΛΕΓΜΑ ΕΝΣΤΑΣΕΩΝ (MASTER GRID)

        public ActionResult Enstaseis(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("TaxisNetLogin", "USER_TEACHERS");
            }
            else
            {
                loggedTeacher = GetLoginTeacher();
            }
            bool enstasi_allow = c.GetOpenProkirixiEnstasi();
            if (enstasi_allow == false)
            {
                string Msg = "Δεν μπορείτε να υποβάλλετε ένσταση διότι η υποβολή ενστάσεων είναι απενεργοποιημένη.";
                return RedirectToAction("Index", "Teachers", new { notify = Msg });
            }

            if (notify != null)
            {
                this.AddNotification(notify, NotificationType.WARNING);
            }
            if (!AitisisEnstasiExist())
            {
                string msg = "Δεν βρέθηκαν αιτήσεις για τις οποίες επιτρέπεται μεταφόρτωση ενστάσεων.";
                return RedirectToAction("Index", "Teachers", new { notify = msg });
            }
            populateSchoolYears();
            populateEnstasiAitisis();

            return View();
        }

        public ActionResult Enstasi_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetEnstaseisModelFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Enstasi_Create([DataSourceRequest] DataSourceRequest request, EnstaseisViewModel data)
        {
            loggedTeacher = GetLoginTeacher();
            prokirixiId = c.GetEnstasiProkirixiID();

            if (!(prokirixiId > 0))
                ModelState.AddModelError("", "Δεν βρέθηκε προκήρυξη ανοικτή για ενστάσεις.");

            var newdata = new EnstaseisViewModel();

            if (data != null && ModelState.IsValid)
            {
                Enstaseis entity = new Enstaseis()
                {
                    TeacherAFM = loggedTeacher.UserAfm,
                    ProkirixiID = prokirixiId,
                    SchoolID = db.Aitisis.Find(data.AitisiID).School,
                    AitisiID = data.AitisiID,
                    EnstasiDate = data.EnstasiDate,
                    EnstasiSummary = data.EnstasiSummary
                };
                db.Enstaseis.Add(entity);
                db.SaveChanges();
                data.EnstasiID = entity.EnstasiID;
                newdata = RefreshEnstaseisModelFromDB(data.EnstasiID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Enstasi_Update([DataSourceRequest] DataSourceRequest request, EnstaseisViewModel data)
        {
            loggedTeacher = GetLoginTeacher();
            prokirixiId = c.GetEnstasiProkirixiID();

            var newdata = new EnstaseisViewModel();

            if (data != null && ModelState.IsValid)
            {
                Enstaseis entity = db.Enstaseis.Find(data.EnstasiID);

                entity.TeacherAFM = loggedTeacher.UserAfm;
                entity.ProkirixiID = prokirixiId;
                entity.AitisiID = data.AitisiID;
                entity.SchoolID = db.Aitisis.Find(data.AitisiID).School;
                entity.EnstasiDate = data.EnstasiDate;
                entity.EnstasiSummary = data.EnstasiSummary;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                newdata = RefreshEnstaseisModelFromDB(entity.EnstasiID);
            }

            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Enstasi_Destroy([DataSourceRequest] DataSourceRequest request, EnstaseisViewModel data)
        {
            if (data != null)
            {
                Enstaseis entity = db.Enstaseis.Find(data.EnstasiID);
                if (entity != null)
                {
                    if (k.CanDeleteEnstasi(data.EnstasiID))
                    {
                        db.Entry(entity).State = EntityState.Deleted;
                        db.Enstaseis.Remove(entity);
                        db.SaveChanges();
                    }
                    else ModelState.AddModelError("", "Για να γίνει διαγραφή πρέπει πρώτα να διαγραφούν τα σχετικά ανεβασμένα αρχεία");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public EnstaseisViewModel RefreshEnstaseisModelFromDB(int recordId)
        {
            loggedTeacher = GetLoginTeacher();
            prokirixiId = c.GetEnstasiProkirixiID();

            var data = (from d in db.Enstaseis
                        where d.EnstasiID == recordId
                        select new EnstaseisViewModel
                        {
                            EnstasiID = d.EnstasiID,
                            AitisiID = d.AitisiID,
                            EnstasiDate = d.EnstasiDate,
                            EnstasiSummary = d.EnstasiSummary,
                            ProkirixiID = d.ProkirixiID,
                            SchoolID = d.SchoolID,
                            TeacherAFM = d.TeacherAFM
                        }).FirstOrDefault();

            return (data);
        }

        public List<EnstaseisViewModel> GetEnstaseisModelFromDB()
        {
            loggedTeacher = GetLoginTeacher();
            prokirixiId = c.GetEnstasiProkirixiID();

            var data = (from d in db.Enstaseis
                        where d.ProkirixiID == prokirixiId && d.TeacherAFM == loggedTeacher.UserAfm
                        orderby d.EnstasiDate descending
                        select new EnstaseisViewModel
                        {
                            EnstasiID = d.EnstasiID,
                            AitisiID = d.AitisiID,
                            EnstasiDate = d.EnstasiDate,
                            EnstasiSummary = d.EnstasiSummary,
                            ProkirixiID = d.ProkirixiID,
                            SchoolID = d.SchoolID,
                            TeacherAFM = d.TeacherAFM
                        }).ToList();

            return (data);
        }

        public bool AitisisEnstasiExist()
        {
            loggedTeacher = GetLoginTeacher();
            prokirixiId = c.GetEnstasiProkirixiID();

            var aitiseis = (from d in db.Aitisis where d.ProkirixisID == prokirixiId && d.TeacherAFM == loggedTeacher.UserAfm select d).Count();
            if (aitiseis > 0)
                return true;
            return false;
        }

        #endregion


        #region ΠΛΕΓΜΑ ΑΡΧΕΙΩΝ ΕΝΣΤΑΣΕΩΝ (CHILD GRID)

        public ActionResult EnstasiFile_Read([DataSourceRequest] DataSourceRequest request, int uploadId = 0)
        {
            var data = GetEnstaseisFilesFromDB(uploadId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EnstasiFile_Destroy([DataSourceRequest] DataSourceRequest request, EnstaseisFilesViewModel data)
        {
            if (data != null)
            {
                EnstaseisFiles entity = db.EnstaseisFiles.Find(data.UploadFileID);
                if (entity != null)
                {
                    // First delete the physical file and then the info record. Important!
                    DeleteEnstasiUploadedFile(data.UploadFileID);

                    db.Entry(entity).State = EntityState.Deleted;
                    db.EnstaseisFiles.Remove(entity);
                    db.SaveChanges();
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public List<EnstaseisFilesViewModel> GetEnstaseisFilesFromDB(int uploadId = 0)
        {
            var data = (from d in db.EnstaseisFiles
                        where d.EnstasiID == uploadId
                        orderby d.FileName
                        select new EnstaseisFilesViewModel
                        {
                            UploadFileID = d.UploadFileID,
                            EnstasiID = d.EnstasiID,
                            SchoolYearText = d.SchoolYearText,
                            FileName = d.FileName,
                            TeacherAFM = d.TeacherAFM
                        }).ToList();

            return (data);
        }

        public FileResult DownloadEnstasiFile(int file_id)
        {
            loggedTeacher = GetLoginTeacher();
            string physicalPath = ENSTASEIS_PATH + loggedTeacher.UserAfm + "/";
            string filename = "";

            var fileinfo = (from d in db.EnstaseisFiles where d.UploadFileID == file_id select d).FirstOrDefault();
            if (fileinfo != null)
            {
                filename = fileinfo.FileName;
            }

            return File(Path.Combine(Server.MapPath(physicalPath), filename), System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }

        public void DeleteEnstasiUploadedFile(int fileId)
        {
            loggedTeacher = GetLoginTeacher();
            string uploadPath = ENSTASEIS_PATH + loggedTeacher.UserAfm + "/";
            string filename = "";

            var data = (from d in db.EnstaseisFiles where d.UploadFileID == fileId select d).FirstOrDefault();
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

        #endregion


        #region ΦΟΡΜΑ ΜΕΤΑΦΟΡΤΩΣΗΣ ΕΝΣΤΑΣΕΩΝ

        public ActionResult EnstasiUpload(int uploadId, string notify = null)
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
            if (notify != null)
                this.AddNotification(notify, NotificationType.WARNING);

            if (!(uploadId > 0))
            {
                string msg = "Άκυρος κωδικός ένστασης. Πρέπει πρώτα να αποθηκεύσετε την εγγραφή ένστασης.";
                return RedirectToAction("ErrorData", "Aitisis", new { notify = msg });
            }
            ViewData["uploadId"] = uploadId;

            return View();
        }

        public ActionResult EnstasiFile_Upload(IEnumerable<HttpPostedFileBase> files, int uploadId = 0)
        {
            loggedTeacher = GetLoginTeacher();
            prokirixiId = c.GetOpenProkirixiID();

            int schoolYearId = (int)db.Prokirixis.Find(prokirixiId).SchoolYear;
            string schoolYearText = c.GetSchoolYearText(schoolYearId);
            string uploadPath = ENSTASEIS_PATH + loggedTeacher.UserAfm + "/";

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

                            EnstaseisFiles fileDetail = new EnstaseisFiles()
                            {
                                FileName = fileName,
                                SchoolYearText = schoolYearText,
                                EnstasiID = uploadId,
                                TeacherAFM = loggedTeacher.UserAfm
                            };
                            db.EnstaseisFiles.Add(fileDetail);
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

        public ActionResult EnstasiFile_Remove(string[] fileNames, int expId)
        {
            // The parameter of the Remove action must be called "fileNames"
            loggedTeacher = GetLoginTeacher();
            string uploadPath = ENSTASEIS_PATH + loggedTeacher.UserAfm + "/";

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
                        DeleteEnstasiFileRecord(fileName);
                    }
                }
            }
            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult DeleteEnstasiFileRecord(string filename)
        {
            EnstaseisFiles entity = db.EnstaseisFiles.Where(d => d.FileName == filename).FirstOrDefault();
            if (entity != null)
            {
                db.Entry(entity).State = EntityState.Deleted;
                db.EnstaseisFiles.Remove(entity);
                db.SaveChanges();
            }
            return Content("");
        }

        #endregion

        #endregion


        #region ΠΡΟΒΟΛΗ ΑΞΙΟΛΟΓΗΣΗΣ

        public ActionResult AitisisResults()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("TaxisNetLogin", "UserTeachers");
            }
            else
            {
                loggedTeacher = GetLoginTeacher();
            }

            int prokirixiId = c.GetOpenProkirixiID();
            if (prokirixiId == 0)
            {
                return RedirectToAction("Index", "Teachers", new { notify = "Δεν βρέθηκε Ανοικτή Προκήρυξη." });
            }
            // first, find the teacher if he/she exists
            ViewAllowed = c.GetOpenProkirixiUserView();
            if (ViewAllowed == false)
            {
                Msg = "Η προβολή αποτελεσμάτων αξιολόγησης είναι προσωρινά απενεργοποιημένη.";
                return RedirectToAction("Index", "Teachers", new { notify = Msg });
            }

            return View();
        }

        public ActionResult TeacherAitiseis_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetTeacherAitiseisFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<TeacherAitiseisViewModel> GetTeacherAitiseisFromDB()
        {
            loggedTeacher = GetLoginTeacher();

            var data = (from d in db.sqlTeacherAitiseis
                        where d.TeacherAFM == loggedTeacher.UserAfm
                        orderby d.ProkirixisID descending, d.AitisisProtocol ascending
                        select new TeacherAitiseisViewModel
                        {
                            AitisisID = d.AitisisID,
                            AitisisProtocol = d.AitisisProtocol,
                            EidikotitaText = d.EidikotitaText,
                            FullName = d.FullName,
                            OaedCheckStatus = d.OaedCheckStatus ?? false,
                            OaedEnstasi = d.OaedEnstasi ?? false,
                            PeriferiaID = d.PeriferiaID,
                            PeriferiakiID = d.PeriferiakiID,
                            PeriferiakiName = d.PeriferiakiName,
                            PeriferiaName = d.PeriferiaName,
                            ProkirixisID = d.ProkirixisID,
                            Protocol = d.Protocol,
                            SchoolID = d.SchoolID,
                            SchoolName = d.SchoolName,
                            TeacherAFM = d.TeacherAFM
                        }).ToList();

            return data;
        }


        public ActionResult AitisiView(int aitisiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
                return RedirectToAction("TaxisNetLogin", "UserTeachers");
            else
                loggedTeacher = GetLoginTeacher();

            AitisiDataViewModel model = GetAitisiDataFromDB(aitisiId);

            return View(model);
        }


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

        public ActionResult AitisisResultsPrint(int aitisiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
                return RedirectToAction("TaxisNetLogin", "UserTeachers");
            else
                loggedTeacher = GetLoginTeacher();

            AitisisParameters model = new AitisisParameters();
            model.AitisiID = aitisiId;

            return View(model);
        }


        #endregion


        #region GETTERS

        public JsonResult GetPtyxia()
        {
            var data = db.SysPtyxia.Select(p => new PtyxiaViewModel
            {
                PtyxioID = p.PtyxioID,
                PtyxioText = p.PtyxioText
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetKlados()
        {
            var data = db.SysKlados.Select(p => new KladosViewModel
            {
                KladosID = p.KladosID,
                KladosName = p.KladosName
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEidikotites(int kladosId)
        {
            var data = (from d in db.sqlEidikotitesSelector
                        where d.EidikotitaKladosID == kladosId
                        orderby d.EidikotitaDesc
                        select new EidikotitesSelectorViewModel
                        {
                            EidikotitaID = d.EidikotitaID,
                            EidikotitaDesc = d.EidikotitaDesc
                        }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBasicEducation()
        {
            var data = db.SysBasicEducation.Select(p => new BasicEducationViewModel
            {
                EducationID = p.EducationID,
                EducationText = p.EducationText
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSynafeia()
        {
            var data = db.SysSynafeia.Select(p => new SynafeiaViewModel
            {
                SynafeiaID = p.SynafeiaID,
                SynafeiaText = p.SynafeiaText
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEpagelmaCategory()
        {
            var data = db.EpagelmaCategory.Select(p => new EpagelmaCategoryViewModel
            {
                EpagelmaID = p.EpagelmaID,
                EpagelmaText = p.EpagelmaText
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadePeriferies(int? eidikotitaId)
        {
            var prokirixiSchools = (from p in db.ProkirixisEidikotites select p.SchoolID).ToList();
            if (eidikotitaId.HasValue)
            {
                prokirixiSchools = (from p in db.ProkirixisEidikotites where (p.EidikotitaID == eidikotitaId) select p.SchoolID).ToList();
            }
            var prokirixiSchoolsPeriferies = (from s in db.SysSchools where prokirixiSchools.Contains(s.SchoolID) select s.SchoolPeriferiaID).ToList();

            var periferiesWithSchools = (from p in db.SysPeriferies
                                         where prokirixiSchoolsPeriferies.Contains(p.PeriferiaID)
                                         select new PeriferiesViewModel
                                         {
                                             PeriferiaID = p.PeriferiaID,
                                             PeriferiaName = p.PeriferiaName
                                         });

            return Json(periferiesWithSchools, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeSchools(int? periferia, int? eidikotita)
        {
            //Εύρεση ανοικτής προκύρηξης
            var openProkirixi = (from p in db.Prokirixis where p.Status == 1 select p.ProkirixiID).FirstOrDefault();

            //Εύρεση προκυρησόμενων σχολείων με την ειδικότητα της αίτησης
            var prokirixiSchools = (from p in db.ProkirixisEidikotites
                                    where p.ProkirixiID == openProkirixi && p.EidikotitaID == eidikotita
                                    select p.SchoolID).ToList();

            var schools = db.sqlPeriferiesSchools.AsQueryable()
                            .Where(s => s.SchoolPeriferiaID == periferia && prokirixiSchools.Contains(s.SchoolID))
                            .GroupBy(p => p.SchoolID).Select(grp => grp.FirstOrDefault());

            if (periferia != null)
            {
                schools = schools.Where(p => p.SchoolPeriferiaID == periferia).GroupBy(p => p.SchoolID).Select(grp => grp.FirstOrDefault());
            }

            return Json(schools.Select(p => new { SchoolID = p.SchoolID, SchoolName = p.SchoolName }), JsonRequestBehavior.AllowGet);
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


        #region POPULATORS

        public void populateSchoolYears()
        {
            var syears = (from s in db.SysSchoolYears
                          orderby s.SchoolYearText descending
                          select s).ToList();

            ViewData["school_years"] = syears;
            ViewData["defaultSchoolYear"] = syears.First().SchoolYearID;
        }

        public void populateEidikotites()
        {
            var eidikotites = (from e in db.SysEidikotites
                               orderby e.EidikotitaKladosID, e.EidikotitaCode, e.EidikotitaName
                               select new EidikotitesViewModel
                               {
                                   EidikotitaID = e.EidikotitaID,
                                   EidikotitaCode = e.EidikotitaCode,
                                   EidikotitaName = e.EidikotitaName,
                                   EidikotitaKladosID = e.EidikotitaKladosID
                               }).ToList();
            ViewData["eidikotites"] = eidikotites;
        }

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

        public void populateSchools(int aitisiID = 0)
        {
            //Εύρεση ειδικότητας της αίτησης
            var eidikotitaAitisis = (from d in db.Aitisis where d.AitisisID == aitisiID select d.Eidikotita).FirstOrDefault();
            //Εύρεση ενεργής προκύρηξης
            var openProkirixi = (from p in db.Prokirixis where p.Status == 1 select p.ProkirixiID).FirstOrDefault();
            //Εύρεση προκυρησσόμενων σχολείων με την ειδικότητα της αίτησης
            var prokirixiSchools = (from d in db.ProkirixisEidikotites where d.ProkirixiID == openProkirixi && d.EidikotitaID == eidikotitaAitisis select d.SchoolID).ToList();
            //Εύρεση περιφέριας της αίτησης
            var periferiaAitisis = (from d in db.Aitisis where d.AitisisID == aitisiID select d.Periferia);
            // aitisiID = 0, return all schools
            var schools = (from s in db.SysSchools select s).ToList();
            //Εύρεση σχολείων στην περιφέρεια τα οποία έχει προκυρηχθεί η ειδικότητα της αίτησης
            var filteredSchools = (from d in db.SysSchools
                                   where periferiaAitisis.Contains(d.SchoolPeriferiaID) && prokirixiSchools.Contains(d.SchoolID)
                                   orderby d.SchoolName
                                   select new SchoolsViewModel
                                   {
                                       SchoolID = d.SchoolID,
                                       SchoolName = d.SchoolName,
                                       SchoolPeriferiaID = d.SchoolPeriferiaID
                                   }).ToList();

            if (filteredSchools.Count > 0)
            {
                ViewData["schools"] = filteredSchools;
                ViewData["defaultSchool"] = filteredSchools.First().SchoolID;
            }
            else
            {
                ViewData["schools"] = schools;
                ViewData["defaultSchool"] = schools.First().SchoolID;
            }
        }

        public void populatePeriferies()
        {
            var data = (from d in db.SysPeriferies
                        orderby d.PeriferiaName
                        select new PeriferiesViewModel
                        {
                            PeriferiaID = d.PeriferiaID,
                            PeriferiaName = d.PeriferiaName,
                        }).ToList();

            ViewData["periferies"] = data;
            ViewData["defaultPeriferia"] = data.First().PeriferiaID;
        }

        public void populateEnstasiAitisis()
        {
            int prokirixiId = c.GetEnstasiProkirixiID();
            loggedTeacher = GetLoginTeacher();

            var aitiseis = (from d in db.Aitisis where d.ProkirixisID == prokirixiId && d.TeacherAFM == loggedTeacher.UserAfm select d).ToList();

            ViewData["aitiseis"] = aitiseis;
            ViewData["defaultAitisi"] = aitiseis.First().AitisisID;
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