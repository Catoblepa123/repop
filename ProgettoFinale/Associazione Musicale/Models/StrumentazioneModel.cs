using Associazione_Musicale.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Associazione_Musicale.Models
{
    public class StrumentazioneModel
    {
        public int ID { get; set; }
        public String  Nome { get; set; }
        public String Modello { get; set; }
        public String Descrizione { get; set; }

        public StrumentazioneModel()
        {

        }

        public StrumentazioneModel(Strumentazione strumentazione)
        {
            ID = strumentazione.ID;
            Nome = strumentazione.NomeGenerico;
            Modello = strumentazione.NomeModello;
            Descrizione = strumentazione.Descrizione;
        }
    }
}