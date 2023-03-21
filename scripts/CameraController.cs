using Godot;

public partial class CameraController : Camera2D {
	[Export] float zoomSpeed = 0.05f;
	Vector2 mouseOld;

    public override void _Process(double delta) {
		Vector2 mouseDelta = GetViewport().GetMousePosition() - mouseOld;
		mouseOld += mouseDelta;

		if (Input.IsActionPressed("CameraPan")) {
			Position -= mouseDelta;
		}

		if (Input.IsActionJustReleased("CameraZoomIn")) {
			GD.Print(Zoom);
			Zoom = new Vector2(Zoom.X + zoomSpeed, Zoom.Y + zoomSpeed);
			GD.Print(Zoom);
		}

		if (Input.IsActionJustReleased("CameraZoomOut")) {
			GD.Print(Zoom);
			Zoom = new Vector2(Zoom.X - zoomSpeed, Zoom.Y - zoomSpeed);
			GD.Print(Zoom);
		}
    }
}
