using Snap_Bank.Services;
using Snap_Bank.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Snap_Bank.Controllers
{
    public class LoginController : Controller
    {
        public static RegisterViewModel viewModel;
        IAccountTableService accountTableService;
        IPersonalDetailsService personalDetailsService;
        ISecurityQuestionsService securityQuestionsService;
        ITransactionsService transactionsService;

        public LoginController(IAccountTableService _accountTableService, IPersonalDetailsService _personalDetailsService, ISecurityQuestionsService _securityQuestionsService, ITransactionsService _transactionsService)
        {
            accountTableService = _accountTableService;
            personalDetailsService = _personalDetailsService;
            securityQuestionsService = _securityQuestionsService;
            transactionsService = _transactionsService;
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Signin()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Signin(crediantials cred, String redirectUrl)
        {
            if (cred.username != null)
            {
                if (cred.password != null)
                {
                    if (accountTableService.CheckUserPassword(cred.username, cred.password))
                    {
                        Session.Add("user", cred.username);
                        return RedirectToRoute(new { controller = "Snap", action = "Home" });
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
                        Session.Add("user", cred.AccountNumber);
                        return RedirectToRoute(new { controller = "Snap", action = "Home" });
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

            if (!String.IsNullOrEmpty(redirectUrl))
            {
                var controller_action = redirectUrl.Split('/');
                return RedirectToRoute(new { controller = controller_action[1], action = controller_action[2] });
            }
            return RedirectToRoute(new { controller = "Snap", action = "Home" });
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
            return View("Signin");
        }
    }
}