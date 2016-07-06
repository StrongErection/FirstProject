using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Entity;
using DTOs;
using BusinessLogic;
using DataAccessLayer;
using AutoMapper;
using Mapping;
namespace WcfWsService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DataAccessService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DataAccessService.svc or DataAccessService.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class Service : IService
    {
        private PlayerLogic playerLogic = new PlayerLogic();
        private TeamLogic teamLogic = new TeamLogic();
        public void AddPlayer(PlayerDTO player)
        {
            playerLogic.AddPlayer(StaticMapper.Map<Player, PlayerDTO>(player));
        }

        public PlayerDTO FindPlayer(int id)
        {
            return StaticMapper.Map<PlayerDTO, Player>(playerLogic.FindPlayer(id));
        }

        public ICollection<PlayerDTO> FindAllPlayers()
        {
            return StaticMapper.MapCollections<PlayerDTO, Player>(playerLogic.GetAllPlayers());
        }

        public void EditPlayer(PlayerDTO player)
        {
            playerLogic.EditPlayer(StaticMapper.Map<Player, PlayerDTO>(player));
        }

        public void DeletePlayer(int id)
        {
            playerLogic.DeletePlayer(id);
        }

        public TeamDTO FindTeam(int id)
        {
            return StaticMapper.Map<TeamDTO, Team>(teamLogic.FindTeam(id));
        }

        public ICollection<TeamDTO> FindAllTeams()
        {
            return StaticMapper.MapCollections<TeamDTO, Team>(teamLogic.GetAllTeams());
        }



    }
}
