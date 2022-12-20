using CitadellesDotIO.Engine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CitadellesDotIO.Engine.HubsClients
{
    /// <summary>
    /// Interface utilisée pour définir les signatures des méthodes du client SignalR associé
    /// Le comportement est surchargé en js dans connection.on("nomDeLaMethode",(mesParametres) =>{lambda de fou});
    /// Elles sont executées quand LobbiesHub les appelle sur les clients concernés
    /// </summary>
    public interface ILobbiesHubClient
    {
        Task PullLobbies(IList<Lobby> lobbies);

        Task PullLobbyId(string lobbyId);

        Task PullPlayers(IList<Player> players);

        Task PullGame(Game game);
    }
}
