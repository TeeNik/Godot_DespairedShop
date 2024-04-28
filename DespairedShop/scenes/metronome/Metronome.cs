using Godot;
using System;

public partial class Metronome : Control
{
	[Export] private int _bpm = 130;
	[Export] private float _beatLifetime = 3;
	[Export] private float _beatHitThreshold = 0.1f;
	[Export] private PackedScene _beatScene;
	
	[Export] private Control _beatsParent;
	[Export] private Control _beatSpawnPoint;
	[Export] private Control _beatTargetPoint;
	
	private Timer _beatTimer;
	private float _beatPeriod;
	private float _currentBeatTime;

	private static Metronome _instance;

	public static Metronome Get()
	{
		return _instance;
	}
	
	public override void _Ready()
	{
		_instance = this;
		
		_beatPeriod = 60.0f / _bpm;
		_currentBeatTime = _beatPeriod;
		_beatTimer = GetNode<Timer>("BeatTimer");
		
		_beatTimer.Timeout += OnBeatTimerTimeout;
		_beatTimer.Start(_beatPeriod);
	}

	private void OnBeatTimerTimeout()
	{
		_currentBeatTime = _beatPeriod;
		Beat beat = _beatScene.Instantiate<Beat>();
		_beatsParent.AddChild(beat);
		beat.SetPosition(_beatSpawnPoint.Position);
		beat.InitBeat(_beatPeriod, _beatSpawnPoint.Position, _beatTargetPoint.Position);
	}

	public override void _Process(double delta)
	{
		_currentBeatTime -= (float)delta;
	}

	public bool IsHitBeat()
	{
		GD.Print(_currentBeatTime);
		return _currentBeatTime < _beatHitThreshold;
	}
}
