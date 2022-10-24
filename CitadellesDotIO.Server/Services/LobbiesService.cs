using CitadellesDotIO.Engine;
using CitadellesDotIO.Engine.Factories;
using CitadellesDotIO.Server.Models;
using System.Collections.Concurrent;
using System.Reflection;

namespace CitadellesDotIO.Server.Services
{
    public class LobbiesService : ILobbiesService
    {
        private readonly ConcurrentDictionary<string, Lobby> Lobbies = new();
        private readonly ConcurrentDictionary<string, Player> Players = new();
        public LobbiesService()
        {
            // population à la con pour l'instant
            Lobby dummy = new Lobby("Dummy 3 players lobby")
            {
                Players = new List<Player>() { new("Sam"), new("Clover"), new("Alex") }
            };
            this.Lobbies.AddOrUpdate(dummy.Id, dummy, (key, value) => value);

            PlayersFactory.BuddiesPlayerList(4).ToList().ForEach(p =>
            {
                this.Players.AddOrUpdate(Guid.NewGuid().ToString(), p, (key, value) => value);
            });
        }

        public async Task<bool> RegisterPlayerAsync(Player player)
        => await Task.FromResult(this.Players.TryAdd(player.Id, player));
        

        public async Task<bool> AddPlayerToLobby(string lobbyId, string playerId)
        {
            // Retester avec la modif sur le constructeur des dummy
            // Voir si on a besoin de chercher l'id du player mais je crois pas
            // Voir pour ajouter le player a la liste des joueurs sans lobby a la connexion

            if (this.Players.TryGetValue(playerId, out Player? joiner) &&
               this.Lobbies.TryGetValue(lobbyId, out Lobby? toJoin))
            {
                toJoin.Players.Add(joiner);
                return await Task.FromResult(this.Players.TryRemove(new(playerId, joiner)));
            }
            else return await Task.FromResult(false);
        }

        public async Task<List<Lobby>> GetLobbiesAsync()
            => await Task.FromResult(this.Lobbies.Values.ToList());

        public async Task<List<Player>> GetPlayersAsync()
            => await Task.FromResult(this.Players.Values.ToList());

        public async Task<bool> CreateLobbyAsync(Lobby newLobby)
        {
            this.Lobbies.AddOrUpdate(newLobby.Id, newLobby, (key, value) => value);
            return await Task.FromResult(true);
        }
    }
}
