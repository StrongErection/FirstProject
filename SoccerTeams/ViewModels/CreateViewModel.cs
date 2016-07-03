using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoccerTeams.Models;
using System.Web.Mvc;
using DTOs;
namespace SoccerTeams.ViewModels
{
    public class CreateViewModel
    {
        public PlayerDTO Player { get; set; }
        public SelectList TeamList { get; set; }
        public Roles Role { get; set; }
        
    }
}