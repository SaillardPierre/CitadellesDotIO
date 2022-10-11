using CitadellesDotIO.WebServer.Hubs;
using CitadellesDotIO.WebServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;

namespace CitadellesDotIO.WebServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<LobbiesHub, ILobbiesHub> _lobbiesHubContext;

        private List<Lobby> Lobbies { get; set; } = new List<Lobby>() { new Lobby() { Name = "ProutLobby"} };


        public HomeController(ILogger<HomeController> logger, IHubContext<LobbiesHub, ILobbiesHub> lobbiesHubContext)
        {
            _lobbiesHubContext = lobbiesHubContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateLobby(string newLobbyName)
        {
            this.Lobbies.Add(new Lobby()
            {
                Name = newLobbyName
            });
            this.UpdateLobbiesList();
            return StatusCode(StatusCodes.Status201Created);
        }

        // Notifie tous les clients : Correspond a l'evenement on("getLobbies")
        private void UpdateLobbiesList()
        =>  _lobbiesHubContext.Clients.All.GetLobbies(this.Lobbies);        

        public void Get()
        {
            this.UpdateLobbiesList();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}