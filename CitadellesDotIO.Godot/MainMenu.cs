using Godot;
using System;

public partial class MainMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void On_MultiPlayer_Button_Pressed()
	{
		this.GetTree().ChangeSceneToFile("MultiPlayerLobby.tscn");
	}

    public void On_Quit_Button_Pressed()
	{
		this.GetTree().Quit();
	}

}
