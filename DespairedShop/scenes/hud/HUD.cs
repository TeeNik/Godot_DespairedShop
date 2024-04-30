using Godot;
using System;

public partial class Hud : Control
{
	[Export] private Metronome _metronome;
	[Export] private Player _player;
	
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}
}
