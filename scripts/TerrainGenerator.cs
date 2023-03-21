using Godot;
using System.Collections.Generic;

[Tool]
public partial class TerrainGenerator : TileMap {
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
    [Export] public int octaves = 5;
    [Export] public float lacunarity = 1f;

    private void GenerateMap() {
        Clear();

        FastNoiseLite noise = new FastNoiseLite();

        noise.NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex;
        noise.Seed = 1;
        noise.FractalOctaves = octaves;
        noise.FractalLacunarity = lacunarity;

        int maxElevation = gradientTest.GetPointCount();
        for (float y = 0f - mapSize.Y / 2f; y < mapSize.Y / 2f; y++) {
            for (float x = 0f - mapSize.X / 2f; x < mapSize.X / 2f; x++) {
                int isoX = Mathf.FloorToInt((x - y - 1) * 0.5f);
                int isoY = Mathf.FloorToInt((x + y + (mapSize.Y % 2 == 1 ? 0 : 1)) * 1f);

                float noiseValue = (noise.GetNoise2D(x * noiseReduce, y * noiseReduce) + 1) / 2f;
                int spriteIndex = Mathf.RoundToInt(gradientTest.Sample(noiseValue).R * 10f);
                int elevation = Mathf.RoundToInt(gradientTest.Sample(noiseValue).G * 10f);

                SetCell(elevation, new Vector2I(isoX, isoY + ((maxElevation - elevation) * 2)), 0, new Vector2I(spriteIndex, 0));
            }
        }

        int layers = GetLayersCount();
        for (int i = layers; i < maxElevation; i++) {
            AddLayer(-1);
            SetLayerYSortEnabled(-1, true);
        }
        for (int i = 0; i < GetLayersCount(); i++) {
            SetLayerZIndex(i, i);
        }
    }
}
