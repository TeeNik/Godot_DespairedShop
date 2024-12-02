using Godot;
using System;

public partial class Weapon : Node3D
{
	private Camera3D _camera;

	public override void _Ready()
	{
		var Player = FindParent("Player") as Player;
		_camera = Player.Camera;
	}

	public void Shoot()
	{
		var spaceState = _camera.GetWorld3D().DirectSpaceState;
		var screenCenter = GetViewport().GetVisibleRect().Size / 2;

		var start = _camera.ProjectRayOrigin(screenCenter);
		var end = start + _camera.ProjectRayNormal(screenCenter) * 1000;

		var query = PhysicsRayQueryParameters3D.Create(start, end);
		query.CollideWithBodies = true;
		
		var result = spaceState.IntersectRay(query);
		GD.Print(result);
	}
}
