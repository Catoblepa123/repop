using Associazione_Musicale.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Associazione_Musicale.Objects
{
    public class SessionObjects
    {
        public static Utente LoggedUser
        {
            get
            {
                if(HttpContext.Current.Session["LoggedUser_sd455"] != null)
                {

                return (Utente)HttpContext.Current.Session["LoggedUser_sd455"];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Session["LoggedUser_sd455"] = value;
            }
        }
    }
}