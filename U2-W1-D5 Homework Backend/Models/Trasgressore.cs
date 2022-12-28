using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace U2_W1_D5_Homework_Backend.Models
{
    public class Trasgressore
    {
        public int IDAnagrafica { get; set; }
        [DisplayName("Cognome")]
        public string Cognome { get; set; }
        [DisplayName("Nome")]
        public string Nome { get; set; }
        [DisplayName("Indirizzo")]
        public string Indirizzo { get; set; }
        [DisplayName("Città")]
        public string Citta { get; set; }
        [DisplayName("CAP")]
        public string Cap { get; set; }
        [DisplayName("Cod. Fisc.")]
        public string Cod_Fisc { get; set; }

        public static Trasgressore GetTrasgressore(int id)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            Trasgressore trasg = new Trasgressore();
            try
            {
                con.Open();
                SqlDataReader reader = ConnectionClass.GetReader($"Select * from ANAGRAFICA where IDAnagrafica = {id}", con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        trasg.IDAnagrafica = Convert.ToInt32(reader["IDAnagrafica"]);
                        trasg.Cognome = reader["Cognome"].ToString();
                        trasg.Nome = reader["Nome"].ToString();
                        trasg.Indirizzo = reader["Indirizzo"].ToString();
                        trasg.Citta = reader["Citta"].ToString();
                        trasg.Cap = reader["CAP"].ToString();
                        trasg.Cod_Fisc = reader["Cod_Fisc"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
            return trasg;
        }

        public static List<Trasgressore> GetTrasgressori()
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            List<Trasgressore> ListaTrasgressori= new List<Trasgressore>();
            try
            {
                con.Open();
                SqlDataReader reader = ConnectionClass.GetReader("Select * from ANAGRAFICA", con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Trasgressore trasg = new Trasgressore();
                        trasg.IDAnagrafica = Convert.ToInt32(reader["IDAnagrafica"]);
                        trasg.Cognome = reader["Cognome"].ToString();
                        trasg.Nome = reader["Nome"].ToString();
                        trasg.Indirizzo = reader["Indirizzo"].ToString();
                        trasg.Citta = reader["Citta"].ToString();
                        trasg.Cap = reader["CAP"].ToString();
                        trasg.Cod_Fisc = reader["Cod_Fisc"].ToString();
                        ListaTrasgressori.Add(trasg);
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
            return ListaTrasgressori;
        }

        public static void CreateTrasgressore(Trasgressore trasg)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand();
                command.Parameters.AddWithValue("@COGNOME", trasg.Cognome);
                command.Parameters.AddWithValue("@NOME", trasg.Nome);
                command.Parameters.AddWithValue("@INDIRIZZO", trasg.Indirizzo);
                command.Parameters.AddWithValue("@CITTA", trasg.Citta);
                command.Parameters.AddWithValue("@CAP", trasg.Cap);
                command.Parameters.AddWithValue("@CODFISC", trasg.Cod_Fisc);

                command.CommandText = "Insert into ANAGRAFICA values (@COGNOME, @NOME, @INDIRIZZO, @CITTA, @CAP, @CODFISC)";
                command.Connection= con;
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close ();
        }

        public static Trasgressore EditTrasgressore(Trasgressore trasg, int id)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand();
                command.Parameters.AddWithValue("@COGNOME", trasg.Cognome);
                command.Parameters.AddWithValue("@NOME", trasg.Nome);
                command.Parameters.AddWithValue("@INDIRIZZO", trasg.Indirizzo);
                command.Parameters.AddWithValue("@CITTA", trasg.Citta);
                command.Parameters.AddWithValue("@CAP", trasg.Cap);
                command.Parameters.AddWithValue("@CODFISC", trasg.Cod_Fisc);

                command.CommandText = $"Update ANAGRAFICA set Cognome = @COGNOME, Nome = @NOME, Indirizzo = @INDIRIZZO, Citta = @CITTA, CAP = @CAP, Cod_Fisc = @CODFISC where IDAnagrafica = {id}";
                command.Connection = con;
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
            return trasg;
        }

        public static void DeleteTrasgressore(int id)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            try
            {
            con.Open();
            SqlDataReader reader = ConnectionClass.GetReader($"Delete from ANAGRAFICA where IDAnagrafica = {id}", con);
            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
        }
    }
}