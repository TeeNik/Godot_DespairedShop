using Godot;

public partial class MySprite2D : Sprite2D
{
    private int _speed = 400;
    private float _angularSpeed = Mathf.Pi;
    
    public MySprite2D()
    {
        GD.Print("Hello World!");
    }

    public override void _Ready()
    {
        var timer = GetNode<Timer>("Timer");
        timer.Timeout += OnTimerTimeout;

        var node = GetTree().Root.GetNode<NodeWithButton>("node_with_button");
        if (node != null)
        {
            node.HealthDepleted += OnHealthDepleted;
        }
    }

    private void OnHealthDepleted(int oldValue, int newValue)
    {
        GD.Print("Damage Taken: %d %d", oldValue, newValue);
    }

    public override void _Process(double delta)
    {
        Rotation += _angularSpeed * (float)delta;
        var velocity = Vector2.Up.Rotated(Rotation) * _speed;
        Position += velocity * (float)delta;
        
        //var direction = 0.0f;
        //if (Input.IsActionPressed("ui_left"))
        //{
        //    direction -= 1;
        //}
        //if (Input.IsActionPressed("ui_right"))
        //{
        //    direction += 1;
        //}
        //
        //Rotation += _angularSpeed * direction * (float)delta;

        //var velocity = Vector2.Zero;
        //if (Input.IsActionPressed("ui_up"))
        //{
        //    velocity = Vector2.Up.Rotated(Rotation) * _speed;
        //}
        //
        //Position += velocity * (float)delta;
    }

    private void OnButtonPressed()
    {
        SetProcess(!IsProcessing());
        GD.Print("OnButtonPressed");
    }
    
    private void OnTimerTimeout()
    {
        //Visible = !Visible;
    }
}
