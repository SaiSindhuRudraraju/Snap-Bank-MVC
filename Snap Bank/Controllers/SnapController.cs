﻿using Snap_Bank.Filter;
using Snap_Bank.Models;
using Snap_Bank.Services;
using Snap_Bank.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Snap_Bank.Controllers
{
    [AuthenticationFilter]
    public class SnapController : Controller
    {
        public static RegisterViewModel viewModel;
        IAccountTableService accountTableService;
        IPersonalDetailsService personalDetailsService;
        ISecurityQuestionsService securityQuestionsService;
        ITransactionsService transactionsService;

        //Constructor for Getting Instance of the services
        public SnapController(IAccountTableService _accountTableService, IPersonalDetailsService _personalDetailsService, ISecurityQuestionsService _securityQuestionsService, ITransactionsService _transactionsService)
        {
            accountTableService = _accountTableService;
            personalDetailsService = _personalDetailsService;
            securityQuestionsService = _securityQuestionsService;
            transactionsService = _transactionsService;
        }

        //GET: Snap
        public ActionResult Index()
        {
            return View("Home");
        }

        //When Clicked Next in Register Page at the time of creation of second account of a existing user
        [HttpPost]
        public ActionResult Register()
        {
            accountTableService.Save(viewModel);
            personalDetailsService.Save(viewModel);
            securityQuestionsService.Save(viewModel);
            return RedirectToAction("Signin", "Login");
        }

        public ActionResult FundTransfer()
        {
            FundTransferViewModel fundTransferViewModel = new FundTransferViewModel();
            fundTransferViewModel.selfAccountViewModel = new SelfAccountTransferViewModel();
            fundTransferViewModel.differentAccountTransferModel = new DifferentAccountTransferModel();

            fundTransferViewModel.differentAccountTransferModel.sortcode1 =12;
            fundTransferViewModel.differentAccountTransferModel.sortcode2 =93;
            fundTransferViewModel.differentAccountTransferModel.sortcode3 =64;
            return View(fundTransferViewModel);
        }

        [HttpPost]
        public ActionResult SelfFundTransfer(FundTransferViewModel fundTransferViewModel)
        {
            if (ModelState.IsValid)
            {
                return Redirect("PaymentSuccess");
            }
            return View(fundTransferViewModel);
        }

        [HttpPost]
        public ActionResult DifferentAccounFundTransfer(FundTransferViewModel fundTransferViewModel)
        {
            if (ModelState.IsValid)
            {
                return Redirect("PaymentSuccess");
            }
            return View(fundTransferViewModel);
        }

        //When Clicked Settings button in Home Page
        public ActionResult Settings()
        {
            var user = (crediantials)Session["user"];
            //Getting username first whatever the login details be
            String username;
            if(user.AccountNumber !=null)
            {
                username = accountTableService.GetUserName(int.Parse(user.AccountNumber));
            }
            else
            {
                username = user.username;
            }
            int numberOfAccounts = accountTableService.GetNumberOfUsers(username);

            if (numberOfAccounts == 1)
            {
                if (accountTableService.getUserAccountType(username) == "SavingsAccount")
                {
                    ViewBag.AccountType = "Current Account";
                }
                else
                {
                    ViewBag.AccountType = "Savings Account";
                }
            }
            else
            {
                ViewBag.AccountType = "";
            }
            return View();
        }

        //Adding Second Account In Settings Page
        public ActionResult AddAnotherAccount()
        {
            var user = (crediantials)Session["user"];

            RegisterViewModel registerViewModel = new RegisterViewModel();
            Random rnd = new Random();
            int myRandomNo = rnd.Next(100000000, 999999999);
            registerViewModel.AccountNumber = myRandomNo;
            registerViewModel.SortCode1 = 12;
            registerViewModel.SortCode2 = 93;
            registerViewModel.SortCode3 = 64;
            registerViewModel.CompleteSortCode = 129364;
            //Getting username first whatever the login details be
            String exsistingAccountNumber;
            if (user.username != null)
            {
                exsistingAccountNumber = accountTableService.GetUserNumber(user.username).ToString();
            }
            else
            {
                exsistingAccountNumber = user.AccountNumber;
            }

            registerViewModel = accountTableService.GetUserDetails(int.Parse(exsistingAccountNumber),registerViewModel);
            registerViewModel = personalDetailsService.GetUserDetails(int.Parse(exsistingAccountNumber), registerViewModel);
            registerViewModel = securityQuestionsService.GetUserDetails(int.Parse(exsistingAccountNumber), registerViewModel);
            registerViewModel.ConfirmPassword = registerViewModel.Password;
            viewModel = registerViewModel;
            return View("Register", registerViewModel);
        }

        //Home Page Of User After Loggin in with correct credentials
        public ActionResult Home()
        {
            HomePageDetailesViewModel homePageDetailesViewModel = new HomePageDetailesViewModel();
            var user = (crediantials)Session["user"];
            int numberOfAccounts = 0;
            if (user.username != null)
                numberOfAccounts =  accountTableService.GetNumberOfUsers(user.username);
            else if (user.AccountNumber!=null)
            {
                numberOfAccounts =  accountTableService.GetNumberOfUsers(int.Parse(user.AccountNumber));
            }
            if (user.username != null)
            {
                homePageDetailesViewModel = accountTableService.GetUserByName(user.username);
            }
            else if (user.AccountNumber != null)
            {
                homePageDetailesViewModel = accountTableService.GerUserByNumber(int.Parse(user.AccountNumber));
            }
            homePageDetailesViewModel.NumberOfAccounts = numberOfAccounts;
            return View(homePageDetailesViewModel);
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }
        public ActionResult Support()
        {
            return View();
        }

        public ActionResult AccountActivity()
        {
            return View();
        }
        public ActionResult PaymentSuccess()
        {
            return View();
        }
    }
}