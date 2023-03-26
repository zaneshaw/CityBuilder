using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainGenerator : MonoBehaviour {
    [SerializeField] private Vector2Int mapSize;
    [SerializeField] private Vector2Int devTile;

    [SerializeField] private Tilemap terrainTilemap;
    [SerializeField] private TileBase grassTile;
    [SerializeField] private TileBase waterTile;

    [ContextMenu("Generate Map")]
    public void GenerateMap() {
        terrainTilemap.ClearAllTiles();

        for (int y = 0 - mapSize.y / 2; y < mapSize.y / 2f; y++) {
            for (int x = 0 - mapSize.x / 2; x < mapSize.x / 2f; x++) {

                terrainTilemap.SetTile(new Vector3Int(x, y), x == devTile.x && y == devTile.y ? waterTile : grassTile);
            }
        }
    }
}
