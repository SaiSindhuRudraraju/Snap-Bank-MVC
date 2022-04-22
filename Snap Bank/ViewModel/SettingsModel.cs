using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Snap_Bank.ViewModel
{
    public class SettingsModel
    {
        public ChangePassword changePassword { get; set; }
        public ChangeDetailes changedetailes { get; set; }
    }
    public class ChangePassword
    {
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

    public class ChangeDetailes
    {
        [Required(ErrorMessage = "Please Enter Gmail")]
        public string email { get; set; }

        [Required(ErrorMessage = "Please Enter Mobile Number")]
        public string phonenumber { get; set; }
    }
}