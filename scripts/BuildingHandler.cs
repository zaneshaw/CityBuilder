using Godot;
using System.Collections.Generic;

public partial class BuildingHandler : TileMap {
    [Export] public TerrainGenerator terrain;
    [Export] public Godot.Collections.Array<Resource> buildingList;
    [Export] public CameraController camera;
    [Export] public CanvasLayer canvasLayer;
    [Export] public Label scoreLabel;

    [ExportGroup("Sources")]
    [Export(PropertyHint.Range, "0,100,")] public int highlightTileSource = 2;
    [Export(PropertyHint.Range, "0,100,")] public Vector2I highlightTileCoords = new Vector2I(0, 0);

    private bool flip;
    private Vector2I highlightPos;
    private int highlightZ;
    private bool noPlace;
    private List<Building> buildings = new List<Building>();
    private BuildingData currentBuilding;
    private int score;

    public override void _Ready() {
        currentBuilding = new BuildingData {
            source = (int)buildingList[1].Get("source"),
            coords = (Vector2I)buildingList[1].Get("coords"),
            duration = (float)buildingList[1].Get("duration"),
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

        SimulateBuildings((float)delta);
    }

    private void SimulateBuildings(float delta) {
        for (int i = 0; i < buildings.Count; i++) {
            buildings[i].timeLeft -= delta;

            if (buildings[i].timeLeft <= 0d) {
                buildings[i].timeLeft = buildings[i].buildingData.duration;

                Vector2 pos = MapToLocal(buildings[i].coords) + GetViewportRect().Size / 2f;
                pos.Y -= 40f;

                Label label = new Label();
                label.Name = "Score Effect";
                label.SetPosition(pos - camera.Position);
                label.Text = "+1";
                canvasLayer.AddChild(label);

                animate(label);

                score++;
            }
        }
        scoreLabel.Text = score.ToString();

        async void animate(Label label) {
            float distance = 250f;
            float currentDistance = 0f;
            float speed = 4f;

            while (currentDistance <= distance) {
                label.SetPosition(new Vector2(label.Position.X, label.Position.Y - speed));
                currentDistance += speed;
                await ToSignal(GetTree().CreateTimer(0.01f), "timeout");
            }

            canvasLayer.RemoveChild(label);
        }
    }

    /// <returns>
    /// True if the building was placed
    /// </returns>
    private bool PlaceBuilding(Vector2I coords, bool setCell = true) {
        if (!IsTerrainTile(coords) || IsBuilding(coords)) {
            return false;
        }

        Building building = new Building { coords = coords, flipped = flip, timeLeft = currentBuilding.duration, buildingData = currentBuilding };

        buildings.Add(building);

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
        public float timeLeft;
        public BuildingData buildingData;
    }

    public struct BuildingData {
        public int source;
        public Vector2I coords;
        public float duration;
    }
}
