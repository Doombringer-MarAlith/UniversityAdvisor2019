using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebScripts;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountController _accountController;

        public AccountController(IAccountController accountController)
        {
            _accountController = accountController;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAccount()
        {
            var account = _accountController.GetAccount("das");
            ViewBag.GetAccount = account;
            return Index();
        }
    }
}