using System;
using System.Collections.Generic;
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
        public string username { get; set; }
        public string password { get; set; }
        public string confirmpassword { get; set; }
    }

    public class ChangeDetailes
    {
        public string email { get; set; }
        public string phonenumber { get; set; }
    }
}