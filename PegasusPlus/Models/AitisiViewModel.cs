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
    public class AitisisViewModel
    {
        public int AitisisID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public DateTime? AitisisDate { get; set; }

        [Display(Name = "Αρ. Πρωτοκόλλου")]
        public string AitisisProtocol { get; set; }

        [Display(Name = "Προκήρυξη")]
        public int? ProkirixisID { get; set; }

        [Display(Name = "Α.Φ.Μ.")]
        public string TeacherAFM { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση κλάδου")]
        [Display(Name = "Κλάδος *")]
        public int? Klados { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση ειδικότητας")]
        [Display(Name = "Ειδικότητα *")]
        public int? Eidikotita { get; set; }

        [Display(Name = "Ομάδα ειδικότητας")]
        public int? EidikotitaGroup { get; set; }

        [Display(Name = "Βασική εκπαίδευση (για εμπειροτέχνες)")]
        public int? BasicEducation { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση είδους πτυχίου")]
        [Display(Name = "Είδος πτυχίου *")]
        public int? PtyxioType { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Τίτλος *")]
        public string PtyxioTitlos { get; set; }

        [Range(1950, 2022, ErrorMessage = "Εισάγετε έγκυρο αριθμό από 1950 έως 2022")]
        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση έτους πτυχίου")]
        [Display(Name = "Έτος *")]
        public int? PtyxioYear { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση βαθμού πτυχίου")]
        [Display(Name = "Βαθμός *")]
        public decimal? PtyxioGrade { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση εκπαιδευτικού ιδρύματος")]
        [Display(Name = "Εκπαιδευτικό ίδρυμα *")]
        public string PtyxioInstitution { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση σχολής")]
        [Display(Name = "Σχολή *")]
        public string PtyxioSchool { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση τμήματος")]
        [Display(Name = "Τμήμα *")]
        public string PtyxioTmima { get; set; }

        [Display(Name = "Εξωτερικού")]
        public bool PtyxioForeign { get; set; }

        [Display(Name = "Αναγνώριση ΔΟΑΤΑΠ-ΑΤΕΕΝ/ΣΑΕΠ")]
        public bool PtyxioAnagnorisi { get; set; }

        [Display(Name = "Αρχείο πτυχίου")]
        public string PtyxioFilename { get; set; }

        [MaxFileSize(900 * 1024, ErrorMessage = "Μέγιστο επιτρεπόμενο μέγεθος αρχείου {0} bytes")]
        [Display(Name = "Επιλογή αρχείου για το πτυχίο")]
        public HttpPostedFileBase FilePtyxio { get; set; }

        [Display(Name = "Αρχείο αναγνώρισης")]
        public string PtyxioAnagnorisiFilename { get; set; }

        [MaxFileSize(900 * 1024, ErrorMessage = "Μέγιστο επιτρεπόμενο μέγεθος αρχείου {0} bytes")]
        [Display(Name = "Επιλογή αρχείου για την αναγνώριση")]
        public HttpPostedFileBase FilePtyxioAnagnorisi { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Τίτλος")]
        public string MscTitlos { get; set; }

        [Display(Name = "Έτος")]
        public int? MscYear { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Εκπαιδευτικό ίδρυμα")]
        public string MscInstitution { get; set; }

        [Display(Name = "Συνάφεια")]
        public int? MscSynafeia { get; set; }

        [Display(Name = "Εξωτερικού")]
        public bool MscForeign { get; set; }

        [Display(Name = "Αναγνώριση ΔΟΑΤΑΠ-ΑΤΕΕΝ/ΣΑΕΠ")]
        public bool MscAnagnorisi { get; set; }

        [Display(Name = "Αρχείο μεταπτυχιακού")]
        public string MscFilename { get; set; }

        [MaxFileSize(900 * 1024, ErrorMessage = "Μέγιστο επιτρεπόμενο μέγεθος αρχείου {0} bytes")]
        [Display(Name = "Επιλογή αρχείου για το μεταπτυχιακό")]
        public HttpPostedFileBase FileMsc { get; set; }

        [Display(Name = "Αρχείο αναγνώρισης")]
        public string MscAnagnorisiFilename { get; set; }

        [MaxFileSize(900 * 1024, ErrorMessage = "Μέγιστο επιτρεπόμενο μέγεθος αρχείου {0} bytes")]
        [Display(Name = "Επιλογή αρχείου για την αναγνώριση")]
        public HttpPostedFileBase FileMscAnagnorisi { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Τίτλος")]
        public string PhdTitlos { get; set; }

        [Display(Name = "Έτος")]
        public int? PhdYear { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Εκπαιδευτικό ίδρυμα")]
        public string PhdInstitution { get; set; }

        [Display(Name = "Συνάφεια")]
        public int? PhdSynafeia { get; set; }

        [Display(Name = "Εξωτερικού")]
        public bool PhdForeign { get; set; }

        [Display(Name = "Αναγνώριση ΔΟΑΤΑΠ-ΑΤΕΕΝ/ΣΑΕΠ")]
        public bool PhdAnagnorisi { get; set; }

        [Display(Name = "Αρχείο διδακτορικού")]
        public string PhdFilename { get; set; }

        [MaxFileSize(900 * 1024, ErrorMessage = "Μέγιστο επιτρεπόμενο μέγεθος αρχείου {0} bytes")]
        [Display(Name = "Επιλογή αρχείου για το διδακτορικό")]
        public HttpPostedFileBase FilePhd { get; set; }

        [Display(Name = "Αρχείο αναγνώρισης")]
        public string PhdAnagnorisiFilename { get; set; }

        [MaxFileSize(900 * 1024, ErrorMessage = "Μέγιστο επιτρεπόμενο μέγεθος αρχείου {0} bytes")]
        [Display(Name = "Επιλογή αρχείου για την αναγνώριση")]
        public HttpPostedFileBase FilePhdAnagnorisi { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση ιδιότητας")]
        [Display(Name = "Επαγγελματική Ιδιότητα *")]
        public int? EpagelmaCategory { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση περιφέρειας")]
        [Display(Name = "Περιφέρεια *")]
        public int? Periferia { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση σχολικής μονάδας")]
        [Display(Name = "Σχολείο 1ης επιλογής *")]
        public int? School { get; set; }

        [Display(Name = "Μόρια πτυχίου")]
        public decimal MoriaPtyxio { get; set; }

        [Display(Name = "Μόρια μεταπτυχιακού")]
        public decimal MoriaMsc { get; set; }

        [Display(Name = "Μόρια διδακτορικού")]
        public decimal MoriaPhd { get; set; }

        [Display(Name = "Μόρια ξένης γλώσσας")]
        public decimal MoriaLanguage { get; set; }

        [Display(Name = "Μόρια γνώσης Η/Υ")]
        public decimal MoriaComputer { get; set; }

        [Display(Name = "Μόρια επιμόρφωσης")]
        public decimal MoriaEpimorfosi { get; set; }

        [Display(Name = "Μόρια πιστοποίησης")]
        public decimal MoriaCertified { get; set; }

        [Display(Name = "Μόρια διδακτικής εμπειρίας")]
        public decimal MoriaTeach { get; set; }

        [Display(Name = "Μόρια επαγγελματικής")]
        public decimal MoriaWork { get; set; }

        [Display(Name = "Μόρια ανεργίας")]
        public decimal MoriaAnergia { get; set; }

        [Display(Name = "Μόρια κοινωνικά")]
        public decimal MoriaSocial { get; set; }

        [Display(Name = "Μόρια σύνολο")]
        public decimal MoriaTotal { get; set; }

        [Display(Name = "Έγκυρο πτυχίο")]
        public bool OaedPtyxioConfirm { get; set; }

        [Display(Name = "Έγκυρο μεταπτυχιακό")]
        public bool OaedMscConfirm { get; set; }

        [Display(Name = "Έγκυρο διδακτορικό")]
        public bool OaedPhdConfirm { get; set; }

        [Display(Name = "Ημερομηνία μοριοδότησης")]
        public DateTime? OaedMoriodotisiDate { get; set; }

        [Display(Name = "Αποκλεισμός")]
        public bool OaedApoklismos { get; set; }

        [Display(Name = "Αιτία αποκλεισμού")]
        public int? OaedApoklismosAitia { get; set; }

        [Display(Name = "Κείμενο Α'/βάθμιας Επιτροπής")]
        public string OaedEpitropi1Text { get; set; }

        [Display(Name = "Κείμενο Β'/βάθμιας Επιτροπής")]
        public string OaedEpitropi2Text { get; set; }

        [Display(Name = "Έγινε έλεγχος")]
        public bool OaedCheckStatus { get; set; }

        [Display(Name = "Έγινε ένσταση")]
        public bool OaedEnstasi { get; set; }

        public virtual Teachers Teachers { get; set; }

    }
    
    public class AitisisGridViewModel
    {
        public int AitisisID { get; set; }

        [Display(Name = "Αρ. Πρωτοκόλλου")]
        public string AitisisProtocol { get; set; }

        public int? ProkirixisID { get; set; }

        [Display(Name = "Ειδικότητα")]
        public int? Eidikotita { get; set; }

        [Display(Name = "Περιφέρεια")]
        public int? Periferia { get; set; }

    }

    public class AitisiSchoolsViewModel
    {
        public int RowID { get; set; }

        public int AitisiID { get; set; }

        [Display(Name = "Εκπαιδευτική Μονάδα")]
        public int SchoolID { get; set; }

        [Display(Name = "Περιφέρεια")]
        public int PeriferiaID { get; set; }
    }

    public class AitisiSelectorViewModel
    {
        public int AitisisID { get; set; }

        public int? ProkirixisID { get; set; }

        public string TeacherAFM { get; set; }

        public string AitisisInfo { get; set; }
    }

    public class TeacherAitiseisViewModel
    {
        public int AitisisID { get; set; }

        [Display(Name = "Α.Π. Αίτησης")]
        public string AitisisProtocol { get; set; }

        public int? ProkirixisID { get; set; }

        [Display(Name = "Προκήρυξη")]
        public string Protocol { get; set; }

        [Display(Name = "Α.Φ.Μ.")]
        public string TeacherAFM { get; set; }

        [Display(Name = "Εκπαιδευτικός")]
        public string FullName { get; set; }

        [Display(Name = "Κλάδος-Ειδικότητα")]
        public string EidikotitaText { get; set; }

        public int SchoolID { get; set; }

        [Display(Name = "Σχολική Μονάδα")]
        public string SchoolName { get; set; }

        public int PeriferiaID { get; set; }

        [Display(Name = "Περιφέρεια")]
        public string PeriferiaName { get; set; }

        public int PeriferiakiID { get; set; }

        [Display(Name = "Περιφερειακή")]
        public string PeriferiakiName { get; set; }

        [Display(Name = "Έλεγχος")]
        public bool OaedCheckStatus { get; set; }

        [Display(Name = "ΈΝσταση")]
        public bool OaedEnstasi { get; set; }
    }


}