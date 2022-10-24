using CitadellesDotIO.Engine;
using CitadellesDotIO.Server.Data;
using CitadellesDotIO.Server.Models;
using Microsoft.AspNetCore.SignalR;

namespace CitadellesDotIO.Server.HubsClients
{
    /// <summary>
    /// Interface utilisée pour définir les signatures des méthodes du client SignalR associé
    /// Le comportement est surchargé en js dans connection.on("nomDeLaMethode",(mesParametres) =>{lambda de fou});
    /// Elles sont executées quand LobbiesHub les appelle sur les clients concernés
    /// </summary>
    public interface ILobbiesHubClient
    {
        Task PullLobbies(IList<Lobby> lobbies);

        Task PullPlayers(IList<Player> players);
    }
}
