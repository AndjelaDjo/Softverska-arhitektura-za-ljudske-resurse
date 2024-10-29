using DataLayer.Model;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ZaposleniRepository : IZaposleniRepository
    {
        private string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HRDatabase;";
        public List<Zaposleni> GetAllZaposleni()
        {
            List<Zaposleni> listaZaposleni = new List<Zaposleni>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Zaposleni";
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Zaposleni zaposleni = new Zaposleni();
                    zaposleni.idZaposlenog = reader.GetInt32(0);
                    zaposleni.Ime = reader.GetString(1);
                    zaposleni.Prezime = reader.GetString(2);
                    zaposleni.Email = reader.GetString(3);
                    zaposleni.Pozicija = reader.GetString(4);
                    zaposleni.Pol = reader.GetString(5);
                    zaposleni.Starost = reader.GetInt32(6);
                    zaposleni.idOdeljenja = reader.GetInt32(7);
                    listaZaposleni.Add(zaposleni);
                }
            }
            return listaZaposleni; 
        }

        public bool InsertZaposleni(Zaposleni zaposleni)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = $"INSERT INTO Zaposleni(Ime, Prezime, Email, Pozicija, Pol, Starost, idOdeljenja)" +
                    $"VALUES('{zaposleni.Ime}','{zaposleni.Prezime}','{zaposleni.Email}','{zaposleni.Pozicija}','{zaposleni.Pol}','{zaposleni.Starost}','{zaposleni.idOdeljenja}')";
                int res = command.ExecuteNonQuery();
                return res > 0;
            }
        }

        public bool UpdateZaposleni(Zaposleni zaposleni)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Zaposleni SET Ime=@Ime, Prezime=@Prezime, Email=@Email, Pozicija=@Pozicija, Pol=@Pol, Starost=@Starost WHERE IdZaposlenog=@idZaposlenog";
                command.Parameters.AddWithValue("@Ime", zaposleni.Ime);
                command.Parameters.AddWithValue("@Prezime", zaposleni.Prezime);
                command.Parameters.AddWithValue("@Email", zaposleni.Email);
                command.Parameters.AddWithValue("@Pozicija", zaposleni.Pozicija);
                command.Parameters.AddWithValue("@Pol", zaposleni.Pol);
                command.Parameters.AddWithValue("@Starost", zaposleni.Starost);
                command.Parameters.AddWithValue("@idOdeljenja", zaposleni.idOdeljenja);
                command.Parameters.AddWithValue("@idZaposlenog", zaposleni.idZaposlenog);
                int res = command.ExecuteNonQuery();
                return res > 0;
            }
        }

        public bool DeleteZaposleni(Zaposleni zaposleni)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Zaposleni WHERE IdZaposlenog=@idZaposlenog";
                command.Parameters.AddWithValue("@idZaposlenog", zaposleni.idZaposlenog);
                int res = command.ExecuteNonQuery();
                return res > 0;
            }
        }

        public int SumOfZaposleni()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM Zaposleni", connection);
                connection.Open();
                return (int)sqlCommand.ExecuteScalar();
            }
        }

        public int NumOfMen()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM Zaposleni WHERE Pol = 'Muški'", connection);
                connection.Open();
                object result = sqlCommand.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return 0;
                }
            }
        }

        public int NumOfWomen()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM Zaposleni WHERE Pol = 'Ženski'", connection);
                connection.Open();
                object result = sqlCommand.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return 0;
                }
            }
        }

        public decimal AvgYearsOfMen()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT AVG(Starost) FROM Zaposleni WHERE Pol = 'Muški'", connection);
                connection.Open();
                object result = sqlCommand.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    return Convert.ToDecimal(result);
                }
                else
                {
                    return 0;
                }
            }
        }


        public decimal AvgYearsOfWomen()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT AVG(Starost) FROM Zaposleni WHERE Pol = 'Ženski'", connection);
                connection.Open();
                object result = sqlCommand.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    return Convert.ToDecimal(result);
                }
                else
                {
                    return 0;
                }
            }
        }

        public Zaposleni GetNameSurnameById(int idZaposlenog)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Zaposleni WHERE IdZaposlenog=@idZaposlenog";
                command.Parameters.AddWithValue("@idZaposlenog", idZaposlenog);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Zaposleni zaposleni = new Zaposleni();
                    zaposleni.idZaposlenog = reader.GetInt32(0);
                    zaposleni.Ime = reader.GetString(1);
                    zaposleni.Prezime = reader.GetString(2);
                    zaposleni.Email = reader.GetString(3);
                    zaposleni.Pozicija = reader.GetString(4);
                    zaposleni.Pol = reader.GetString(5);
                    zaposleni.Starost = reader.GetInt32(6);
                    zaposleni.idOdeljenja = reader.GetInt32(7);

                    return zaposleni;
                }
                else
                {
                    
                    return null;
                }
            }
        }

    }

}
