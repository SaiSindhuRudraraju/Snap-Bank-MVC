using Snap_Bank.Models;
using Snap_Bank.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snap_Bank.Services
{
    public class PersonalDetailsService : IPersonalDetailsService
    {
        SnapDbContext snapDbContext;
        Mapper.Mapper map;
        PersonalDetails personalDetail;

        public PersonalDetailsService()
        {
            map = new Mapper.Mapper();
            personalDetail = new PersonalDetails();
            snapDbContext = new SnapDbContext();
        }

        public bool Save(RegisterViewModel registerViewModel)
        {
            personalDetail = map.MapRegisterViewModelToPersonalDetails(registerViewModel, personalDetail);
            using (var dbContext = new SnapDbContext())
            {
                dbContext.personalDetails.Add(personalDetail);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public RegisterViewModel GetUserDetails(int accountnumber, RegisterViewModel registerViewModel)
        {
            var user = (from u in snapDbContext.personalDetails where u.AccountNumber == accountnumber select u).FirstOrDefault();
            return map.MapExistingPersonalDetalisToRegisterViewModel(registerViewModel, user);
        }
    }
}