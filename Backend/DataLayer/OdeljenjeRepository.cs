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
    public class OdeljenjeRepository : IOdeljenjeRepository
    {
        private string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HRDatabase;";
        public List<Odeljenje> GetAllOdeljenja()
        {
            List<Odeljenje> listaOdeljenja = new List<Odeljenje>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Odeljenja";
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Odeljenje odeljenje = new Odeljenje();
                    odeljenje.idOdeljenja = reader.GetInt32(0);
                    odeljenje.Naziv = reader.GetString(1);
                    odeljenje.Opis = reader.GetString(2);
                    listaOdeljenja.Add(odeljenje);
                }
            }
            return listaOdeljenja;
        }

        public bool InsertOdeljenje(Odeljenje odeljenje)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = $"INSERT INTO Odeljenja(Naziv, Opis)" +
                    $"VALUES('{odeljenje.Naziv}','{odeljenje.Opis}')";
                int res = command.ExecuteNonQuery();
                return res > 0;
            }
        }

        public bool UpdateOdeljenje(Odeljenje odeljenje)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Odeljenja SET Naziv=@Naziv, Opis=@Opis WHERE IdOdeljenja=@idOdeljenja";
                command.Parameters.AddWithValue("@Naziv", odeljenje.Naziv);
                command.Parameters.AddWithValue("@Opis", odeljenje.Opis);
                command.Parameters.AddWithValue("@idOdeljenja", odeljenje.idOdeljenja);
                int res = command.ExecuteNonQuery();
                return res > 0;
            }
        }

        public bool DeleteOdeljenje(Odeljenje odeljenje)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Odeljenja WHERE IdOdeljenja=@idOdeljenja";
                command.Parameters.AddWithValue("@idOdeljenja", odeljenje.idOdeljenja);
                int res = command.ExecuteNonQuery();
                return res > 0;
            }
        }

        public decimal NumOfZaposlenihPoOdeljenju(string Naziv)
        {
            int idOdeljenja;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string sqlCommand = "SELECT idOdeljenja FROM Odeljenja WHERE Naziv = @Naziv";
                SqlCommand command = new SqlCommand(sqlCommand, connection);
                command.Parameters.AddWithValue("@Naziv", Naziv);
                connection.Open();

                var result = command.ExecuteScalar();
                if (result != null)
                {
                    idOdeljenja = Convert.ToInt32(result);
                }
                else
                {
                    return 0;
                }
            }

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string sqlCommand = "SELECT COUNT(*) FROM Zaposleni WHERE idOdeljenja = @idODeljenja";
                SqlCommand command = new SqlCommand(sqlCommand, connection);
                command.Parameters.AddWithValue("@idOdeljenja", idOdeljenja);
                connection.Open();

                object result = command.ExecuteScalar();
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
    }
}
