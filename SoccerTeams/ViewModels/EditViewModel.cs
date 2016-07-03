using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoccerTeams.Models;
using DTOs;
using System.Web.Mvc;
namespace SoccerTeams.ViewModels
{
    public class EditViewModel 
    {
        public PlayerDTO Player { get; set; }
        public SelectList TeamList { get; set; }
        public Roles Role { get; set; }
    }
}