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
        IAccountTableService accountTableService;

        public LoginController(IAccountTableService _accountTableService)
        {
            accountTableService = _accountTableService;
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
    }
}