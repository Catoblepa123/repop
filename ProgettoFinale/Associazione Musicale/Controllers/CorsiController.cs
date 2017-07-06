using Associazione_Musicale.DB;
using Associazione_Musicale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Associazione_Musicale.Controllers
{
    public class CorsiController : Controller
    {
        public ActionResult Index()
        {
            CorsiModel model = new CorsiModel();
            try
            {
                List<CorsoModel> corsi = DB_Manager.corsi_getList();

                model.Corsi = corsi;
            }catch(Exception e)
            {

            }

            return View(model);
        }
    }
}