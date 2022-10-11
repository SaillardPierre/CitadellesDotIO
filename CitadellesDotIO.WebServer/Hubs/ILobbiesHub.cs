namespace CitadellesDotIO.WebServer.Hubs
{
    public interface ILobbiesHub
    {
        public Task GetLobbies(List<Lobby> lobbies);

        public Task CreateLobby(string lobbyName);
    }
}
