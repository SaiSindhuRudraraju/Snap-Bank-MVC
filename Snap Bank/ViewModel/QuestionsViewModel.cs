using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Snap_Bank.ViewModel
{
    public class QuestionsViewModel
    {
        [Required(ErrorMessage = "Please Enter Your Birth Place")]
        public String SecurityQuestion1 { get; set; }

        [Required(ErrorMessage = "Please Enter Your Pet Name")]
        public String SecurityQuestion2 { get; set; }

        [Required(ErrorMessage = "Please Enter Your Favourite Food")]
        public String SecurityQuestion3 { get; set; }
    }
}