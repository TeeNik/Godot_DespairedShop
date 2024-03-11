using Godot;
using System;

public partial class Main : Node
{

	[Export]
	public PackedScene MobScene { get; set; }

	private Timer _mobTimer;
	private PathFollow3D _mobSpawnLocation;
	private Player _player;
	
	public override void _Ready()
	{
		_mobTimer = GetNode<Timer>("MobTimer");
		_mobSpawnLocation = GetNode<PathFollow3D>("SpawnPath/SpawnLocation");
		_player = GetNode<Player>("Player");
		
		_mobTimer.Timeout += OnMobTimerTimeout;
		_player.Hit += OnPlayerHit;
	}

	private void OnMobTimerTimeout()
	{
		Mob mob = MobScene.Instantiate<Mob>();

		_mobSpawnLocation.ProgressRatio = GD.Randf();

		Vector3 playerPosition = _player.Position;
		mob.Initialize(_mobSpawnLocation.Position, playerPosition);
		
		AddChild(mob);
	}

	public override void _Process(double delta)
	{
	}
	
	private void OnPlayerHit()
	{
		_mobTimer.Stop();
	}
}
