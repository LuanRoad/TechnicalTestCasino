using System;
using System.Collections.Generic;
using DAL.RepositoryPattern;
using Entities;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using System.Linq;

namespace DAL
{
    public class DaoPlayer : IPlayerDao
    {
        #region this methods were made using ado.net
        /// <summary>
        /// Method to get all players from the database
        /// </summary>
        /// <returns>list of players</returns>
        public IEnumerable<Player> GetAll()
        {
            var players = new List<Player>();

            try
            {
                using (DatabaseContext db = new DatabaseContext())
                {
                    using (SqlCommand cmd = new SqlCommand("spGetAllPlayers", db.con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Player player = new Player();
                                player.PlayerId = Convert.ToInt32(reader["PlayerId"]);
                                player.FirstName = Convert.ToString(reader["FirstName"]);
                                player.MiddleName = Convert.ToString(reader["MiddleName"]);
                                player.LastName = Convert.ToString(reader["LastName"]);
                                player.Age = Convert.ToInt32(reader["Age"]);

                                players.Add(player);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }

            return players;
        }

        /// <summary>
        /// get one player from the database
        /// </summary>
        /// <param name="id">identifier of player</param>
        /// <returns>just one player</returns>
        public Player GetById(int id)
        {
            try
            {
                Player player = new Player();
                using (DatabaseContext db = new DatabaseContext())
                {

                    using (SqlCommand cmd = new SqlCommand("spGetAPlayer", db.con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PlayerId", id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                player.PlayerId = Convert.ToInt32(reader["PlayerId"]);
                                player.FirstName = Convert.ToString(reader["FirstName"]);
                                player.MiddleName = Convert.ToString(reader["MiddleName"]);
                                player.LastName = Convert.ToString(reader["LastName"]);
                                player.Age = Convert.ToInt32(reader["Age"]);
                            }
                        }
                    }
                    db.CloseConnection();
                }
                return player;
            }
            catch (Exception ex)
            {
                return new Player();
            }
        }

        /// <summary>
        /// method to insert a player in the database
        /// </summary>
        /// <param name="player">object player with the properties</param>
        /// <returns>boolean to specify if the procedure was excecuted successfuly</returns>
        public bool Insert(Player player)
        {
            try
            {
                using (DatabaseContext db = new DatabaseContext())
                {
                    using (SqlCommand cmd = new SqlCommand("spInsertPlayer", db.con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FirstName", player.FirstName ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@MiddleName", player.MiddleName ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@LastName", player.LastName ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Age", player.Age);

                        if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (SqlException sqlex)
            {
                _ = sqlex.Message;
                return false;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// method to update the record in the database of a specify player
        /// </summary>
        /// <param name="player">player with new properties</param>
        /// <returns>boolean to specify if the procedure was excecuted successfuly</returns>
        public bool Update(Player player)
        {
            try
            {
                using (DatabaseContext db = new DatabaseContext())
                {
                    using (SqlCommand cmd = new SqlCommand("spUpdatePlayer", db.con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@PlayerId", player.PlayerId);
                        cmd.Parameters.AddWithValue("@FirstName", player.FirstName ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@MiddleName", player.MiddleName ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@LastName", player.LastName ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Age", player.Age);

                        if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (SqlException sqlex)
            {
                _ = sqlex.Message;
                return false;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Method to delete a player from the database
        /// </summary>
        /// <param name="id">unique identifier of player in the database</param>
        /// <returns>boolean to indicate if the procedure was excecuted successfuly</returns>
        public bool Delete(int id)
        {
            try
            {
                using (DatabaseContext db = new DatabaseContext())
                {
                    using (SqlCommand cmd = new SqlCommand("spDeletePlayer", db.con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@PlayerId", id);

                        if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (SqlException sqlex)
            {
                _ = sqlex.Message;
                return false;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return false;
            }
        }
        #endregion


        #region this methods were made using ORM dapper
        /// <summary>
        /// method that use dapper to execute a store
        /// </summary>
        /// <param name="player">players' informtion</param>
        /// <returns>boolean</returns>
        public bool InsertDepper(Player player)
        {
            try
            {
                using (DatabaseContext db = new DatabaseContext())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("FirstName", player.FirstName);
                    parameters.Add("MiddleName", player.MiddleName);
                    parameters.Add("LastName", player.LastName);
                    parameters.Add("Age", player.Age);

                    return db.ExecuteQuery("spInsertPlayer", parameters);
                }
            }
            catch (SqlException sqlex)
            {
                _ = sqlex.Message;
                return false;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// method to use dapper to get all the players from the database
        /// </summary>
        /// <returns>players list</returns>
        public IEnumerable<Player> GetAllDapper()
        {
            var players = new List<Player>();

            try
            {
                using (DatabaseContext db = new DatabaseContext())
                {
                    players = db.Query<Player>("spGetAllPlayers").ToList();
                    //players = sql.Query<Player>("spGetAllPlayers",commandType:CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }

            return players;
        }

        /// <summary>
        /// method to use dapper to get a players from the database
        /// </summary>
        /// <param name="id">identifier player</param>
        /// <returns></returns>
        public Player GetAPlayerDapper(int id)
        {
            Player player = new Player();
            try
            {
                using (DatabaseContext db = new DatabaseContext())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("PlayerId", id);
                    player = db.Query<Player>("spGetAPlayer", parameters).ToList().FirstOrDefault();
                    //players = sql.Query<Player>("spGetAllPlayers",commandType:CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }

            return player;
        }

        #endregion
    }
}
