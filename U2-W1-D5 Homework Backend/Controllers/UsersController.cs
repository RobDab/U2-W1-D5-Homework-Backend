using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using U2_W1_D5_Homework_Backend.Models;

namespace U2_W1_D5_Homework_Backend.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            return View(UserLogin.GetUsers());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserLogin utente)
        {
            if (ModelState.IsValid) 
            {
                UserLogin.CreateUser(utente);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
            
        }

        public JsonResult UsernameEsistente(string username)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            con.Open();
            SqlDataReader reader = ConnectionClass.GetReader($"Select Username from [User] where Username='{username}'", con);
            if (reader.HasRows)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}