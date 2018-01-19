using UnityEngine;

[CreateAssetMenu(menuName = "Building")]
public class BuildingAsset : ScriptableObject
{
    [Header("Attributes")]
    public string buildingName;

    [TextArea]
    public string description;
    public int cost;
    public int buildingLimit;
    public Sprite sprite;
    public GameObject buildingPrefab;

    

}
