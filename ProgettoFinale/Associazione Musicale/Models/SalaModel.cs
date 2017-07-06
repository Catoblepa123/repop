using Associazione_Musicale.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Associazione_Musicale.Models
{
    public class SalaModel
    {
        public int ID { get; set; }
        public String Nome { get; set; }
        public String  Descrizione { get; set; }

        public SalaModel()
        {

        }

        public SalaModel(Sala sala)
        {
            ID = sala.ID;
            Nome = sala.Nome;
            Descrizione = sala.Descrizione;
        }
    }
}