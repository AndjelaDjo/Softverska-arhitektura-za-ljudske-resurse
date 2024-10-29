using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataLayer
{
    public class OdsustvoRepository : IOdsustvoRepository
    {
        private string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HRDatabase;";

        public List<Odsustvo> GetAllOdsustva()
        {
            List<Odsustvo> listaOdsustva = new List<Odsustvo>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Odsustva";
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Odsustvo odsustvo = new Odsustvo();
                    odsustvo.idOdsustva = reader.GetInt32(0);
                    odsustvo.datumPocetka = reader.GetDateTime(1);
                    odsustvo.datumZavrsetka = reader.GetDateTime(2);
                    odsustvo.Razlog = reader.GetString(3);
                    odsustvo.idZaposlenog = reader.GetInt32(4);
                    listaOdsustva.Add(odsustvo);
                }
            }
            return listaOdsustva;
        }

        public bool InsertOdsustvo(Odsustvo odsustvo)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = $"INSERT INTO Odsustva(datumPocetka, datumZavrsetka, Razlog, idZaposlenog) " +
                    $"VALUES('{odsustvo.datumPocetka}','{odsustvo.datumZavrsetka}','{odsustvo.Razlog}','{odsustvo.idZaposlenog}')";
                int res = command.ExecuteNonQuery();
                return res > 0;
            }
        }

        public bool UpdateOdsustvo(Odsustvo odsustvo)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Odsustva SET DatumPocetka=@datumPocetka, DatumZavrsetka=@datumZavrsetka, Razlog=@Razlog, IdZaposlenog=@idZaposlenog WHERE IdOdsustva=@idOdsustva";
                command.Parameters.AddWithValue("@datumPocetka", odsustvo.datumPocetka);
                command.Parameters.AddWithValue("@datumZavrsetka", odsustvo.datumZavrsetka);
                command.Parameters.AddWithValue("@Razlog", odsustvo.Razlog);
                command.Parameters.AddWithValue("@idZaposlenog", odsustvo.idZaposlenog);
                command.Parameters.AddWithValue("@idOdsustva", odsustvo.idOdsustva);
                int res = command.ExecuteNonQuery();
                return res > 0;
            }
        }

        public bool DeleteOdsustvo(Odsustvo odsustvo)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Odsustva WHERE IdOdsustva=@idOdsustva";
                command.Parameters.AddWithValue("@idOdsustva", odsustvo.idOdsustva);
                int res = command.ExecuteNonQuery();
                return res > 0;
            }
        }

        public List<Odsustvo> OdsustvaPoZaposleniId(int idZaposlenog)
        {
            List<Odsustvo> odsustva = new List<Odsustvo>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string sqlCommand = "SELECT * FROM Odsustva WHERE IdZaposlenog = @idZaposlenog";
                SqlCommand command = new SqlCommand(sqlCommand, connection);
                command.Parameters.AddWithValue("@idZaposlenog", idZaposlenog);
                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Odsustvo odsustvo = new Odsustvo();
                    odsustvo.idOdsustva = dataReader.GetInt32(0);
                    odsustvo.datumPocetka = dataReader.GetDateTime(1);
                    odsustvo.datumZavrsetka = dataReader.GetDateTime(2);
                    odsustvo.Razlog = dataReader.GetString(3);
                    odsustvo.idZaposlenog = dataReader.GetInt32(4);
                    odsustva.Add(odsustvo);
                }
            }

            return odsustva;
        }

        public List<Odsustvo> OdsustvaPoDatumu(DateTime datumPocetka)
        {
            List<Odsustvo> odsustva = new List<Odsustvo>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string sqlCommand = "SELECT * FROM Odsustva WHERE CAST(DatumPocetka AS DATE) = CAST(@datumPocetka AS DATE)";
                SqlCommand command = new SqlCommand(sqlCommand, connection);
                command.Parameters.AddWithValue("@datumPocetka", datumPocetka);
                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Odsustvo odsustvo = new Odsustvo();
                    odsustvo.idOdsustva = dataReader.GetInt32(0);
                    odsustvo.datumPocetka = dataReader.GetDateTime(1);
                    odsustvo.datumZavrsetka = dataReader.GetDateTime(2);
                    odsustvo.Razlog = dataReader.GetString(3);
                    odsustvo.idZaposlenog = dataReader.GetInt32(4);
                    odsustva.Add(odsustvo);
                }
            }
            return odsustva;
        }

        public string CheckIfOdsustvoAllowed(int idZaposlenog, DateTime datumPocetka)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT idOdeljenja FROM Zaposleni WHERE idZaposlenog = @idZaposlenog", connection);
                command.Parameters.AddWithValue("@idZaposlenog", idZaposlenog);
                int idOdeljenja = (int)command.ExecuteScalar();

                command = new SqlCommand("SELECT COUNT(*) FROM Zaposleni WHERE idOdeljenja = @idOdeljenja", connection);
                command.Parameters.AddWithValue("@idOdeljenja", idOdeljenja);
                int ukupanBrojZaposlenih = (int)command.ExecuteScalar();

                command = new SqlCommand("SELECT COUNT(*) FROM Odsustva WHERE idZaposlenog IN (SELECT idZaposlenog FROM Zaposleni WHERE idOdeljenja = @idOdeljenja) AND @datumPocetka BETWEEN DatumPocetka AND DatumZavrsetka", connection);
                command.Parameters.AddWithValue("@idOdeljenja", idOdeljenja);
                command.Parameters.AddWithValue("@datumPocetka", datumPocetka);
                int zaposleniNaOdsustvu = (int)command.ExecuteScalar();

                double percentageOnLeave = (double)zaposleniNaOdsustvu / ukupanBrojZaposlenih;

                if (percentageOnLeave > 0.20)
                {
                    command = new SqlCommand("SELECT MIN(DatumZavrsetka) FROM Odsustva WHERE idZaposlenog IN (SELECT idZaposlenog FROM Zaposleni WHERE idOdeljenja = @idOdeljenja) AND DatumZavrsetka > @datumPocetka", connection);
                    command.Parameters.AddWithValue("@idOdeljenja", idOdeljenja);
                    command.Parameters.AddWithValue("@datumPocetka", datumPocetka);
                    DateTime? naredniDatum = (DateTime?)command.ExecuteScalar();

                    if (naredniDatum.HasValue)
                    {
                        return $"Currently, leave cannot be taken. The first available date is {naredniDatum.Value.ToString("yyyy-MM-dd")}.";
                    }
                    else
                    {
                        return "Currently, leave cannot be taken. Please try again later.";
                    }
                }
                else
                {
                    return "Leave can be approved.";
                }
            }
        }

        public List<Odsustvo> GetExistingLeaves(int idZaposlenog)
        {
            List<Odsustvo> existingLeaves = new List<Odsustvo>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string sqlCommand = "SELECT * FROM Odsustva WHERE idZaposlenog = @idZaposlenog AND DatumZavrsetka >= GETDATE()";
                SqlCommand command = new SqlCommand(sqlCommand, connection);
                command.Parameters.AddWithValue("@idZaposlenog", idZaposlenog);
                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Odsustvo odsustvo = new Odsustvo
                    {
                        idOdsustva = dataReader.GetInt32(0),
                        datumPocetka = dataReader.GetDateTime(1),
                        datumZavrsetka = dataReader.GetDateTime(2),
                        Razlog = dataReader.GetString(3),
                        idZaposlenog = dataReader.GetInt32(4)
                    };
                    existingLeaves.Add(odsustvo);
                }
            }

            return existingLeaves;
        }

    }
}
