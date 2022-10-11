using Microsoft.AspNetCore.SignalR.SignalR;



namespace CitadellesDotIO.Hubs
{
    public class LobbiesHub : Hub
    {
        private readonly LobbiesManager lobbiesManager;

        public LobbiesHub(LobbiesManager lobbiesManager)
        {
            this.lobbiesManager = lobbiesManager;
        }

        public IEnumerable<Lobby> GetLobbies()
        {
            return this.lobbiesManager.Lobbies;
        }
    }
}
