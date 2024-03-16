using Godot;
using System;

public partial class Scroll : Control
{
	[Export] private Node _lines;

	private bool _pressed;
	private Line2D _currentLine;

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton)
		{
			_pressed = @event.IsPressed();

			if (_pressed)
			{
				_currentLine = new Line2D();
				_currentLine.Antialiased = true;
				_lines.AddChild(_currentLine);
			}
		}

		if (@event is InputEventMouseMotion mouseEvent && _pressed)
		{
			_currentLine.AddPoint(mouseEvent.Position);
			GD.Print(mouseEvent.Position);
		}
	}
}
