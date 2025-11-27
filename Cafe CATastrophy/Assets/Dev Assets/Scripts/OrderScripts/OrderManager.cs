using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;
    [SerializeField] private int maxOrderCount = 2;
    public Order[] availableOrders;
    [SerializeField] private OrderGenerator orderGenerator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GenerateInitialOrders();
    }

    private void GenerateInitialOrders()
    {
        availableOrders = new Order[maxOrderCount];
        for (int i = 0; i < maxOrderCount; i++)
        {
            availableOrders[i] = orderGenerator.GenerateRandomOrder();
        }
    }

    public void RegisterItem(InventoryItems itemToRegister)
    {
        for (int i = 0; i < availableOrders.Length; i++)
        {
            Order currentOrder = availableOrders[i];
            foreach (InventoryItems requiredItem in currentOrder.requiredItems)
            {
                if (requiredItem == itemToRegister)
                {
                    Debug.Log("Item " + itemToRegister.itemName + " registered for order " + currentOrder.orderId);
                    // Here you can add logic to mark the item as delivered for the order
                    return;
                }
            }
        }
        Debug.Log("Item " + itemToRegister.itemName + " does not match any available orders.");


    }
}

