using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using PegasusPlus.DAL;

namespace PegasusPlus.Models
{
    public class TaxisnetViewModel
    {
        public int TAXISNET_ID { get; set; }

        public int? RANDOM_NUMBER { get; set; }

        public string TAXISNET_AFM { get; set; }
    }

    public class GendersViewModel
    {
        public int GenderID { get; set; }
        public string GenderText { get; set; }
    }

    public class SchoolYearsViewModel
    {
        public int SchoolYearID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(9, ErrorMessage = "Πρέπει να είναι μέχρι 9 χαρακτήρες (π.χ.2015-2016).")]
        [Display(Name = "Σχολ. Έτος")]

        public string SchoolYearText { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ημερομηνία Έναρξης")]
        [DataType(DataType.Date)]
        public DateTime? DateStart { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ημερομηνία Λήξης")]
        [DataType(DataType.Date)]
        public DateTime? DateEnd { get; set; }
    }

    public class TaxFreeViewModel
    {
        public int YearID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(4, ErrorMessage = "Πρέπει να είναι μέχρι 4 χαρακτήρες (π.χ. 2015).")]
        [Display(Name = "Έτος Εισοδήματος")]
        public string YearText { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Αφορολόγητο Εισόδημα")]
        public float? TaxFree { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "Νόμισμα")]
        public string Nomisma { get; set; }
    }

    public class SchoolsViewModel
    {
        [Display(Name = "Κωδ. Σχολείου")]
        public int SchoolID { get; set; }

        [Display(Name = "Σχολική Μονάδα")]
        public string SchoolName { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "E-Mail")]
        public string SchoolEmail { get; set; }

        [Display(Name = "Περιφερειακή Διεύθυνση")]
        public int? SchoolPeriferiakiID { get; set; }

        [Display(Name = "Περιφερειακή Ενότητα")]
        public Nullable<int> SchoolPeriferiaID { get; set; }
    }

    public class PeriferiakesViewModel
    {
        public int PeriferiakiID { get; set; }

        [Display(Name = "Περιφερειακή Διεύθυνση")]
        public string PeriferiakiName { get; set; }

        [Display(Name = "Περιφερειακή συντομογραφία")]
        public string PeriferiakiShortName { get; set; }
    }

    public class PeriferiesViewModel
    {
        [Display(Name = "Κωδικός")]
        public int PeriferiaID { get; set; }

        [Display(Name = "Περιφερειακή Ενότητα")]
        public string PeriferiaName { get; set; }
    }

    public class AnergiaViewModel
    {
        public int AnergiaID { get; set; }

        [Display(Name = "Ανεργία")]
        public string AnergiaText { get; set; }

        [Display(Name = "Μόρια")]
        public Nullable<decimal> AnergiaMoria { get; set; }
    }

    public class BasicEducationViewModel
    {
        public int EducationID { get; set; }

        [Display(Name = "Βασική εκπαίδευση")]
        public string EducationText { get; set; }
    }

    public class ComputerAsepViewModel
    {
        public int CertificateID { get; set; }

        [Display(Name = "Πιστοποιητικό")]
        public string CertificateText { get; set; }
    }

    public class TeachTypesViewModel
    {
        public int TypeID { get; set; }

        [Display(Name = "Βαθμίδα εκπαίδευσης")]
        public string TypeText { get; set; }

        [Display(Name = "Μόρια (έτος ή ώρα)")]
        public decimal? TypeMoria { get; set; }

        [Display(Name = "Μόρια Όριο")]
        public int? TypeMoriaMax { get; set; }

        [Display(Name = "Περιγραφή μορίων")]
        public string TypeMoriaInfo { get; set; }
    }

    public class ProkirixiStatusViewModel
    {
        public int StatusID { get; set; }
        public string StatusText { get; set; }
    }

    public class LanguageViewModel
    {
        public int LanguageID { get; set; }
        public string LanguageText { get; set; }
    }

    public class LanguageLevelViewModel
    {
        public int LevelID { get; set; }

        [Display(Name = "Επίπεδο")]
        public string LevelText { get; set; }

        [Display(Name = "Μόρια 1ης γλώσσας")]
        public decimal? MoriaFirst { get; set; }

        [Display(Name = "Μόρια 2ης γλώσσας")]
        public decimal? MoriaSecond { get; set; }
    }

    public class MScPeriodViewModel
    {
        public int MScPeriodID { get; set; }
        public string MScPeriodText { get; set; }
    }

    public class ApoklismoiViewModel
    {
        public int ApoklismosID { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Αιτία αποκλεισμού")]
        public string ApoklismosText { get; set; }
    }

    public class NomosViewModel
    {
        public int NomosID { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Νομός")]
        public string NomosText { get; set; }

        [Display(Name = "Περιφέρεια")]
        public int? PeriferiaMajorID { get; set; }

        public virtual SysPeriferiesMajor SysPeriferiesMajor { get; set; }
    }

    public class PeriferiaMajorViewModel
    {
        public int PeriferiaMajorID { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Περιφέρεια")]
        public string PeriferiaMajor { get; set; }
    }

    public class FamilyStatusViewModel
    {
        public int FamilyStatusID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Οικογενειακή κατάσταση")]
        public string FamilyStatusText { get; set; }
    }

    public class IdiotitaViewModel
    {
        public int IdiotitaID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Ιδιότητα υποψήφιου")]
        public string IdiotitaText { get; set; }
    }

    public class EpagelmaCategoryViewModel
    {
        public int EpagelmaID { get; set; }
        public string EpagelmaText { get; set; }
    }

    public class PtyxiaViewModel
    {
        public int PtyxioID { get; set; }
        public string PtyxioText { get; set; }
    }

    public class SynafeiaViewModel
    {
        public int SynafeiaID { get; set; }
        public string SynafeiaText { get; set; }
    }

    public class EpimorfosiTypesViewModel
    {
        public int EpimorfosiTypeID { get; set; }
        public string EpimorfosiTypeText { get; set; }
    }

    public class PtyxiaTypesViewModel
    {
        public int DegreeTypeID { get; set; }
        public string DegreeTypeText { get; set; }
    }


    #region REPORTS PARAMETERS

    public class TeacherRegistryParameters
    {
        public int? ProkirixiID { get; set; }
        public int? SchoolID { get; set; }
    }

    public class AitisisParameters
    {
        public int? AitisiID { get; set; }
        public int? ProkirixiID { get; set; }
    }

    public class ReportParameters
    {
        public int? ProkirixiID { get; set; }
        public int? PeriferiakiID { get; set; }

        public int? SchoolID { get; set; }

        public string Afm { get; set; }

    }


    #endregion
}