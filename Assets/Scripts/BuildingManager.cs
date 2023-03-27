using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class BuildingManager : MonoBehaviour {
    [SerializeField] private Canvas canvas;
    [SerializeField] private Tilemap buildingsTilemap;
    [SerializeField] private Tilemap ghostTilemap;
    [SerializeField] private TerrainGenerator terrain;
    [SerializeField] private List<BuildingType> buildingPalette;

    [SerializeField] private TMP_Text scoreText;

    private List<Building> buildings = new List<Building>();
    private bool flippedPlacement;
    private Vector3Int cellHighlight;
    private int score;

    private void Update() {
        UpdateHighlight();

        if (Input.GetKeyUp("r")) {
            flippedPlacement = !flippedPlacement;
        }

        if (Input.GetMouseButtonDown(0)) {
            Building building = new Building(buildingPalette[0], cellHighlight, flippedPlacement);
            PlaceBuilding(building);
        }

        SimulateBuildings();
    }

    private void SimulateBuildings() {
        foreach (Building building in buildings) {
            building.timeLeft -= Time.deltaTime;

            if (building.timeLeft <= 0f) {
                building.timeLeft = building.type.rate;

                Vector2 pos = Camera.main.WorldToScreenPoint(buildingsTilemap.CellToWorld(building.coords));

                GameObject label = new GameObject("score +1");
                label.transform.SetParent(canvas.transform);
                label.transform.position = pos + Vector2.up * 16f;

                TMP_Text labelText = label.AddComponent<TextMeshProUGUI>();
                labelText.text = "+1";
                labelText.alignment = TextAlignmentOptions.Center;
                labelText.fontSize = 16;

                Animate(label.transform);

                score++;
            }
        }

        scoreText.text = score.ToString();

        async void Animate(Transform label) {
            float distance = 250f;
            float currentDistance = 0f;
            float speed = 4f;

            while (currentDistance <= distance) {
                await Task.Delay(10);
                if (Application.isPlaying) {
                    label.position = new Vector2(label.position.x, label.position.y + speed);
                    currentDistance += speed;
                }
            }

            Destroy(label.gameObject);
        }
    }

    private void PlaceBuilding(Building building) {
        if (buildings.Exists(_building => _building.coords == building.coords)) {
            Debug.Log("Slot is already occupied!");
            return;
        }

        buildings.Add(building);

        Matrix4x4 tileTransform = Matrix4x4.Scale(new Vector3(building.flipped ? -1f : 1f, 1f, 1f));
        TileChangeData tileData = new TileChangeData {
            position = building.coords,
            tile = building.type.tile,
            transform = tileTransform,
        };
        buildingsTilemap.SetTile(tileData, false);
    }

    private void UpdateHighlight() {
        ghostTilemap.SetTile(cellHighlight, null);

        Vector3 mousePos = Input.mousePosition;
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        cellHighlight = buildingsTilemap.WorldToCell(worldPos);

        Matrix4x4 tileTransform = Matrix4x4.Scale(new Vector3(flippedPlacement ? -1f : 1f, 1f, 1f));
        TileChangeData tileData = new TileChangeData {
            position = cellHighlight,
            tile = buildingPalette[0].tile,
            transform = tileTransform,
        };
        ghostTilemap.SetTile(tileData, false);
    }

    private class Building {
        public BuildingType type;
        public Vector3Int coords;
        public bool flipped;
        public float timeLeft;

        public Building(BuildingType type, Vector3Int coords, bool flipped = false) {
            this.type = type;
            this.coords = coords;
            this.flipped = flipped;
        }
    }
}
