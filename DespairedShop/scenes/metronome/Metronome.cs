using Godot;
using System;

public partial class Metronome : Control
{
	[Export] public int BPM = 130;
	[Export] public float BeatLifetime = 3;
	[Export] public PackedScene BeatScene;
	
	private Timer _beatTimer;
	private float _beatPeriod;
	private Control _beatSpawmPoint;
	private Control _beatTargetPoint;
	
	public override void _Ready()
	{
		_beatPeriod = 60.0f / BPM;
		_beatTimer = GetNode<Timer>("BeatTimer");
		_beatSpawmPoint = GetNode<Control>("BeatSpawnPoint");
		_beatTargetPoint = GetNode<Control>("BeatTargetPoint");
		
		_beatTimer.Timeout += OnBeatTimerTimeout;
		_beatTimer.Start(_beatPeriod);
	}

	private void OnBeatTimerTimeout()
	{
		GD.Print("Beat");
		Beat beat = BeatScene.Instantiate<Beat>();
		beat.InitBeat(BeatLifetime, _beatSpawmPoint.GlobalPosition, _beatTargetPoint.GlobalPosition);
	}

	public override void _Process(double delta)
	{
	}
}
