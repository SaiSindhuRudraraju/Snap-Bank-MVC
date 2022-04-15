using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snap_Bank.ViewModel
{
    public class HomePageDetailesViewModel
    {

        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Date { get; set; }
        public int AccountNumber { get; set; }
        public int AccountNumber2 { get; set; }
        public int NumberOfAccounts { get; set; }
        public string AccountType { get; set; }
        public string AccountType2 { get; set; }
        public int LastDigits { get; set; }
        public List<String> AccountActivity { get; set; }
        public List<String> SentMoneyActivity { get; set; }
        public List<String> RecivedMoneyActivity { get; set; }
    }
}