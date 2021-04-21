using PegasusPlus.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PegasusPlus.Models
{
    public class ProkirixisViewModel
    {
        public int ProkirixiID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Πρωτόκολλο")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        public string Protocol { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Φ.Ε.Κ.")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        public string Fek { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. Έτος")]
        public int? SchoolYear { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ημ. Έναρξης")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? DateStart { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ημ. Λήξης")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? DateEnd { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διοικητής")]
        [StringLength(100, ErrorMessage = "Πρέπει να είναι μέχρι 100 χαρακτήρες.")]
        public string Dioikitis { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Καθεστώς")]
        public Nullable<short> Status { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ενεργή")]
        public bool Active { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχείρ.")]
        public bool Admin { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Προβολή")]
        public bool UserView { get; set; }

        [Display(Name = "Ενστάσεις")]
        public bool Enstaseis { get; set; }
    }

    public class ProkirixisEidikotitesViewModel
    {
        public int PSE_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Προκήρυξη")]
        public int ProkirixiID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική Μονάδα")]
        public int SchoolID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Κλάδος-Ειδικότητα")]
        public int EidikotitaID { get; set; }

        public virtual SysEidikotites SysEidikotites { get; set; }
    }

}