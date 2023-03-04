using CitadellesDotIO.Engine.DTOs;
using CitadellesDotIO.Engine.HubsClient;
using CitadellesDotIO.Engine.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Engine.Hubs
{
    public class GameHub : Hub<IGameHubClient>
    {
        private readonly IGamesService gamesService;
        public GameHub(IGamesService gamesService)
        {
            this.gamesService = gamesService;
        }

        public async override Task OnConnectedAsync()
        {
            await this.Clients.Caller.RegisterPlayer();
        }

        // Peut etre ne pas attendre ici ?
        public async Task UpdateGame(string gameSecret, GameDto gameDto)
        => await this.Clients.Group(gameSecret).UpdateGame(gameDto);

        public async Task UpdateGameById(string gameId)
        {
            var (secret, gameDto) = this.gamesService.GetGameWithSecret(gameId);
            await this.Clients.Group(secret).UpdateGame(gameDto);
        }

        public async Task<bool> RegisterPlayer(string gameId, string gameSecret, string playerName)
        {
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, gameSecret);
            if (!this.gamesService.TrySetPlayerConnection(gameId, playerName, this.Context.ConnectionId, out GameDto game))
            {
                return false;
            }
            Console.WriteLine("Player " + playerName + " joined " + gameId + " with connection " + this.Context.ConnectionId);
            // Peut etre ne pas attendre ici ?
            _ = this.UpdateGame(gameSecret, game);
            return true;
        }

        public async Task SetReadyStateAsync(string gameId, bool isReady)
        {
            if (!this.gamesService.TrySetReadyState(gameId, this.Context.ConnectionId, isReady, out GameDto gameDto))
            {
                return;
            }
            string ready = isReady ? "ready " : "unready ";
            Console.WriteLine("Player " + gameDto.Players.Single(p => p.Id == this.Context.ConnectionId).Name + " has set " + ready + " for game" + gameId);
            // Peut etre ne pas attendre ici ?
            //await this.UpdateGame(gameDto);
        }

    }
}

