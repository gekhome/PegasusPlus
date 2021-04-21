using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using PegasusPlus.DAL;

namespace PegasusPlus.Models
{
    public class EidikotitesViewModel
    {
        public int EidikotitaID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Κωδικός")]
        public string EidikotitaCode { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(150, ErrorMessage = "Πρέπει να είναι μέχρι 150 χαρακτήρες.")]
        [Display(Name = "Ειδικότητα")]
        public string EidikotitaName { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ειδικότητα ενιαία")]
        public string EidikotitaUnified { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Κλάδος ενοποίησης")]
        public int? KladosUnified { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Κλάδος")]
        public int? EidikotitaKladosID { get; set; }

        [Display(Name = "Ομάδα")]
        public int? EidikotitaGroupID { get; set; }

        public virtual SysKlados SysKlados { get; set; }
        public virtual SysKladosEniaios SysKladosEniaios { get; set; }
    }

    public class EidikotitesGroupsViewModel
    {
        public int GroupID { get; set; }

        [Display(Name = "Ομάδα")]
        public string GroupText { get; set; }

        [Display(Name = "Κλάδος")]
        public int? Klados { get; set; }
    }

    public class KladosViewModel
    {
        public int KladosID { get; set; }

        [Display(Name = "Κλάδος")]
        public string KladosName { get; set; }

        [Display(Name = "Κατηγορία κλάδου")]
        public string KladosCategory { get; set; }

        public virtual ICollection<SysEidikotites> SysEidikotites { get; set; }
    }

    public partial class KladosEniaiosViewModel
    {
        public int KladosEniaiosID { get; set; }

        [Display(Name = "Κλάδος ενοποίησης")]
        public string KladosEniaiosText { get; set; }

        [Display(Name = "Κλάδος")]
        public int? Klados { get; set; }

        public virtual ICollection<SysEidikotites> SysEidikotites { get; set; }
    }

    public class EidikotitesSelectorViewModel
    {
        [Display(Name = "Ειδικότητα")]
        public int EidikotitaID { get; set; }

        [Display(Name = "Κλάδος-Ειδικότητα")]
        public string EidikotitaDesc { get; set; }

        [Display(Name = "Κλάδος")]
        public int? EidikotitaKladosID { get; set; }

        [Display(Name = "Ομάδα")]
        public int? EidikotitaGroupID { get; set; }
    }

    public class EidikotititesKladosEniaiosViewModel
    {
        public int EidikotitaID { get; set; }

        [Display(Name = "Κλάδος-Πτυχίο")]
        public string EidikotitaPtyxio { get; set; }

        [Display(Name = "Κλάδος Ενιαίος")]
        public int? KladosUnified { get; set; }

        [Display(Name = "Κλάδος")]
        public int? EidikotitaKladosID { get; set; }
    }

}