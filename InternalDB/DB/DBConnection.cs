using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Npgsql;

namespace InternalDB
{
    public static class DBConnection
    {
        private const string connectionString = "Host=127.0.0.1; Port=5432; User Id=postgres; Password=password; Database=InternalDB";

        public static UserState AuthenticateUser(string username, string password)
        {
            UserState state= UserState.AuthenticationFailed;

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                NpgsqlCommand cmd = new NpgsqlCommand($"SELECT COUNT(username) FROM employee_usernamepassword WHERE username = '{username}' AND password = '{password}'", conn);
                NpgsqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {

                    DataTable dataTable = new DataTable();
                    dataTable.Load(dataReader);
                    var count = dataTable.Rows[0]["count"].ToString();

                    if(count == "1")
                    {
                        if (username == "admin")
                        {
                            state= UserState.AdminAuthenticated;
                        }
                        else
                        {
                            state= UserState.UserAuthenticated;
                        }
                    }
                }

                dataReader.Close();
            }
            return state;
        }
    }
}
