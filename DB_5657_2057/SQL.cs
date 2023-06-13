using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace DB_5657_2057
{
    public static class SQL
    {
        private static MySqlConnection _connection;

        /// <summary>
        /// Connects and logs into the database
        /// </summary>
        /// <param name="server">Hosting server of the database</param>
        /// <param name="database">Database name</param>
        /// <param name="user">Username</param>
        /// <param name="password">Password</param>
        public static void Login(string server, string database, string user, string password)
        {
            string connectionStr = $"server={server};database={database};user={user};password={password}";

            _connection = new MySqlConnection(connectionStr);

            try
            {
                _connection.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Sends a query to the database
        /// </summary>
        /// <param name="query">Query</param>
        /// <returns>The requested data if provided</returns>
        /// <exception cref="Exception">If not connected to the database</exception>
        public static IEnumerable<IEnumerable<object>> Query(string query)
        {
            if (_connection.State == System.Data.ConnectionState.Closed)
                throw new Exception("Not connected");


            MySqlDataReader rdr;

            try
            {
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                rdr = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            List<object[]> result = new List<object[]>();

            while (rdr.Read())
            {
                object[] temp = new object[rdr.FieldCount];
                rdr.GetValues(temp);
                result.Add(temp);
            }

            rdr.Close();

            return result;
        }

        /// <summary>
        /// Logs out and disconnects from the database
        /// </summary>
        /// <exception cref="Exception">If not connected to the database</exception>
        public static void Logout()
        {
            if (_connection.State == System.Data.ConnectionState.Closed)
                throw new Exception("Not connected");

            _connection.Close();
        }
    }
}