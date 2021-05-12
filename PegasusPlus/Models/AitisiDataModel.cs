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
    public class AitisiDataViewModel
    {
        public int AitisisID { get; set; }

        public int? ProkirixisID { get; set; }

        [Display(Name = "Α.Π. Αίτησης")]
        public string AitisisProtocol { get; set; }

        [Display(Name = "Περιφέρεια Αίτησης")]
        public string PeriferiaName { get; set; }

        [Display(Name = "Σχολική Μονάδα Αίτησης")]
        public string SchoolName { get; set; }

        [Display(Name = "Α.Φ.Μ.")]
        public string TeacherAFM { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string FullName { get; set; }

        [Display(Name = "Κλάδος - Ειδικότητα")]
        public string KladosEidikotita { get; set; }

        [Display(Name = "Είδος πτυχίου")]
        public string PtyxioTypeText { get; set; }

        [Display(Name = "Τίτλος πτυχίου")]
        public string PtyxioTitlos { get; set; }

        [Display(Name = "Μεταπτυχιακό")]
        public string MscTitlos { get; set; }

        [Display(Name = "Διδακτορικό")]
        public string PhdTitlos { get; set; }

        [Display(Name = "Δεύτερο πτυχίο")]
        public string Ptyxio2Titlos { get; set; }

        [Display(Name = "Γνώση 1ης ξένης γλώσσας")]
        public string Language1Epipedo { get; set; }

        [Display(Name = "Γνώση 2ης ξένης γλώσσας")]
        public string Language2Epipedo { get; set; }

        [Display(Name = "Γνώση H/Y")]
        public string ComputerTitle { get; set; }

        [Display(Name = "Ώρες επιμόρφωσης")]
        public int EpimorfosiHours { get; set; }

        [Display(Name = "Πιστοποίηση εκπαιδευτή ενηλίκων")]
        public string CertifiedTrainerAM { get; set; }

        [Display(Name = "Τρίτεκνη οικογένεια")]
        public string SocialTriteknosText { get; set; }

        [Display(Name = "Πολύτεκνη οικογένεια")]
        public string SocialPolyteknosText { get; set; }

        [Display(Name = "Μονογονεϊκή οικογένεια")]
        public string SocialSingleParentText { get; set; }

        [Display(Name = "Οικογένεια ΑΜΕΑ")]
        public string SocialAmeaText { get; set; }

        [Display(Name = "Ανεργία")]
        public string AnergiaDiarkeiaText { get; set; }

        [Display(Name = "Επαγγελματική ιδιότητα")]
        public string EpagelmaText { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Μόρια πτυχίου (1ου και 2ου)")]
        public decimal MoriaPtyxio { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Μόρια μεταπτυχιακού")]
        public decimal MoriaMsc { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Μόρια διδακτορικού")]
        public decimal MoriaPhd { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Μόρια ξένων γλωσσών")]
        public decimal MoriaLanguages { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Μόρια γνώσης Η/Υ")]
        public decimal MoriaComputer { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Μόρια επιμόρφωσης")]
        public decimal MoriaEpimorfosi { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Μόρια πιστοποίησης ΕΟΠΠΕΠ")]
        public decimal MoriaCertified { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Μόρια διδακτικής εμπειρίας")]
        public decimal MoriaTeach { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Μόρια επαγγελματικής εμπειρίας")]
        public decimal MoriaWork { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Μόρια ανεργίας")]
        public decimal MoriaAnergia { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Μόρια ειδικών κατηγοριών")]
        public decimal MoriaSocial { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Συνολικά Μόρια")]
        public decimal MoriaTotal { get; set; }
    }

}