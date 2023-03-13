using CitadellesDotIO.Client;
using CitadellesDotIO.Engine.DTOs;
using Godot;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

public partial class node_2d : Node2D
{
    PlayerClient Client = new PlayerClient("https://localhost:7257", "Pierre");

    Label DebuggerText;
    Button CreateGameButton;
    // Called when the node enters the scene tree for the first time.
    public override async void _Ready()
    {
        this.DebuggerText = GetNode<Label>(nameof(DebuggerText));
        this.CreateGameButton = GetNode<Button>(nameof(CreateGameButton));
        CreateGameButton.Pressed += async () => { await Client.CreateGame("GameName"); };
        await Client.StartLobbyConnection();
    }

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
}
