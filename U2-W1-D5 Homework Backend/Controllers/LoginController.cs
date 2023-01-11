using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using U2_W1_D5_Homework_Backend.Models;

namespace U2_W1_D5_Homework_Backend.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn([Bind(Exclude ="Ruolo")] UserLogin U)
        {
            if (UserLogin.Autenticato(U.Username, U.Password))
            {
                FormsAuthentication.SetAuthCookie(U.Username, false);
                return Redirect(FormsAuthentication.DefaultUrl);
            }
            ViewBag.messaggio = "Username e/o Password errati";
            return View();
        }
    }
}