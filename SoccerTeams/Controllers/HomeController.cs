using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SoccerTeams.DatabaseOperationsService;
using System.Runtime.Serialization;
using System.IO;
using SoccerTeams.ViewModels;
using SoccerTeams.Models;
using AutoMapper;
using SoccerTeams.ServiceReference1;
using DTOs;
using SoccerTeams.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace SoccerTeams.Controllers
{
    public class HomeController : Controller
    {

        private ServiceClient client = new ServiceClient();
        
        // Выводим всех футболистов
        public ActionResult Index()
        {
            using (ServiceClient client = new ServiceClient()) {
                return View(new IndexViewModel { Players = client.FindAllPlayers().ToList<PlayerDTO>() });
            }
        }
        public ActionResult TeamDetails(int id)
        {
            using (ServiceClient client = new ServiceClient())
            {
                TeamDTO team = client.FindTeam(id);
                if (team == null)
                {
                    return HttpNotFound();
                }
                return View(new TeamDetailsViewModel { Team = team });
            }
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        [HttpGet]
        public ActionResult Create()
        {
            using (ServiceClient client = new ServiceClient())
            {
                return View(new CreateViewModel { Player = new PlayerDTO { Age = 20 }, TeamList = new SelectList(client.FindAllTeams(), "Id", "Name"), Role = Roles.Запасной });
            }
        }

        [HttpPost]
        public ActionResult Create(CreateViewModel model)
        {
            using (ServiceClient client = new ServiceClient())
            {
                PlayerDTO player = model.Player;
                player.Position = model.Role.ToString();
                //Добавляем игрока в таблицу
                if (ModelState.IsValid && ValidateCaptcha())
                {
                    //Добавляем игрока в таблицу

                    client.AddPlayer(player);
                    // перенаправляем на главную страницу
                    return RedirectToAction("Index");
                }
                return View(new CreateViewModel { Player = new PlayerDTO(), TeamList = new SelectList(client.FindAllTeams(), "Id", "Name") });
            }
        }
       
        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (ServiceClient client = new ServiceClient())
            {
                PlayerDTO player = client.FindPlayer(id);
                if (player != null)
                {
                    // Создаем список команд для передачи в представление        
                    return View(new EditViewModel { Player = player, TeamList = new SelectList(client.FindAllTeams(), "Id", "Name", player.TeamId), Role = (Roles)Enum.Parse(typeof(Roles), player.Position) });
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel model)
        {
            using (ServiceClient client = new ServiceClient())
            {
                PlayerDTO player = model.Player;
                player.Position = model.Role.ToString();
                if (ModelState.IsValid && ValidateCaptcha())
                {
                    client.EditPlayer(player);
                    return RedirectToAction("Index");
                }
                return View(new EditViewModel { Player = player, TeamList = new SelectList(client.FindAllTeams(), "Id", "Name", player.TeamId), Role = (Roles)Enum.Parse(typeof(Roles), player.Position) });
            }
            
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (ServiceClient client = new ServiceClient())
            {
                // Находим в бд футболиста
                PlayerDTO player = client.FindPlayer(id);
                if (player != null)
                {
                    return View(new DeleteViewModel { Player = player });
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ServiceClient client = new ServiceClient())
            {
                client.DeletePlayer(id);
                return RedirectToAction("Index");
            }
        }

        private TDest Map<TDest, TSrc>(TSrc src)
            where TDest : class
            where TSrc : class
        {
            Mapper.Initialize(cfg => cfg.CreateMap<TSrc, TDest>());
            TDest dest = Mapper.DynamicMap<TSrc, TDest>(src);
            Mapper.AssertConfigurationIsValid();
            Mapper.Reset();
            return dest;
        }

        private bool ValidateCaptcha()
        {
            var response = Request["g-recaptcha-response"];
            //secret that was generated in key value pair
            const string secret = "6LdMJiQTAAAAABizYhOu-4o9k2drTpyjbKDhi2PJ";

            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

            //when response is false check for the error message
            if (!captchaResponse.Success)
            {
                if (captchaResponse.ErrorCodes.Count <= 0) return true;

                var error = captchaResponse.ErrorCodes[0].ToLower();
                switch (error)
                {
                    case ("missing-input-secret"):
                        ViewBag.Message = "The secret parameter is missing.";
                        break;
                    case ("invalid-input-secret"):
                        ViewBag.Message = "The secret parameter is invalid or malformed.";
                        break;

                    case ("missing-input-response"):
                        ViewBag.Message = "The response parameter is missing.";
                        break;
                    case ("invalid-input-response"):
                        ViewBag.Message = "The response parameter is invalid or malformed.";
                        break;

                    default:
                        ViewBag.Message = "Error occured. Please try again";
                        break;
                }
                return false;
            }
            else
            {
                ViewBag.Message = "Valid";
            }
            return true;
        }
    }
}