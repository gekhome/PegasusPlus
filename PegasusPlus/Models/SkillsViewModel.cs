using PegasusPlus.BPM;
using PegasusPlus.DAL;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace PegasusPlus.Models
{
    public class TeacherSkillsViewModel
    {
        public int SkillsID { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string TeacherAFM { get; set; }

        [Display(Name = "Είδος 2ου πτυχίου")]
        public int? Ptyxio2Type { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Τίτλος")]
        public string Ptyxio2Titlos { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Εκπαιδευτικό ίδρυμα")]
        public string Ptyxio2Institution { get; set; }

        [Range(1950, 2022, ErrorMessage = "Εισάγετε έγκυρο αριθμό από 1950 έως 2022")]
        [Display(Name = "Έτος")]
        public int? Ptyxio2Year { get; set; }

        [Display(Name = "Εξωτερικού")]
        public bool Ptyxio2Foreign { get; set; }

        [Display(Name = "Αναγνώριση ΔΟΑΤΑΠ-ΑΤΕΕΝ/ΣΑΕΠ")]
        public bool Ptyxio2Anagnorisi { get; set; }

        [Display(Name = "Αρχείο πτυχίου")]
        public string Ptyxio2Filename { get; set; }

        [MaxFileSize(900 * 1024, ErrorMessage = "Μέγιστο επιτρεπόμενο μέγεθος αρχείου {0} bytes")]
        [Display(Name = "Επιλογή αρχείου για το πτυχίο")]
        public HttpPostedFileBase FilePtyxio2 { get; set; }

        [Display(Name = "Αρχείο αναγνώρισης")]
        public string Ptyxio2AnagnorisiFilename { get; set; }

        [MaxFileSize(900 * 1024, ErrorMessage = "Μέγιστο επιτρεπόμενο μέγεθος αρχείου {0} bytes")]
        [Display(Name = "Επιλογή αρχείου για την αναγνώριση")]
        public HttpPostedFileBase FileAnagnorisi { get; set; }

        [Display(Name = "1η Ξένη Γλώσσα")]
        public int? Language1 { get; set; }

        [Display(Name = "Επίπεδο γνώσης")]
        public int? Language1Level { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Τίτλος")]
        public string Language1Titlos { get; set; }

        [Display(Name = "Αρχείο πιστοποιητικού")]
        public string Language1Filename { get; set; }

        [MaxFileSize(900 * 1024, ErrorMessage = "Μέγιστο επιτρεπόμενο μέγεθος αρχείου {0} bytes")]
        [Display(Name = "Επιλογή αρχείου για τη 1η Γλώσσα")]
        public HttpPostedFileBase FileLanguage1 { get; set; }

        [Display(Name = "2η Ξένη Γλώσσα")]
        public int? Language2 { get; set; }

        [Display(Name = "Επίπεδο γνώσης")]
        public int? Language2Level { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Τίτλος")]
        public string Language2Titlos { get; set; }

        [Display(Name = "Αρχείο πιστοποιητικού")]
        public string Language2Filename { get; set; }

        [MaxFileSize(900 * 1024, ErrorMessage = "Μέγιστο επιτρεπόμενο μέγεθος αρχείου {0} bytes")]
        [Display(Name = "Επιλογή αρχείου για τη 2η Γλώσσα")]
        public HttpPostedFileBase FileLanguage2 { get; set; }

        [Display(Name = "Πιστοποιητικό κατά ΑΣΕΠ")]
        public int? ComputerCertificate { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Τίτλος πιστοποιητικού")]
        public string ComputerTitlos { get; set; }

        [Display(Name = "Έτος")]
        public int? ComputerYear { get; set; }

        [Display(Name = "Αρχείο πιστοποιητικού")]
        public string ComputerFilename { get; set; }

        [MaxFileSize(900 * 1024, ErrorMessage = "Μέγιστο επιτρεπόμενο μέγεθος αρχείου {0} bytes")]
        [Display(Name = "Επιλογή αρχείου για γνώση Η/Υ")]
        public HttpPostedFileBase FileComputer { get; set; }

        [Display(Name = "Επιμόρφωση στο διδακτικό αντικείμενο της θέσης")]
        public bool Epimorfosi { get; set; }

        [Display(Name = "Σύνολο ωρών")]
        public int? EpimorfosiTotalHours { get; set; }

        [Display(Name = "Πιστοποιημένος εκπαιδευτής ενηλίκων")]
        public bool CertifiedTrainer { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Αριθμός Μητρώου ΕΟΠΠΕΠ")]
        public string CertifiedTrainerAM { get; set; }

        [Display(Name = "Αρχείο πιστοποιητικού")]
        public string CertifiedTrainerFilename { get; set; }

        [MaxFileSize(900 * 1024, ErrorMessage = "Μέγιστο επιτρεπόμενο μέγεθος αρχείου {0} bytes")]
        [Display(Name = "Επιλογή αρχείου για πιστοποίηση")]
        public HttpPostedFileBase FileCertified { get; set; }

        [Display(Name = "Προστατευόμενος του Ν.2190")]
        public bool N2190Protect { get; set; }

        [Display(Name = "Αρχείο βεβαίωσης Ν.2190")]
        public string N2190ProtectFilename { get; set; }

        [MaxFileSize(900 * 1024, ErrorMessage = "Μέγιστο επιτρεπόμενο μέγεθος αρχείου {0} bytes")]
        [Display(Name = "Επιλογή αρχείου για βεβαίωση N.2190")]
        public HttpPostedFileBase FileN2190 { get; set; }

        [Display(Name = "Έγκυρο 2ο πτυχίο")]
        public bool OaedPtyxio2Confirm { get; set; }

        [Display(Name = "Έγκυρη 1η Γλώσσα")]
        public bool OaedLanguage1Confirm { get; set; }

        [Display(Name = "Έγκυρη 2η Γλώσσα")]
        public bool OaedLanguage2Confirm { get; set; }

        [Display(Name = "Έγκυρη γνώση Η/Υ")]
        public bool OaedComputerConfirm { get; set; }

        [Display(Name = "Έγκυρη επιμόρφωση")]
        public bool OaedEpimorfosiConfirm { get; set; }

        [Display(Name = "Έγκυρη πιστοποίηση ΕΟΠΠΕΠ")]
        public bool OaedCertifiedConfirm { get; set; }

        [Display(Name = "Έγκυρος προστατευόμενος Ν.2190")]
        public bool OaedN2190Confirm { get; set; }

        public virtual Teachers Teachers { get; set; }
    }

}