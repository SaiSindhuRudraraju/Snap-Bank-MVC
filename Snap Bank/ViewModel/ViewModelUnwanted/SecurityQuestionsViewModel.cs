using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Snap_Bank.ViewModel
{
    public class SecurityQuestionsViewModel
    {
        [Required(ErrorMessage = "Please enter User Id.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter Birth Place.")]
        public String BirthPlace { get; set; }

        [Required(ErrorMessage = "Please enter Pet name.")]
        public String PetName { get; set; }

        [Required(ErrorMessage = "Please enter Favourite Food.")]
        public String FavouriteFood { get; set; }
    }
}