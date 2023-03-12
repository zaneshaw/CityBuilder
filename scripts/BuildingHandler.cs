using Godot;
using System;

public partial class BuildingHandler : TileMap {
	private Vector2I tilePos;

	public override void _Ready() {
		
	}

	public override void _Process(double delta) {
		Vector2I newTilePos = LocalToMap(GetLocalMousePosition());

		if (newTilePos != tilePos) {
			SetCell(2, tilePos, -1);
			tilePos = newTilePos;
			SetCell(2, tilePos, 2, new Vector2I(0, 0));
		}

		if (Input.IsActionJustPressed("SecondaryInteract")) {
			SetCell(1, tilePos, -1);
			GD.Print(tilePos);
		}

		if (Input.IsActionJustPressed("PrimaryInteract")) {
			SetCell(1, tilePos, 1, new Vector2I(1, 0));
			GD.Print(tilePos);
		}
	}
}
