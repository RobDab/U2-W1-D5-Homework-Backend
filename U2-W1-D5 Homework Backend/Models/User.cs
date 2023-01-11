using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace U2_W1_D5_Homework_Backend.Models
{
    public class UserLogin
    {
        public int ID { get; set; }
        [Required]
        [NoSpace(ErrorMessage = "L'Username non deve contenere spazi")]
        [Remote("UsernameEsistente", "Users", ErrorMessage = "Username già esistente")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        private string _ruolo;
        public string Ruolo 
        {
            get 
            {
                if (_ruolo == null)
                {
                    return "";
                }
                else
                {
                    return _ruolo;
                }
            }
            set 
            {
                if (value == null)
                {
                    _ruolo = "";
                }
                else
                {
                    _ruolo = value;
                }
            }
        }


        public static bool Autenticato(string username, string password)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("Select * from [User] where Username=@username and [Password]=@password" , con);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    return true;
                }
                else
                {
                return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
            finally 
            { 
                con.Close();
            }
        }

        public static List<UserLogin> GetUsers()
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            List<UserLogin> ListaUtenti = new List<UserLogin>();
            try
            {
                con.Open();
                SqlDataReader reader = ConnectionClass.GetReader("Select * from [User]", con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UserLogin utente = new UserLogin();
                        utente.ID = Convert.ToInt32(reader["ID"]);
                        utente.Username = reader["Username"].ToString();
                        utente.Password = reader["Password"].ToString();
                        utente.Ruolo = reader["Ruolo"].ToString();
                        ListaUtenti.Add(utente);
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
            return ListaUtenti;
        }

        public static void CreateUser(UserLogin utente)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand();
                command.Parameters.AddWithValue("@username", utente.Username);
                command.Parameters.AddWithValue("@password", utente.Password);
                command.Parameters.AddWithValue("@ruolo", utente.Ruolo);

                command.CommandText = "Insert into [User] values (@username, @password, @ruolo)";
                command.Connection = con;
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
        }
    }
}