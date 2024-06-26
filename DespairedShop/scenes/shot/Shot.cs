using Godot;
using System;

public partial class Shot : Area2D
{
	[Export] private Timer _returnTimer;
	[Export] private AnimationPlayer _animPlayer;
	[Export] private float _returnTimeoutInBeats = 4;
	[Export] private float _throwSpeed = 5.0f;
	[Export] private float _returnSpeed = 5.0f;
	
	private Node _slot;
	private float _returnTimeout;

	private Vector2 _velocity;
	private bool _isAvailable;
	
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		Position += _velocity;
	}

	public void Init(Node slot)
	{
		_slot = slot;
		_slot.AddChild(this);
		_returnTimeout = _returnTimeoutInBeats * Metronome.Get().BeatPeriod;
		_returnTimer.Timeout += OnReturnTimerTimeout;
		_isAvailable = true;
	}

	private void OnReturnTimerTimeout()
	{
		var parent = GetParent();
		parent.RemoveChild(this);
		_velocity = Vector2.Zero;
		_animPlayer.Play("Appear");
		_slot.AddChild(this);
		Position = Vector2.Zero;
		_isAvailable = true;
	}

	public void Shoot(Vector2 mousePos)
	{
		var worldPos = GlobalPosition;
		var root = GetTree().Root;
		_slot.RemoveChild(this);
		root.AddChild(this);
		GlobalPosition = worldPos;
		Vector2 dir = (mousePos - worldPos).Normalized();
		_velocity += dir * _throwSpeed;
		_isAvailable = false;
		
		_returnTimer.Start(_returnTimeout);
	}

	public void Dissolve()
	{
		_animPlayer.Play("Dissolve");
		_isAvailable = false;
		
		_returnTimer.Start(_returnTimeout);
	}

	public bool IsAvailable()
	{
		return _isAvailable;
	}
}
