using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Mapping;
namespace DataAccessLayer
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal SoccerEntities context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(SoccerEntities context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public TEntity Find(int id)
        {
            return dbSet.Find(id);
        }

        public void Create(TEntity item)
        {
            dbSet.Add(item);
          
        }

        public void Update(TEntity item)
        {
            context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            TEntity entity = dbSet.Find(id);
            dbSet.Remove(entity);
        }

        public ICollection<TEntity> GetAll()
        {
            return dbSet.ToList<TEntity>();
        }

        public ICollection<Player> GetAllPlayers()
        {
            return context.Players.Include(p => p.Team).ToList<Player>();
        }

        public ICollection<Team> GetAllTeams()
        {
            return context.Teams.Include(t => t.Players).ToList<Team>();
        }
        //stored procedure
        public ICollection<Player> GetPlayersFromTo_Age(int minAge, int maxAge)
        {
            return StaticMapper.MapCollections<Player, AgeRange_Result>(context.AgeRange(minAge, maxAge).ToList<AgeRange_Result>());
        }

        public ICollection<Player> GetAllTeammates(int teamId)
        {
            return context.Players.Where(p => p.TeamId == teamId).ToList<Player>();
        }

    }
}
