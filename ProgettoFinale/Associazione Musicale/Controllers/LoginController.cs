using Associazione_Musicale.DB;
using Associazione_Musicale.Models;
using Associazione_Musicale.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Associazione_Musicale.Controllers
{
    public class LoginController : Controller
    {

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Utente utente = DB_Manager.utente_login(model.Email, model.Password);

            if(utente != null)
            {
                SessionObjects.LoggedUser = utente;

                return RedirectToAction("Index", "Riepilogo");
            }

            ModelState.AddModelError("", "Tentativo di accesso non valido.");

            return View(model);
        }

        public ActionResult Logout()
        {
            SessionObjects.LoggedUser = null;

            return RedirectToAction("Index", "Home");
        }
    }
}