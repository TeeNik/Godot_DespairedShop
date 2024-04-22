using Godot;
using System;

public partial class Metronome : Control
{
	[Export] public int BPM = 130;
	[Export] public PackedScene BeatScene;
	
	private Timer _beatTimer;
	
	public override void _Ready()
	{
		_beatTimer = GetNode<Timer>("BeatTimer");
		
		_beatTimer.Timeout += OnBeatTimerTimeout;
	}

	private void OnBeatTimerTimeout()
	{
		
	}

	public override void _Process(double delta)
	{
	}
}
