using UnityEngine;

public class ItemInstance : MonoBehaviour
{
    public InventoryItems itemData;

    public void Initialize(InventoryItems data)
    {
        itemData = data;
#if UNITY_EDITOR
        if (data != null) gameObject.name = data.itemName + "_Instance";
#endif
    }
}