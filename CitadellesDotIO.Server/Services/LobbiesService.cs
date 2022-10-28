using CitadellesDotIO.Engine;
using CitadellesDotIO.Engine.Factories;
using CitadellesDotIO.Server.Models;
using System.Collections.Concurrent;

namespace CitadellesDotIO.Server.Services
{
    public class LobbiesService : ILobbiesService
    {
        private readonly ConcurrentDictionary<string, Lobby> Lobbies = new();
        private readonly ConcurrentDictionary<string, Player> Players = new();
        public LobbiesService()
        {
            Lobby lobby = new Lobby("Empty lobby")
            {
                Players = new List<Player>() { }
            };
            this.Lobbies.AddOrUpdate(lobby.Id, lobby, (key, value) => value);
            for (int i = 0; i < 2; i++)
            {
                // population à la con pour l'instant
                Lobby dummy = new Lobby("Dummy 3 players lobby")
                {
                    Players = new List<Player>() { new("Sam"), new("Clover"), new("Alex") }
                };
                this.Lobbies.AddOrUpdate(dummy.Id, dummy, (key, value) => value);
            }           

            List<Player> buddies = new List<Player>();
            buddies.AddRange(PlayersFactory.BuddiesPlayerList(8));
            buddies.AddRange(PlayersFactory.BuddiesPlayerList(8));
            buddies.AddRange(PlayersFactory.BuddiesPlayerList(8));
            buddies.AddRange(PlayersFactory.BuddiesPlayerList(8));
            buddies.AddRange(PlayersFactory.BuddiesPlayerList(8));
            buddies.ForEach(p =>
            {
                this.Players.AddOrUpdate(Guid.NewGuid().ToString(), p, (key, value) => value);
            });
        }

        public async Task<bool> RegisterPlayerAsync(Player player)
        => await Task.FromResult(this.Players.TryAdd(player.Id, player));

        public async Task<bool> AddPlayerToLobby(string lobbyId, string playerId)
        {
            if (this.Players.TryGetValue(playerId, out Player? joiner) &&
               this.Lobbies.TryGetValue(lobbyId, out Lobby? toJoin))
            {
                toJoin.Players.Add(joiner);
                return await this.RemovePlayerFromPlayers(playerId);
            }
            else return await Task.FromResult(false);
        }

        public async Task<bool> RemovePlayerFromLobby(string lobbyId, string playerId)
        {
            if (this.Lobbies.TryGetValue(lobbyId, out Lobby? toLeave))
            {
                Player? leaver = toLeave.Players.SingleOrDefault(p => p.Id == playerId);
                if (leaver != null)
                {
                    toLeave.Players.Remove(leaver);
                    if(!toLeave.Players.Any())
                    {
                        this.Lobbies.TryRemove(lobbyId, out _);
                    }
                    return await this.RegisterPlayerAsync(leaver);
                }
            }
            return await Task.FromResult(false);
        }

        public async Task<bool> RemovePlayerFromPlayers(string playerId)
        {
            if (this.Players.TryGetValue(playerId, out Player? leaver))
            {
                return await Task.FromResult(this.Players.TryRemove(new(playerId, leaver)));
            }
            return await Task.FromResult(false);
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
