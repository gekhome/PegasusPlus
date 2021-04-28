using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using PegasusPlus.DAL;
using PegasusPlus.BPM;

namespace PegasusPlus.Models
{ 
    public class TeacherViewModel
    {
        [Display(Name = "ΑΦΜ")]
        public string AFM { get; set; }

        [Display(Name = "Αρχείο για ΑΦΜ")]
        public string AFM_FILENAME { get; set; }

        [MaxFileSize(900 * 1024, ErrorMessage = "Μέγιστο επιτρεπόμενο μέγεθος αρχείου {0} bytes")]
        [Display(Name = "Επιλογή αρχείου για ΑΦΜ *")]
        public HttpPostedFileBase FileAFM { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[ΆΈΊΉΌΎΏΑ-Ω]+[ ΆΈΊΉΌΎΏΑ-Ω.'ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Δ.Ο.Υ. *")]
        public string DOY { get; set; }

        [Display(Name = "Α.Δ.Τ. *")]
        public string ADT { get; set; }

        [Display(Name = "Αρχείο για ΑΔΤ")]
        public string ADT_FILENAME { get; set; }

        [MaxFileSize(900 * 1024, ErrorMessage = "Μέγιστο επιτρεπόμενο μέγεθος αρχείου {0} bytes")]
        [Display(Name = "Επιλογή αρχείου για ΑΔΤ *")]
        public HttpPostedFileBase FileADT { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(150, ErrorMessage = "Πρέπει να είναι μέχρι 150 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Όνομα *")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(150, ErrorMessage = "Πρέπει να είναι μέχρι 150 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Επώνυμο *")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(150, ErrorMessage = "Πρέπει να είναι μέχρι 150 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Πατρώνυμο *")]
        public string FatherName { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(150, ErrorMessage = "Πρέπει να είναι μέχρι 150 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Μητρώνυμο *")]
        public string MotherName { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Φύλο *")]
        public int? Gender { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Οικογενειακή κατάσταση *")]
        public int? FamilyStatus { get; set; }

        [Range(0, 20, ErrorMessage = "Εισάγετε έγκυρο αριθμό 0 έως 20")]
        [Display(Name = "Αριθμός τέκνων")]
        public int? Children { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία γέννησης *")]
        public DateTime? Birthdate { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Νομός κατοικίας *")]
        public int? Nomos { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Πόλη κατοικίας *")]
        public string City { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ταχυδρομική διεύθυνση *")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ταχυδρομικός κώδικας *")]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σταθερό τηλέφωνο *")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Κινητό τηλέφωνο *")]
        public string CellPhone { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage ="Δώστε έγκυρη διεύθυνση e-mail")]
        [StringLength(150, ErrorMessage = "Πρέπει να είναι μέχρι 150 χαρακτήρες.")]
        [EmailAddress]
        [Display(Name = "E-Mail *")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Επάγγελμα *")]
        public string Epagelma { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ιδιότητα *")]
        public int? Idiotita { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Α.Μ.Κ.Α. *")]
        public string AMKA { get; set; }

        [Display(Name = "Αρχείο για ΑΜΚΑ")]
        public string AMKA_FILENAME { get; set; }

        [MaxFileSize(900 * 1024, ErrorMessage = "Μέγιστο επιτρεπόμενο μέγεθος αρχείου {0} bytes")]
        [Display(Name = "Επιλογή αρχείου για ΑΜΚΑ *")]
        public HttpPostedFileBase FileAMKA { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Αριθμός μητρώου ασφάλισης *")]
        public string AMA { get; set; }

        [Display(Name = "Αρχείο για ΑΜΑ")]
        public string AMA_FILENAME { get; set; }

        [MaxFileSize(900 * 1024, ErrorMessage = "Μέγιστο επιτρεπόμενο μέγεθος αρχείου {0} bytes")]
        [Display(Name = "Επιλογή αρχείου για ΑΜΑ *")]
        public HttpPostedFileBase FileAMA { get; set; }

        [StringLength(150, ErrorMessage = "Πρέπει να είναι μέχρι 150 χαρακτήρες.")]
        [Display(Name = "Ασφάλιση κύριας θέσης *")]
        public string InsuranceMain { get; set; }

        [Range(1940, 2030, ErrorMessage = "Εισάγετε έγκυρο αριθμό 1940 έως 2030")]
        [Display(Name = "Έτος πρώτης ασφάλισης")]
        public int? InsuranceFirstYear { get; set; }

        [Display(Name = "Γονέας τρίτεκνης οικογένειας")]
        public bool SocialTriteknos { get; set; }

        [Display(Name = "Μέλος πολύτεκνης οικογένειας")]
        public bool SocialPolyteknos { get; set; }

        [Display(Name = "Μέλος μονογονεϊκής οικογένειας")]
        public bool SocialSingleParent { get; set; }

        [Display(Name = "ΑΜΕΑ (ο ίδιος ή τέκνα αυτού)")]
        public bool SocialAmea { get; set; }

        [Display(Name = "Άνεργος - Διάρκεια")]
        public int? SocialAnergos { get; set; }

        [Display(Name = "Ημερομηνία λήξης της κάρτας")]
        public DateTime? AnergiaCardExpireDate { get; set; }

        [Display(Name = "Αρχείο για τρίτεκνη")]
        public string SocialTriteknosFilename { get; set; }

        [MaxFileSize(900 * 1024, ErrorMessage = "Μέγιστο επιτρεπόμενο μέγεθος αρχείου {0} bytes")]
        [Display(Name = "Επιλογή αρχείου για τρίτεκνη")]
        public HttpPostedFileBase FileTriteknos { get; set; }

        [Display(Name = "Αρχείο για πολύτεκνη")]
        public string SocialPolyteknosFilename { get; set; }

        [MaxFileSize(900 * 1024, ErrorMessage = "Μέγιστο επιτρεπόμενο μέγεθος αρχείου {0} bytes")]
        [Display(Name = "Επιλογή αρχείου για πολύτεκνη")]
        public HttpPostedFileBase FilePolyteknos { get; set; }

        [Display(Name = "Αρχείο για μονογονεϊκή")]
        public string SocialSingleParentFilename { get; set; }

        [MaxFileSize(900 * 1024, ErrorMessage = "Μέγιστο επιτρεπόμενο μέγεθος αρχείου {0} bytes")]
        [Display(Name = "Επιλογή αρχείου για μονογονεϊκή")]
        public HttpPostedFileBase FileSingleParent { get; set; }

        [Display(Name = "Αρχείο για ΑΜΕΑ")]
        public string SocialAmeaFilename { get; set; }

        [MaxFileSize(900 * 1024, ErrorMessage = "Μέγιστο επιτρεπόμενο μέγεθος αρχείου {0} bytes")]
        [Display(Name = "Επιλογή αρχείου για ΑΜΕΑ")]
        public HttpPostedFileBase FileAMEA { get; set; }

        [Display(Name = "Αρχείο για ανεργία")]
        public string SocialAnergosFilename { get; set; }

        [MaxFileSize(900 * 1024, ErrorMessage = "Μέγιστο επιτρεπόμενο μέγεθος αρχείου {0} bytes")]
        [Display(Name = "Επιλογή αρχείου για ανεργία")]
        public HttpPostedFileBase FileAnergia { get; set; }

        public TeacherViewModel() { }

        public TeacherViewModel(Teachers entity)
        {
            AFM = entity.AFM;
            AFM_FILENAME = entity.AFM_FILENAME;
            DOY = entity.DOY;
            ADT = entity.ADT;
            ADT_FILENAME = entity.ADT_FILENAME;
            FirstName = entity.FirstName;
            LastName = entity.LastName;
            FatherName = entity.FatherName;
            MotherName = entity.MotherName;
            Gender = entity.Gender;
            FamilyStatus = entity.FamilyStatus;
            Children = entity.Children;
            Birthdate = entity.Birthdate;
            Nomos = entity.Nomos;
            City = entity.City;
            Address = entity.Address;
            PostCode = entity.PostCode;
            Telephone = entity.Telephone;
            CellPhone = entity.CellPhone;
            Email = entity.Email;
            Epagelma = entity.Epagelma;
            Idiotita = entity.Idiotita;
            AMKA = entity.AMKA;
            AMKA_FILENAME = entity.AMKA_FILENAME;
            AMA = entity.AMA;
            AMA_FILENAME = entity.AMA_FILENAME;
            InsuranceMain = entity.InsuranceMain;
            InsuranceFirstYear = entity.InsuranceFirstYear;
            SocialTriteknos = entity.SocialTriteknos ?? false;
            SocialPolyteknos = entity.SocialPolyteknos ?? false;
            SocialSingleParent = entity.SocialSingleParent ?? false;
            SocialAmea = entity.SocialAmea ?? false;
            SocialAnergos = entity.SocialAnergos;
            AnergiaCardExpireDate = entity.AnergiaCardExpireDate;
            SocialTriteknosFilename = entity.SocialTriteknosFilename;
            SocialPolyteknosFilename = entity.SocialPolyteknosFilename;
            SocialSingleParentFilename = entity.SocialSingleParentFilename;
            SocialAmeaFilename = entity.SocialAmeaFilename;
            SocialAnergosFilename = entity.SocialAnergosFilename;
        }

    }

    public class TeacherUploadViewModel
    {
        public int UploadFileID { get; set; }

        [Display(Name = "ΑΦΜ Εκπαιδευτή")]
        public string TeacherAFM { get; set; }

        [Display(Name = "Όνομα αρχείου")]
        public string FileName { get; set; }

        [Display(Name = "Επέκταση")]
        public string FilePath { get; set; }

        [Display(Name = "Περιγραφή αρχείου")]
        public string Description { get; set; }

        [Display(Name = "Σχολικό έτος")]
        public string SchoolYearText { get; set; }
    }

}