using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingManager : MonoBehaviour {
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private List<BuildingType> buildingPalette;

    private List<Building> buildings = new List<Building>();

    private void Start() {
        Building building = new Building(buildingPalette[0], new Vector3Int(0, 0, 0));
        PlaceBuilding(building);
    }

    private void PlaceBuilding(Building building) {
        if (buildings.Exists(_building => _building.coords == building.coords)) {
            Debug.Log("Slot is already occupied!");
            return;
        }

        buildings.Add(building);

        tilemap.SetTile(building.coords, building.type.tile);
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
