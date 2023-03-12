using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class BuildingHandler : TileMap {
    public bool flip;

    [ExportGroup("Layers")]
    [Export(PropertyHint.Range, "0,100,")] public int terrainLayer = 0;
    [Export(PropertyHint.Range, "0,100,")] public int buildingsLayer = 1;
    [Export(PropertyHint.Range, "0,100,")] public int highlightLayer = 2;

    [ExportGroup("Sources")]
    [Export(PropertyHint.Range, "0,100,")] public int building1TileSource = 1;
    [Export(PropertyHint.Range, "0,100,")] public Vector2I building1TileCoords = new Vector2I(1, 0);
    [Export(PropertyHint.Range, "0,100,")] public int highlightTileSource = 2;
    [Export(PropertyHint.Range, "0,100,")] public Vector2I highlightTileCoords = new Vector2I(0, 0);

    private Vector2I highlightPos;
    private bool noPlace;
    private List<Building> buildings = new List<Building>();

    public override void _Ready() {
        foreach (var building in GetUsedCells(1)) {
            PlaceBuilding(building, false);
        }
    }

    public override void _Process(double delta) {
        Vector2I newTileCoords = LocalToMap(GetLocalMousePosition());

		noPlace = false;
        if (IsTerrainTile(newTileCoords)) {
            if (newTileCoords != highlightPos) {
                SetCell(highlightLayer, highlightPos, -1);
                highlightPos = newTileCoords;
                SetCell(highlightLayer, highlightPos, building1TileSource, building1TileCoords);
            }
        } else {
			noPlace = true;
            SetCell(highlightLayer, highlightPos, -1);
        }

        if (Input.IsActionJustPressed("FlipBuilding")) {
            flip = !flip;
        }

        if (Input.IsActionJustPressed("SecondaryInteract") && !noPlace) {
            DemolishBuilding(highlightPos);
        }

        if (Input.IsActionJustPressed("PrimaryInteract") && !noPlace) {
            PlaceBuilding(highlightPos);
        }
    }

    /// <returns>
    /// True if the building was placed
    /// </returns>
    private bool PlaceBuilding(Vector2I coords, bool setCell = true) {
        if (!IsTerrainTile(coords) || IsBuilding(coords)) {
            return false;
        }

        Building building = new Building { coords = coords };

        buildings.Add(building);
        GD.Print("Building placed!");

        if (setCell) SetCell(buildingsLayer, highlightPos, building1TileSource, building1TileCoords, flip ? 1 : 0);

        return true;
    }

    /// <returns>
    /// True if the building was demolished
    /// </returns>
    private bool DemolishBuilding(Vector2I coords) {
        if (!IsBuilding(coords)) {
            return false;
        }

        Building building = buildings.Find((building) => building.coords == coords);

        buildings.Remove(building);
        GD.Print("Building demolished!");

        SetCell(buildingsLayer, highlightPos, -1);

        return true;
    }

    private bool IsTerrainTile(Vector2I coords) {
        return GetCellSourceId(terrainLayer, coords) != -1;
    }

    private bool IsBuilding(Vector2I coords) {
        return buildings.Exists((building) => building.coords == coords);
    }

    public class Building {
        public Vector2I coords;
    }
}
