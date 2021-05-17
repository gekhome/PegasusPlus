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
    public class ExperienceController : Controller
    {
        private PegasusPlusDBEntities db = new PegasusPlusDBEntities();
        private string Msg;
        private int prokirixiId;
        private const string UPLOAD_PATH = "~/Uploads/FilesExperience/";

        private const string TEACHING_TEXT = "ΔΙΔΑΚΤΙΚΗ ΕΜΠΕΙΡΊΑ";
        private const string VOCATION_TEXT = "ΕΠΑΓΓΕΛΜΑΤΙΚΗ ΕΜΠΕΙΡΙΑ";
        private const string FREELANCE_TEXT = "ΕΛΕΥΘΕΡΟ ΕΠΑΓΓΕΛΜΑ";

        Common c = new Common();
        Kerberos k = new Kerberos();

        private UserTeachers loggedTeacher;
        private Teachers loggedTeacherData;


        #region TEACHING GRID AND UPLOADED FILES

        #region TEACHING GRID (MASTER GRID)

        public ActionResult TeachingMain()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
                return RedirectToAction("TaxisNetLogin", "UserTeachers");
            else
                loggedTeacher = GetLoginTeacher();

            prokirixiId = c.GetOpenProkirixiID();
            if (prokirixiId == 0)
            {
                return RedirectToAction("Index", "Teachers", new { notify = "Δεν βρέθηκε Ανοικτή Προκήρυξη." });
            }
            // first, find the teacher if he/she exists
            var data = (from d in db.Teachers where d.AFM == loggedTeacher.UserAfm select d).FirstOrDefault();
            if (data == null)
            {
                Msg = "Δεν βρέθηκε εκπαιδευτικός με αυτό το ΑΦΜ. Καταχωρήστε πρώτα τα προσωπικά στοιχεία.";
                return RedirectToAction("Index", "Teachers", new { notify = Msg });
            }
            var aitiseis = (from d in db.Aitisis where d.TeacherAFM == loggedTeacher.UserAfm && d.ProkirixisID == prokirixiId select d).Count();
            if (aitiseis == 0)
            {
                Msg = "Δεν βρέθηκαν καταχωρημένες αιτήσεις. Πρέπει πρώτα να καταχωρήσετε τουλάχιστον μια αίτηση.";
                return RedirectToAction("Index", "Teachers", new { notify = Msg });
            }

            populateSchoolYears();
            populateTeachTypes();

            return View();
        }

        public ActionResult Teaching_Read([DataSourceRequest] DataSourceRequest request, int aitisiId = 0)
        {
            var data = GetTeachingModelFromDB(aitisiId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Teaching_Create([DataSourceRequest] DataSourceRequest request, WorkTeachingViewModel data, int aitisiId = 0)
        {
            loggedTeacher = GetLoginTeacher();

            var newdata = new WorkTeachingViewModel();

            if (aitisiId > 0)
            {
                if (data != null && ModelState.IsValid)
                {
                    WorkTeaching entity = new WorkTeaching()
                    {
                        AitisiID = aitisiId,
                        ProkirixiID = c.GetOpenProkirixiID(),
                        SchoolYear = data.SchoolYear,
                        TeachType = data.TeachType,
                        DateStart = data.DateStart,
                        DateFinal = data.DateFinal,
                        HoursTotal = data.HoursTotal,
                        HoursWeek = data.HoursWeek,
                        Moria = k.MoriaTeaching(data),
                        DocumentProtocol = data.DocumentProtocol,
                        DocumentForeas = data.DocumentForeas,
                        Valid = true,
                        TeacherAFM = loggedTeacher.UserAfm,
                        SchoolYearText = c.GetSchoolYearText((int)data.SchoolYear)
                    };
                    db.WorkTeaching.Add(entity);
                    db.SaveChanges();
                    data.ExperienceID = entity.ExperienceID;
                    newdata = RefreshTeachingFromDB(data.ExperienceID);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να γίνει επιλογή αίτησης. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Teaching_Update([DataSourceRequest] DataSourceRequest request, WorkTeachingViewModel data, int aitisiId = 0)
        {
            loggedTeacher = GetLoginTeacher();
            int schoolYearId = (int)c.GetOpenProkirixi().SchoolYear;

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
                    entity.Valid = true;
                    entity.TeacherAFM = loggedTeacher.UserAfm;
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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Teaching_Destroy([DataSourceRequest] DataSourceRequest request, WorkTeachingViewModel data)
        {
            if (data != null)
            {
                WorkTeaching entity = db.WorkTeaching.Find(data.ExperienceID);
                if (entity != null)
                {
                    if (k.CanDeleteTeaching(data.ExperienceID))
                    {
                        db.Entry(entity).State = EntityState.Deleted;
                        db.WorkTeaching.Remove(entity);
                        db.SaveChanges();
                    }
                    else
                        ModelState.AddModelError("", "Δεν μπορεί να γίνει διαγραφή της διδακτικής διότι έχει συσχετισμένα ανεβασμένα αρχεία.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
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

        public List<WorkTeachingViewModel> GetTeachingModelFromDB(int aitisiId = 0)
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


        #region UPLOADED FILES GRID (CHILD GRID)

        public ActionResult UploadTeachingFiles_Read([DataSourceRequest] DataSourceRequest request, int expID = 0)
        {
            var data = GetUploadsTeachingFromDB(expID);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UploadTeachingFiles_Destroy([DataSourceRequest] DataSourceRequest request, UploadsTeachingViewModel data)
        {
            if (data != null)
            {
                UploadsTeaching entity = db.UploadsTeaching.Find(data.UploadFileID);
                if (entity != null)
                {
                    // First delete the physical file and then the info record. Important!
                    DeleteTeachingUploadedFile(data.UploadFileID);

                    db.Entry(entity).State = EntityState.Deleted;
                    db.UploadsTeaching.Remove(entity);
                    db.SaveChanges();
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public List<UploadsTeachingViewModel> GetUploadsTeachingFromDB(int expID = 0)
        {
            var data = (from d in db.UploadsTeaching
                        where d.ExperienceID == expID
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

        public void DeleteTeachingUploadedFile(int fileId)
        {
            loggedTeacher = GetLoginTeacher();
            string uploadPath = UPLOAD_PATH + loggedTeacher.UserAfm + "/";
            string filename = "";

            var data = (from d in db.UploadsTeaching where d.UploadFileID == fileId select d).FirstOrDefault();
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

        public FileResult DownloadTeachingFile(int file_id)
        {
            loggedTeacher = GetLoginTeacher();
            string physicalPath = UPLOAD_PATH + loggedTeacher.UserAfm + "/";
            string filename = "";

            var fileinfo = (from d in db.UploadsTeaching where d.UploadFileID == file_id select d).FirstOrDefault();
            if (fileinfo != null)
            {
                filename = fileinfo.FileName;
            }

            return File(Path.Combine(Server.MapPath(physicalPath), filename), System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }

        #endregion


        #region UPLOAD FORM

        public ActionResult UploadTeaching(int uploadId, string notify = null)
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
                    return RedirectToAction("Error", "Experience", new { notify = "Δεν βρέθηκε εξουσιοδοτημένος χρήστης για το αίτημα." });
            }
            if (notify != null)
                this.AddNotification(notify, NotificationType.WARNING);

            if (!(uploadId > 0))
            {
                string msg = "Άκυρος κωδικός διδακτικής. Πρέπει πρώτα να αποθηκεύσετε την εγγραφή διδακτικής και μετά να ανεβάσετε αρχείο.";
                return RedirectToAction("ErrorData", "Experience", new { notify = msg });
            }
            ViewData["experienceId"] = uploadId;

            return View();
        }

        public ActionResult TeachingFile_Upload(IEnumerable<HttpPostedFileBase> files, int expId = 0)
        {
            loggedTeacher = GetLoginTeacher();
            string uploadPath = UPLOAD_PATH + loggedTeacher.UserAfm + "/";

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

                            UploadsTeaching fileDetail = new UploadsTeaching()
                            {
                                FileName = fileName,
                                TeacherAFM = loggedTeacher.UserAfm,
                                Description = "ΒΕΒΑΙΩΣΗ ΔΙΔΑΚΤΙΚΗΣ (" + loggedTeacher.UserAfm + ")",
                                Category = TEACHING_TEXT,
                                ExperienceID = expId
                            };
                            db.UploadsTeaching.Add(fileDetail);
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

        public ActionResult TeachingFile_Remove(string[] fileNames, int expId)
        {
            // The parameter of the Remove action must be called "fileNames"
            loggedTeacher = GetLoginTeacher();
            string uploadPath = UPLOAD_PATH + loggedTeacher.UserAfm + "/";

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
                        DeleteTeachingUploadFileRecord(fileName);
                    }
                }
            }
            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult DeleteTeachingUploadFileRecord(string filename)
        {
            UploadsTeaching entity = db.UploadsTeaching.Where(d => d.FileName == filename).FirstOrDefault();
            if (entity != null)
            {
                db.Entry(entity).State = EntityState.Deleted;
                db.UploadsTeaching.Remove(entity);
                db.SaveChanges();
            }
            return Content("");
        }

        #endregion

        #endregion


        #region VOCATION GRID AND UPLOADED FILES

        #region VOCATION GRID (MASTER GRID)

        public ActionResult VocationMain()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
                return RedirectToAction("TaxisNetLogin", "UserTeachers");
            else
                loggedTeacher = GetLoginTeacher();

            prokirixiId = c.GetOpenProkirixiID();
            if (prokirixiId == 0)
            {
                return RedirectToAction("Index", "Teachers", new { notify = "Δεν βρέθηκε Ανοικτή Προκήρυξη." });
            }
            // first, find the teacher if he/she exists
            var data = (from d in db.Teachers where d.AFM == loggedTeacher.UserAfm select d).FirstOrDefault();
            if (data == null)
            {
                Msg = "Δεν βρέθηκε εκπαιδευτικός με αυτό το ΑΦΜ. Καταχωρήστε πρώτα τα προσωπικά στοιχεία.";
                return RedirectToAction("Index", "Teachers", new { notify = Msg });
            }
            var aitiseis = (from d in db.Aitisis where d.TeacherAFM == loggedTeacher.UserAfm && d.ProkirixisID == prokirixiId select d).Count();
            if (aitiseis == 0)
            {
                Msg = "Δεν βρέθηκαν καταχωρημένες αιτήσεις. Πρέπει πρώτα να καταχωρήσετε τουλάχιστον μια αίτηση.";
                return RedirectToAction("Index", "Teachers", new { notify = Msg });
            }

            return View();
        }

        public ActionResult Vocation_Read([DataSourceRequest] DataSourceRequest request, int aitisiId = 0)
        {
            var data = GetVocationModelFromDB(aitisiId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Vocation_Create([DataSourceRequest] DataSourceRequest request, WorkVocationViewModel data, int aitisiId = 0)
        {
            loggedTeacher = GetLoginTeacher();
            var newdata = new WorkVocationViewModel();

            if (aitisiId > 0)
            {
                if (data != null && ModelState.IsValid)
                {
                    WorkVocation entity = new WorkVocation()
                    {
                        AitisiID = aitisiId,
                        ProkirixiID = c.GetOpenProkirixiID(),
                        DateStart = data.DateStart,
                        DateFinal = data.DateFinal,
                        DaysAuto = k.SetDaysAutoVocation(data),
                        DaysManual = data.DaysManual,
                        Moria = k.MoriaVocation(data),
                        DocumentProtocol = data.DocumentProtocol,
                        DocumentForeas = data.DocumentForeas,
                        Position = data.Position,
                        Subject = data.Subject,
                        Valid = true,
                        TeacherAFM = loggedTeacher.UserAfm
                    };
                    db.WorkVocation.Add(entity);
                    db.SaveChanges();
                    data.ExperienceID = entity.ExperienceID;
                    newdata = RefreshVocationModelFromDB(data.ExperienceID);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να γίνει επιλογή αίτησης. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Vocation_Update([DataSourceRequest] DataSourceRequest request, WorkVocationViewModel data, int aitisiId = 0)
        {
            loggedTeacher = GetLoginTeacher();
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
                    entity.Valid = true;
                    entity.TeacherAFM = loggedTeacher.UserAfm;

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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Vocation_Destroy([DataSourceRequest] DataSourceRequest request, WorkVocationViewModel data)
        {
            if (data != null)
            {
                WorkVocation entity = db.WorkVocation.Find(data.ExperienceID);
                if (entity != null)
                {
                    if (k.CanDeleteVocation(data.ExperienceID))
                    {
                        db.Entry(entity).State = EntityState.Deleted;
                        db.WorkVocation.Remove(entity);
                        db.SaveChanges();
                    }
                    else
                        ModelState.AddModelError("", "Δεν μπορεί να διαγραφεί αυτή η επαγγελματική διότι έχει συσχετισμένα ανεβασμένα αρχεία");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
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

        public List<WorkVocationViewModel> GetVocationModelFromDB(int aitisiId = 0)
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


        #region UPLOADED FILES GRID (CHILD GRID)

        public ActionResult UploadVocationFiles_Read([DataSourceRequest] DataSourceRequest request, int expID = 0)
        {
            var data = GetUploadsVocationFromDB(expID);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UploadVocationFiles_Destroy([DataSourceRequest] DataSourceRequest request, UploadsVocationViewModel data)
        {
            if (data != null)
            {
                UploadsVocation entity = db.UploadsVocation.Find(data.UploadFileID);
                if (entity != null)
                {
                    // First delete the physical file and then the info record. Important!
                    DeleteVocationUploadedFile(data.UploadFileID);

                    db.Entry(entity).State = EntityState.Deleted;
                    db.UploadsVocation.Remove(entity);
                    db.SaveChanges();
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public List<UploadsVocationViewModel> GetUploadsVocationFromDB(int expID = 0)
        {
            var data = (from d in db.UploadsVocation
                        where d.ExperienceID == expID
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

        public void DeleteVocationUploadedFile(int fileId)
        {
            loggedTeacher = GetLoginTeacher();
            string uploadPath = UPLOAD_PATH + loggedTeacher.UserAfm + "/";
            string filename = "";

            var data = (from d in db.UploadsVocation where d.UploadFileID == fileId select d).FirstOrDefault();
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

        public FileResult DownloadVocationFile(int file_id)
        {
            loggedTeacher = GetLoginTeacher();
            string physicalPath = UPLOAD_PATH + loggedTeacher.UserAfm + "/";
            string filename = "";

            var fileinfo = (from d in db.UploadsVocation where d.UploadFileID == file_id select d).FirstOrDefault();
            if (fileinfo != null)
            {
                filename = fileinfo.FileName;
            }

            return File(Path.Combine(Server.MapPath(physicalPath), filename), System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }

        #endregion


        #region UPLOAD FORM

        public ActionResult UploadVocation(int uploadId, string notify = null)
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
                    return RedirectToAction("Error", "Experience", new { notify = "Δεν βρέθηκε εξουσιοδοτημένος χρήστης για το αίτημα." });
            }
            if (notify != null)
                this.AddNotification(notify, NotificationType.WARNING);

            if (!(uploadId > 0))
            {
                string msg = "Άκυρος κωδικός επαγγελματικής. Πρέπει πρώτα να αποθηκεύσετε την εγγραφή επαγγελματικής και μετά να ανεβάσετε αρχείο.";
                return RedirectToAction("ErrorData", "Experience", new { notify = msg });
            }
            ViewData["experienceId"] = uploadId;

            return View();
        }

        public ActionResult VocationFile_Upload(IEnumerable<HttpPostedFileBase> files, int expId = 0)
        {
            loggedTeacher = GetLoginTeacher();
            string uploadPath = UPLOAD_PATH + loggedTeacher.UserAfm + "/";

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

                            UploadsVocation fileDetail = new UploadsVocation()
                            {
                                FileName = fileName,
                                TeacherAFM = loggedTeacher.UserAfm,
                                Description = "ΒΕΒΑΙΩΣΗ ΕΠΑΓΓΕΛΜΑΤΙΚΗΣ (" + loggedTeacher.UserAfm + ")",
                                Category = VOCATION_TEXT,
                                ExperienceID = expId
                            };
                            db.UploadsVocation.Add(fileDetail);
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

        public ActionResult VocationFile_Remove(string[] fileNames, int expId)
        {
            // The parameter of the Remove action must be called "fileNames"
            loggedTeacher = GetLoginTeacher();
            string uploadPath = UPLOAD_PATH + loggedTeacher.UserAfm + "/";

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
                        DeleteVocationUploadFileRecord(fileName);
                    }
                }
            }
            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult DeleteVocationUploadFileRecord(string filename)
        {
            UploadsVocation entity = db.UploadsVocation.Where(d => d.FileName == filename).FirstOrDefault();
            if (entity != null)
            {
                db.Entry(entity).State = EntityState.Deleted;
                db.UploadsVocation.Remove(entity);
                db.SaveChanges();
            }
            return Content("");
        }

        #endregion

        #endregion


        #region FREELANCE GRID AND UPLOADED FILES

        #region FREELANCE GRID (MASTER GRID)

        public ActionResult FreelanceMain()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
                return RedirectToAction("TaxisNetLogin", "UserTeachers");
            else
                loggedTeacher = GetLoginTeacher();

            prokirixiId = c.GetOpenProkirixiID();
            if (prokirixiId == 0)
            {
                return RedirectToAction("Index", "Teachers", new { notify = "Δεν βρέθηκε Ανοικτή Προκήρυξη." });
            }
            // first, find the teacher if he/she exists
            var data = (from d in db.Teachers where d.AFM == loggedTeacher.UserAfm select d).FirstOrDefault();
            if (data == null)
            {
                Msg = "Δεν βρέθηκε εκπαιδευτικός με αυτό το ΑΦΜ. Καταχωρήστε πρώτα τα προσωπικά στοιχεία.";
                return RedirectToAction("Index", "Teachers", new { notify = Msg });
            }
            var aitiseis = (from d in db.Aitisis where d.TeacherAFM == loggedTeacher.UserAfm && d.ProkirixisID == prokirixiId select d).Count();
            if (aitiseis == 0)
            {
                Msg = "Δεν βρέθηκαν καταχωρημένες αιτήσεις. Πρέπει πρώτα να καταχωρήσετε τουλάχιστον μια αίτηση.";
                return RedirectToAction("Index", "Teachers", new { notify = Msg });
            }
            populateIncomeYears();
 
            return View();
        }

        public ActionResult Freelance_Read([DataSourceRequest] DataSourceRequest request, int aitisiId = 0)
        {
            var data = GetFreelanceModelFromDB(aitisiId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Freelance_Create([DataSourceRequest] DataSourceRequest request, WorkFreelanceViewModel data, int aitisiId = 0)
        {
            loggedTeacher = GetLoginTeacher();
            var newdata = new WorkFreelanceViewModel();
            float _taxfree = 0;
            string _nomisma = "";

            if (aitisiId > 0)
            {
                if (data != null && ModelState.IsValid)
                {
                    _taxfree = setIncomeTaxfree((int)data.IncomeYear);
                    _nomisma = setIncomeNomisma((int)data.IncomeYear);

                    WorkFreelance entity = new WorkFreelance()
                    {
                        AitisiID = aitisiId,
                        ProkirixiID = c.GetOpenProkirixiID(),
                        DateStart = data.DateStart,
                        DateFinal = data.DateFinal,
                        DaysAuto = k.SetDaysAutoFreelance(data),
                        DaysManual = data.DaysManual,
                        Moria = k.MoriaFreelance(data),
                        Income = data.Income,
                        IncomeYear = data.IncomeYear,
                        IncomeTaxFree = _taxfree,
                        IncomeCurrency = _nomisma,
                        WorkEvidence = data.WorkEvidence,
                        Subject = data.Subject,
                        Valid = true,
                        TeacherAFM = loggedTeacher.UserAfm
                    };
                    db.WorkFreelance.Add(entity);
                    db.SaveChanges();
                    data.ExperienceID = entity.ExperienceID;
                    newdata = RefreshFreelanceModelFromDB(data.ExperienceID);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να γίνει επιλογή αίτησης. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Freelance_Update([DataSourceRequest] DataSourceRequest request, WorkFreelanceViewModel data, int aitisiId = 0)
        {
            loggedTeacher = GetLoginTeacher();
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
                    entity.Valid = true;
                    entity.TeacherAFM = loggedTeacher.UserAfm;

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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Freelance_Destroy([DataSourceRequest] DataSourceRequest request, WorkFreelanceViewModel data)
        {
            if (data != null)
            {
                WorkFreelance entity = db.WorkFreelance.Find(data.ExperienceID);
                if (entity != null)
                {
                    if (k.CanDeleteFreelance(data.ExperienceID))
                    {
                        db.Entry(entity).State = EntityState.Deleted;
                        db.WorkFreelance.Remove(entity);
                        db.SaveChanges();
                    }
                    else
                        ModelState.AddModelError("", "Δεν μπορεί να γίνει διαγραφή της εγγραφής αυτής διότι έχει συσχετισμένα ανεβασμένα αρχεία.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
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

        public List<WorkFreelanceViewModel> GetFreelanceModelFromDB(int aitisiId = 0)
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


        #region UPLOADED FILES GRID (CHILD GRID)

        public ActionResult UploadFreelanceFiles_Read([DataSourceRequest] DataSourceRequest request, int expID = 0)
        {
            var data = GetUploadsFreelanceFromDB(expID);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UploadFreelanceFiles_Destroy([DataSourceRequest] DataSourceRequest request, UploadsFreelanceViewModel data)
        {
            if (data != null)
            {
                UploadsFreelance entity = db.UploadsFreelance.Find(data.UploadFileID);
                if (entity != null)
                {
                    // First delete the physical file and then the info record. Important!
                    DeleteFreelanceUploadedFile(data.UploadFileID);

                    db.Entry(entity).State = EntityState.Deleted;
                    db.UploadsFreelance.Remove(entity);
                    db.SaveChanges();
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public List<UploadsFreelanceViewModel> GetUploadsFreelanceFromDB(int expID = 0)
        {
            var data = (from d in db.UploadsFreelance
                        where d.ExperienceID == expID
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

        public void DeleteFreelanceUploadedFile(int fileId)
        {
            loggedTeacher = GetLoginTeacher();
            string uploadPath = UPLOAD_PATH + loggedTeacher.UserAfm + "/";
            string filename = "";

            var data = (from d in db.UploadsFreelance where d.UploadFileID == fileId select d).FirstOrDefault();
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

        public FileResult DownloadFreelanceFile(int file_id)
        {
            loggedTeacher = GetLoginTeacher();
            string physicalPath = UPLOAD_PATH + loggedTeacher.UserAfm + "/";
            string filename = "";

            var fileinfo = (from d in db.UploadsFreelance where d.UploadFileID == file_id select d).FirstOrDefault();
            if (fileinfo != null)
            {
                filename = fileinfo.FileName;
            }

            return File(Path.Combine(Server.MapPath(physicalPath), filename), System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }

        #endregion


        #region UPLOAD FORM

        public ActionResult UploadFreelance(int uploadId, string notify = null)
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
                    return RedirectToAction("Error", "Experience", new { notify = "Δεν βρέθηκε εξουσιοδοτημένος χρήστης για το αίτημα." });
            }
            if (notify != null)
                this.AddNotification(notify, NotificationType.WARNING);

            if (!(uploadId > 0))
            {
                string msg = "Άκυρος κωδικός επιτηδεύματος. Πρέπει πρώτα να αποθηκεύσετε την εγγραφή επιτηδεύματος και μετά να ανεβάσετε αρχείο.";
                return RedirectToAction("ErrorData", "Experience", new { notify = msg });
            }
            ViewData["experienceId"] = uploadId;

            return View();
        }

        public ActionResult FreelanceFile_Upload(IEnumerable<HttpPostedFileBase> files, int expId = 0)
        {
            loggedTeacher = GetLoginTeacher();
            string uploadPath = UPLOAD_PATH + loggedTeacher.UserAfm + "/";

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

                            UploadsFreelance fileDetail = new UploadsFreelance()
                            {
                                FileName = fileName,
                                TeacherAFM = loggedTeacher.UserAfm,
                                Description = "ΒΕΒΑΙΩΣΗ ΕΠΙΤΗΔΕΥΜΑΤΟΣ (" + loggedTeacher.UserAfm + ")",
                                Category = FREELANCE_TEXT,
                                ExperienceID = expId
                            };
                            db.UploadsFreelance.Add(fileDetail);
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

        public ActionResult FreelanceFile_Remove(string[] fileNames, int expId)
        {
            // The parameter of the Remove action must be called "fileNames"
            loggedTeacher = GetLoginTeacher();
            string uploadPath = UPLOAD_PATH + loggedTeacher.UserAfm + "/";

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
                        DeleteFreelanceUploadFileRecord(fileName);
                    }
                }
            }
            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult DeleteFreelanceUploadFileRecord(string filename)
        {
            UploadsFreelance entity = db.UploadsFreelance.Where(d => d.FileName == filename).FirstOrDefault();
            if (entity != null)
            {
                db.Entry(entity).State = EntityState.Deleted;
                db.UploadsFreelance.Remove(entity);
                db.SaveChanges();
            }
            return Content("");
        }

        #endregion

        #endregion


        #region POPULATORS

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

        #endregion


        #region GETTERS

        public JsonResult GetAitisiSelector()
        {
            loggedTeacher = GetLoginTeacher();
            prokirixiId = c.GetOpenProkirixiID();

            var data = (from d in db.sqlAitisiSelector
                        where d.TeacherAFM == loggedTeacher.UserAfm && d.ProkirixisID == prokirixiId
                        select new AitisiSelectorViewModel
                        {
                            AitisisID = d.AitisisID,
                            AitisisInfo = d.AitisisInfo
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTeachTypes()
        {
            var data = db.SysTeachTypes.Select(p => new TeachTypesViewModel
            {
                TypeID = p.TypeID,
                TypeText = p.TypeText
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSchoolYears(string text)
        {
            var data = db.SysSchoolYears.Select(p => new SchoolYearsViewModel
            {
                SchoolYearID = p.SchoolYearID,
                SchoolYearText = p.SchoolYearText
            }).OrderBy(d => d.SchoolYearText);

            return Json(data, JsonRequestBehavior.AllowGet);
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