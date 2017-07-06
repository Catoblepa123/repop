using Associazione_Musicale.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Associazione_Musicale.Models
{
    public class PrenotazioneModel
    {
        public int ID { get; set; }
        public int IDUtente { get; set; }
        public String Sala { get; set; }
        public String Giorno { get; set; }
        public String OraInizio { get; set; }
        public String OraFine { get; set; }
        public List<StrumentazioneModel> Strumentazione { get; set; }

        public PrenotazioneModel()
        {
            Strumentazione = new List<StrumentazioneModel>();
        }

        public PrenotazioneModel(Prenotazione prenotazione, Sala sala)
        {
            ID = prenotazione.ID;
            IDUtente = prenotazione.Utente;
            Sala = sala.Nome;
            Giorno = prenotazione.Giorno.ToShortDateString();
            OraInizio = prenotazione.OraI.ToString(@"hh\:mm");
            OraFine = prenotazione.OraF.ToString(@"hh\:mm");

            Strumentazione = new List<StrumentazioneModel>();
        }
    }

}