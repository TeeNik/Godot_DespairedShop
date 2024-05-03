using Godot;
using System;

public partial class HUD : CanvasLayer
{
	[Export] private Metronome _metronome;
	[Export] private Player _player;

	[Export] private TextureRect _playerShootIndicator;
	[Export] private Color _hitBeatColor;
	[Export] private Color _missBeatColor;
	
	public override void _Ready()
	{
		_player.Shoot += OnPlayerShoot;
	}

	private void OnPlayerShoot(bool isHitBeat)
	{
		ShowShootIndicator(isHitBeat);
	}

	private async void ShowShootIndicator(bool isHitBeat)
	{
		_playerShootIndicator.Show();
		_playerShootIndicator.Modulate = isHitBeat ? _hitBeatColor : _missBeatColor;
		
		await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
		_playerShootIndicator.Hide();
	}
}
