using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace U2_W1_D5_Homework_Backend.Models
{
    public class ConnectionClass
    {
        public static SqlConnection GetConnectionDB()
        {
            string constring = ConfigurationManager.ConnectionStrings["MunicipaleDB"].ToString();
            SqlConnection con = new SqlConnection(constring);
            return con;
        }

        public static SqlDataReader GetReader(string commandtext, SqlConnection con) {
            SqlCommand command = new SqlCommand(commandtext, con);
            SqlDataReader reader = command.ExecuteReader();
            return reader;
        }
    }
}