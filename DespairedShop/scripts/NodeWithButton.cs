using Godot;
using System;

public partial class NodeWithButton : Node2D
{
	[Signal]
	public delegate void HealthDepletedEventHandler(int oldValue, int newValue);

	private int _health = 10;
	
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("ui_down"))
		{
			TakeDamage(5);
		}
	}

	public void TakeDamage(int damage)
	{
		int oldHealth = _health;
		_health -= damage;
		if (_health <= 0)
		{
			EmitSignal(SignalName.HealthDepleted, oldHealth, _health);
		}
	}
}
