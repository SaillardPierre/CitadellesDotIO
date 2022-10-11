using CitadellesDotIO.WebServer.Hubs;
using CitadellesDotIO.WebServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CitadellesDotIO.WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LobbiesController : ControllerBase
    {
        private readonly ILogger<HomeController> logger;
        private readonly IHubContext<LobbiesHub, ILobbiesHub> lobbiesHubContext;
        private readonly ILobbiesService lobbiesService;
        public LobbiesController(
            ILogger<HomeController> logger,
            IHubContext<LobbiesHub, ILobbiesHub> lobbiesHubContext,
            ILobbiesService lobbiesService)
        {
            this.lobbiesHubContext = lobbiesHubContext;
            this.logger = logger;
            this.lobbiesService = lobbiesService;
        }
    }
}
