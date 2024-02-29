using Godot;

public partial class Enemy : RigidBody2D
{
	public override void _Ready()
	{
		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		string[] mobTypes = animatedSprite2D.SpriteFrames.GetAnimationNames();
		animatedSprite2D.Play(mobTypes[GD.Randi() % mobTypes.Length]);

		var VisibleOnScreenNotifier2D = GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D ");
		VisibleOnScreenNotifier2D.ScreenExited += OnScreenExited;
	}

	private void OnScreenExited()
	{
		QueueFree();
	}

	public override void _Process(double delta)
	{
	}
}
