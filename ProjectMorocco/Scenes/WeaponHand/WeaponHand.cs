using Godot;
using System;

public partial class WeaponHand : Node3D
{
	
	private Player _player;

	private float _swingTime = 0.0f;
	private Vector3 _initialPosition;

	[Export] private Weapon _currentWeapon;
	
	[Export] public float SwingFrequency = 2.0f;
	[Export] public float SwingAmplitude = 0.08f;
	
	public override void _Ready()
	{
		_player = FindParent("Player") as Player;
		_initialPosition = Position;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (_player.IsOnFloor())
		{
			_swingTime += (float)delta * 2;
			Vector3 bobPos = new Vector3(Mathf.Cos(_swingTime * SwingFrequency * 0.5f) * SwingAmplitude, Mathf.Sin(_swingTime * SwingFrequency) * SwingAmplitude, 0);
			Position = _initialPosition + bobPos;
		}

		if (Input.IsActionJustPressed("attack"))
		{
			GD.Print("Attack!");
			_currentWeapon.Shoot();
		}
	}
	
}
