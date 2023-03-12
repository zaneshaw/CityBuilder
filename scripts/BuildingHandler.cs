using Godot;
using System.Collections.Generic;

public partial class BuildingHandler : TileMap {
    private Vector2I highlightPos;
    private List<Vector2I> buildings = new List<Vector2I>();

    public override void _Ready() {
        foreach (var building in GetUsedCells(1)) {
			PlaceBuilding(building, false);
        }
    }

    public override void _Process(double delta) {
        Vector2I newTilePos = LocalToMap(GetLocalMousePosition());

        if (newTilePos != highlightPos) {
            SetCell(2, highlightPos, -1);
            highlightPos = newTilePos;
            SetCell(2, highlightPos, 2, new Vector2I(0, 0));
        }

        if (Input.IsActionJustPressed("SecondaryInteract")) {
            DemolishBuilding(highlightPos);
        }

        if (Input.IsActionJustPressed("PrimaryInteract")) {
            PlaceBuilding(highlightPos);
        }
    }

    /// <returns>
    /// True if the building was placed
    /// </returns>
    private bool PlaceBuilding(Vector2I tilePosition, bool setCell = true) {
        if (GetCellSourceId(0, tilePosition) == -1 || buildings.Contains(tilePosition)) {
            return false;
        }

        buildings.Add(tilePosition);
        GD.Print("Building placed!");
        GD.Print($"Buildings: {string.Join(",", buildings.ToArray())}");

        if (setCell) SetCell(1, highlightPos, 1, new Vector2I(1, 0));

        return true;
    }

    /// <returns>
    /// True if the building was demolished
    /// </returns>
    private bool DemolishBuilding(Vector2I tilePosition) {
        if (!buildings.Contains(tilePosition)) {
            return false;
        }

        buildings.Remove(tilePosition);
        GD.Print("Building demolished!");
        GD.Print($"Buildings: {string.Join(",", buildings.ToArray())}");

        SetCell(1, highlightPos, -1);

        return true;
    }
}
