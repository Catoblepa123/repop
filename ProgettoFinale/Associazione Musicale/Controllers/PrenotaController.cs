using Associazione_Musicale.Models;
using Associazione_Musicale.Objects;
using Associazione_Musicale.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Associazione_Musicale.Controllers
{
    public class PrenotaController : Controller
    {
        
        public ActionResult Sala()
        {
            if (SessionObjects.LoggedUser == null)
            {
                return RedirectToAction("Login", "Login");
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult CercaSale(DateTime giorno, Int32 oraI, Int32 oraF)
        {
            List<SalaModel> saleDisponibili = new List<SalaModel>();
            try
            {
                DateTime dataOraInizio = giorno.AddHours(oraI);
                DateTime dataOraFine = giorno.AddHours(oraF);

                saleDisponibili = DB_Manager.saleDisponibili_getList(dataOraInizio, dataOraFine);
                
            }catch(Exception e)
            {

            }
            
            return View("~/Views/Prenota/SaleDisponibili.cshtml", saleDisponibili);
        }

        public ActionResult PrenotaSala(DateTime giorno, Int32 oraI, Int32 oraF, Int32 idSala)
        {
            string messaggio = "";
            try
            {
                DateTime dataOraInizio = giorno.AddHours(oraI);
                DateTime dataOraFine = giorno.AddHours(oraF);

                Boolean esito = DB_Manager.sala_prenota(dataOraInizio, dataOraFine, idSala);
                if (esito)
                {
                    messaggio = "<p style='color:green'>Salvataggio prenotazione avvenuto con successo</p>";
                }
                else
                {
                    messaggio = "<p style='color:red'>Si è verificato un errore</p>";
                }
            }
            catch (Exception e)
            {

            }


            return Content(messaggio);
        }
    }
}