using Snap_Bank.Filter;
using Snap_Bank.Models;
using Snap_Bank.Services;
using Snap_Bank.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var user = (crediantials)Session["user"];
            var username = "";
            if (user.username == null)
            {
                username = accountTableService.GetUserName(int.Parse(user.AccountNumber));
            }
            else
            {
                username = user.username;
            }

            ViewBag.CurrentAccountBalance = accountTableService.GetCurrentAccountAmount(username);
            ViewBag.SavingsAccountBalance = accountTableService.GetSavingAccountAmount(username);


            return View(fundTransferViewModel);
        }

        [HttpPost]
        public ActionResult SelfFundTransfer(FundTransferViewModel fundTransferViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = (crediantials)Session["user"];
                var username = "";
                if (user.username == null)
                {
                    username = accountTableService.GetUserName(int.Parse(user.AccountNumber));
                }
                else
                {
                    username = user.username;
                }
                var fromAccountNumber = accountTableService.GetAccountNumberByAccountType(username, fundTransferViewModel.selfAccountViewModel.FromAccountType);
                var toAccountNumber = accountTableService.GetAccountNumberByAccountType(username, fundTransferViewModel.selfAccountViewModel.ToAccountType);
                bool status = accountTableService.TransferAmountFromTo(fundTransferViewModel.selfAccountViewModel, fromAccountNumber, toAccountNumber);
                if (status ==true)
                    return Redirect("PaymentSuccess");
                else
                    return Redirect("PaymentUnsuccess");
            }
            return View(fundTransferViewModel);
        }

        [HttpPost]
        public ActionResult DifferentAccounFundTransfer(FundTransferViewModel fundTransferViewModel)
        {

            var user = (crediantials)Session["user"];
            var username = "";
            if (user.username == null)
            {
                username = accountTableService.GetUserName(int.Parse(user.AccountNumber));
            }
            else
            {
                username = user.username;
            }
            var fromAccountNumber = accountTableService.GetAccountNumberByAccountType(username, fundTransferViewModel.differentAccountTransferModel.FromAccountType);
            var toAccountNumber = fundTransferViewModel.differentAccountTransferModel.AccountNumber;
            bool status = accountTableService.TransferAmountFromTo(fundTransferViewModel.differentAccountTransferModel, fromAccountNumber, (int)toAccountNumber);
            if (status ==true)
                return Redirect("PaymentSuccess");
            else
                return Redirect("PaymentUnsuccess");

            return View(fundTransferViewModel);
        }

        //When Clicked Settings button in Home Page
        public ActionResult Settings()
        {
            var user = (crediantials)Session["user"];
            //Getting username first whatever the login details be
            String username;
            if (user.AccountNumber !=null)
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

        public void logout()
        {
            Session.RemoveAll();
        }
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

            registerViewModel = accountTableService.GetUserDetails(int.Parse(exsistingAccountNumber), registerViewModel);
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
                homePageDetailesViewModel = accountTableService.GetUserByNumber(int.Parse(user.AccountNumber));
            }
            homePageDetailesViewModel.NumberOfAccounts = numberOfAccounts;
            return View(homePageDetailesViewModel);
        }
        public String GetName(int id)
        {
            var name = accountTableService.GetUserName(id);
            if (name != null)
                return name.ToString();
            return null;
        }

        public ActionResult Support()
        {
            return View();
        }

        public ActionResult AccountActivity()
        {
            var user = (crediantials)Session["user"];
            String username;
            if (user.AccountNumber !=null)
            {
                username = accountTableService.GetUserName(int.Parse(user.AccountNumber));
            }
            else
            {
                username = user.username;
            }
            List<Transactions> list = transactionsService.Get(int.Parse(accountTableService.GetUserNumber(username)));
            return View(list);
        }
        public ActionResult GetTransactions(String account, String transactionrange, String fromdate, String todate)
        {
            var user = (crediantials)Session["user"];
            DateTime totansactiondate = DateTime.Today;
            DateTime fromtansactiondate = DateTime.Today;
            String username;
            if (user.AccountNumber !=null)
            {
                username = accountTableService.GetUserName(int.Parse(user.AccountNumber));
            }
            else
            {
                username = user.username;
            }
            if (transactionrange!=null && account!=null)
            {
                int AccountNumber = accountTableService.GetAccountNumber(username, account);

                if (transactionrange.Equals("lastweek"))
                {
                    fromtansactiondate = DateTime.Now.AddDays(-7);
                }
                else if (transactionrange.Equals("last2weeks"))
                {
                    fromtansactiondate = DateTime.Now.AddDays(-14);
                }
                else if (transactionrange.Equals("lastmonth"))
                {
                    fromtansactiondate = DateTime.Now.AddMonths(-1);
                }
                else if (transactionrange.Equals("coustum") && fromdate != null && todate != null)
                {
                    totansactiondate = Convert.ToDateTime(todate);
                    fromtansactiondate = Convert.ToDateTime(fromdate);
                }
                List<Transactions> list = transactionsService.GetTransactionsByName(AccountNumber, fromtansactiondate, totansactiondate);
                return View("AccountActivity", list);
            }
            return RedirectToAction("AccountActivity");
        }

        public ActionResult PaymentSuccess()
        {
            return View();
        }
        public ActionResult PaymentUnsuccess()
        {
            return View();
        }

        public ActionResult UpdatePassword(SettingsModel settingsModel)
        {
            var user = (crediantials)Session["user"];
            String username;
            if (ModelState.IsValid)
            {
                if (user.AccountNumber != null)
                {
                    username = accountTableService.GetUserName(int.Parse(user.AccountNumber));
                }
                else
                {
                    username = user.username;
                }
                int count = accountTableService.GetNumberOfUsers(username);
                if (count == 2)
                {
                    accountTableService.UpdatePassword(accountTableService.GetAccountNumber(username, "SavingsAccount"), settingsModel.changePassword.password);
                    accountTableService.UpdatePassword(accountTableService.GetAccountNumber(username, "CurrentAccount"), settingsModel.changePassword.password);
                }
                else
                {
                    accountTableService.UpdatePassword(accountTableService.GetAccountNumber(username, accountTableService.getUserAccountType(username)), settingsModel.changePassword.password);
                }
            }
            return RedirectToAction("Signin", "Login");
        }
        public async Task<decimal> GetConversionRate(string country)
        {
            ApiServices apiservices = new ApiServices();

            decimal conversionrate = await apiservices.GetConversionRate(country);
            return conversionrate;
        }
        public ActionResult UpdateDetails(SettingsModel settingsModel)
        {
            var user = (crediantials)Session["user"];
            String username;
            if (ModelState.IsValid)
            {
                if (user.AccountNumber != null)
                {
                    username = accountTableService.GetUserName(int.Parse(user.AccountNumber));
                }
                else
                {
                    username = user.username;
                }
                int count = accountTableService.GetNumberOfUsers(username);
                if (count == 2)
                {
                    personalDetailsService.UpdateDetails(accountTableService.GetAccountNumber(username, "SavingsAccount"), settingsModel.changedetailes.email, settingsModel.changedetailes.phonenumber);
                    personalDetailsService.UpdateDetails(accountTableService.GetAccountNumber(username, "CurrentAccount"), settingsModel.changedetailes.email, settingsModel.changedetailes.phonenumber);
                }
                else
                {
                    personalDetailsService.UpdateDetails(accountTableService.GetAccountNumber(username, accountTableService.getUserAccountType(username)), settingsModel.changedetailes.email, settingsModel.changedetailes.phonenumber);
                }
            }
            return View("Settings", settingsModel);
        }

    }
}