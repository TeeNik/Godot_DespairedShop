using Godot;
using System;

public partial class Player : CharacterBody3D
{
    private const float SPEED = 3.5f;
    private const float JUMP_VELOCITY = 4.5f;
    private const float SENSIVITY = 0.003f;

    private static readonly float Gravity = (float)ProjectSettings.GetSetting("physics/3d/default_gravity").AsDouble();

    [Export] private Node3D _head;
    [Export] private Camera3D _camera;
    
    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseMotion eventMouseMotion)
        {
            var mouseEvent = @event as InputEventMouseMotion;
            _head.RotateY(-@mouseEvent.Relative.X * SENSIVITY);
            _camera.RotateX(-mouseEvent.Relative.Y * SENSIVITY);
            _camera.Rotation = new Vector3(Mathf.Clamp(_camera.Rotation.X, Mathf.DegToRad(-40), Mathf.DegToRad(60)), _camera.Rotation.Y, _camera.Rotation.Z);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector3 newVelocity = Velocity;
        if (!IsOnFloor())
        {
            newVelocity.Y -= Gravity * (float)delta;
        }

        if (Input.IsActionJustPressed("jump") && IsOnFloor())
        {
            newVelocity.Y = JUMP_VELOCITY;
        }
        
        var inputDir = Input.GetVector("move_left", "move_right", "move_forward", "move_backward");
        var direction = (_head.Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
        if (direction != Vector3.Zero)
        {
            newVelocity.X = direction.X * SPEED;
            newVelocity.Z = direction.Z * SPEED;
        }
        else
        {
            newVelocity.X = 0.0f;
            newVelocity.Z = 0.0f;
        }

        Velocity = newVelocity;
        MoveAndSlide();
    }
}
