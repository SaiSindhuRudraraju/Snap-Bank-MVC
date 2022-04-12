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
            SnapDbContext db = new SnapDbContext();
            //db.securityQuestions.Add(new Models.SecurityQuestions { BirthPlace="hyasdd", PetName="ca", FavouriteFood="ck" });
            //db.accountNumbers.Add(new Models.AccountNumber{ Date = "9819", number = 20 });
            //db.SaveChanges();
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
            return View(registerViewModel);
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            registerViewModel.CompleteSortCode = int.Parse(registerViewModel.SortCode1.ToString() + registerViewModel.SortCode2.ToString() + registerViewModel.SortCode3.ToString());
            if (ModelState.IsValid)
            {
                viewModel = registerViewModel;
                return RedirectToAction("Questions");
            }
            return View(registerViewModel);
        }

        public ActionResult Home()
        {
            return View();
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

        public ActionResult ForgetPassword()
        {
            return View();
        }
        public ActionResult Support()
        {
            return View();
        }
        public ActionResult Settings()
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
        public ActionResult FundTransfer()
        {
            FundTransferViewModel fundTransferViewModel = new FundTransferViewModel();
            fundTransferViewModel.selfAccountViewModel = new SelfAccountTransferViewModel();
            fundTransferViewModel.differentAccountTransferModel = new DifferentAccountTransferModel();
            return View();
        }
    }
}