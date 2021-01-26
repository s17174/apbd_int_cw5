using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace apbd_int_cw5.Services
{
    public class RefreshTokenServices :IRefreshTokenServices
    {
        
        public bool CheckToken(Guid token)
        {
            using SqlConnection connection = new SqlConnection(SqlUtils.SqlUtils.DB_ADDRESS);
            using SqlCommand command = new SqlCommand();
            connection.Open();
            command.Connection = connection;
            command.CommandText = "SELECT * from RefreshToken WHERE Token = @token"; ;
            command.Parameters.AddWithValue("@token", token);
            var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                return false;
            }

            return true;
        }

        public void SetToken(Guid token)
        {
            using SqlConnection connection = new SqlConnection(SqlUtils.SqlUtils.DB_ADDRESS);
            using SqlCommand command = new SqlCommand();
            connection.Open();
            command.Connection = connection;
            command.CommandText = "INSERT INTO RefreshToken VALUES(@token)";
            command.Parameters.AddWithValue("@token", token);
            command.ExecuteNonQuery();
        }
    }
}
