using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoccerTeams.Models;
using SoccerTeams.DatabaseOperationsService;
namespace SoccerTeams.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<PlayerDTO> Players { get; set; }
    }
}