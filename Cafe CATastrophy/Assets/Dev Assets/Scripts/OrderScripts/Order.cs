using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "New Order", menuName = "Order System/Order")]

public class Order
{
    public string orderId;
    public InventoryItems[] requiredItems;
}
