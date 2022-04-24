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
    public class LoginController : Controller
    {
        public static RegisterViewModel viewModel;
        IAccountTableService accountTableService;
        IPersonalDetailsService personalDetailsService;
        ISecurityQuestionsService securityQuestionsService;
        ITransactionsService transactionsService;
        String username;

        //Constructor to get instance of service
        public LoginController(IAccountTableService _accountTableService, IPersonalDetailsService _personalDetailsService, ISecurityQuestionsService _securityQuestionsService, ITransactionsService _transactionsService)
        {
            accountTableService = _accountTableService;
            personalDetailsService = _personalDetailsService;
            securityQuestionsService = _securityQuestionsService;
            transactionsService = _transactionsService;
        }

        //First View available to the user when application run (To login or register)
        public ActionResult Signin()
        {
            Session.Add("user", null);
            return View();
        }

        //When Clicked Login button in Signin page
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
                        Session.Add("user", cred);
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
            else if (cred.AccountNumber!=null && cred.AccountNumber.ToString() != null)
            {
                if (cred.pin.ToString() != null)
                {
                    if (accountTableService.CheckUserPin(int.Parse(cred.AccountNumber), int.Parse(cred.pin)))
                    {
                        Session.Add("user", cred);
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

        //When Clicked Register option for registering new User in signin page(First Account Creation)
        public async Task<ActionResult> Register()
        {
            RegisterViewModel registerViewModel = new RegisterViewModel();
            Random rnd = new Random();
            int myRandomNo = rnd.Next(100000000, 999999999);
            registerViewModel.AccountNumber = myRandomNo;
            registerViewModel.SortCode1 = 12;
            registerViewModel.SortCode2 = 93;
            registerViewModel.SortCode3 = 64;
            return View(registerViewModel);
        }



        //For A New Users First Accounts Details When Clicked Next in Register page To goto Questions Page
        [HttpPost]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {

            if (!accountTableService.CheckUserName(registerViewModel.UserName))
            {
                registerViewModel.CompleteSortCode = int.Parse(registerViewModel.SortCode1.ToString() + registerViewModel.SortCode2.ToString() + registerViewModel.SortCode3.ToString());
                accountTableService.ValidateAccountType(registerViewModel);
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

        //When Clicked Next in Register Page For First Account Of The User
        public ActionResult Questions()
        {
            return View(new QuestionsViewModel());
        }

        //When Clicked Submit in the Questions Page to Save User's first account Data in the database
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

        public ActionResult ForgetPassword(ForgetPasswordModel forgetPasswordModel)
        {
            if (ModelState.IsValid)
            {
                if (forgetPasswordModel.securityQuestions != null && accountTableService.GetUserNumber(forgetPasswordModel.securityQuestions.username)!=null)
                {
                    forgetPasswordModel.securityQuestions.isMatched = securityQuestionsService.VerifyAnswers(forgetPasswordModel.securityQuestions, int.Parse(accountTableService.GetUserNumber(forgetPasswordModel.securityQuestions.username)));
                    if (forgetPasswordModel.securityQuestions.isMatched)
                    {
                        forgetPasswordModel.newPasswords = new NewPasswords();
                        forgetPasswordModel.newPasswords.username = forgetPasswordModel.securityQuestions.username;
                        return View("ChangePassword", forgetPasswordModel);
                    }
                    else
                    {
                        ModelState.AddModelError("securityQuestions.username", "Your UserName and Answers doesnot match!. Enter Correct Answers");
                        return View(forgetPasswordModel);
                    }
                }
                else if (forgetPasswordModel.securityQuestions != null)
                {
                    ModelState.AddModelError("securityQuestions.username", "Your UserName doesnot Exist!!. Enter Correct UserName");
                    return View(forgetPasswordModel);
                }
            }
            return View();
        }

        public ActionResult ChangePassword(ForgetPasswordModel forgetPasswordModel)
        {
            int count = accountTableService.GetNumberOfUsers(forgetPasswordModel.newPasswords.username);
            if (count == 2)
            {
                accountTableService.UpdatePassword(accountTableService.GetAccountNumber(forgetPasswordModel.newPasswords.username, "SavingsAccount"), forgetPasswordModel.newPasswords.password);
                accountTableService.UpdatePassword(accountTableService.GetAccountNumber(forgetPasswordModel.newPasswords.username, "CurrentAccount"), forgetPasswordModel.newPasswords.password);
                return View("Signin");
            }
            else
            {
                accountTableService.UpdatePassword(accountTableService.GetAccountNumber(forgetPasswordModel.newPasswords.username, accountTableService.getUserAccountType(forgetPasswordModel.newPasswords.username)), forgetPasswordModel.newPasswords.password);
                return View("Signin");
            }
            return View();
        }
    }
}