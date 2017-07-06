using Associazione_Musicale.Models;
using Associazione_Musicale.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Associazione_Musicale.DB
{
    public class DB_Manager
    {
        #region Utente

        public static Utente utente_login(String email, String password)
        {
            Utente utente = null;

            try
            {
                using (Associazione_DBDataContext db = new Associazione_DBDataContext())
                {
                    var sql = from user in db.Utente
                              where user.Email == email
                              && user.Pass == password
                              select user;

                    if (sql != null)
                    {
                        utente = sql.FirstOrDefault();
                    }
                }
            }
            catch (Exception e)
            {

            }

            return utente;
        }

        #endregion

        #region Corso

        public static List<CorsoModel> corsi_getList()
        {
            List<CorsoModel> corsi = new List<CorsoModel>();

            try
            {
                using (Associazione_DBDataContext db = new Associazione_DBDataContext())
                {
                    var sql = from corso in db.Corso
                              join insegnante in db.Insegnante on corso.Insegnante equals insegnante.ID
                              select new CorsoModel(corso, insegnante);

                    if (sql != null)
                    {
                        corsi = sql.ToList();
                    }
                }
            }catch(Exception e)
            {

            }

            return corsi;
        }

        public static List<CorsoModel> corsi_getListByUser()
        {
            List<CorsoModel> corsi = new List<CorsoModel>();

            try
            {
                using (Associazione_DBDataContext db = new Associazione_DBDataContext())
                {
                    var sql = from corso in db.Corso
                              join insegnante in db.Insegnante on corso.Insegnante equals insegnante.ID
                              join iscrizione in db.IscrizioneCorso on corso.ID equals iscrizione.ID_Corso
                              where iscrizione.ID_Utente == SessionObjects.LoggedUser.ID
                              select new CorsoModel(corso, insegnante);

                    if (sql != null)
                    {
                        corsi = sql.ToList();
                    }
                }
            }
            catch (Exception e)
            {

            }

            return corsi;
        }

        #endregion

        #region Lezione

        public static LezioneModel prossimaLezione_get()
        {
            LezioneModel model = null;

            try
            {
                using (Associazione_DBDataContext db = new Associazione_DBDataContext())
                {
                    var sql = from lezione in db.Lezione
                              join corso in db.Corso on lezione.Corso equals corso.ID
                              join iscrizione in db.IscrizioneCorso on corso.ID equals iscrizione.ID_Corso
                              where iscrizione.ID_Utente == SessionObjects.LoggedUser.ID
                              //&& lezione.Giorno.Add(lezione.OraI) >= DateTime.Now
                              orderby lezione.OraI
                              select new LezioneModel(lezione, corso);

                    if (sql != null)
                    {
                        model = sql.FirstOrDefault();
                    }
                }
            }
            catch (Exception e)
            {

            }

            return model;
        }

        #endregion

        #region Prenotazioni

        public static List<PrenotazioneModel> prenotazioni_geList()
        {
            List<PrenotazioneModel> model = new List<PrenotazioneModel>();

            try
            {
                using (Associazione_DBDataContext db = new Associazione_DBDataContext())
                {
                    var sql = from prenotazione in db.Prenotazione
                              join sala in db.Sala on prenotazione.Sala equals sala.ID
                              join ponte in db.PrenotazioneStrumentazione on prenotazione.ID equals ponte.ID_Prenotazione into p
                              from ponte in p.DefaultIfEmpty()
                              join strumetazione in db.Strumentazione on ponte.ID_Strumentazione equals strumetazione.ID into s
                              from strumentazione in s.DefaultIfEmpty()
                              where prenotazione.Utente == SessionObjects.LoggedUser.ID
                              && prenotazione.Giorno.Add(prenotazione.OraI) >= DateTime.Now
                              orderby prenotazione.OraI
                              select new { Prenotazione = prenotazione, Sala = sala, Strumentazione  = (strumentazione != null ? strumentazione : null) };

                    if (sql != null)
                    {
                        var temp = sql.ToList();

                        if(temp.Count > 0)
                        {
                            foreach(var item in temp)
                            {
                                PrenotazioneModel tempModel = model.Where(x => x.ID == item.Prenotazione.ID).FirstOrDefault();
                                if(tempModel == null)
                                {
                                    tempModel = new PrenotazioneModel(item.Prenotazione, item.Sala);
                                    model.Add(tempModel);
                                }

                                if(item.Strumentazione != null)
                                {
                                    tempModel.Strumentazione.Add(new StrumentazioneModel(item.Strumentazione));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }

            return model;
        }

        #endregion

        #region Sala

        public static List<SalaModel> saleDisponibili_getList(DateTime dataOraInizio, DateTime dataOraFine)
        {
            List<SalaModel> model = new List<SalaModel>();

            try
            {
                using (Associazione_DBDataContext db = new Associazione_DBDataContext())
                {
                    var sql = from sala in db.Sala
                              join prenotazione in db.Prenotazione on sala.ID equals prenotazione.Sala into p
                              from prenotazione in p.DefaultIfEmpty()
                              where prenotazione == null
                              || (prenotazione.Giorno.Add(prenotazione.OraI) > dataOraInizio && prenotazione.Giorno.Add(prenotazione.OraI) > dataOraFine)
                              || (prenotazione.Giorno.Add(prenotazione.OraF) < dataOraInizio && prenotazione.Giorno.Add(prenotazione.OraF) < dataOraFine)
                              select new SalaModel(sala);

                    if (sql != null)
                    {
                        model = sql.Distinct().ToList();
                    }
                }
            }
            catch (Exception e)
            {

            }

            return model;
        }

        public static Boolean sala_prenota(DateTime dataOraInizio, DateTime dataOraFine, Int32 idSala)
        {            
            try
            {
                using (Associazione_DBDataContext db = new Associazione_DBDataContext())
                {
                    var sql = from sala in db.Sala
                              where sala.ID == idSala
                              select sala;

                    if (sql != null)
                    {
                        Sala sala = sql.FirstOrDefault();

                        if(sala != null)
                        {
                            Prenotazione newPrenotazione = new Prenotazione()
                            {
                                Sala = sala.ID,
                                Giorno = dataOraInizio.Date,
                                OraI = dataOraInizio.TimeOfDay,
                                OraF = dataOraFine.TimeOfDay,
                                Utente= SessionObjects.LoggedUser.ID
                            };

                            db.Prenotazione.InsertOnSubmit(newPrenotazione);

                            db.SubmitChanges();

                            return true;
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }

            return false;
        }

        #endregion
    }
}