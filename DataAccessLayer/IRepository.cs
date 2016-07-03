using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IRepository<TEntity> where TEntity : class
    {
        ICollection<TEntity> GetAll();
        TEntity Find(int id);
        void Create(TEntity item);
        void Update(TEntity item);
        void Delete(int id);
        ICollection<Player> GetAllPlayers();
        ICollection<Team> GetAllTeams();
        ICollection<Player> GetPlayersFromTo_Age(int minAge, int maxAge);
        ICollection<Player> GetAllTeammates(int teamId);
    }
}
