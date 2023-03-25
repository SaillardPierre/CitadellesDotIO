using CitadellesDotIO.Client;
using Godot;
using System.Text;

namespace CitadellesDotIO.Godot
{
    public partial class node_2d : Node2D
    {
        PlayerClient Client = new PlayerClient("https://localhost:7257", "Pierre");

        Label DebuggerText;
        Button MultiPlayerButton;

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
            if (this.Client != null
            && this.DebuggerText != null)
            {
                StringBuilder builder = new();
                builder.Append("LobbyConnection : ").AppendLine(Client.LobbyConnectionState.ToString());
                builder.Append("Game : ").AppendLine(Client.GameConnectionState.ToString());
                DebuggerText.Text = builder.ToString();
            }
        }

        public void Init()
        {
            var player = GetNode<Area2D>("Player");
            this.DebuggerText = player.GetNode<Label>(nameof(DebuggerText));
            this.MultiPlayerButton = player.GetNode<VBoxContainer>("Menu").GetNode<Button>(nameof(MultiPlayerButton));
            MultiPlayerButton.Pressed += async () =>
            {
                await Client.StartLobbyConnection();
            };
        }

        public override void _Ready()
        {
            EnsureChildrenReady();
        }

        private void EnsureChildrenReady()
        {
            // Check if all children nodes are ready
            bool allChildrenReady = true;
            foreach (Node child in GetChildren())
            {
                if (!child.IsInsideTree())
                {
                    allChildrenReady = false;
                    break;
                }
            }

            // If all children nodes are ready, execute the code
            if (!allChildrenReady)
            {
                // If not all children nodes are ready, create a new timer to wait again
                Timer timer = new();
                timer.OneShot = true;
                timer.Connect("timeout", new Callable(this, nameof(EnsureChildrenReady)));
                timer.Start(0);
            }
            else Init();
        }
    }
}