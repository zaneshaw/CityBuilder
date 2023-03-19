using Godot;
using System.Collections.Generic;

[Tool]
public partial class BuildingHandler : TileMap {
    [Export] public Godot.Collections.Array<Resource> buildingList;

    [ExportGroup("Generation")]
    [Export]
    public bool generateMap {
        get => false;
        set {
            if (value) {
                GenerateMap();
            }
        }
    }
    [Export] public Vector2I mapSize;
    [Export] public int noiseReduce = 1;
    [Export] public Gradient gradientTest;

    [ExportGroup("Layers")]
    [Export(PropertyHint.Range, "0,100,")] public int terrainLayer = 0;
    [Export(PropertyHint.Range, "0,100,")] public int highlightLayer = 2;

    [ExportGroup("Sources")]
    [Export(PropertyHint.Range, "0,100,")] public int highlightTileSource = 2;
    [Export(PropertyHint.Range, "0,100,")] public Vector2I highlightTileCoords = new Vector2I(0, 0);

    private bool flip;
    private Vector2I highlightPos;
    private bool noPlace;
    private List<Building> buildings = new List<Building>();
    private BuildingData currentBuilding;

    public override void _Ready() {
        if (!Engine.IsEditorHint()) {
            currentBuilding = new BuildingData {
                layer = (int)buildingList[1].Get("layer"),
                source = (int)buildingList[1].Get("source"),
                coords = (Vector2I)buildingList[1].Get("coords"),
            };

            foreach (var building in GetUsedCells(1)) {
                PlaceBuilding(building, false);
            }
        }
    }

    public override void _Process(double delta) {
        if (!Engine.IsEditorHint()) {
            Vector2I newTileCoords = LocalToMap(GetLocalMousePosition());

            noPlace = false;
            if (IsTerrainTile(newTileCoords)) {
                if (newTileCoords != highlightPos) {
                    UpdateGhost(newTileCoords);
                }
            } else {
                noPlace = true;
                SetCell(highlightLayer, highlightPos, -1);
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

        if (setCell) SetCell(currentBuilding.layer, highlightPos, currentBuilding.source, currentBuilding.coords, flip ? 1 : 0);

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

        SetCell(currentBuilding.layer, highlightPos, -1);

        return true;
    }

    private void UpdateGhost(Vector2I coords) {
        SetCell(highlightLayer, highlightPos, -1);
        highlightPos = coords;
        SetCell(highlightLayer, highlightPos, currentBuilding.source, currentBuilding.coords, flip ? 1 : 0);
    }

    private void GenerateMap() {
        ClearLayer(terrainLayer);

        FastNoiseLite noise = new FastNoiseLite();
        RandomNumberGenerator RNG = new RandomNumberGenerator();
        RNG.Randomize();
        GD.Print(RNG.Seed);
        
        noise.NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex;
        noise.Seed = (int)GD.Randi();
        noise.FractalOctaves = 4;
        noise.FractalLacunarity = 1.5f;

        for (float y = 0f - mapSize.Y / 2f; y < mapSize.Y / 2f; y++) {
            for (float x = 0f - mapSize.X / 2f; x < mapSize.X / 2f; x++) {
                int isoX = Mathf.FloorToInt((x - y - 1) * 0.5f);
                int isoY = Mathf.FloorToInt((x + y + (mapSize.Y % 2 == 1 ? 0 : 1)) * 1f);

                float noiseValue = (noise.GetNoise2D(x * noiseReduce, y * noiseReduce) + 1) / 2f;
                int spriteIndex = Mathf.RoundToInt(gradientTest.Sample(noiseValue).R * 10f);

                SetCell(terrainLayer, new Vector2I(isoX, isoY), 0, new Vector2I(spriteIndex, 0));
            }
        }
    }

    private bool IsTerrainTile(Vector2I coords) {
        return GetCellSourceId(terrainLayer, coords) != -1;
    }

    private bool IsBuilding(Vector2I coords) {
        return buildings.Exists((building) => building.coords == coords);
    }

    public class Building {
        public Vector2I coords;
        public bool flipped;
    }

    public struct BuildingData {
        public int layer;
        public int source;
        public Vector2I coords;
    }
}
