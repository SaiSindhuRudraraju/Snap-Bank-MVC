using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Snap_Bank.ViewModel
{
    public class PersonalDetailsViewModel
    {
        [Required]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter User name.")]
        public String FirstName { get; set; }

        [Required]
        public String LastName { get; set; }

        [RegularExpression(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$", ErrorMessage = "Invalid date format.")]
        public DateTime DateOfBirth { get; set; }


        [Required(ErrorMessage = "Please Provide Gender")]
        public String Gender { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email format")]
        public String Gmail { get; set; }

        [Required]
        public Decimal MobileNumber { get; set; }
    }
}