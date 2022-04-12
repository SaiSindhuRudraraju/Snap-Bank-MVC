using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Snap_Bank.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please Enter First Name")]
        public String FirstName { get; set; }
        [Required(ErrorMessage = "Please Enter Last Name")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "Please Enter Data of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        [Required(ErrorMessage = "Please Enter Gender")]
        public String Gender { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Please Enter Phone")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone format is not valid.")]
        public String Phone { get; set; }
        public int SortCode1 { get; set; }
        public int SortCode2 { get; set; }
        public int SortCode3 { get; set; }
        public int CompleteSortCode { get; set; }

        [Required(ErrorMessage = "Please Choose Account Type")]
        public String AccountType { get; set; }
        public int AccountNumber { get; set; }

        [Required(ErrorMessage = "Please Enter UserName")]
        public String UserName { get; set; }

        [Required(ErrorMessage = "Please Enter password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password \"{0}\" must have {2} character", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{6,}$", ErrorMessage = "Password must contain: Minimum 8 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character")]
        public String Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Please Enter Confirm password")]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [DataType(DataType.Password)]
        public String ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please Enter Pin")]
        public int Pin { get; set; }
        public String SecurityQuestion1 { get; set; }
        public String SecurityQuestion2 { get; set; }
        public String SecurityQuestion3 { get; set; }
    }
    public enum AccountType
    {
        SavingsAccount,
        CurrentAccount
    }
    public enum Gender
    {
        Male,
        Female,
        Others
    }
}