using CitadellesDotIO.WebServer.Models;
using Microsoft.AspNetCore.SignalR;

namespace CitadellesDotIO.WebServer.Hubs
{
    public interface ILobbiesHub
    {
        // Méthodes dont le comportement est défini en js dans connection.on("nomDeLaMethode",(mesParametres) =>{lambda de fou});
        // Sont executées quand LobbiesHub les appelle sur les clients concernés
        [HubMethodName("PullLobbies")]
        public Task PullLobbies(IEnumerable<Lobby> lobbies);
    }
}
