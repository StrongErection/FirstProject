using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SoccerTeams.DatabaseOperationsService;
using SoccerTeams.ViewModels;
using SoccerTeams.Models;
using System.Transactions;
using SoccerTeams.Utils;
using Newtonsoft.Json;
using System.ServiceModel;
namespace SoccerTeams.Controllers
{
    public class HomeController : Controller
    {
        private DataAccessServiceClient client = new DataAccessServiceClient();
        // Выводим всех футболистов
        public ActionResult Index()
        {
            IndexViewModel viewModel;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromSeconds(180)))
            {
                viewModel = new IndexViewModel { Players = client.FindAllPlayers().ToList<PlayerDTO>() };
                scope.Complete();
                client.Close();
            }
            return View(viewModel);
        }

        public ActionResult Error()
        {
            return View();
        }


        public ActionResult TeamDetails(int id)
        {
            TeamDTO team;
            using (TransactionScope scope = new TransactionScope())
            {
                team = client.FindTeam(id);
                scope.Complete();
                client.Close();
            }
            return View(new TeamDetailsViewModel { Team = team });
        }
            
        
        [HttpGet]
        public ActionResult Create()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                CreateViewModel viewModel = new CreateViewModel { Player = new PlayerDTO { Age = 20 }, TeamList = new SelectList(client.FindAllTeams(), "Id", "Name"), Role = Roles.Запасной };
                scope.Complete();
                client.Close();
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Create(CreateViewModel model)
        { 
            CreateViewModel viewModel;
            using (TransactionScope scope = new TransactionScope())
            {
                
                PlayerDTO player = model.Player;
                player.Position = model.Role.ToString();
                //Добавляем игрока в таблицу
                if (ModelState.IsValid && ValidateCaptcha())
                {
                    //Добавляем игрока в таблицу

                    client.AddPlayer(player);
                    scope.Complete();
                    client.Close();
                    // перенаправляем на главную страницу
                    return RedirectToAction("Index");
                }
                viewModel = new CreateViewModel { Player = new PlayerDTO(), TeamList = new SelectList(client.FindAllTeams(), "Id", "Name") };
                scope.Complete();
                client.Close();
                    
            }
            return View(viewModel);
            
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                PlayerDTO player = client.FindPlayer(id);
                EditViewModel viewModel = new EditViewModel { Player = player, TeamList = new SelectList(client.FindAllTeams(), "Id", "Name", player.TeamId), Role = (Roles)Enum.Parse(typeof(Roles), player.Position) };
                scope.Complete();
                client.Close();       
                return View(viewModel);   
            }
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel model)
        {
            EditViewModel viewModel;
            using (TransactionScope scope = new TransactionScope())
            {
                PlayerDTO player = model.Player;
                player.Position = model.Role.ToString();
                if (ModelState.IsValid && ValidateCaptcha())
                {
                    client.EditPlayer(player);
                    scope.Complete();
                    client.Close();
                    return RedirectToAction("Index");
                }
                viewModel = new EditViewModel { Player = player, TeamList = new SelectList(client.FindAllTeams(), "Id", "Name", player.TeamId), Role = (Roles)Enum.Parse(typeof(Roles), player.Position) };        
            }
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Находим в бд футболиста
                PlayerDTO player = client.FindPlayer(id);
                scope.Complete();
                client.Close();
                return View(new DeleteViewModel { Player = player });  
            }
        }
            
        
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                client.DeletePlayer(id);
                scope.Complete();
                client.Close();
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public ActionResult AjaxTest(int min, int max)
        {
            AjaxViewModel viewModel;
            using (TransactionScope scope = new TransactionScope())
            {
                viewModel = new AjaxViewModel { Players = client.FilterPlayersByAge(min, max), Min = min, Max = max };
                scope.Complete();
                client.Close();   
            }
            return PartialView(viewModel);
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