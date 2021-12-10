using System.Collections.Generic;
using Entities;

namespace DAL.RepositoryPattern
{
    /// <summary>
    /// Interface contract with the daoPlayer
    /// </summary>
    interface IPlayerDao
    {
        IEnumerable<Player> GetAll();
        Player GetById(int id);
        bool Insert(Player obj);
        bool Update(Player obj);
        bool Delete(int id);
        bool InsertDepper(Player player);
        IEnumerable<Player> GetAllDapper();
        Player GetAPlayerDapper(int id);
    }
}
