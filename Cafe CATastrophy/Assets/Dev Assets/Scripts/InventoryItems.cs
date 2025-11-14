using UnityEngine;

[CreateAssetMenu]
public class InventoryItems : ScriptableObject
{
    public string itemName;
    public bool ingredient;
    public Sprite icon;
    public GameObject itemPrefab;
    [TextArea]
    public string description;
}