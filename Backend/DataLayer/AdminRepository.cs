using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class AdminRepository:IAdminRepository
    {
        private string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HRDatabase;";

        public List<Admin> GetAllAdmin()
        {
            List<Admin> adminList = new List<Admin>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Admin";
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Admin admin = new Admin();
                    admin.idAdmin = reader.GetInt32(0);
                    admin.Email = reader.GetString(1);
                    admin.Password = reader.GetString(2);
                    adminList.Add(admin);
                }
            }
            return adminList;
        }

        public bool InsertAdmin(Admin admin)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = $"INSERT INTO Admin(Email, Password) VALUES('{admin.Email}','{admin.Password}')";
                int res = command.ExecuteNonQuery();
                return res > 0;
            }
        }

        public Admin GetByEmailAndPassword(string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Admin WHERE Email=@Email AND Password=@Password";
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Admin admin = new Admin();
                    admin.idAdmin = reader.GetInt32(0);
                    admin.Email = reader.GetString(1);
                    admin.Password = reader.GetString(2);
                    return admin;
                }
                else
                {
                    return null;
                }
            }
        }

    }
}
