using Godot;
using System;

public partial class GlobalTargetComponent : Node
{
	[Export] private RayCast3D _rayCast;
	
	// Color of the bounding box
	[Export]
	public Color BoxColor { get; set; } = Colors.White;

	// Line thickness
	[Export]
	public float LineWidth { get; set; } = 2.0f;
	
	private Camera3D _camera;

	public override void _Ready()
	{
		_camera = GetViewport().GetCamera3D();
	}

	public override void _PhysicsProcess(double delta)
	{
		var collider = _rayCast.GetCollider();
		GD.Print(collider == null ? "" : collider.ToString());

		if (collider != null)
		{
			
		}
	}
}
