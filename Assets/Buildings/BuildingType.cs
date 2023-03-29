using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "CityBuilder/BuildingType")]
public class BuildingType : ScriptableObject {
    public string id;
    public Tile tile;
    public float rate;
}
