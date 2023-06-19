using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace DB_5657_2057
{
    /// <summary>
    /// This class eases the communication with the SQL server
    /// </summary>
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
        /// Loads SQL File
        /// </summary>
        /// <param name="strQueryFile">SQL file path</param>
        /// <param name="info">local parameters</param>
        /// <returns>SQL file content</returns>
        public static string LoadQuery(string strQueryFile, params object[] info)
        {
            string query = "";
            using (StreamReader sr = new StreamReader(strQueryFile))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(sr.ReadToEnd().Replace("\r", "").Replace("\n", " "), info);
                query = sb.ToString();
            }

            return query;
        }

        /// <summary>
        /// Sends a query to the database
        /// </summary>
        /// <param name="query">Query</param>
        /// <returns>The requested data if provided</returns>
        /// <exception cref="Exception">If not connected to the database</exception>
        public static IEnumerable<IEnumerable<object>> Query(string query)
        {
            if (_connection.State == ConnectionState.Closed)
                throw new Exception("Not connected");


            MySqlDataReader rdr;

            try
            {
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                rdr = cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                throw e;
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
        /// Calls a procedure
        /// </summary>
        /// <param name="procedureName">Procedure name</param>
        /// <param name="inParams">IN parameters. Item Format: [0] Name; [1] Type; [2] Value</param>
        /// <param name="outParams">OUT parameters. Can be NULL if there are not OUT parameters.
        ///                         Item Format: [0] Name; [1] Type</param>
        /// <returns>If OUT paramters is not NULL, the result. Otherwise NULL</returns>
        public static IEnumerable<object> Procedure(string procedureName,
                                                    IEnumerable<List<object>> inParams,
                                                    IEnumerable<List<object>> outParams)
        {
            List<object> result = outParams == null ? null : new List<object>();

            try
            {
                MySqlCommand cmd = new MySqlCommand(procedureName, _connection);
                cmd.CommandType = CommandType.StoredProcedure;

                List<MySqlParameter> inParameters = new List<MySqlParameter>();
                foreach (var inParam in inParams)
                {
                    if (inParam.Count != 3)
                        throw new Exception("Bad parameters");

                    cmd.Parameters.Add(new MySqlParameter($"@{inParam[0]}", (MySqlDbType) inParam[1]));
                    cmd.Parameters[$"@{inParam[0]}"].Direction = ParameterDirection.Input;
                    cmd.Parameters[$"@{inParam[0]}"].Value = inParam[2];
                }

                cmd.Parameters.AddRange(inParameters.ToArray());

                foreach (var outParam in outParams)
                {
                    if (outParam.Count != 2)
                        throw new Exception("Bad parameters");

                    cmd.Parameters.Add(new MySqlParameter($"@{outParam[0]}", (MySqlDbType) outParam[1]));
                    cmd.Parameters[$"@{outParam[0]}"].Direction = ParameterDirection.Output;
                }

                cmd.ExecuteNonQuery();

                foreach (var outParam in outParams)
                    result.Add(cmd.Parameters[$"@{outParam[0]}"].Value);
            }
            catch (Exception e)
            {
                throw e;
            }

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