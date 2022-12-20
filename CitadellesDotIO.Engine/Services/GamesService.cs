using CitadellesDotIO.Engine.DTOs;
using CitadellesDotIO.Engine.Factory;
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

        public GamesService()
        {

        }

        public async Task<string> CreateGameAsync(string gameName, string playerName)
        {
            if (string.IsNullOrWhiteSpace(gameName))
            {
                throw new ArgumentException("Le nom de la partie ne peut être vide");
            }
            if (string.IsNullOrWhiteSpace(playerName))
            {
                throw new ArgumentException("Le nom du joueur ne peut être vide");
            }

            Game newGame = GameFactory.VanillaGame(gameName);
            if(!this.Games.TryAdd(newGame.Id, newGame))
            {
                throw new Exception("Echec à l'ajout de la partie");
            }
            return newGame.Id;
        }

        public Task<IEnumerable<GameDto>> GetGamesAsync()
        => Task.FromResult(this.Games.Values.Select(g => g.ToGameDto()));

        public async Task<bool> AddPlayerToGameAsync(string gameId, string playerName)
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

            Player player = new Player(playerName);
            return game.AddPlayer(player);
        }
    }

}
