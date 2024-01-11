using DAL.Entities;
using DAL.Interfaces;
using DAL.Mappers;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepositoryDb : IUserRepository
    {

        private readonly string _connectionString;
        public UserRepositoryDb(string connectionString)
        {
            _connectionString = connectionString;
        }


        
        public User Create(User user)
        {
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Users OUTPUT inserted.Id VALUES(@email, @password, @pseudo)";

                    cmd.Parameters.AddWithValue("email", user.Email);
                    cmd.Parameters.AddWithValue("password", user.Password);
                    cmd.Parameters.AddWithValue("pseudo", user.Pseudo);

                    conn.Open();

                    user.Id = Convert.ToInt32(cmd.ExecuteScalar());

                    conn.Close();

                    return user;

                }
            }
        }

        public bool Delete(User user)
        {
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM users WHERE id = @id";

                    cmd.Parameters.AddWithValue("id", user.Id);

                    conn.Open();

                    int rowAffected = cmd.ExecuteNonQuery();

                    conn.Close();

                    return rowAffected == 1;

                }
            }
        }

        public IEnumerable<User> GetAll()
        {
            using(SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd =  con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Users";

                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<User> users = new List<User>();

                    while (reader.Read())
                    {
                        users.Add(reader.ToUser());
                    }

                    con.Close();

                    return users;
                }
            }
        }

        public User? GetByEmail(string email)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                using(SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Users WHERE Email = @email";

                    cmd.Parameters.AddWithValue("email", email);

                    connection.Open();

                    SqlDataReader r = cmd.ExecuteReader();

                    User? u = null;

                    while (r.Read())
                    {
                        u = r.ToUser();
                    }

                    connection.Close();

                    return u;
                }
            }
        }

        public User? GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Users WHERE Id = @id";

                    cmd.Parameters.AddWithValue("id", id);

                    connection.Open();

                    SqlDataReader r = cmd.ExecuteReader();

                    User? u = null;

                    while (r.Read())
                    {
                        u = r.ToUser();
                    }

                    connection.Close();

                    return u;
                }
            }
        }

        public bool Update(User user)
        {
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                using(SqlCommand cmd =  conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Users SET Email = @email, Password = @password, Pseudo = @pseudo WHERE Id = @id";

                    cmd.Parameters.AddWithValue ("email", user.Email);
                    cmd.Parameters.AddWithValue("password", user.Password);
                    cmd.Parameters.AddWithValue("pseudo", user.Pseudo);
                    cmd.Parameters.AddWithValue("id", user.Id);

                    conn.Open();

                    int rowAffected = cmd.ExecuteNonQuery();

                    conn.Close();

                    return rowAffected == 1;
                }
            }
        }
    }
}
