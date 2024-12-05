using Godot;
using System;

public partial class ColorMenu : NinePatchRect
{
	[Export] private Button _button1;
	[Export] private Button _button2;
	[Export] private Button _button3;
	
	[Export] private ColorRect _backgroundColor;

	private bool _selfOpen = false;

	public override void _Ready()
	{
		_button1.Pressed += Button1OnPressed;
		_button2.Pressed += Button2OnPressed;
		_button3.Pressed += Button3OnPressed;
	}

	private void Button1OnPressed()
	{
		throw new NotImplementedException();
	}

	private void Button2OnPressed()
	{
		throw new NotImplementedException();
	}

	private void Button3OnPressed()
	{
		throw new NotImplementedException();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("ui_accept"))
		{
			ToggleMenu();
		}
	}

	private void ToggleMenu()
	{
		_selfOpen = !_selfOpen;
		Visible = _selfOpen;
	}
}
