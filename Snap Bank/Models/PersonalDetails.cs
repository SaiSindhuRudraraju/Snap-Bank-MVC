using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Snap_Bank.Models
{
    [Table("PersonalDetails")]
    public class PersonalDetails
    {
        [Key]
        public int UserId { get; set; }
        public int AccountNumber { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String Gender { get; set; }
        public String Gmail { get; set; }
        public String MobileNumber { get; set; }
    }
}