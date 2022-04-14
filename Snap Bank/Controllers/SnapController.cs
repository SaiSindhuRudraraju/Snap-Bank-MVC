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
            return View();
        }

        [HttpPost]
        public ActionResult Index(crediantials cred)
        {
            if (cred.username != null)
            {
                if (cred.password != null)
                {
                    if (accountTableService.CheckUserPassword(cred.username, cred.password))
                    {
                        return RedirectToAction("Home");
                    }
                    else
                    {
                        ModelState.AddModelError("password", "Incorrect Password!");
                        ModelState.AddModelError("AccountNumber", " ");
                        ModelState.AddModelError("pin", " ");
                        return View(cred);
                    }
                }
                else
                {
                    ModelState.AddModelError("password", "Enter Password!");
                    return View(cred);
                }
            }
            else if (cred.AccountNumber.ToString() != null)
            {
                if (cred.pin.ToString() != null)
                {
                    if (accountTableService.CheckUserPin(int.Parse(cred.AccountNumber), int.Parse(cred.pin)))
                    {
                        return RedirectToAction("Home");
                    }
                    else
                    {
                        ModelState.AddModelError("pin", "Incorrect Pin!");
                        ModelState.AddModelError("username", " ");
                        ModelState.AddModelError("password", " ");
                        return View(cred);
                    }
                }
                else
                {
                    ModelState.AddModelError("pin", "Enter Pin!");
                    return View(cred);
                }
            }
            return View();
        }

        public ActionResult Register()
        {
            RegisterViewModel registerViewModel = new RegisterViewModel();
            Random rnd = new Random();
            int myRandomNo = rnd.Next(100000000, 999999999);
            registerViewModel.AccountNumber = myRandomNo;
            registerViewModel.SortCode1 = 12;
            registerViewModel.SortCode2 = 93;
            registerViewModel.SortCode3 = 64;
            registerViewModel.AccountType = "Current Account";
            return View(registerViewModel);
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            if (!accountTableService.CheckUserName(registerViewModel.UserName))
            {
                registerViewModel.CompleteSortCode = int.Parse(registerViewModel.SortCode1.ToString() + registerViewModel.SortCode2.ToString() + registerViewModel.SortCode3.ToString());
                if (ModelState.IsValid)
                {
                    viewModel = registerViewModel;
                    return RedirectToAction("Questions");
                }
                return View(registerViewModel);
            }
            ModelState.AddModelError("UserName", "User Already Exists!");
            return View(registerViewModel);
        }
        public ActionResult Questions()
        {
            return View(new QuestionsViewModel());
        }

        [HttpPost]
        public ActionResult Questions(QuestionsViewModel questionsViewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.SecurityQuestion1 = questionsViewModel.SecurityQuestion1.ToString();
                viewModel.SecurityQuestion2 = questionsViewModel.SecurityQuestion2.ToString();
                viewModel.SecurityQuestion3 = questionsViewModel.SecurityQuestion3.ToString();
                //Save to DB

                accountTableService.Save(viewModel);
                personalDetailsService.Save(viewModel);
                securityQuestionsService.Save(viewModel);
            }
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
            //get detailes of user and set it to the input feilds in setting feild
            return View();
        }
        public ActionResult AddSavingsAccount()
        {
            //get personal detailes of user and set it to model
            return View("Register");
        }
        public ActionResult Home()
        {
            var user = (crediantials)Session["user"];
            if(user.username != null)
            {
                viewModel = accountTableService.GetUserByName(user.username);
            }
            else if(user.AccountNumber != null)
            {
                viewModel = accountTableService.GerUserByNumber(int.Parse(user.AccountNumber));
            }
            return View();
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