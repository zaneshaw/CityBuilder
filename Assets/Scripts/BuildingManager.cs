using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingManager : MonoBehaviour {
    [SerializeField] private Tilemap buildingsTilemap;
    [SerializeField] private Tilemap ghostTilemap;
    [SerializeField] private TerrainGenerator terrain;
    [SerializeField] private List<BuildingType> buildingPalette;

    private List<Building> buildings = new List<Building>();
    private bool flippedPlacement;
    private Vector3Int cellHighlight;

    private void Update() {
        UpdateHighlight();

        if (Input.GetKeyUp("r")) {
            flippedPlacement = !flippedPlacement;
        }

        if (Input.GetMouseButtonDown(0)) {
            Building building = new Building(buildingPalette[0], cellHighlight);
            PlaceBuilding(building);
        }
    }

    private void PlaceBuilding(Building building) {
        if (buildings.Exists(_building => _building.coords == building.coords)) {
            Debug.Log("Slot is already occupied!");
            return;
        }

        buildings.Add(building);

        Matrix4x4 tileTransform = Matrix4x4.Scale(new Vector3(flippedPlacement ? -1f : 1f, 1f, 1f));
        TileChangeData data = new TileChangeData {
            position = building.coords,
            tile = building.type.tile,
            transform = tileTransform,
        };
        buildingsTilemap.SetTile(data, false);
    }

    private void UpdateHighlight() {
        ghostTilemap.SetTile(cellHighlight, null);

        Vector3 mousePos = Input.mousePosition;
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        cellHighlight = buildingsTilemap.WorldToCell(worldPos);

        ghostTilemap.SetTile(cellHighlight, buildingPalette[0].tile);
    }

    private class Building {
        public BuildingType type;
        public Vector3Int coords;

        public Building(BuildingType type, Vector3Int coords) {
            this.type = type;
            this.coords = coords;
        }
    }
}
