using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Associazione_Musicale.DB;

namespace Associazione_Musicale.Models
{
    public class LezioneModel
    {
        public int ID { get; set; }
        public int Numero { get; set; }
        public string Corso { get; set; }
        public String Giorno { get; set; }
        public String OraInizio { get; set; }
        public String OraFine { get; set; }
        public string Descrizione { get; set; }

        public LezioneModel()
        {

        }

        public LezioneModel(Lezione lezione, Corso corso)
        {
            ID = lezione.ID;
            Numero = lezione.Numero;
            Corso = corso.Nome;
            Giorno = lezione.Giorno.ToShortDateString();
            OraInizio = lezione.OraI.ToString(@"hh\:mm");
            OraFine = lezione.OraF.ToString(@"hh\:mm");
            Descrizione = lezione.Descrizione;
        }   


    }
}