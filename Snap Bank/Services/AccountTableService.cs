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

        public bool Save(RegisterViewModel registerViewModel)
        {
            accountTable = map.MapRegisterViewModelToAccountTable(registerViewModel, accountTable);
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
            var data = snapDbContext.AccountTables.Where(s => s.AccountNumber == accountnumber);
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
            var data = snapDbContext.AccountTables.Where(s => s.UserName == user.UserName);
            if (data.Count() == 2)
            {
                //error in linqs
                var userAccountDetailes1 = data.ElementAt(0);
                var userAccountDetailes2 = data.ElementAt(1);
                var userPersonalDetailes = snapDbContext.personalDetails.Where(s => s.UserId == data.FirstOrDefault().UserId).FirstOrDefault();
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