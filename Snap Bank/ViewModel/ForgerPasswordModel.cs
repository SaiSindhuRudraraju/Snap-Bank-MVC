using System;
using System.Collections.Generic;
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
        public string username { get; set; }
        public string birthPlace { get; set; }
        public string petName { get; set; }
        public string FavouritFood { get; set; }
    }
    public class NewPasswords
    {
        public string password { get; set; }
        public string confirmpassword { get; set; }
    }
}