using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SlackOverload.Models;

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