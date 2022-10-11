﻿using CitadellesDotIO.WebServer.Hubs;
using CitadellesDotIO.WebServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;

namespace CitadellesDotIO.WebServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IHubContext<LobbiesHub, ILobbiesHub> lobbiesHubContext;
        private readonly ILobbiesService lobbiesService;

        public HomeController(
            ILogger<HomeController> logger,
            IHubContext<LobbiesHub, ILobbiesHub> lobbiesHubContext,
            ILobbiesService lobbiesService)
        {
            this.lobbiesHubContext = lobbiesHubContext;
            this.logger = logger;
            this.lobbiesService = lobbiesService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}