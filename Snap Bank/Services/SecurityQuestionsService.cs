using Snap_Bank.Models;
using Snap_Bank.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snap_Bank.Services
{
    public class SecurityQuestionsService : ISecurityQuestionsService
    {
        SnapDbContext snapDbContext;
        Mapper.Mapper map;
        SecurityQuestions securityQuestion;

        public SecurityQuestionsService()
        {
            map = new Mapper.Mapper();
            securityQuestion = new SecurityQuestions();
            snapDbContext = new SnapDbContext();
        }

        public bool Save(RegisterViewModel registerViewModel)
        {
            securityQuestion = map.MapRegisterViewModelToSecurityQuestions(registerViewModel, securityQuestion);

            using (var dbContext = new SnapDbContext())
            {
                dbContext.securityQuestions.Add(securityQuestion);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public RegisterViewModel GetUserDetails(int accountnumber, RegisterViewModel registerViewModel)
        {
            var user = (from u in snapDbContext.securityQuestions where u.AccountNumber == accountnumber select u).FirstOrDefault();
            return map.MapExistingQuestionsToRegisterViewModel(registerViewModel, user);
        }

        public bool VerifyAnswers(CheckSecurityQuestions answers,int accountnumber)
        {
            var user = (from u in snapDbContext.securityQuestions where u.AccountNumber == accountnumber select u).FirstOrDefault();
            if(user.BirthPlace==answers.birthPlace && user.PetName == answers.petName && user.FavouriteFood == answers.FavouritFood)
            {
                return true;
            }
            return false;
        }

    }
}