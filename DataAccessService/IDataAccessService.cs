using System.Runtime.Serialization;
using System.Collections.Generic;
using System.ServiceModel;
using DTOs;
namespace DataAccessService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDataAccessService" in both code and config file together.
    [ServiceContract(SessionMode = SessionMode.Required)]

    public interface IDataAccessService
    {
        [TransactionFlow(TransactionFlowOption.Mandatory)]
        [FaultContract(typeof(ServiceData))]
        [OperationContract]
        void AddPlayer(PlayerDTO player);

        [TransactionFlow(TransactionFlowOption.Allowed)]
        [FaultContract(typeof(ServiceData))]
        [OperationContract]
        PlayerDTO FindPlayer(int id);

        [TransactionFlow(TransactionFlowOption.Allowed)]
        [FaultContract(typeof(ServiceData))]
        [OperationContract]
        ICollection<PlayerDTO> FindAllPlayers();

        [TransactionFlow(TransactionFlowOption.Mandatory)]
        [FaultContract(typeof(ServiceData))]
        [OperationContract]
        void EditPlayer(PlayerDTO player);

        [TransactionFlow(TransactionFlowOption.Mandatory)]
        [FaultContract(typeof(ServiceData))]
        [OperationContract]
        void DeletePlayer(int id);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        [FaultContract(typeof(ServiceData))]
        TeamDTO FindTeam(int id);

        [TransactionFlow(TransactionFlowOption.Allowed)]
        [FaultContract(typeof(ServiceData))]
        [OperationContract]
        ICollection<TeamDTO> FindAllTeams();

        [TransactionFlow(TransactionFlowOption.Allowed)]
        [FaultContract(typeof(ServiceData))]
        [OperationContract]
        ICollection<PlayerDTO> FilterPlayersByAge(int minAge, int maxAge);
    }

    [DataContract]
    public class ServiceData
    {
        [DataMember]
        public string ErrorMessage { get; set; }
        [DataMember]
        public string ErrorDetails { get; set; }
    }

}
