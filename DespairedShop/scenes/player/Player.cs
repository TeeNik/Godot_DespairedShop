using Godot;
using Godot.Collections;

public partial class Player : Area2D
{
	[Signal] public delegate void HitEventHandler();
	[Signal] public delegate void ShootEventHandler(bool isHitBeat);
	
	[Export] private int _speed = 400;
	[Export] private Node2D _pivot;

	[Export] private PackedScene _shotScene;

	private Vector2 _screenSize;
	private CollisionShape2D _collisionShape2D;
	
	private Array<Node> _shotSlots = new ();
	private Array<Shot> _shots = new();

	public override void _Ready()
	{
		_screenSize = GetViewportRect().Size;
		BodyEntered += OnBodyEntered;
		
		_collisionShape2D = GetNode<CollisionShape2D>("CollisionShape2D");

		_shotSlots = _pivot.GetChildren();
		foreach (var slot in _shotSlots)
		{
			Shot shot = _shotScene.Instantiate<Shot>();
			shot.Init(slot);
			_shots.Add(shot);
		}
		
		//Hide();
	}

	public void Start(Vector2 position)
	{
		Position = position;
		Show();
		_collisionShape2D.Disabled = false;
	}

	public override void _Process(double delta)
	{
		var velocity = Vector2.Zero;

		if (Input.IsActionPressed("move_right"))
		{
			velocity.X += 1;
		}
		if (Input.IsActionPressed("move_left"))
		{
			velocity.X -= 1;
		}
		if (Input.IsActionPressed("move_down"))
		{
			velocity.Y += 1;
		}
		if (Input.IsActionPressed("move_up"))
		{
			velocity.Y -= 1;
		}

		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * _speed;
		}

		Position += velocity * (float)delta;
		Position = new Vector2(Mathf.Clamp(Position.X, 0, _screenSize.X), Mathf.Clamp(Position.Y, 0, _screenSize.Y));

		_pivot.Rotate((float)delta);
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton && @event.IsPressed())
		{
			InputEventMouseButton mouseEvent = (InputEventMouseButton)@event;
			
			bool isHitBeat = Metronome.Get().IsHitBeat();
			var shot = GetFirstAvailableShot();
			if (shot != null)
			{
				if (isHitBeat)
				{
					shot.Shoot(mouseEvent.Position);
				}
				else
				{
					shot.Dissolve();
				}
				EmitSignal(SignalName.Shoot, isHitBeat);
			}
			
			//string Text = Metronome.Get().IsHitBeat() ? "Hit" : "Miss";
			//GD.Print(Text);
		}
	}

	private void OnBodyEntered(Node2D body)
	{
		Hide();
		EmitSignal(SignalName.Hit);
		_collisionShape2D.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
	}

	private Shot GetFirstAvailableShot()
	{
		foreach (var shot in _shots)
		{
			if (shot.IsAvailable())
			{
				return shot;
			}
		}
		return null;
	}
}
