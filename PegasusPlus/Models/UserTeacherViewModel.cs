using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PegasusPlus.Models
{
    public class UserTeacherViewModel
    {
        public int UserID { get; set; }

        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [Display(Name = "Όνομα χρήστη (άκυρο)")]
        public string Username { get; set; }

        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [DataType(DataType.Password)]
        [Display(Name = "Κωδικός πρόσβασης (άκυρος)")]
        public string Password { get; set; }

        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.", MinimumLength = 9)]
        [Display(Name = "ΑΦΜ")]
        public string UserAfm { get; set; }

        [Display(Name = "Ενεργός")]
        public bool? IsActive { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία εγγραφής")]
        public DateTime? CreateDate { get; set; }
    }

    public class TeacherAccountInfoViewModel
    {
        public int UserID { get; set; }
        public string Username { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string FullName { get; set; }

        [Display(Name = "Πατρώνυμο")]
        public string FatherName { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string AFM { get; set; }

        [Display(Name = "ΑΔΤ")]
        public string ADT { get; set; }

        [Display(Name = "Τηλέφωνο")]
        public string Telephone { get; set; }

        [Display(Name = "Κινητό")]
        public string CellPhone { get; set; }

        [Display(Name = "E-Mail")]
        public string Email { get; set; }
    }

}