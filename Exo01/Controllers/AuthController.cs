using Exo01.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Exo01.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }
        // POST: Auth
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            User user = new User(username, password);
            bool res = user.Login();
            if (res)
            {
                TempData["Username"] = user.Username;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Username or password incorrect";
                return View();
            }
        }
    }
}