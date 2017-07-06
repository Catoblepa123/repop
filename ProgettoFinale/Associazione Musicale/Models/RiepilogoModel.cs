using Associazione_Musicale.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Associazione_Musicale.Models
{
    public class RiepilogoModel
    {
        public LezioneModel Lezione { get; set; }
        public List<CorsoModel> Corsi { get; set; }
        public List<PrenotazioneModel> Prenotazioni { get; set; }
    }

    
}