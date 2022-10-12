using CitadellesDotIO.WebServer.Models;
using Microsoft.AspNetCore.SignalR;

namespace CitadellesDotIO.WebServer.HubsClients
{
    /// <summary>
    /// Interface utilisée pour définir les signatures des méthodes du client SignalR associé
    /// Le comportement est surchargé en js dans connection.on("nomDeLaMethode",(mesParametres) =>{lambda de fou});
    /// Elles sont executées quand LobbiesHub les appelle sur les clients concernés
    /// </summary>
    public interface ILobbiesHubClient
    {
        [HubMethodName("PullLobbies")]
        public Task PullLobbies(IEnumerable<Lobby> lobbies);
    }
}
