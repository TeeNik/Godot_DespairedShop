using Godot;

public partial class Main : Node
{
	
	[Export]
	public PackedScene EnemyScene { get; set; }

	private int _score;

	private Player _player;
	private Timer _enemyTimer;
	private Timer _scoreTimer;
	private Timer _startTimer;
	private Marker2D _startPosition;
	
	public override void _Ready()
	{
		_player = GetNode<Player>("Player");
		_player.Hit += GameOver;

		_enemyTimer = GetNode<Timer>("EnemyTimer");
		_scoreTimer = GetNode<Timer>("ScoreTimer");
		_startTimer = GetNode<Timer>("StartTimer");
		_startPosition = GetNode<Marker2D>("StartPosition");
		
		_scoreTimer.Timeout += OnScoreTimerTimeout;
		_startTimer.Timeout += OnStartTimerTimeout;
		_enemyTimer.Timeout += OnEnemyTimerTimeout;
		
		NewGame();
	}

	private void OnEnemyTimerTimeout()
	{
		Enemy enemy = EnemyScene.Instantiate<Enemy>();

		var enemySpawnLocation = GetNode<PathFollow2D>("EnemyPath/EnemySpawnLocation");
		enemySpawnLocation.ProgressRatio = GD.Randf();

		float direction = enemySpawnLocation.Rotation + Mathf.Pi / 2;

		enemy.Position = enemySpawnLocation.Position;

		direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
		enemy.Rotation = direction;

		var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
		enemy.LinearVelocity = velocity.Rotated(direction);
		
		AddChild(enemy);
	}

	private void OnStartTimerTimeout()
	{
		_enemyTimer.Start();
		_scoreTimer.Start();
	}

	private void OnScoreTimerTimeout()
	{
		++_score;
	}

	private void GameOver()
	{
		_enemyTimer.Stop();
		_scoreTimer.Stop();
	}

	public void NewGame()
	{
		_score = 0;
		_player.Start(_startPosition.Position);
		_startTimer.Start();
	}

	public override void _Process(double delta)
	{
	}
}
