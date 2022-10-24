using CitadellesDotIO.Engine;
using CitadellesDotIO.Engine.Factories;
using CitadellesDotIO.Engine.Factory;
using CitadellesDotIO.Engine.View;
using CitadellesDotIO.Server.Data;
using CitadellesDotIO.Server.Hubs;
using System.Collections.Concurrent;

namespace CitadellesDotIO.Server.Services
{
    public class GamesService : IGamesService
    {
        private readonly ConcurrentDictionary<string, Game> games = new();

        public async Task CreateGameAsync(GamesHub hub, GameParameters gameParameters, Player gameCreator)
        {
            string newGameGuid = Guid.NewGuid().ToString();
            games.TryAdd(newGameGuid, new Game(
               new List<Player>(),
               CharactersFactory.MatchNames(gameParameters.CharactersListName),
               DeckFactory.MatchNames(gameParameters.DistrictsDeckName),
               new RandomActionView(),
               newGameGuid
            ));

            // Ajout de l'appelant au groupe nommé comme la game -- Pas sur de ca
            gameCreator.Id = hub.Context.ConnectionId;
            await hub.Groups.AddToGroupAsync(hub.Context.ConnectionId, newGameGuid);
            await hub.BroadcastGamesAsync();
            // Broadcast la nouvelle game -- Pas implémenté coté client
            //await hub.Clients.Group(newGameGuid).PullGameAsync(games[newGameGuid]);
        }

        public async Task<List<GameDto>> GetGamesAsync()
        {
            List<GameDto> dtos = new();
            foreach (var game in games.Values)
            {
                dtos.Add(new GameDto()
                {
                    GameId = game.Id,
                    Players = game.Players
                });
            }
            dtos.Add(new GameDto()
            {
                GameId = Guid.NewGuid().ToString(),
                Players = new() { new Player()
                {
                    Name = "Pierre"
                } }
            }) ;
            return await Task.FromResult(dtos);
        }

        //public async Task PlayerJoinedAsync(GamesHub hub, Player player, string gameId)
        //{
        //    player.Id = hub.Context.ConnectionId;
        //    Game game = games[gameId];
        //    game.JoinPlayer(player);
        //    await hub.Groups.AddToGroupAsync(hub.Context.ConnectionId, gameId);
        //    await hub.Clients.Group(gameId).PullGameAsync(game);
        //}
    }
}
