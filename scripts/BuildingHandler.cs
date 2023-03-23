using Godot;
using System.Collections.Generic;

public partial class BuildingHandler : TileMap {
    [Export] public TerrainGenerator terrain;
    [Export] public Godot.Collections.Array<Resource> buildingList;

    [ExportGroup("Sources")]
    [Export(PropertyHint.Range, "0,100,")] public int highlightTileSource = 2;
    [Export(PropertyHint.Range, "0,100,")] public Vector2I highlightTileCoords = new Vector2I(0, 0);

    private bool flip;
    private Vector2I highlightPos;
    private int highlightZ;
    private bool noPlace;
    private List<Building> buildings = new List<Building>();
    private BuildingData currentBuilding;

    public override void _Ready() {
        currentBuilding = new BuildingData {
            source = (int)buildingList[1].Get("source"),
            coords = (Vector2I)buildingList[1].Get("coords"),
        };

        foreach (var building in GetUsedCells(1)) {
            PlaceBuilding(building, false);
        }
    }

    public override void _Process(double delta) {
        Vector2I newTileCoords = LocalToMap(GetLocalMousePosition());

        noPlace = false;
        if (IsTerrainTile(newTileCoords)) {
            if (newTileCoords != highlightPos) {
                UpdateGhost(newTileCoords);
            }
        } else {
            noPlace = true;
            SetCell(1, highlightPos, -1);
        }

        if (Input.IsActionJustPressed("FlipBuilding")) {
            flip = !flip;
            UpdateGhost(highlightPos);
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

        Building building = new Building { coords = coords, flipped = flip };

        buildings.Add(building);
        GD.Print("Building placed!");

        if (setCell) SetCell(0, highlightPos, currentBuilding.source, currentBuilding.coords, flip ? 1 : 0);

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

        SetCell(0, highlightPos, -1);

        return true;
    }

    private void UpdateGhost(Vector2I coords) {
        SetCell(1, highlightPos, -1);
        highlightPos = coords;
        SetCell(1, highlightPos, currentBuilding.source, currentBuilding.coords, flip ? 1 : 0);

        SetLayerZIndex(1, highlightZ);
    }

    private bool IsTerrainTile(Vector2I coords) {
        for (int i = terrain.GetLayersCount() - 1; i >= 0; i--) {
            if (terrain.GetCellSourceId(i, coords) != -1) {
                highlightZ = i + 1; // Temporary fix (This script needs a rewrite)
                return true;
            }
        }

        return false;
    }

    private bool IsBuilding(Vector2I coords) {
        return buildings.Exists((building) => building.coords == coords);
    }

    public class Building {
        public Vector2I coords;
        public bool flipped;
    }

    public struct BuildingData {
        public int source;
        public Vector2I coords;
    }
}
