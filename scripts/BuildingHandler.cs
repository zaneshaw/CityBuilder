using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class BuildingHandler : TileMap {
	[ExportGroup("Layers")]
	[Export(PropertyHint.Range, "0,100,")] public int terrainLayer = 0;
	[Export(PropertyHint.Range, "0,100,")] public int buildingsLayer = 1;
	[Export(PropertyHint.Range, "0,100,")] public int highlightLayer = 2;

	[ExportGroup("Sources")]
	[Export(PropertyHint.Range, "0,100,")] public int building1Source = 1;
	[Export(PropertyHint.Range, "0,100,")] public Vector2I building1Coords = new Vector2I(1, 0);
	[Export(PropertyHint.Range, "0,100,")] public int highlightSource = 2;
	[Export(PropertyHint.Range, "0,100,")] public Vector2I highlightCoords = new Vector2I(0, 0);

    private Vector2I highlightPos;
    private List<Building> buildings = new List<Building>();

    public override void _Ready() {
        foreach (var building in GetUsedCells(1)) {
            PlaceBuilding(building, false);
        }
    }

    public override void _Process(double delta) {
        Vector2I newTilePos = LocalToMap(GetLocalMousePosition());

        if (newTilePos != highlightPos) {
            SetCell(highlightLayer, highlightPos, -1);
            highlightPos = newTilePos;
            SetCell(highlightLayer, highlightPos, highlightSource, highlightCoords);
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
    private bool PlaceBuilding(Vector2I coords, bool setCell = true) {
        if (GetCellSourceId(terrainLayer, coords) == -1 || buildings.Exists((building) => building.coords == coords)) {
            return false;
        }

        Building building = new Building { coords = coords };

        buildings.Add(building);
        GD.Print("Building placed!");
        GD.Print($"Buildings: {string.Join(",", buildings.Select((building) => building.coords).ToArray())}");

        if (setCell) SetCell(buildingsLayer, highlightPos, building1Source, building1Coords);

        return true;
    }

    /// <returns>
    /// True if the building was demolished
    /// </returns>
    private bool DemolishBuilding(Vector2I coords) {
        System.Predicate<Building> predicate = (building) => building.coords == coords;
        if (!buildings.Exists(predicate)) {
            return false;
        }

        Building building = buildings.Find(predicate);

        buildings.Remove(building);
        GD.Print("Building demolished!");
        GD.Print($"Buildings: {string.Join(",", buildings.Select((building) => building.coords).ToArray())}");

        SetCell(buildingsLayer, highlightPos, -1);

        return true;
    }

    public class Building {
        public Vector2I coords;
    }
}
