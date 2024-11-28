using Godot;
using System;

public partial class Player : CharacterBody3D
{
    [ExportGroup("Movement")]
    [Export] public float Speed = 3.5f;
    [Export] public float JumpVelocity = 4.5f;
    
    [Export] public float Acceleration = 5.0f;
    [Export] public bool SmoothMotion = true;
    [Export] public bool CanMoveInAir = true;
    [Export] public bool JumpEnabled = true;
    [Export] public bool ContinuousJump = true;
    [Export] public float Sensivity = 0.003f;
    
    [ExportGroup("Camera Bobing")]
    [Export] public bool CameraBobing = true;
    [Export] public float BobFrequency = 2.0f;
    [Export] public float BobAmplitude = 0.08f;

    [ExportGroup("Dynamic FOV")]
    [Export] public bool DynamicFOV = true;
    [Export] public float StantingFOV = 80.0f;
    [Export] public float RunningFOV = 85.0f;
    
    private float _bobTime = 0.0f;

    private static readonly float Gravity = (float)ProjectSettings.GetSetting("physics/3d/default_gravity").AsDouble();

    [Export] public bool GravityEnabled = true;

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
            
            _head.RotationDegrees -= new Vector3(mouseEvent.Relative.Y, mouseEvent.Relative.X, 0) * Sensivity;
            _head.Rotation = new Vector3(Mathf.Clamp(_head.Rotation.X, Mathf.DegToRad(-40), Mathf.DegToRad(60)), _head.Rotation.Y, _head.Rotation.Z);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        //gravity
        ProcessGravity(delta);

        //jumping
        ProcessJump();
        
        //movement
        var inputDir = Input.GetVector("move_left", "move_right", "move_forward", "move_backward");
        ProcessMovement(delta, inputDir);

        //camera bob
        ProcessCameraBob(delta);
        
        //update FOV
        ProcessFOV();
    }

    private void ProcessFOV()
    {
        if (DynamicFOV)
        {
            float alpha = Mathf.Clamp(Mathf.InverseLerp(0.0f, Speed, Velocity.Length()), 0.0f, 1.0f);
            _camera.Fov = Mathf.Lerp(StantingFOV, RunningFOV, alpha);
        }
    }

    private void ProcessCameraBob(double delta)
    {
        if (CameraBobing)
        {
            if (IsOnFloor())
            {
                _bobTime += (float)delta * Velocity.Length();
                Vector3 bobPos = new Vector3(Mathf.Cos(_bobTime * BobFrequency * 0.5f) * BobAmplitude, Mathf.Sin(_bobTime * BobFrequency) * BobAmplitude, 0);
                _head.Position = bobPos;
            }
        }
    }

    private void ProcessMovement(double delta, Vector2 inputDir)
    {
        var direction = inputDir.Rotated(-_head.Rotation.Y);
        MoveAndSlide();

        if (!inputDir.IsZeroApprox() && Input.IsActionJustPressed("shift"))
        {
            direction *= 100;
        }

        if (CanMoveInAir || IsOnFloor())
        {
            float velocityX = Velocity.X;
            float velocityZ = Velocity.Z;
            if (SmoothMotion)
            {
                velocityX = Mathf.Lerp(velocityX, direction.X * Speed, Acceleration * (float)delta);
                velocityZ = Mathf.Lerp(velocityZ, direction.Y * Speed, Acceleration * (float)delta);
            }
            else
            {
                velocityX = direction.X * Speed;
                velocityZ = direction.Y * Speed;
            }
            Velocity = new Vector3(velocityX, Velocity.Y, velocityZ);
        }
    }

    private void ProcessJump()
    {
        if (JumpEnabled && IsOnFloor())
        {
            if (ContinuousJump)
            {
                if (Input.IsActionPressed("jump"))
                {
                    AddVelocity(0, JumpVelocity, 0);
                }
            }
            else
            {
                if (Input.IsActionJustPressed("jump"))
                {
                    AddVelocity(0, JumpVelocity, 0);
                }
            }
        }
    }

    private void ProcessGravity(double delta)
    {
        if (!IsOnFloor() && GravityEnabled)
        {
            AddVelocity(0, -Gravity * (float)delta, 0);
        }
    }


    private void AddVelocity(float x, float y, float z)
    {
        Velocity += new Vector3(x, y, z);
    }

}
