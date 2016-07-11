using System;
using System.Collections.Generic;
using SoccerTeams.DatabaseOperationsService;
namespace SoccerTeams.ViewModels
{
    public class AjaxViewModel
    {
        public IEnumerable<PlayerDTO> Players { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
    }
}