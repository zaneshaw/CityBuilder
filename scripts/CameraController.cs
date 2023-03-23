using Godot;

public partial class CameraController : Camera2D {
    [Export] float zoomSpeed = 0.05f;
    Vector2 mouseOld;

    public override void _Process(double delta) {
        Vector2 mouseDelta = GetViewport().GetMousePosition() - mouseOld;
        mouseOld += mouseDelta;

        if (Input.IsActionPressed("CameraPan")) {
            Position -= mouseDelta / Zoom;
        }

        if (Input.IsActionJustReleased("CameraZoomIn")) {
            Zoom = new Vector2(Zoom.X + zoomSpeed, Zoom.Y + zoomSpeed);
        }

        if (Input.IsActionJustReleased("CameraZoomOut")) {
            Zoom = new Vector2(Zoom.X - zoomSpeed, Zoom.Y - zoomSpeed);
        }
    }
}
