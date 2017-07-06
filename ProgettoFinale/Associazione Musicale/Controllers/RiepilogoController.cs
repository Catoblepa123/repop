using Associazione_Musicale.Models;
using Associazione_Musicale.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Associazione_Musicale.Objects;

namespace Associazione_Musicale.Controllers
{
    public class RiepilogoController : Controller
    {
        public ActionResult Index()
        {
            if(SessionObjects.LoggedUser == null)
            {                
                return RedirectToAction("Login", "Login");
            }

            RiepilogoModel model = new RiepilogoModel();

            try
            {
                model.Corsi = DB_Manager.corsi_getListByUser();
                model.Lezione = DB_Manager.prossimaLezione_get();
                model.Prenotazioni = DB_Manager.prenotazioni_geList();

            }catch(Exception e)
            {

            }

            return View(model);
        }
    }
}