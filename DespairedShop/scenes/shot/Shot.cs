using Godot;
using System;

public partial class Shot : Area2D
{
	[Export] private Timer _returnTimer;
	[Export] private int _returnTimeoutInBeats = 4;
	[Export] private float _throwSpeed = 10.0f;
	[Export] private float _returnSpeed = 10.0f;
	
	private Node2D _slot;
	private float _returnTimeout;

	private Vector2 _velocity;
	
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		Position += _velocity;
	}

	public void Init(Node2D slot, int bpm)
	{
		_slot = slot;
		_returnTimeout = _returnTimeoutInBeats * (60.0f / bpm);
		_returnTimer.Timeout += OnReturnTimerTimeout;
	}

	private void OnReturnTimerTimeout()
	{
		
	}

	public void Shoot(Vector2 dir)
	{
		GetTree().Root.AddChild(this);

		_velocity += dir * _throwSpeed;
	}

	public bool IsAvailable()
	{
		return false;
	}
}
