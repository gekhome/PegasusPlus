using System;
using System.ComponentModel.DataAnnotations;

namespace PegasusPlus.Models
{
    public class UserAdminViewModel
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση ονόματος χρήστη")]
        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [Display(Name = "Όνομα χρήστη")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση κωδικού πρόσβασης")]
        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [DataType(DataType.Password)]
        [Display(Name = "Κωδικός πρόσβασης")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(150, ErrorMessage = "Πρέπει να είναι μέχρι 150 χαρακτήρες.")]
        [Display(Name = "Ονοματεπώνυμο")]
        public string FullName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ημ/νία εγγραφής")]
        public DateTime? CreateDate { get; set; }
    }
}