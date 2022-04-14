﻿using Snap_Bank.Models;
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

            using (var dbContext = new SnapDbContext())
            {
                dbContext.AccountTables.Add(accountTable);
                dbContext.SaveChanges();
                return true;
            }
            return false;
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

        public RegisterViewModel GetUserByName(String username)
        {
            RegisterViewModel viewModel = new RegisterViewModel();
            var user = snapDbContext.AccountTables.Where(s => s.UserName == username).FirstOrDefault();
            return (map.MapAccountTableToRegisterViewModel(user, viewModel));
        }

        public RegisterViewModel GerUserByNumber(int accountnumber)
        {
            RegisterViewModel viewModel = new RegisterViewModel();
            var user = snapDbContext.AccountTables.Where(s => s.AccountNumber == accountnumber).FirstOrDefault();
            return (map.MapAccountTableToRegisterViewModel(user, viewModel));
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