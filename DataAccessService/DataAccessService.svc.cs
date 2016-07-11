using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Data;
using System.Transactions;
using DTOs;
using BusinessLogic;
using DataAccessLayer;
using Mapping;
using AutoMapper;

namespace DataAccessService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DataAccessService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DataAccessService.svc or DataAccessService.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class DataAccessService : IDataAccessService
    {
        private PlayerLogic playerLogic = new PlayerLogic();
        private TeamLogic teamLogic = new TeamLogic();

        [OperationBehavior(TransactionScopeRequired = true,
            TransactionAutoComplete = true)]
        public void AddPlayer(PlayerDTO player)
        {
            try
            {
                playerLogic.AddPlayer(StaticMapper.Map<Player, PlayerDTO>(player));
            }
            #region Catches
            catch(EntityException ex)
            {
                ThrowFault(ex, "Unable to connect to database");
            }
            catch(AutoMapperConfigurationException ex)
            {
                ThrowFault(ex, "Invalid mapper configuration");
            }
            catch(AutoMapperMappingException ex)
            {
                ThrowFault(ex, "Unsuccessful mapping");
            }
            catch(Exception ex)
            {
                ThrowFault(ex, "Unknown server error");
            }
            #endregion
        }

        [OperationBehavior(TransactionScopeRequired = true,
            TransactionAutoComplete = true)]
        public PlayerDTO FindPlayer(int id)
        {
            try
            {
                return StaticMapper.Map<PlayerDTO, Player>(playerLogic.FindPlayer(id));
            }
            #region Catches
            catch (EntityException ex)
            {
                ThrowFault(ex, "Unable to connect to database");
            }
            catch (AutoMapperConfigurationException ex)
            {
                ThrowFault(ex, "Invalid mapper configuration");
            }
            catch (AutoMapperMappingException ex)
            {
                ThrowFault(ex, "Unsuccessful mapping");
            }
            catch (NullReferenceException ex)
            {
                ThrowFault(ex, "Requested item not found");
            }
            catch (Exception ex)
            {
                ThrowFault(ex, "Unknown server error");
            }
            #endregion
            return new PlayerDTO();
        }


        [OperationBehavior(TransactionScopeRequired = true,
            TransactionAutoComplete = true)]
        public ICollection<PlayerDTO> FindAllPlayers()
        {
            ServiceData serviceData = new ServiceData();
            try
            {
                return StaticMapper.MapCollections<PlayerDTO, Player>(playerLogic.GetAllPlayers());
            }
            #region Catches
            catch (EntityException ex)
            {
                ThrowFault(ex, "Unable to connect to database");
            }
            catch (AutoMapperConfigurationException ex)
            {
                ThrowFault(ex, "Invalid mapper configuration");
            }
            catch (AutoMapperMappingException ex)
            {
                ThrowFault(ex, "Unsuccessful mapping");
            }
            catch (NullReferenceException ex)
            {
                ThrowFault(ex, "Requested item not found");
            }
            catch (Exception ex)
            {
                ThrowFault(ex, "Unknown server error");
            }
            #endregion
            return new List<PlayerDTO>();
        }

        [OperationBehavior(TransactionScopeRequired = true,
            TransactionAutoComplete = true)]
        public void EditPlayer(PlayerDTO player)
        {
            try
            {
                playerLogic.EditPlayer(StaticMapper.Map<Player, PlayerDTO>(player));
            }
            #region Catches
            catch (EntityException ex)
            {
                ThrowFault(ex, "Unable to connect to database");
            }
            catch (AutoMapperConfigurationException ex)
            {
                ThrowFault(ex, "Invalid mapper configuration");
            }
            catch (AutoMapperMappingException ex)
            {
                ThrowFault(ex, "Unsuccessful mapping");
            }
            catch (NullReferenceException ex)
            {
                ThrowFault(ex, "Requested item not found");
            }
            catch (Exception ex)
            {
                ThrowFault(ex, "Unknown server error");
            } 
            #endregion
        }

        [OperationBehavior(TransactionScopeRequired = true,
            TransactionAutoComplete = true)]
        public void DeletePlayer(int id)
        {
            try
            {
                playerLogic.DeletePlayer(id);
            }
            #region Catches
            catch (EntityException ex)
            {
                ThrowFault(ex, "Unable to connect to database");
            }
            catch (AutoMapperConfigurationException ex)
            {
                ThrowFault(ex, "Invalid mapper configuration");
            }
            catch (AutoMapperMappingException ex)
            {
                ThrowFault(ex, "Unsuccessful mapping");
            }
            catch (NullReferenceException ex)
            {
                ThrowFault(ex, "Requested item not found");
            }
            catch (Exception ex)
            {
                ThrowFault(ex, "Unknown server error");
            } 
            #endregion
        }

        [OperationBehavior(TransactionScopeRequired = true,
            TransactionAutoComplete = true)]
        public TeamDTO FindTeam(int id)
        {
            try
            {
                return StaticMapper.Map<TeamDTO, Team>(teamLogic.FindTeam(id));
            }
            #region Catches
            catch (EntityException ex)
            {
                ThrowFault(ex, "Unable to connect to database");
            }
            catch (AutoMapperConfigurationException ex)
            {
                ThrowFault(ex, "Invalid mapper configuration");
            }
            catch (AutoMapperMappingException ex)
            {
                ThrowFault(ex, "Unsuccessful mapping");
            }
            catch (NullReferenceException ex)
            {
                ThrowFault(ex, "Requested item not found");
            }
            catch (Exception ex)
            {
                ThrowFault(ex, "Unknown server error");
            }
            #endregion
            return new TeamDTO();
        }

        [OperationBehavior(TransactionScopeRequired = true,
            TransactionAutoComplete = true)]
        public ICollection<TeamDTO> FindAllTeams()
        {
            try
            {
                return StaticMapper.MapCollections<TeamDTO, Team>(teamLogic.GetAllTeams());
            }
            #region Catches
            catch (EntityException ex)
            {
                ThrowFault(ex, "Unable to connect to database");
            }
            catch (AutoMapperConfigurationException ex)
            {
                ThrowFault(ex, "Invalid mapper configuration");
            }
            catch (AutoMapperMappingException ex)
            {
                ThrowFault(ex, "Unsuccessful mapping");
            }
            catch (NullReferenceException ex)
            {
                ThrowFault(ex, "Requested item not found");
            }
            catch (Exception ex)
            {
                ThrowFault(ex, "Unknown server error");
            }
            #endregion
            return new List<TeamDTO>();
        }

        [OperationBehavior(TransactionScopeRequired = true,
            TransactionAutoComplete = true)]
        public ICollection<PlayerDTO> FilterPlayersByAge(int minAge, int maxAge)
        {
            try
            {
                return StaticMapper.MapCollections<PlayerDTO, Player>(playerLogic.FilterByAge(minAge, maxAge));
            }
            #region Catches
            catch (EntityException ex)
            {
                ThrowFault(ex, "Unable to connect to database");
            }
            catch (AutoMapperConfigurationException ex)
            {
                ThrowFault(ex, "Invalid mapper configuration");
            }
            catch (AutoMapperMappingException ex)
            {
                ThrowFault(ex, "Unsuccessful mapping");
            }
            catch (NullReferenceException ex)
            {
                ThrowFault(ex, "Requested item not found");
            }
            catch (Exception ex)
            {
                ThrowFault(ex, "Unknown server error");
            }
            #endregion
            return new List<PlayerDTO>();
        }

        private void ThrowFault(Exception exception, string message)
        {
            ServiceData serviceData = new ServiceData();
            serviceData.ErrorMessage = message;
            serviceData.ErrorDetails = exception.ToString();
            throw new FaultException<ServiceData>(serviceData, exception.Message);
        }


    }
}
