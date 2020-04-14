using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SlackOverload.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string username)
        {
            if (username == "Admin")
            {
                HttpContext.Session.SetString("Username", "Admin");
                HttpContext.Session.SetInt32("UID", 12345);
                return View();
            }
            else
            {
                TempData["ErrorMsg"] = "User/password error!";
                return RedirectToAction("Index");
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return View();
        }
    }
}