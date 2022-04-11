using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Snap_Bank.ViewModel
{
    public class AccountViewModel
    {
        [Required]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter User name.")]
        public String UserName { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be between 6 and 20 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        public String Password { get; set; }


        public Decimal AccountNumber { get; set; }

        [Required]
        public int Pin { get; set; }

        [Required]
        public Decimal SortCode { get; set; }

        [Required]
        public String AccountType { get; set; }
    }
}