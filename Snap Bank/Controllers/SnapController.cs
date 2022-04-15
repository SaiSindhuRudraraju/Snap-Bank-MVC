using Snap_Bank.Filter;
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

        public SnapController(IAccountTableService _accountTableService, IPersonalDetailsService _personalDetailsService, ISecurityQuestionsService _securityQuestionsService, ITransactionsService _transactionsService)
        {
            accountTableService = _accountTableService;
            personalDetailsService = _personalDetailsService;
            securityQuestionsService = _securityQuestionsService;
            transactionsService = _transactionsService;
        }

        // GET: Snap
        public ActionResult Index()
        {
            return View("Home");
        }

        //[HttpPost]
        //public ActionResult Index(crediantials cred)
        //{
        //    if (cred.username != null)
        //    {
        //        if (cred.password != null)
        //        {
        //            if (accountTableService.CheckUserPassword(cred.username, cred.password))
        //            {
        //                return RedirectToAction("Home");
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("password", "Incorrect Password!");
        //                ModelState.AddModelError("AccountNumber", " ");
        //                ModelState.AddModelError("pin", " ");
        //                return View(cred);
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("password", "Enter Password!");
        //            return View(cred);
        //        }
        //    }
        //    else if (cred.AccountNumber.ToString() != null)
        //    {
        //        if (cred.pin.ToString() != null)
        //        {
        //            if (accountTableService.CheckUserPin(int.Parse(cred.AccountNumber), int.Parse(cred.pin)))
        //            {
        //                return RedirectToAction("Home");
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("pin", "Incorrect Pin!");
        //                ModelState.AddModelError("username", " ");
        //                ModelState.AddModelError("password", " ");
        //                return View(cred);
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("pin", "Enter Pin!");
        //            return View(cred);
        //        }
        //    }
        //    return View();
        //}

        public ActionResult Register()
        {
            RegisterViewModel registerViewModel = new RegisterViewModel();
            Random rnd = new Random();
            int myRandomNo = rnd.Next(100000000, 999999999);
            registerViewModel.AccountNumber = myRandomNo;
            //registerViewModel.SortCode1 = 12;
            //registerViewModel.SortCode2 = 93;
            //registerViewModel.SortCode3 = 64;
            return View(registerViewModel);
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            registerViewModel.CompleteSortCode = int.Parse(registerViewModel.SortCode1.ToString() + registerViewModel.SortCode2.ToString() + registerViewModel.SortCode3.ToString());
            accountTableService.Save(registerViewModel);
            personalDetailsService.Save(registerViewModel);
            securityQuestionsService.Save(registerViewModel);
            return View("Home");
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
        public ActionResult Settings()
        {
            var user = (crediantials)Session["user"];
            int numberOfusers = 0;
            if (user.username != null)
                numberOfusers =  accountTableService.GetNumberOfUsers(user.username);
            else if (user.AccountNumber!=null)
            {
                numberOfusers =  accountTableService.GetNumberOfUsers(int.Parse(user.AccountNumber));
            }
            if (numberOfusers == 1)
            {
                if (user.username != null)
                {
                    if (accountTableService.getUserAccountType(user.username) == "SavingsAccount")
                    {
                        ViewBag.AccountType = "Current Account";
                    }
                    else
                    {
                        ViewBag.AccountType = "Savings Account";
                    }
                }
                else if (user.AccountNumber!=null)
                {
                    if (accountTableService.getUserAccountType(user.AccountNumber) == "SavingsAccount")
                    {
                        ViewBag.AccountType = "Current Account";
                    }
                    else
                    {
                        ViewBag.AccountType = "Savings Account";
                    }
                }

            }
            else
            {
                ViewBag.AccountType = "";
            }
            return View();
        }
        public ActionResult AddAnotherAccount()
        {
            RegisterViewModel registerViewModel = new RegisterViewModel();
            var user = (crediantials)Session["user"];
            if (user.username != null)
                registerViewModel =  accountTableService.GetUserData(user.username);
            else if (user.AccountNumber!=null)
            {
                registerViewModel =  accountTableService.GetUserData(int.Parse(user.AccountNumber));
            }
            return View("Register", registerViewModel);
        }
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