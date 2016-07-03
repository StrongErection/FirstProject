using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
namespace DataAccessLayer
{
    public class UnitOfWork : IDisposable
    {
        private SoccerEntities context = new SoccerEntities();
        private IRepository<Player> playerRepository;
        private IRepository<Team> teamRepository;


        public IRepository<Player> PlayerRepository
        {
            get
            {

                if (this.playerRepository == null)
                {
                    IKernel ninjectKernel = new StandardKernel();
                    ninjectKernel.Bind<IRepository<Player>>().To<GenericRepository<Player>>()
                        .WithConstructorArgument("context", context);
                    playerRepository = ninjectKernel.Get<IRepository<Player>>();
                }
                return playerRepository;
            }
        }

        public IRepository<Team> TeamRepository
        {
            get
            {

                if (this.teamRepository == null)
                {
                    IKernel ninjectKernel = new StandardKernel();
                    ninjectKernel.Bind<IRepository<Team>>().To<GenericRepository<Team>>()
                        .WithConstructorArgument("context", context);
                    teamRepository = ninjectKernel.Get<IRepository<Team>>();
                }
                return teamRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
