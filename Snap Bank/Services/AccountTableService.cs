using Snap_Bank.Models;
using Snap_Bank.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Snap_Bank.Services
{
    public class AccountTableService : IAccountTableService
    {
        SnapDbContext snapDbContext;
        Mapper.Mapper map;
        AccountTable accountTable;
        public AccountTableService()
        {
            map = new Mapper.Mapper();
            accountTable = new AccountTable();
            snapDbContext = new SnapDbContext();
        }
        public bool Save(RegisterViewModel registerViewModel)
        {
            accountTable = map.MapRegisterViewModelToAccountTable(registerViewModel, accountTable);
            accountTable.Amount = 20000;
            using (var dbContext = new SnapDbContext())
            {
                dbContext.AccountTables.Add(accountTable);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public int GetAccountNumberByAccountType(string username, string AccountType)
        {
            return snapDbContext.AccountTables.Where(c => c.UserName == username && c.AccountType == AccountType).FirstOrDefault().AccountNumber;
        }
        public bool TransferAmountFromTo(SelfAccountTransferViewModel selfAccountViewModel, int fromAccount, int toAccount)
        {
            decimal totalamount = snapDbContext.AccountTables.Where(s => s.AccountNumber == fromAccount).FirstOrDefault().Amount;
            decimal transferamount = selfAccountViewModel.AmountToTransfer;
            bool isTransactionSuccessFull = false;
            if (totalamount > transferamount)
            {
                using (var tran = snapDbContext.Database.BeginTransaction())
                {
                    try
                    {
                        snapDbContext.AccountTables.Where(s => s.AccountNumber == fromAccount).FirstOrDefault().Amount = (int)(totalamount-transferamount);
                        snapDbContext.AccountTables.Where(s => s.AccountNumber == toAccount).FirstOrDefault().Amount = (int)(snapDbContext.AccountTables.Where(s => s.AccountNumber == toAccount).FirstOrDefault().Amount+transferamount);
                        var transactiondetailes1 = map.MapTrannsactionViewModels(fromAccount, toAccount, transferamount, "Debit", true);
                        snapDbContext.transactions.Add(transactiondetailes1);
                        var transactiondetailes2 = map.MapTrannsactionViewModels(toAccount, fromAccount, transferamount, "credit", true);
                        snapDbContext.transactions.Add(transactiondetailes2);
                        snapDbContext.SaveChanges();
                        isTransactionSuccessFull = true;
                        tran.Commit();

                    }
                    catch (Exception ex)
                    {
                        isTransactionSuccessFull=false;
                        tran.Rollback();
                    }
                }
            }
            if (isTransactionSuccessFull==true)
                return true;
            return false;
        }
        public bool TransferAmountFromTo(DifferentAccountTransferModel differentAccountTransferModel, int fromAccount, int toAccount)
        {
            decimal totalamount = snapDbContext.AccountTables.Where(s => s.AccountNumber == fromAccount).FirstOrDefault().Amount;
            decimal transferamount = differentAccountTransferModel.AmountToTransfer;
            bool isTransactionSuccessFull = false;
            if (totalamount > transferamount)
            {
                using (var tran = snapDbContext.Database.BeginTransaction())
                {
                    try
                    {
                        snapDbContext.AccountTables.Where(s => s.AccountNumber == fromAccount).FirstOrDefault().Amount = (int)(totalamount-transferamount);
                        snapDbContext.AccountTables.Where(s => s.AccountNumber == toAccount).FirstOrDefault().Amount = (int)(snapDbContext.AccountTables.Where(s => s.AccountNumber == toAccount).FirstOrDefault().Amount+transferamount);
                        var transactiondetailes1 = map.MapTrannsactionViewModels(fromAccount, toAccount, transferamount, "Debit", true);
                        snapDbContext.transactions.Add(transactiondetailes1);
                        var transactiondetailes2 = map.MapTrannsactionViewModels(toAccount, fromAccount, transferamount, "credit", true);
                        snapDbContext.transactions.Add(transactiondetailes2);
                        snapDbContext.SaveChanges();
                        isTransactionSuccessFull = true;
                        tran.Commit();

                    }
                    catch (Exception ex)
                    {
                        isTransactionSuccessFull=false;
                        tran.Rollback();
                    }
                }
            }
            if (isTransactionSuccessFull==true)
                return true;
            return false;
        }
        public decimal GetCurrentAccountAmount(string username)
        {
            return snapDbContext.AccountTables.Where(s => s.UserName == username && s.AccountType == "CurrentAccount").FirstOrDefault().Amount;
        }
        public decimal GetSavingAccountAmount(string username)
        {
            return snapDbContext.AccountTables.Where(s => s.UserName == username && s.AccountType == "SavingsAccount").FirstOrDefault().Amount;
        }
        public void ValidateAccountType(RegisterViewModel registerViewModel)
        {
            var records = snapDbContext.AccountTables.ToList();
            var UserRecords = records.Where(c => c.UserName.Contains(registerViewModel.UserName));
        }
        public bool CheckUserName(String username)
        {
            var check = snapDbContext.AccountTables.ToList();
            var result = check.Where(c => c.UserName.Contains(username));
            if (result.Count() == 0)
            {
                return false;
            }
            return true;
        }
        public bool CheckUserPassword(String username, String password)
        {
            var user = (from u in snapDbContext.AccountTables where u.UserName == username select u).FirstOrDefault();
            if (user!= null && user.Password == password)
            {
                return true;
            }
            return false;
        }
        public bool CheckUserPin(int accountnumber, int pin)
        {
            var user = (from u in snapDbContext.AccountTables where u.AccountNumber == accountnumber select u).FirstOrDefault();
            if (user!= null && user.Pin == pin)
            {
                return true;
            }
            return false;
        }
        public String GetUserName(int accountnumber)
        {
            var user = (from u in snapDbContext.AccountTables where u.AccountNumber == accountnumber select u).FirstOrDefault();
            if (user==null)
                return null;
            return user.UserName;
        }
        public String GetUserNumber(string username)
        {
            var user = (from u in snapDbContext.AccountTables where u.UserName == username select u).FirstOrDefault();
            return user.AccountNumber.ToString();
        }
        public RegisterViewModel GetUserDetails(int accountnumber, RegisterViewModel registerViewModel)
        {
            var user = (from u in snapDbContext.AccountTables where u.AccountNumber == accountnumber select u).FirstOrDefault();
            return map.MapExistingAccountTableToRegisterViewModel(registerViewModel, user);
        }
        public int GetNumberOfUsers(String username)
        {
            var data = snapDbContext.AccountTables.Where(s => s.UserName == username);
            return data.Count();
        }
        public int GetNumberOfUsers(int accountnumber)
        {
            var username = snapDbContext.AccountTables.Where(u => u.AccountNumber == accountnumber).FirstOrDefault().UserName;
            var data = snapDbContext.AccountTables.Where(s => s.UserName == username).ToList();
            return data.Count();
        }
        public string getUserAccountType(String username)
        {
            return snapDbContext.AccountTables.Where(s => s.UserName == username).FirstOrDefault().AccountType;
        }
        public HomePageDetailesViewModel GetUserByName(String username)
        {
            HomePageDetailesViewModel homePageDetailesViewModel = new HomePageDetailesViewModel();
            var data = snapDbContext.AccountTables.Where(s => s.UserName == username).ToList();
            if (data.Count() == 2)
            {
                var userAccountDetailes1 = data[0];
                var userAccountDetailes2 = data[1];
                var userPersonalDetailes = snapDbContext.personalDetails.Where(s => s.UserId == userAccountDetailes1.UserId).FirstOrDefault();
                return map.MapUserAccountPersonalAccountToHomePage(userAccountDetailes1, userAccountDetailes2, userPersonalDetailes, homePageDetailesViewModel);
            }
            else
            {
                var userAccountDetailes = data.FirstOrDefault();
                var userPersonalDetailes = snapDbContext.personalDetails.Where(s => s.AccountNumber == userAccountDetailes.AccountNumber).FirstOrDefault();
                return (map.MapAccountTableToHomeDetailesViewModel(userAccountDetailes, userPersonalDetailes, homePageDetailesViewModel));
            }
        }
        public HomePageDetailesViewModel GerUserByNumber(int accountnumber)
        {
            HomePageDetailesViewModel homePageDetailesViewModel = new HomePageDetailesViewModel();
            var user = snapDbContext.AccountTables.Where(s => s.AccountNumber == accountnumber).FirstOrDefault();
            var data = snapDbContext.AccountTables.Where(s => s.UserName == user.UserName).ToList();
            if (data.Count() == 2)
            {
                //error in linqs
                var userAccountDetailes1 = data[0];
                var userAccountDetailes2 = data[1];
                var userPersonalDetailes = snapDbContext.personalDetails.Where(s => s.UserId == userAccountDetailes1.UserId).FirstOrDefault();
                return map.MapUserAccountPersonalAccountToHomePage(userAccountDetailes1, userAccountDetailes2, userPersonalDetailes, homePageDetailesViewModel);
            }
            else
            {
                var userAccountDetailes = data.FirstOrDefault();
                var userPersonalDetailes = snapDbContext.personalDetails.Where(s => s.AccountNumber == userAccountDetailes.AccountNumber).FirstOrDefault();
                return (map.MapAccountTableToHomeDetailesViewModel(userAccountDetailes, userPersonalDetailes, homePageDetailesViewModel));
            }
        }
    }
}