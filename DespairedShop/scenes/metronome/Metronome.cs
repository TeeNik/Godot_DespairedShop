using Godot;
using System;

public partial class Metronome : Control
{
	[Export] public int BPM = 130;
	[Export] public float BeatLifetime = 3;
	[Export] public PackedScene BeatScene;
	
	[Export] private Control _beatsParent;
	[Export] private Control _beatSpawnPoint;
	[Export] private Control _beatTargetPoint;
	
	private Timer _beatTimer;
	private float _beatPeriod;
	
	public override void _Ready()
	{
		_beatPeriod = 60.0f / BPM;
		_beatTimer = GetNode<Timer>("BeatTimer");
		
		_beatTimer.Timeout += OnBeatTimerTimeout;
		_beatTimer.Start(_beatPeriod);
	}

	private void OnBeatTimerTimeout()
	{
		GD.Print("Beat");
		Beat beat = BeatScene.Instantiate<Beat>();
		_beatsParent.AddChild(beat);
		beat.SetPosition(_beatSpawnPoint.Position);
		beat.InitBeat(BeatLifetime, _beatSpawnPoint.Position, _beatTargetPoint.Position);
	}

	public override void _Process(double delta)
	{
	}
}
