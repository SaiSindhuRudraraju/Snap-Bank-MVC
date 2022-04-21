using Snap_Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snap_Bank.Services
{
    public class TransactionsService : ITransactionsService
    {
        SnapDbContext snapDbContext;

        public TransactionsService()
        {
            snapDbContext = new SnapDbContext();
        }

        public List<Transactions> Get(int AccountNumber)
        {
            var list = snapDbContext.transactions.Where(s => s.AccountNumber == AccountNumber).ToList();
            return list;
        }
        public List<Transactions> GetTransferDetailesByType(int AccountNumber, String AccountType)
        {
            var list = snapDbContext.transactions.Where(s => s.AccountNumber == AccountNumber).ToList();
            return list;
        }
        public bool Save(Transactions transaction)
        {
            using (var dbContext = new SnapDbContext())
            {
                dbContext.transactions.Add(transaction);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Delete(int id)
        {
            using (var ent = new SnapDbContext())
            {
                //var user = ent.transactions.Where(s => s.UserId == id).FirstOrDefault();
                //if (user != null)
                //{
                //    ent.transactions.Remove(user);
                //    ent.SaveChanges();
                //    return true;
                //}
                return false;
            }
        }
        public bool Put(Transactions transaction)
        {
            //using (var ent = new SnapDbContext())
            //{
            //    var temp = ent.transactions.Find(transaction.UserId);
            //    if (temp != null)
            //    {
            //        temp.TransactionDate = transaction.TransactionDate;
            //        temp.FromAccount = transaction.FromAccount;
            //        temp.ToAccount = transaction.ToAccount;
            //        temp.Amount = transaction.Amount;
            //        temp.TransactionCompleted = transaction.TransactionCompleted;
            //        temp.mode = transaction.mode;
            //        temp.TransactionType = transaction.TransactionType;
            //        temp.AccountType = transaction.AccountType;
            //    }
            //    ent.SaveChanges();
            //    return true;
            //}
            return false;
        }
    }
}