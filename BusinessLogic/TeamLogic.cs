
using System.Collections.Generic;
using DataAccessLayer;
namespace BusinessLogic
{
    public class TeamLogic
    {
        public Team FindTeam(int id)
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                Team team = unit.TeamRepository.Find(id);
                team.Players = unit.PlayerRepository.GetAllTeammates(team.Id);
                return team;
            }
        }
        public ICollection<Team> GetAllTeams()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                return unit.TeamRepository.GetAllTeams();
            }
        }
    }
}
