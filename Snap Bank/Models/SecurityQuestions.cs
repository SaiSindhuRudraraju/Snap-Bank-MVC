using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Snap_Bank.Models
{
    [Table("SecurityQuestions")]
    public class SecurityQuestions
    {
        [Key]
        public int UserId { get; set; }
        public int AccountNumber { get; set; }
        public String BirthPlace { get; set; }
        public String PetName { get; set; }
        public String FavouriteFood { get; set; }
    }
}