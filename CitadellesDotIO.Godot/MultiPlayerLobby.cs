using CitadellesDotIO.Client;
using Godot;
using System.Text;

namespace CitadellesDotIO.Godot
{
    public partial class MultiPlayerLobby : Node2D
    {
        PlayerClient Client = new(Config.SiteUrl, "Pierre");
        Label DebuggerText;

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
            DisplayDebuggerText();
        }

        public override async void _Ready()
        {
            this.EnsureChildrenReady();
            var player = GetNode<Area2D>("Player");
            this.DebuggerText = player.GetNode<Label>(nameof(DebuggerText));
            await Client.StartLobbyConnection();
        }

        public void DisplayDebuggerText()
        {
            StringBuilder builder = new();
            builder.Append("LobbyConnection : ").AppendLine(Client.LobbyConnectionState.ToString());
            builder.Append("Game : ").AppendLine(Client.GameConnectionState.ToString());
            DebuggerText.Text = builder.ToString();
        }
    }
}