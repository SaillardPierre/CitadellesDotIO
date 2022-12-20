using CitadellesDotIO.Client.CustomEventArgs;
using CitadellesDotIO.Engine;
using CitadellesDotIO.Engine.View;

namespace CitadellesDotIO.Client
{
    public class PlayerClient : IAsyncDisposable
    {
        private static string HostAdress = "https://localhost:7257";
        public Player Player;
        public LobbiesConnection LobbiesConnection;
        private PlayerClient(Player player, LobbiesConnection lobbiesConnection)
        {
            this.Player = player;
            this.LobbiesConnection = lobbiesConnection;
        }

        public static async Task<PlayerClient> BuildPlayerClientAsync(string playerName)
        {
            Player player = new(playerName, new RandomActionView());
            return new PlayerClient(player, await BuildLobbiesConnectionAsync(player));
        }

        public static async Task<PlayerClient> BuildPlayerClientAsync(string playerName, IView view)
        {
            Player player = new(playerName, view);
            return new PlayerClient(player, await BuildLobbiesConnectionAsync(player));
        }

        private static async Task<LobbiesConnection> BuildLobbiesConnectionAsync(Player player)
        {
            LobbiesConnection lobbiesConnection = new(player, HostAdress, StateChanged);
            await lobbiesConnection.StartAsync();
            return lobbiesConnection;
        }

        async static void StateChanged(object sender, HubConnectionStateChangedEventArgs e)
        {
            
        }

        public async ValueTask DisposeAsync()
        {
            if (this.LobbiesConnection != null && this.LobbiesConnection.IsConnected)
            {
                await this.LobbiesConnection.StopAsync();
            }
        }
    }
}