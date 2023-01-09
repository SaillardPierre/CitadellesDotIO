using CitadellesDotIO.Engine.DTOs;
using CitadellesDotIO.Engine.Factory;
using CitadellesDotIO.Engine.Hubs;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitadellesDotIO.Engine.Services
{
    public class GamesService : IGamesService
    {
        private readonly ConcurrentDictionary<string, Game> Games = new();

        private readonly IHubContext<GameHub> hubContext;

        public GamesService(IHubContext<GameHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public string CreateGame(string gameName, string playerName)
        {
            if (string.IsNullOrWhiteSpace(gameName))
            {
                throw new ArgumentException("Le nom de la partie ne peut être vide");
            }
            if (string.IsNullOrWhiteSpace(playerName))
            {
                throw new ArgumentException("Le nom du joueur ne peut être vide");
            }

            Game newGame = GameFactory.VanillaGame(gameName, hubContext);
            if(!this.Games.TryAdd(newGame.Id, newGame))
            {
                return string.Empty;
            }
            return newGame.Id;
        }

        public async Task<IEnumerable<GameDto>> GetGames()
        {
            List<GameDto> games = new();
            foreach (Game game in this.Games.Values)
            {
                games.Add(await game.ToGameDto());
            }
            return games;
        }

        public bool SetPlayerConnection(string gameId, string playerName, string connectionId)
        {
            if (this.Games.TryGetValue(gameId, out Game game))
            {
                game.Players.Single(p=>p.Name == playerName).Id = connectionId;
                return true;
            }
            return false;
        }

        public bool AddPlayerToGame(string gameId, string playerName)
        {
            if (string.IsNullOrWhiteSpace(gameId))
            {
                throw new ArgumentException("L'id de la partie ne peut être vide");
            }
            if (string.IsNullOrWhiteSpace(playerName))
            {
                throw new ArgumentException("Le nom du joueur ne peut être vide");
            }
            if(!this.Games.TryGetValue(gameId, out Game game))
            {
                return false;
            }

            Player player = new(playerName);
            return game.AddPlayer(player);
        }

        public bool AddPlayerToGameByGameName(string gameName, string playerName, out string gameId)
        {
            if (string.IsNullOrWhiteSpace(gameName))
            {
                throw new ArgumentException("L'id de la partie ne peut être vide");
            }
            if (string.IsNullOrWhiteSpace(playerName))
            {
                throw new ArgumentException("Le nom du joueur ne peut être vide");
            }

            gameId = string.Empty;
            foreach (KeyValuePair<string, Game> kvp in this.Games)
            {               

                if (kvp.Value.Name.Equals(gameName))
                {
                    gameId = kvp.Key;
                    return AddPlayerToGame(gameId, playerName);                    
                }
            }
            return false;
        }
    }

}
