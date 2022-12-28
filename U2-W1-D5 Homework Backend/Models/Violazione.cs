using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace U2_W1_D5_Homework_Backend.Models
{
    public class Violazione
    {
        public int IDViolazione { get; set; }
        [DisplayName("Violazione")]
        public string Descrizione { get; set; }

        public static List<Violazione> GetViolazioni()
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            List<Violazione> ListaViolazioni = new List<Violazione>();
            try
            {
                con.Open();
                SqlDataReader reader = ConnectionClass.GetReader("Select * from TIPOVIOLAZIONE", con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Violazione viol = new Violazione();
                        viol.IDViolazione = Convert.ToInt32(reader["IDViolazione"]);
                        viol.Descrizione = reader["Descrizione"].ToString();
                        ListaViolazioni.Add(viol);
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
            return ListaViolazioni;
        }

        public static Violazione GetViolazione(int id)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            Violazione viol = new Violazione();
            try
            {
                con.Open();
                SqlDataReader reader = ConnectionClass.GetReader($"Select * from TIPOVIOLAZIONE where IDViolazione = {id}", con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        viol.IDViolazione = Convert.ToInt32(reader["IDViolazione"]);
                        viol.Descrizione = reader["Descrizione"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
            return viol;
        }

        public static void CreateViolazione(Violazione viol)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand();
                command.Parameters.AddWithValue("@DESCRIZIONE", viol.Descrizione);

                command.CommandText = "Insert into TIPOVIOLAZIONE values (@DESCRIZIONE)";
                command.Connection = con;
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
        }

        public static Violazione EditViolazione(Violazione viol, int id)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand();
                command.Parameters.AddWithValue("@DESCRIZIONE", viol.Descrizione);

                command.CommandText = $"Update TIPOVIOLAZIONE set Descrizione = @DESCRIZIONE where IDViolazione = {id}";
                command.Connection = con;
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
            return viol;
        }

        public static void DeleteViolazione(int id)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            try
            {
                con.Open();
                SqlDataReader reader = ConnectionClass.GetReader($"Delete from TIPOVIOLAZIONE where IDViolazione = {id}", con);
            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
        }

        public static List<SelectListItem> GetListaDropdownViolazione()
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            List<SelectListItem> ListaDropdownViolazioni = new List<SelectListItem>();
            try
            {
                con.Open();
                SqlDataReader reader = ConnectionClass.GetReader("Select * from TIPOVIOLAZIONE", con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                       SelectListItem item = new SelectListItem();
                       item.Value = reader["IDViolazione"].ToString();
                       item.Text = reader["Descrizione"].ToString();
                       ListaDropdownViolazioni.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
            return ListaDropdownViolazioni;
        }

        public static List<SelectListItem> GetListaDropdownViolazioneById(int id)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            List<SelectListItem> ListaDropdownViolazioni = new List<SelectListItem>();
            try
            {
                con.Open();
                SqlDataReader reader = ConnectionClass.GetReader($"Select * from TIPOVIOLAZIONE where IDViolazione = {id}", con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SelectListItem item = new SelectListItem();
                        item.Value = reader["IDViolazione"].ToString();
                        item.Text = reader["Descrizione"].ToString();
                        ListaDropdownViolazioni.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
            return ListaDropdownViolazioni;
        }
    }
}