
using DAL;
using Entities;
using System.Collections.Generic;
using System.Linq;

namespace BAL
{
    public class BalPlayer
    {
        private DaoPlayer playerDao = new DaoPlayer();

        /// <summary>
        /// this method is to call de DAL, but if it was neccessary to add more rules
        /// could be implemented here
        /// </summary>
        /// <param name="player">player's information</param>
        /// <returns>boolean if was corrected executed</returns>
        public bool InsertPlayer(Player player)
        {
            var result = playerDao.InsertDepper(player);

            return result;
        }

        /// <summary>
        /// this method is to call de DAL to get all players from the database, but if it was neccessary to add more rules
        /// could be implemented here
        /// </summary>
        /// <returns>List Players</returns>
        public List<Player> GetAllPlayers()
        {
            List<Player> listPlayers = new List<Player>();
            listPlayers = playerDao.GetAllDapper().ToList();

            return listPlayers;
        }

        /// <summary>
        /// this method is to call de DAL to deleted the player from the database, but if it was neccessary to add more rules
        /// could be implemented here
        /// </summary>
        /// <param name="id">identifier player</param>
        /// <returns>boolean if the proccess was executed successfuly</returns>
        public bool DeletePlayer(int id) 
        {
            var result = playerDao.Delete(id);
            return result;
        }

        /// <summary>
        /// this method is to call de DAL to get a player from the database, but if it was neccessary to add more rules
        /// could be implemented here
        /// </summary>
        /// <param name="id">identifier player</param>
        /// <returns>object player</returns>
        public Player GetAPlayer(int id)
        {
            var result = playerDao.GetAPlayerDapper(id);
            return result;
        }

        /// <summary>
        /// this method is to call de DAL to update the player from the database, but if it was neccessary to add more rules
        /// could be implemented here
        /// </summary>
        /// <param name="player">player information</param>
        /// <returns>boolean if the proccess was executed successfuly</returns>
        public bool UpdatePlayer(Player player) 
        {
            var result = playerDao.Update(player);
            return result;
        }
    }
}
