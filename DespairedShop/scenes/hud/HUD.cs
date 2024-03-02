using Godot;
using System;

public partial class HUD : CanvasLayer
{
	[Signal]
	public delegate void StartGameEventHandler();

	private Label _message;
	private Timer _messageTimer;
	private Button _startButton;
	private Label _scoreLabel;
	
	public override void _Ready()
	{
		_message = GetNode<Label>("Message");
		_messageTimer = GetNode<Timer>("MessageTimer");
		_startButton = GetNode<Button>("StartButton");
		_scoreLabel = GetNode<Label>("ScoreLabel");
		
		_startButton.Pressed += OnStartButtonPressed;
		_messageTimer.Timeout += OnMessageTimerTimeout;
	}

	public void ShowMessage(string text)
	{
		_message.Text = text;
		_message.Show();
		
		_messageTimer.Start();
	}

	async public void ShowGameOver()
	{
		ShowMessage("Game Over");
		
		await ToSignal(_messageTimer, Timer.SignalName.Timeout);

		_message.Text = "Dodge the Creeps!";
		_message.Show();

		await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
		_startButton.Show();
	}

	public void UpdateScore(int score)
	{
		_scoreLabel.Text = score.ToString();
	}
	
	private void OnMessageTimerTimeout()
	{
		_message.Hide();
	}

	private void OnStartButtonPressed()
	{
		_startButton.Hide();
		EmitSignal(SignalName.StartGame);
	}
}
