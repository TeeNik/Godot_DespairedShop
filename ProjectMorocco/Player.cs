using Godot;
using System;

public partial class Player : CharacterBody3D
{
    private const float SPEED = 3.5f;
    const float JUMP_VELOCITY = 4.5f;

    private static readonly float Gravity = (float)ProjectSettings.GetSetting("physics/3d/default_gravity").AsDouble();

    public override void _PhysicsProcess(double delta)
    {
        Vector3 newVelocity = Velocity;
        if (!IsOnFloor())
        {
            newVelocity.Y -= Gravity * (float)delta;
        }

        if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
        {
            newVelocity.Y = JUMP_VELOCITY;
        }
        
        var inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        var direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
        if (direction != Vector3.Zero)
        {
            newVelocity.X = direction.X * SPEED;
            newVelocity.Z = direction.Z * SPEED;
        }
        else
        {
            newVelocity.X = Mathf.MoveToward(newVelocity.X, 0, SPEED);
            newVelocity.Z = Mathf.MoveToward(newVelocity.Z, 0, SPEED);
        }

        Velocity = newVelocity;
        MoveAndSlide();
    }
}
