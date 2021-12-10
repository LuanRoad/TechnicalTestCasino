using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace DAL
{
    public class DatabaseContext: IDisposable
    {
        public string ConnectionString = "sqlServer";
        public SqlConnection con;

        /// <summary>
        /// Constructor to get the connection string and open connection 
        /// </summary>
        public DatabaseContext()
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString);
                con.Open();
            }
            catch (Exception ex)
            {
                var x = ex.Message;
            }
        }

        /// <summary>
        /// method to colse the connection
        /// </summary>
        public void CloseConnection()
        {
            con.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="at"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public SqlCommand NullorEmptyParameter(SqlCommand command, string at, string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                command.Parameters.AddWithValue("@"+at, DBNull.Value);
            }
            else
                command.Parameters.AddWithValue("@"+at, value);

            return command;
        }

        /// <summary>
        /// deispose of the class
        /// </summary>
        public void Dispose()
        {
            
        }

        /// <summary>
        /// Method used to execute query with ORM dapper 
        /// </summary>
        /// <param name="stpName">name of the stored procedure</param>
        /// <param name="parameters">parameters</param>
        public bool ExecuteQuery(string stpName, DynamicParameters parameters)
        {
            try
            {
                con.Execute(stpName, parameters, commandType: CommandType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                _ = ex.Message;
            }
            
        }

        /// <summary>
        /// method to use ORM dapper and get a generic entity
        /// </summary>
        /// <typeparam name="T">generic entity</typeparam>
        /// <param name="stpName">name stored procedure</param>
        /// <returns>List of generic entity</returns>
        public IList<T> Query<T>(string stpName)
        {
            var result = new List<T>();
            try
            {
                result = con.Query<T>("spGetAllPlayers", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// method to use ORM dapper and get a generic entity
        /// </summary>
        /// <typeparam name="T">generic entity</typeparam>
        /// <param name="stpName">name stored procedure</param>
        /// <param name="parameters">parameters</param>
        /// <returns>List of generic entity</returns>
        public IEnumerable<T> Query<T>(string stpName, DynamicParameters parameters)
        {
            try
            {
                return con.Query<T>(stpName, parameters,commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return new List<T>();
            }
        }
    }
}
