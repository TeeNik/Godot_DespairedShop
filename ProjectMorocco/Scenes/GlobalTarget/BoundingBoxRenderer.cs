/*using Godot;
using System;

public partial class BoundingBoxDrawer : Node2D
{
    [Export]
    public Node3D TargetObject { get; set; }

    [Export]
    public Color BoxColor { get; set; } = Colors.White;

    [Export]
    public float LineWidth { get; set; } = 2.0f;

    private Camera3D _camera;

    public override void _Ready()
    {
        _camera = GetViewport().GetCamera3D();
    }

    public override void _Process(double delta)
    {
        QueueRedraw();
    }

    public override void _Draw()
    {
        if (TargetObject == null || _camera == null)
            return;

        // Get the world-space AABB of the object
        var globalTransform = TargetObject.GlobalTransform;
        var mesh = TargetObject.GetNode<MeshInstance3D>("MeshInstance3D");
        
        Aabb objectBounds;
        if (mesh != null && mesh.Mesh != null)
        {
            // Use the mesh's local AABB and transform it to world space
            objectBounds = mesh.Mesh.GetAabb().Transformed(globalTransform);
        }
        else
        {
            // Fallback to the object's global AABB
            objectBounds = TargetObject.GetGlobalTransformWithGlobalInverse().Affine_Inverse() * TargetObject.GetAabb();
        }

        // Calculate the 8 corners of the bounding box
        var corners = new Vector3[]
        {
            new Vector3(objectBounds.Position.X, objectBounds.Position.Y, objectBounds.Position.Z),
            new Vector3(objectBounds.Position.X + objectBounds.Size.X, objectBounds.Position.Y, objectBounds.Position.Z),
            new Vector3(objectBounds.Position.X, objectBounds.Position.Y + objectBounds.Size.Y, objectBounds.Position.Z),
            new Vector3(objectBounds.Position.X + objectBounds.Size.X, objectBounds.Position.Y + objectBounds.Size.Y, objectBounds.Position.Z),
            new Vector3(objectBounds.Position.X, objectBounds.Position.Y, objectBounds.Position.Z + objectBounds.Size.Z),
            new Vector3(objectBounds.Position.X + objectBounds.Size.X, objectBounds.Position.Y, objectBounds.Position.Z + objectBounds.Size.Z),
            new Vector3(objectBounds.Position.X, objectBounds.Position.Y + objectBounds.Size.Y, objectBounds.Position.Z + objectBounds.Size.Z),
            new Vector3(objectBounds.Position.X + objectBounds.Size.X, objectBounds.Position.Y + objectBounds.Size.Y, objectBounds.Position.Z + objectBounds.Size.Z)
        };

        // Project corners to 2D screen space
        var projectedCorners = new Vector2[8];
        float minX = float.MaxValue, maxX = float.MinValue;
        float minY = float.MaxValue, maxY = float.MinValue;

        for (int i = 0; i < corners.Length; i++)
        {
            var screenPos = _camera.UnprojectPosition(corners[i]);
            projectedCorners[i] = screenPos;

            // Track min and max screen coordinates
            minX = Mathf.Min(minX, screenPos.X);
            maxX = Mathf.Max(maxX, screenPos.X);
            minY = Mathf.Min(minY, screenPos.Y);
            maxY = Mathf.Max(maxY, screenPos.Y);
        }

        // Create a 2D rectangle from the min/max coordinates
        var rect = new Rect2(minX, minY, maxX - minX, maxY - minY);

        // Draw the 2D bounding rectangle
        DrawRect(rect, BoxColor, false, LineWidth);
    }
}*/