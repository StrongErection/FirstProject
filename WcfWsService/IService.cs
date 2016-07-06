using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Entity;
using DTOs;
namespace WcfWsService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDataAccessService" in both code and config file together.
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        void AddPlayer(PlayerDTO player);

        [OperationContract]
        PlayerDTO FindPlayer(int id);

        [OperationContract]
        ICollection<PlayerDTO> FindAllPlayers();

        [OperationContract]
        void EditPlayer(PlayerDTO player);

        [OperationContract]
        void DeletePlayer(int id);

        [OperationContract]
        TeamDTO FindTeam(int id);

        [OperationContract]
        ICollection<TeamDTO> FindAllTeams();

    }
}
