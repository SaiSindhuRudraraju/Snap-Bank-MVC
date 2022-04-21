using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Snap_Bank.ViewModel
{
    public class ForgetPasswordModel
    {
        public CheckSecurityQuestions securityQuestions { get; set; }
        public NewPasswords newPasswords { get; set; }
    }
    public class CheckSecurityQuestions
    {
        [Required(ErrorMessage = "Please Enter UserName")]
        public string username { get; set; }

        [Required(ErrorMessage = "Please Enter BirthPlace")]
        public string birthPlace { get; set; }

        [Required(ErrorMessage = "Please Enter PetName")]
        public string petName { get; set; }

        [Required(ErrorMessage = "Please Enter FavoriteFood")]
        public string FavouritFood { get; set; }
        public bool isMatched { get; set; }
    }
    public class NewPasswords
    {
        public string username { get; set; }

        [Required(ErrorMessage = "Please Enter password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password \"{0}\" must have {2} character", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{6,}$", ErrorMessage = "Password must contain: Minimum 8 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character")]
        public string password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Please Enter Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [DataType(DataType.Password)]
        public string confirmpassword { get; set; }
    }
}