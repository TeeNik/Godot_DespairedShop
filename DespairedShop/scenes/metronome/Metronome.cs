using Godot;
using System;

public partial class Metronome : Control
{
	[Signal] public delegate void BeatEventHandler();

	[Export] private int _bpm = 130;
	[Export] private float _beatLifetime = 3;
	[Export] private float _beatHitThreshold = 0.1f;
	[Export] private PackedScene _beatScene;
	
	[Export] private Control _beatsParent;
	[Export] private Control _beatSpawnPoint;
	[Export] private Control _beatTargetPoint;
	
	public float BeatPeriod { get; private set; }
	
	private Timer _beatTimer;
	private float _currentBeatTime;

	private static Metronome _instance;

	public static Metronome Get()
	{
		return _instance;
	}
	
	public override void _Ready()
	{
		_instance = this;
		
		BeatPeriod = 60.0f / _bpm;
		_currentBeatTime = BeatPeriod;
		_beatTimer = GetNode<Timer>("BeatTimer");
		
		_beatTimer.Timeout += OnBeatTimerTimeout;
		_beatTimer.Start(BeatPeriod);
	}

	private void OnBeatTimerTimeout()
	{
		_currentBeatTime = BeatPeriod;
		EmitSignal(SignalName.Beat);
		
		Beat beat = _beatScene.Instantiate<Beat>();
		_beatsParent.AddChild(beat);
		beat.SetPosition(_beatSpawnPoint.Position);
		beat.InitBeat(BeatPeriod, _beatSpawnPoint.Position, _beatTargetPoint.Position);
	}

	public override void _PhysicsProcess(double delta)
	{
		_currentBeatTime -= (float)delta;
		//GD.Print(_currentBeatTime);
	}

	public bool IsHitBeat()
	{
		return _currentBeatTime < _beatHitThreshold;
	}
}
