using Godot;
using System;

public partial class Main : Node
{

	[Export] public PackedScene MobScene { get; set; }
	[Export] public Player Player { get; set; }
	[Export] public ScoreLabel ScoreLabel { get; set; }
	[Export] public ColorRect Retry { get; set; }

	private Timer _mobTimer;
	private PathFollow3D _mobSpawnLocation;
	
	public override void _Ready()
	{
		_mobTimer = GetNode<Timer>("MobTimer");
		_mobSpawnLocation = GetNode<PathFollow3D>("SpawnPath/SpawnLocation");
		
		_mobTimer.Timeout += OnMobTimerTimeout;
		Player.Hit += OnPlayerHit;
		
		Retry.Hide();
	}

	private void OnMobTimerTimeout()
	{
		Mob mob = MobScene.Instantiate<Mob>();

		_mobSpawnLocation.ProgressRatio = GD.Randf();

		Vector3 playerPosition = Player.Position;
		mob.Initialize(_mobSpawnLocation.Position, playerPosition);

		mob.Squashed += ScoreLabel.OnMobSquashed;
		
		AddChild(mob);
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (Retry.Visible && Input.IsActionPressed("ui_accept"))
		{
			GetTree().ReloadCurrentScene();
		}
	}

	private void OnPlayerHit()
	{
		_mobTimer.Stop();
		Retry.Show();
	}
}
