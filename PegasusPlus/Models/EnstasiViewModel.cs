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
    public class EnstaseisViewModel
    {
        public int EnstasiID { get; set; }
        public int? ProkirixiID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Αίτηση")]
        public int? AitisiID { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string TeacherAFM { get; set; }

        [Display(Name = "Σχολική Μονάδα")]
        public int? SchoolID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public DateTime? EnstasiDate { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Σύνοψη ένστασης")]
        public string EnstasiSummary { get; set; }
    }

    public class EnstaseisFilesViewModel
    {
        public int UploadFileID { get; set; }

        [Display(Name = "Όνομα αρχείου")]
        public string FileName { get; set; }

        [Display(Name = "Διαδρομή")]
        public string FilePath { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public string SchoolYearText { get; set; }

        public int? EnstasiID { get; set; }

        public virtual Enstaseis Enstaseis { get; set; }
    }

}