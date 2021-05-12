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
    public class WorkTeachingViewModel
    {
        public int ExperienceID { get; set; }
        public int? AitisiID { get; set; }
        public int? ProkirixiID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Κατηγορία")]
        public int? TeachType { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public int? SchoolYear { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία από")]
        public DateTime? DateStart { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία έως")]
        public DateTime? DateFinal { get; set; }

        [Display(Name = "Ωρες/εβδ")]
        public int? HoursWeek { get; set; }

        [Display(Name = "Διάρκεια ώρες")]
        public int? HoursTotal { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Μόρια")]
        public decimal? Moria { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Α.Π. Βεβαίωσης")]
        public string DocumentProtocol { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω']+[ Α-Ω-_ΪΫ']*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Φορέας")]
        public string DocumentForeas { get; set; }

        [Display(Name = "Έγκυρη")]
        public bool Valid { get; set; }
    }

    public class WorkVocationViewModel
    {
        public int ExperienceID { get; set; }
        public int? AitisiID { get; set; }
        public int? ProkirixiID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία από")]
        public DateTime? DateStart { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία έως")]
        public DateTime? DateFinal { get; set; }

        [Display(Name = "Ημέρες(Α)")]
        public int? DaysAuto { get; set; }

        [Display(Name = "Ημέρες")]
        public int? DaysManual { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Μόρια")]
        public decimal? Moria { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Θέση")]
        public string Position { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Αντικείμενο")]
        public string Subject { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Α.Π. Βεβαίωσης")]
        public string DocumentProtocol { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Εργοδότης/Φορέας")]
        public string DocumentForeas { get; set; }

        [Display(Name = "Έγκυρη")]
        public bool Valid { get; set; }
    }

    public class WorkFreelanceViewModel
    {
        public int ExperienceID { get; set; }
        public int? AitisiID { get; set; }
        public int? ProkirixiID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση ημερομηνίας")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία από")]
        public DateTime? DateStart { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση ημερομηνίας")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία έως")]
        public DateTime? DateFinal { get; set; }

        [Display(Name = "Ημέρες(Α)")]
        public int? DaysAuto { get; set; }

        [Display(Name = "Ημέρες")]
        public int? DaysManual { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση εισοδήματος")]
        [Display(Name = "Εισόδημα")]
        public float? Income { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση έτους")]
        [Display(Name = "Έτος Εισοδήματος")]
        public int? IncomeYear { get; set; }

        [Display(Name = "Αφορολόγητο")]
        public float? IncomeTaxFree { get; set; }

        [Display(Name = "Νόμισμα")]
        public string IncomeCurrency { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Μόρια")]
        public decimal? Moria { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Αντικείμενο")]
        public string Subject { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Βεβαίωση")]
        public string WorkEvidence { get; set; }

        [Display(Name = "Έγκυρη")]
        public bool Valid { get; set; }
    }

    #region UPLOADAD FILES

    public class UploadsTeachingViewModel
    {
        public int UploadFileID { get; set; }
        public int? ExperienceID { get; set; }

        [Display(Name = "Ονομα αρχείου")]
        public string FileName { get; set; }

        [Display(Name = "Διαδρομή")]
        public string FilePath { get; set; }

        [Display(Name = "Κατηγορία")]
        public string Category { get; set; }

        [Display(Name = "Περιγραφή")]
        public string Description { get; set; }
    }

    public class UploadsVocationViewModel
    {
        public int UploadFileID { get; set; }
        public int? ExperienceID { get; set; }

        [Display(Name = "Ονομα αρχείου")]
        public string FileName { get; set; }

        [Display(Name = "Διαδρομή")]
        public string FilePath { get; set; }

        [Display(Name = "Κατηγορία")]
        public string Category { get; set; }

        [Display(Name = "Περιγραφή")]
        public string Description { get; set; }
    }

    public class UploadsFreelanceViewModel
    {
        public int UploadFileID { get; set; }
        public int? ExperienceID { get; set; }

        [Display(Name = "Ονομα αρχείου")]
        public string FileName { get; set; }

        [Display(Name = "Διαδρομή")]
        public string FilePath { get; set; }

        [Display(Name = "Κατηγορία")]
        public string Category { get; set; }

        [Display(Name = "Περιγραφή")]
        public string Description { get; set; }
    }

    #endregion
}