using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Snap_Bank.Models
{
    [Table("Transactions")]
    public class Transactions
    {
        [Key]
        public int UserId { get; set; }
        public int AccountNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal FromAccount { get; set; }
        public decimal ToAccount { get; set; }
        public decimal Amount { get; set; }
        public bool TransactionCompleted { get; set; }
        public string mode { get; set; }
        public string TransactionType { get; set; }
        public string AccountType { get; set; }
    }
}