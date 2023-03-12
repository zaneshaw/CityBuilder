using Godot;
using System;

public partial class BuildingHandler : TileMap {
	public override void _Ready() {
		
	}

	public override void _Process(double delta) {
		if (Input.IsActionJustPressed("SecondaryInteract")) {
			Vector2I tilePos = LocalToMap(GetLocalMousePosition());
			SetCell(1, tilePos, -1);
			GD.Print(tilePos);
		}

		if (Input.IsActionJustPressed("PrimaryInteract")) {
			Vector2I tilePos = LocalToMap(GetLocalMousePosition());
			SetCell(1, tilePos, 1, new Vector2I(1, 0));
			GD.Print(tilePos);
		}
	}
}
