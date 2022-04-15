using Snap_Bank.Models;
using Snap_Bank.ViewModel;
using System;
using System.Collections.Generic;
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
        public IEnumerable<AccountTable> Get()
        {
            return snapDbContext.AccountTables.ToList();
        }

        public bool Save(RegisterViewModel registerViewModel)
        {
            accountTable = map.MapRegisterViewModelToAccountTable(registerViewModel, accountTable);
            if (accountTable.AccountType == "Current Account")
                accountTable.HasCurrentAccount = true;
            else if (accountTable.AccountType == "Savings Account")
                accountTable.HasSavings=true;
            using (var dbContext = new SnapDbContext())
            {
                dbContext.AccountTables.Add(accountTable);
                dbContext.SaveChanges();
                return true;
            }
            return false;
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
            if (user.Password == password)
            {
                return true;
            }
            return false;
        }

        public bool CheckUserPin(int accountnumber, int pin)
        {
            var user = (from u in snapDbContext.AccountTables where u.AccountNumber == accountnumber select u).FirstOrDefault();
            if (user.Pin == pin)
            {
                return true;
            }
            return false;
        }
        public RegisterViewModel GetUserData(String Username)
        {
            RegisterViewModel viewModel = new RegisterViewModel();
            var useraccountdata = snapDbContext.AccountTables.Where(s => s.UserName == Username).FirstOrDefault();
            var userpersonaldata = snapDbContext.personalDetails.Where(s => s.UserId == useraccountdata.UserId).FirstOrDefault();
            return map.MapAccountTableToRegisterDetailesViewModel(useraccountdata, userpersonaldata, viewModel);
        }
        public RegisterViewModel GetUserData(int AccountNumber)
        {
            String Username = snapDbContext.AccountTables.Where(s => s.AccountNumber == AccountNumber).FirstOrDefault().UserName;
            RegisterViewModel viewModel = new RegisterViewModel();
            var useraccountdata = snapDbContext.AccountTables.Where(s => s.UserName == Username).FirstOrDefault();
            var userpersonaldata = snapDbContext.personalDetails.Where(s => s.UserId == useraccountdata.UserId).FirstOrDefault();
            return map.MapAccountTableToRegisterDetailesViewModel(useraccountdata, userpersonaldata, viewModel);
        }
        public int GetNumberOfUsers(String username)
        {
            var data = snapDbContext.AccountTables.Where(s => s.UserName == username);
            return data.Count();
        }
        public int GetNumberOfUsers(int accountnumber)
        {
            String Username = snapDbContext.AccountTables.Where(s => s.AccountNumber == accountnumber).FirstOrDefault().UserName;
            var data = snapDbContext.AccountTables.Where(s => s.UserName == Username);
            return data.Count();
        }
        public string getUserAccountType(String username)
        {
            return snapDbContext.AccountTables.Where(s => s.UserName == username).FirstOrDefault().AccountType;
        }
        public string getUserAccountType(int accountnumber)
        {
            return snapDbContext.AccountTables.Where(s => s.AccountNumber == accountnumber).FirstOrDefault().AccountType;

        }
        public HomePageDetailesViewModel GetUserByName(String username)
        {
            HomePageDetailesViewModel homePageDetailesViewModel = new HomePageDetailesViewModel();
            var data = snapDbContext.AccountTables.Where(s => s.UserName == username);
            if (data.Count() == 2)
            {
                var userAccountDetailes1 = data.First();
                var userAccountDetailes2 = data.ElementAt(1);
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
            var userAccountDetailes = snapDbContext.AccountTables.Where(s => s.AccountNumber == accountnumber).FirstOrDefault();
            var userPersonalDetailes = snapDbContext.personalDetails.Where(s => s.AccountNumber == userAccountDetailes.AccountNumber).FirstOrDefault();
            return (map.MapAccountTableToHomeDetailesViewModel(userAccountDetailes, userPersonalDetailes, homePageDetailesViewModel));
        }

        public bool Delete(int id)
        {
            //using (var ent = new SnapDbContext())
            //{
            //    var user = ent.AccountTables.Where(s => s.UserId == id).FirstOrDefault();
            //    if (user != null)
            //    {
            //        ent.AccountTables.Remove(user);
            //        ent.SaveChanges();
            //        return true;
            //    }
            //}
            return false;
        }
        public bool Put(RegisterViewModel registerViewModel)
        {
            //using (var ent = new SnapDbContext())
            //{
            //    var temp = ent.AccountTables.Find(accountTable.UserId);
            //    if (temp != null)
            //    {
            //        temp.UserName = accountTable.UserName;
            //        temp.Password = accountTable.Password;
            //        temp.AccountNumber = accountTable.AccountNumber;
            //        temp.Pin = accountTable.Pin;
            //        temp.SortCode = accountTable.SortCode;
            //        temp.AccountType = accountTable.AccountType;
            //    }
            //    ent.SaveChanges();
            //    return true;
            //}
            return false;
        }
    }
}