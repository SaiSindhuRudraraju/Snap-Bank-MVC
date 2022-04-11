using Snap_Bank.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Snap_Bank
{
    public class SnapDbContext : DbContext
    {
        public SnapDbContext() : base("name=mylocaldb")
        {
        }
        public DbSet<AccountTable> AccountTables { get; set; }
        public DbSet<PersonalDetails> personalDetails { get; set; }
        public DbSet<SecurityQuestions> securityQuestions { get; set; }
        public DbSet<Transactions> transactions { get; set; }
    }
}