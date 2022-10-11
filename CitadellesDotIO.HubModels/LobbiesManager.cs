using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Hubs
{
    public class LobbiesManager
    {
        private static Lazy<LobbiesManager> instance
            => new Lazy<LobbiesManager>(() => new LobbiesManager(GlobalHost.ConnectionManager.GetHubContext<LobbiesHub>().Clients));
        
        public List<Lobby> Lobbies {get;set;}

        private LobbiesManager(IHubConnectionContext<dynamic> clients)
        {
            this.Clients = clients;
            this.Lobbies = new List<Lobby>();
        }

        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        public static LobbiesManager Instance => instance.Value;
    }
}
