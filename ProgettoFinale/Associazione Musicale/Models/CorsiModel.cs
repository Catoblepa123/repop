using Associazione_Musicale.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Associazione_Musicale.Models
{
    public class CorsiModel
    {
        public List<CorsoModel> Corsi { get; set; }

        public CorsiModel()
        {

        }
    }

    public class CorsoModel
    {
        public int  Id { get; set; }
        public String Nome { get; set; }
        public String Descrizione { get; set; }
        public String  DataInizio { get; set; }
        public String DataFine { get; set; }
        public String Insegnante { get; set; }

        public CorsoModel(Corso corso, Insegnante insegnante)
        {
            Id = corso.ID;
            Nome = corso.Nome;
            Descrizione = corso.Descrizione;
            DataInizio = corso.DataI.ToShortDateString();
            DataFine = corso.DataF.ToShortDateString();
            Insegnante = insegnante.Nome + " " + insegnante.Cognome;
        }
    }

}