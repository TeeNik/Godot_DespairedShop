using Godot;
using System;

public partial class Beat : Control
{
	[Export] private Timer _destroyTimer;
	private Vector2 _fromPos;
	private Vector2 _toPos;
	private float _currentTime = 0.0f;
	private float _lifeTime;
	
	public override void _Ready()
	{
		_destroyTimer.Timeout += OnDestroyTimerTimeout;
	}
	
	public void InitBeat(float lifeTime, Vector2 fromPos, Vector2 toPos)
	{
		_lifeTime = lifeTime;
		_destroyTimer.Start(lifeTime);
		_fromPos = fromPos;
		_toPos = toPos;
	}

	public override void _Process(double delta)
	{
		_currentTime += (float)delta;
		Vector2 newPos = _fromPos.Lerp(_toPos, _currentTime / _lifeTime);
		SetPosition(newPos);
	}

	private void OnDestroyTimerTimeout()
	{
		QueueFree();
	}
}
