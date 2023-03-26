using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainGenerator : MonoBehaviour {
    [SerializeField] private Vector2Int mapSize;

    [SerializeField] private Tilemap terrainTilemap;
    [SerializeField] private List<TileBase> tiles;

    [SerializeField] private Gradient gradientTest;
    [SerializeField] private float scale = 1f;
    [SerializeField] private int heightScale = 2;

    [ContextMenu("Generate Map")]
    public void GenerateMap() {
        terrainTilemap.ClearAllTiles();

        for (int y = 0 - mapSize.y / 2; y < mapSize.y / 2f; y++) {
            for (int x = 0 - mapSize.x / 2; x < mapSize.x / 2f; x++) {
                float xV2 = x + mapSize.x / 2f;
                float yV2 = y + mapSize.y / 2f;
                float noiseValue = Mathf.PerlinNoise(0f + (float)xV2 / mapSize.x * scale, 0f + (float)yV2 / mapSize.y * scale);

                int spriteIndex = Mathf.RoundToInt((gradientTest.Evaluate(noiseValue).r * 255f) / 10f);
                int elevation = Mathf.RoundToInt((gradientTest.Evaluate(noiseValue).g * 255f) / 10f);

                terrainTilemap.SetTile(new Vector3Int(x, y, elevation * heightScale), tiles[spriteIndex]);
            }
        }
    }
}
