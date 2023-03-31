using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class TerrainGenerator : MonoBehaviour {
    [SerializeField] private Vector2Int mapSize;

    [SerializeField] public Tilemap tilemap;
    [SerializeField] private List<Tile> tiles;

    [SerializeField] private Gradient gradientTest;
    [SerializeField, Min(0)] private int offset;
    [SerializeField] private float scale = 1f;
    [SerializeField] private int heightScale = 2;

    [ContextMenu("Generate Map")]
    public void GenerateMap() {
        tilemap.ClearAllTiles();

        for (int y = 0 - mapSize.y / 2; y < mapSize.y / 2f; y++) {
            for (int x = 0 - mapSize.x / 2; x < mapSize.x / 2f; x++) {
                float adjX = ((x + offset) + mapSize.x / 2f) / scale;
                float adjY = ((y + offset) + mapSize.y / 2f) / scale;
                float noiseValue = Mathf.PerlinNoise(adjX, adjY);

                int spriteIndex = Mathf.RoundToInt((gradientTest.Evaluate(noiseValue).r * 255f) / 10f);
                int elevation = Mathf.RoundToInt((gradientTest.Evaluate(noiseValue).g * 255f) / 10f);

                tilemap.SetTile(new Vector3Int(x, y, elevation * heightScale), tiles[spriteIndex]);
            }
        }
    }
}
