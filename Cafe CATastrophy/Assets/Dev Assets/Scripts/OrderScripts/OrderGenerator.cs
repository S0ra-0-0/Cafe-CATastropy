using UnityEngine;

public class OrderGenerator : MonoBehaviour
{
    [SerializeField] private InventoryItems[] possibleItems;
    public Order GenerateRandomOrder()
    {
        int itemCount = Random.Range(1, 3);
        InventoryItems[] orderItems = new InventoryItems[itemCount];

        for (int i = 0; i < itemCount; i++)
        {
            int randomIndex = Random.Range(0, possibleItems.Length);
            orderItems[i] = possibleItems[randomIndex];
        }

        string orderId = "ORDER-" + Random.Range(1000, 9999);

        return new Order
        {
            orderId = orderId,
            requiredItems = orderItems
        };
    }
}
