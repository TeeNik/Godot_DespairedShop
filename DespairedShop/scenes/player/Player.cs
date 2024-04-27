using Godot;
using System;

public partial class Player : Area2D
{
	[Signal]
	public delegate void HitEventHandler();
	
	[Export] 
	public int Speed { get; set; } = 400;

	private Vector2 _screenSize;
	private CollisionShape2D _collisionShape2D;
	
	public override void _Ready()
	{
		_screenSize = GetViewportRect().Size;
		BodyEntered += OnBodyEntered;
		
		_collisionShape2D = GetNode<CollisionShape2D>("CollisionShape2D");
		
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
			velocity = velocity.Normalized() * Speed;
		}

		Position += velocity * (float)delta;
		Position = new Vector2(Mathf.Clamp(Position.X, 0, _screenSize.X), Mathf.Clamp(Position.Y, 0, _screenSize.Y));

	}
	
	private void OnBodyEntered(Node2D body)
	{
		Hide();
		EmitSignal(SignalName.Hit);
		_collisionShape2D.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
	}
}
