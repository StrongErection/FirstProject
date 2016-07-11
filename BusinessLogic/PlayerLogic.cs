
using System.Collections.Generic;
using DataAccessLayer;
namespace BusinessLogic
{
    public class PlayerLogic
    {

        public void AddPlayer(Player player)
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                unit.PlayerRepository.Create(player);
                unit.Save();
            }
        }

        public Player FindPlayer(int id)
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                return unit.PlayerRepository.Find(id);
            }
        }
        public ICollection<Player> GetAllPlayers()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                return unit.PlayerRepository.GetAllPlayers();
            }
        }
        public void EditPlayer(Player player)
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                unit.PlayerRepository.Update(player);
                unit.Save();
            }
        }
        public void DeletePlayer(int id)
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                unit.PlayerRepository.Delete(id);
                unit.Save();
            }
        }

        public ICollection<Player> FilterByAge(int minAge, int maxAge)
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                return unit.PlayerRepository.GetPlayersFromTo_Age(minAge, maxAge);
            }
        }
    }
}
