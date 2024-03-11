using Godot;
using System;
using System.Diagnostics.Tracing;

public partial class Player : CharacterBody3D
{
    [Signal] public delegate void HitEventHandler();
    
    [Export] public int Speed { get; set; } = 14;
    [Export] public int FallAcceleration { get; set; } = 75;
    [Export] public int JumpImpulse { get; set; } = 20;
    [Export] public int BounceImpulse { get; set; } = 16;
    
    private Vector3 _targetVelocity = Vector3.Zero;

    private Node3D _pivot;
    private Area3D _mobDetector;
    
    public override void _Ready()
    {
        _pivot = GetNode<Node3D>("Pivot");
        _mobDetector = GetNode<Area3D>("MobDetector");
        
        _mobDetector.BodyEntered += OnBodyEnteredMobDetector;
    }

    public override void _PhysicsProcess(double delta)
    {
        var direction = Vector3.Zero;
        
        if (Input.IsActionPressed("move_right"))
        {
            direction.X += 1.0f;
        }
        if (Input.IsActionPressed("move_left"))
        {
            direction.X -= 1.0f;
        }
        if (Input.IsActionPressed("move_back"))
        {
            direction.Z += 1.0f;
        }
        if (Input.IsActionPressed("move_forward"))
        {
            direction.Z -= 1.0f;
        }

        if (direction != Vector3.Zero)
        {
            direction.Normalized();
            _pivot.Basis = Basis.LookingAt(direction);
        }

        _targetVelocity.X = direction.X * Speed;
        _targetVelocity.Z = direction.Z * Speed;

        if (!IsOnFloor())
        {
            _targetVelocity.Y -= FallAcceleration * (float)delta;
        }
        if (IsOnFloor() && Input.IsActionPressed("jump"))
        {
            _targetVelocity.Y = JumpImpulse;
        }

        Velocity = _targetVelocity;
        MoveAndSlide();

        for (int i = 0; i < GetSlideCollisionCount(); ++i)
        {
            KinematicCollision3D collision = GetSlideCollision(i);
            if (collision.GetCollider() is Mob mob)
            {
                if (Vector3.Up.Dot(collision.GetNormal()) > 0.1f)
                {
                    mob.Squash();
                    _targetVelocity.Y = BounceImpulse;
                    break;
                }
            }
        }
    }
    
    private void OnBodyEnteredMobDetector(Node3D body)
    {
        Die();
    }

    private void Die()
    {
        EmitSignal(SignalName.Hit);
        QueueFree();
    }
}
