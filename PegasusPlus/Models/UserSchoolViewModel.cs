using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PegasusPlus.Models
{
    public class UserSchoolViewModel
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση ονόματος χρήστη")]
        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [Display(Name = "Όνομα χρήστη")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση κωδικού ασφαλείας")]
        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [DataType(DataType.Password)]
        [Display(Name = "Κωδικός πρόσβασης")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική Μονάδα")]
        public int? UserSchoolID { get; set; }
    }
}