using Snap_Bank.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Snap_Bank.Models;

namespace Snap_Bank.Mapper
{
    public class Mapper
    {
        public AccountTable MapRegisterViewModelToAccountTable(RegisterViewModel viewModel, AccountTable accountTable)
        {
            accountTable.AccountNumber = viewModel.AccountNumber;
            accountTable.AccountType = viewModel.AccountType;
            accountTable.Password = viewModel.Password;
            accountTable.SortCode = viewModel.CompleteSortCode;
            accountTable.UserName = viewModel.UserName;
            accountTable.Pin = viewModel.Pin;
            return accountTable;
        }

        public PersonalDetails MapRegisterViewModelToPersonalDetails(RegisterViewModel viewModel, PersonalDetails personalDetails)
        {
            personalDetails.AccountNumber = viewModel.AccountNumber;
            personalDetails.FirstName = viewModel.FirstName;
            personalDetails.LastName = viewModel.LastName;
            personalDetails.DateOfBirth = viewModel.DateOfBirth;
            personalDetails.Gender = viewModel.Gender;
            personalDetails.Gmail = viewModel.Email;
            personalDetails.MobileNumber = viewModel.Phone;
            return personalDetails;
        }

        public SecurityQuestions MapRegisterViewModelToSecurityQuestions(RegisterViewModel viewModel, SecurityQuestions securityQuestions)
        {
            securityQuestions.AccountNumber = viewModel.AccountNumber;
            securityQuestions.BirthPlace = viewModel.SecurityQuestion1;
            securityQuestions.PetName = viewModel.SecurityQuestion2;
            securityQuestions.FavouriteFood = viewModel.SecurityQuestion3;
            return securityQuestions;
        }

        public RegisterViewModel MapExistingAccountTableToRegisterViewModel(RegisterViewModel registerViewModel, AccountTable accountTable)
        {
            if(accountTable.AccountType == "SavingsAccount")
            {
                registerViewModel.AccountType = "CurrentAccount";
            }
            else
            {
                registerViewModel.AccountType = "SavingsAccount";
            }
            registerViewModel.Password = accountTable.Password;
            registerViewModel.CompleteSortCode = accountTable.SortCode;
            registerViewModel.UserName = accountTable.UserName;
            registerViewModel.Pin = accountTable.Pin;
            return registerViewModel;
        }

        public RegisterViewModel MapExistingPersonalDetalisToRegisterViewModel(RegisterViewModel registerViewModel, PersonalDetails personalDetails)
        {
            registerViewModel.FirstName = personalDetails.FirstName;
            registerViewModel.LastName = personalDetails.LastName;
            registerViewModel.DateOfBirth = personalDetails.DateOfBirth;
            registerViewModel.Gender = personalDetails.Gender;
            registerViewModel.Email = personalDetails.Gmail;
            registerViewModel.Phone = personalDetails.MobileNumber;
            return registerViewModel;
        }

        public RegisterViewModel MapExistingQuestionsToRegisterViewModel(RegisterViewModel registerViewModel, SecurityQuestions securityQuestions)
        {
            registerViewModel.SecurityQuestion1 = securityQuestions.BirthPlace;
            registerViewModel.SecurityQuestion2 = securityQuestions.PetName;
            registerViewModel.SecurityQuestion3 = securityQuestions.FavouriteFood;
            return registerViewModel;
        }

        public HomePageDetailesViewModel MapUserAccountPersonalAccountToHomePage(AccountTable account1, AccountTable account2, PersonalDetails personalDetails, HomePageDetailesViewModel viewModel)
        {
            viewModel.AccountNumber =account1.AccountNumber;
            viewModel.AccountNumber2 = account2.AccountNumber;
            viewModel.LastDigits = account1.AccountNumber%10000;
            viewModel.LastDigits2 = account2.AccountNumber%10000;
            viewModel.FirstName = personalDetails.FirstName;
            viewModel.LastName = personalDetails.LastName;
            viewModel.AccountType = account1.AccountType;
            viewModel.AccountType2 = account2.AccountType;
            viewModel.Date = "09/12/2024";
            viewModel.AccountActivity = null;
            viewModel.SentMoneyActivity = null;
            viewModel.RecivedMoneyActivity = null;
            return viewModel;
        }

        public HomePageDetailesViewModel MapAccountTableToHomeDetailesViewModel(AccountTable accountTable, PersonalDetails personalDetails, HomePageDetailesViewModel viewModel)
        {
            viewModel.AccountNumber = accountTable.AccountNumber;
            viewModel.LastDigits = accountTable.AccountNumber%10000;
            viewModel.AccountType = accountTable.AccountType;
            viewModel.FirstName = personalDetails.FirstName;
            viewModel.LastName = personalDetails.LastName;
            viewModel.Date  = "09/12/2024";
            viewModel.AccountActivity = null;
            viewModel.SentMoneyActivity = null;
            viewModel.RecivedMoneyActivity = null;
            return viewModel;
        }
    }
}