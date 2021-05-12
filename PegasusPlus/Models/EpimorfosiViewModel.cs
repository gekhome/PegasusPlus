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
    public class EpimorfosiViewModel
    {
        public int EpimorfosiID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Τίτλος")]
        public string EpimorfosiTitlos { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Φορέας")]
        public string EpimorfosiForeas { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ωρες")]
        public int? EpimorfosiHours { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία από")]
        public DateTime? EpimorfosiDateStart { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία έως")]
        public DateTime? EpimorfosiDateFinal { get; set; }

        [Display(Name = "Αντικείμενο")]
        public int? EpimorfosiType { get; set; }

        public string TeacherAfm { get; set; }

        public int? ProkirixiID { get; set; }


    }

    public class EpimorfosiFileViewModel
    {
        public int UploadFileID { get; set; }

        [Display(Name = "Ονομασία αρχείου")]
        public string Filename { get; set; }

        [Display(Name = "Περιγραφή")]
        public string Description { get; set; }

        [Display(Name = "Διαδρομή")]
        public string Filepath { get; set; }

        public int? EpimorfosiID { get; set; }

        public virtual Epimorfosis Epimorfosis { get; set; }
    }

}