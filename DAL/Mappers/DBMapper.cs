using DAL.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public static class DBMapper
    {
        public static User ToUser(this SqlDataReader reader)
        {
            return new User()
            {
                Id = Convert.ToInt32(reader["Id"]),
                Email = reader["Email"].ToString(),
                Password = reader["Password"].ToString(),
                Pseudo = reader["Pseudo"].ToString()
            };
        }
    }
}
