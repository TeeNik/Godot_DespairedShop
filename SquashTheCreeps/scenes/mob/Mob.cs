using Godot;

public partial class Mob : CharacterBody3D
{
    [Signal]
    public delegate void SquashedEventHandler();
    
    [Export] 
    public int MinSpeed { get; set; } = 10;
    [Export] 
    public int MaxSpeed { get; set; } = 15;

    [Export] public AnimationPlayer AnimationPlayer { get; set; }

    private VisibleOnScreenEnabler3D _visibleOnScreenEnabler;

    public void Initialize(Vector3 startPosition, Vector3 playerPosition)
    {
        LookAtFromPosition(startPosition, playerPosition, Vector3.Up);
        RotateY((float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4));

        int speed = GD.RandRange(MinSpeed, MaxSpeed);
        Velocity = Vector3.Forward * speed;
        Velocity = Velocity.Rotated(Vector3.Up, Rotation.Y);

        AnimationPlayer.SpeedScale = (float)speed / MinSpeed;
    }

    public override void _Ready()
    {
        _visibleOnScreenEnabler = GetNode<VisibleOnScreenEnabler3D>("VisibleOnScreenEnabler3D");
        _visibleOnScreenEnabler.ScreenExited += OnScreenExited;
    }

    private void OnScreenExited()
    {
        QueueFree();
    }

    public override void _PhysicsProcess(double delta)
    {
        MoveAndSlide();
    }

    public void Squash()
    {
        EmitSignal(SignalName.Squashed);
        QueueFree();
    }
}
