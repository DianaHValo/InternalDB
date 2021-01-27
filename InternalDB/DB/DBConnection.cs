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
            UserState state= UserState.NotAuthenticated;

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

        public static bool AddNewUser(Employee employee)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                NpgsqlCommand insertUserCmd = new NpgsqlCommand($"INSERT INTO employee_usernamepassword (username, password) VALUES('{employee.username}', '{employee.password}')", conn);

                insertUserCmd.ExecuteNonQuery();

                NpgsqlCommand cmd = new NpgsqlCommand($"SELECT * FROM employee_usernamepassword WHERE username = '{employee.username}' ", conn);
                NpgsqlDataReader dataReader = cmd.ExecuteReader();


                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);
                var userId = dataTable.Rows[0]["id"].ToString();
             
                int userIDasINT;
                int.TryParse(userId, out userIDasINT);

                    NpgsqlCommand insertEmployeeDatacmd = new NpgsqlCommand($"INSERT INTO employee_data ( lastname, firstname, address, email, phone, username_id) " +
                    $"VALUES('{employee.lastName}', '{employee.firstName}', '{employee.adress}', '{employee.email}', '{employee.phone}','{userIDasINT}')" , conn);

                insertEmployeeDatacmd.ExecuteNonQuery();
                //NpgsqlDataReader dataReader2 = cmd2.ExecuteReader();
                //dataReader2.Close();
                //NpgsqlDataReader dataReader1 = cmd1.ExecuteReader();
                //dataReader1.Close();
                conn.Close();
            }

            return true;
        }

        public static bool DeleteUser(Employee employee)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                NpgsqlCommand cmd = new NpgsqlCommand($"DELETE FROM employee_usernamepassword WHERE username='{employee.username}'", conn);
                cmd.ExecuteNonQuery();
                conn.Close();

            }

            return true;
        }
    }
}
