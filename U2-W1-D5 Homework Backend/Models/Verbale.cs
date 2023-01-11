using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace U2_W1_D5_Homework_Backend.Models
{
    public class Verbale
    {
        public int IDVerbale { get; set; }
        [DisplayName("Data Violazione")]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataViolazione { get; set; }
        [DisplayName("Indirizzo Violazione")]
        public string IndirizzoViolazione { get; set; }
        [DisplayName("Agente")]
        public string NominativoAgente { get; set; }
        [DisplayName("Data Trascrizione")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataTrascrizioneVerbale { get; set; }
        [DisplayName("Importo")]
        public decimal Importo { get; set; }
        [DisplayName("Punti Decurtati")]
        public int DecurtamentoPunti { get; set; }
        public Trasgressore IDAnagrafica { get; set; }
        public Violazione IDViolazione { get; set; }
        [DisplayName("N° Verbali")]
        public int N_Verbali { get; set; }

        public static void CreateVerbale(Verbale verb, int id)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            //Violazione violazione = new Violazione();
            //verb.IDViolazione = violazione;
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand();
                command.Parameters.AddWithValue("@DATAVIOLAZIONE", verb.DataViolazione);
                command.Parameters.AddWithValue("@INDIRIZZO", verb.IndirizzoViolazione);
                command.Parameters.AddWithValue("@AGENTE", verb.NominativoAgente);
                command.Parameters.AddWithValue("@DATATRASCRIZIONE", verb.DataTrascrizioneVerbale);
                command.Parameters.AddWithValue("@IMPORTO", verb.Importo);
                command.Parameters.AddWithValue("@DECURTAMENTO", verb.DecurtamentoPunti);
                command.Parameters.AddWithValue("@IDANAGRAFICA", id);
                command.Parameters.AddWithValue("@IDVIOLAZIONE", verb.IDViolazione.IDViolazione);

                command.CommandText = "Insert into VERBALE values (@DATAVIOLAZIONE, @INDIRIZZO, @AGENTE, @DATATRASCRIZIONE, @IMPORTO, @DECURTAMENTO, @IDANAGRAFICA, @IDVIOLAZIONE)";
                command.Connection = con;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
        }

        public static Verbale GetVerbale (int id)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            Verbale verb = new Verbale();
            Violazione viol = new Violazione();
            try
            {
                con.Open();
                SqlDataReader reader = ConnectionClass.GetReader($"Select * from VERBALE where IDVerbale = {id}", con);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        verb.IDVerbale = Convert.ToInt32(reader["IDVerbale"]);
                        verb.DataViolazione = Convert.ToDateTime(reader["DataViolazione"]);
                        verb.IndirizzoViolazione = reader["IndirizzoViolazione"].ToString();
                        verb.NominativoAgente = reader["NominativoAgente"].ToString();
                        verb.DataTrascrizioneVerbale = Convert.ToDateTime(reader["DataTrascrizioneVerbale"]);
                        verb.Importo = Convert.ToDecimal(reader["Importo"]);
                        verb.DecurtamentoPunti = Convert.ToInt32(reader["DecurtamentoPunti"]);
                        verb.IDViolazione = viol;
                        verb.IDViolazione.IDViolazione = Convert.ToInt32(reader["IDViolazione"]);
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
            return verb;
        }

        public static Verbale EditVerbale(Verbale verb, int id)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand();
                command.Parameters.AddWithValue("@DATAVIOLAZIONE", verb.DataViolazione);
                command.Parameters.AddWithValue("@INDIRIZZOVIOLAZIONE", verb.IndirizzoViolazione);
                command.Parameters.AddWithValue("@NOMINATIVOAGENTE", verb.NominativoAgente);
                command.Parameters.AddWithValue("@DATATRASCRIZIONE", verb.DataTrascrizioneVerbale);
                command.Parameters.AddWithValue("@IMPORTO", verb.Importo);
                command.Parameters.AddWithValue("@DECURTAMENTOPUNTI", verb.DecurtamentoPunti);
                command.Parameters.AddWithValue("@IDVIOLAZIONE", verb.IDViolazione.IDViolazione);
                command.CommandText = $"Update VERBALE set DataViolazione = @DATAVIOLAZIONE, IndirizzoViolazione = @INDIRIZZOVIOLAZIONE, NominativoAgente = @NOMINATIVOAGENTE, DataTrascrizioneVerbale = @DATATRASCRIZIONE, Importo = @IMPORTO, DecurtamentoPunti = @DECURTAMENTOPUNTI, IDViolazione = @IDVIOLAZIONE where IDVerbale = {id}";
                command.Connection = con;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
            return verb;
        }

        public static void DeleteVerbale(int id)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            try
            {
                con.Open();
                SqlDataReader reader = ConnectionClass.GetReader($"Delete from VERBALE where IDVerbale = {id}", con);
            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
        }

        public static List<Verbale> GetVerbaliChart ()
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            List<Verbale> ClassificaVerbali = new List<Verbale>();
            try
            {
                con.Open();
                SqlDataReader reader = ConnectionClass.GetReader("Select Count(*) as N_Verbali, Cognome, Nome from VERBALE inner join ANAGRAFICA on VERBALE.IDAnagrafica = ANAGRAFICA.IDAnagrafica group by Cognome, Nome order by N_Verbali Desc", con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Verbale verb = new Verbale();
                        verb.N_Verbali = Convert.ToInt32(reader["N_Verbali"]);
                        Trasgressore trasg = new Trasgressore();
                        verb.IDAnagrafica = trasg;
                        verb.IDAnagrafica.Cognome = reader["Cognome"].ToString();
                        verb.IDAnagrafica.Nome = reader["Nome"].ToString();
                        ClassificaVerbali.Add(verb);
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
            return ClassificaVerbali;
        }

        public static List<Verbale> GetDecurtamentiChart()
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            List<Verbale> ClassificaDecurtamenti = new List<Verbale>();
            try
            {
                con.Open();
                SqlDataReader reader = ConnectionClass.GetReader("Select Sum(DecurtamentoPunti) as Punti_Decurtati, Cognome, Nome from VERBALE inner join ANAGRAFICA on VERBALE.IDAnagrafica = ANAGRAFICA.IDAnagrafica group by Cognome, Nome order by Punti_Decurtati desc", con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Verbale verb = new Verbale();
                        verb.DecurtamentoPunti = Convert.ToInt32(reader["Punti_Decurtati"]);
                        Trasgressore trasg = new Trasgressore();
                        verb.IDAnagrafica = trasg;
                        verb.IDAnagrafica.Cognome = reader["Cognome"].ToString();
                        verb.IDAnagrafica.Nome = reader["Nome"].ToString();
                        ClassificaDecurtamenti.Add(verb);
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
            return ClassificaDecurtamenti;
        }

        public static List<Verbale> GetDecurtamentiMaggioriDi(int punti)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            List<Verbale> ClassificaDecurtamenti = new List<Verbale>();
            try
            {
                con.Open();
                SqlDataReader reader = ConnectionClass.GetReader($"Select Cognome, Nome, DataViolazione, Importo, DecurtamentoPunti from VERBALE inner join ANAGRAFICA on VERBALE.IDAnagrafica = ANAGRAFICA.IDAnagrafica where DecurtamentoPunti >= {punti} order by DecurtamentoPunti desc", con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Verbale verb = new Verbale();
                        verb.DecurtamentoPunti = Convert.ToInt32(reader["DecurtamentoPunti"]);
                        verb.Importo = Convert.ToDecimal(reader["Importo"]);
                        verb.DataViolazione = Convert.ToDateTime(reader["DataViolazione"]);
                        Trasgressore trasg = new Trasgressore();
                        verb.IDAnagrafica = trasg;
                        verb.IDAnagrafica.Cognome = reader["Cognome"].ToString();
                        verb.IDAnagrafica.Nome = reader["Nome"].ToString();
                        ClassificaDecurtamenti.Add(verb);
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
            return ClassificaDecurtamenti;
        }

        public static List<Verbale> GetImportiMaggioriDi(decimal importo)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            List<Verbale> ClassificaImporti = new List<Verbale>();
            try
            {
                con.Open();
                SqlDataReader reader = ConnectionClass.GetReader($"Select Cognome, Nome, DataViolazione, DecurtamentoPunti, Importo from VERBALE inner join ANAGRAFICA on VERBALE.IDAnagrafica = ANAGRAFICA.IDAnagrafica where Importo >= {importo} order by Importo desc", con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Verbale verb = new Verbale();
                        verb.DataViolazione = Convert.ToDateTime(reader["DataViolazione"]);
                        verb.DecurtamentoPunti = Convert.ToInt32(reader["DecurtamentoPunti"]);
                        verb.Importo = Convert.ToDecimal(reader["Importo"]);
                        Trasgressore trasg = new Trasgressore();
                        verb.IDAnagrafica = trasg;
                        verb.IDAnagrafica.Cognome = reader["Cognome"].ToString();
                        verb.IDAnagrafica.Nome = reader["Nome"].ToString();
                        ClassificaImporti.Add(verb);
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
            return ClassificaImporti;
        }

        public static List<Verbale> GetVerbaliPerTrasg(int id)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            List<Verbale> ListaVerbaliTrasg = new List<Verbale>();
            try
            {
                con.Open();
                SqlDataReader reader = ConnectionClass.GetReader($"SELECT IDVerbale, DataViolazione, NominativoAgente, Importo, DecurtamentoPunti, Verbale.IDAnagrafica, TipoViolazione.IDViolazione, Descrizione FROM VERBALE inner join TIPOVIOLAZIONE ON VERBALE.IDViolazione = TIPOVIOLAZIONE.IDViolazione WHERE VERBALE.IDAnagrafica = {id} ORDER BY VERBALE.DataViolazione DESC", con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Verbale verb = new Verbale();
                        verb.IDVerbale = Convert.ToInt32(reader["IDVerbale"]);
                        verb.DataViolazione = Convert.ToDateTime(reader["DataViolazione"]);
                        verb.NominativoAgente = reader["NominativoAgente"].ToString();
                        verb.Importo = Convert.ToDecimal(reader["Importo"]);
                        verb.DecurtamentoPunti = Convert.ToInt32(reader["DecurtamentoPunti"]);
                        Violazione viol = new Violazione();
                        verb.IDViolazione = viol;
                        verb.IDViolazione.IDViolazione = Convert.ToInt32(reader["IDViolazione"]);
                        verb.IDViolazione.Descrizione = reader["Descrizione"].ToString();
                        ListaVerbaliTrasg.Add(verb);
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
            return ListaVerbaliTrasg;
        }

        public static int GetNumeroVerbali()
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            int numeroVerbali = 0;
            try
            {
                con.Open();
                SqlDataReader reader = ConnectionClass.GetReader("Select Count(*) as N_Verbali from VERBALE", con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {                      
                        numeroVerbali = Convert.ToInt32(reader["N_Verbali"]);
                        
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
            return numeroVerbali;
        }
    }
}