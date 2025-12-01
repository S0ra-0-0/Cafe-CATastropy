using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;
    [SerializeField] private int maxOrderCount = 2;
    public Order[] availableOrders;
    [SerializeField] private OrderGenerator orderGenerator;
    [SerializeField] private OrderUI orderUI;

    [Header("Order timing")]
    [Tooltip("Default time (seconds) for each order before it expires.")]
    [SerializeField] private float defaultOrderDuration = 30f;

    private class ActiveOrder
    {
        public Order order;
        public bool[] delivered;

        // timing
        public float duration;
        public float timeRemaining;

        public ActiveOrder(Order order, float duration)
        {
            this.order = order;
            delivered = new bool[order.requiredItems.Length];
            this.duration = Mathf.Max(0.01f, duration);
            timeRemaining = this.duration;
        }

        public void Tick(float dt)
        {
            timeRemaining -= dt;
            if (timeRemaining < 0f) timeRemaining = 0f;
        }

        public bool IsExpired() => timeRemaining <= 0f;

        public bool TryRegisterItem(InventoryItems item)
        {
            for (int i = 0; i < order.requiredItems.Length; i++)
            {
                if (!delivered[i] && order.requiredItems[i] == item)
                {
                    delivered[i] = true;
                    return true;
                }
            }
            return false;
        }

        public bool IsComplete()
        {
            for (int i = 0; i < delivered.Length; i++)
            {
                if (!delivered[i]) return false;
            }
            return true;
        }

        public int RemainingCount()
        {
            int count = 0;
            for (int i = 0; i < delivered.Length; i++)
                if (!delivered[i]) count++;
            return count;
        }
    }

    private ActiveOrder[] activeOrders;

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

    private void Update()
    {
        if (activeOrders == null) return;

        bool anyExpired = false;
        for (int i = 0; i < activeOrders.Length; i++)
        {
            var a = activeOrders[i];
            if (a == null) continue;

            a.Tick(Time.deltaTime);

            if (a.IsExpired())
            {
                Debug.Log($"Order {a.order.orderId} expired.");
                ReplaceOrderAt(i);
                anyExpired = true;
                // ReplaceOrderAt will also update UI; activeOrders has been reset for this index.
            }
        }

        // Update UI each frame so fill images animate smoothly; cheap with only a couple orders.
        if (!anyExpired) UpdateOrderUI();
    }

    private void GenerateInitialOrders()
    {
        if (orderGenerator == null)
        {
            Debug.LogError("OrderManager: OrderGenerator is not assigned.");
            return;
        }

        availableOrders = new Order[maxOrderCount];
        activeOrders = new ActiveOrder[maxOrderCount];

        for (int i = 0; i < maxOrderCount; i++)
        {
            Order newOrder = orderGenerator.GenerateRandomOrder();
            availableOrders[i] = newOrder;
            activeOrders[i] = new ActiveOrder(newOrder, defaultOrderDuration);
        }

        UpdateOrderUI();
    }

    public void RegisterItem(InventoryItems itemToRegister)
    {
        if (itemToRegister == null)
        {
            Debug.LogWarning("OrderManager.RegisterItem called with null item.");
            return;
        }

        for (int i = 0; i < activeOrders.Length; i++)
        {
            ActiveOrder active = activeOrders[i];
            if (active == null || active.order == null) continue;

            bool matched = active.TryRegisterItem(itemToRegister);
            if (matched)
            {
                Debug.Log($"Item '{itemToRegister.itemName}' registered for order {active.order.orderId}. Remaining: {active.RemainingCount()}");

                // Update UI immediately
                UpdateOrderUI();

                if (active.IsComplete())
                {
                    Debug.Log($"Order {active.order.orderId} completed!");
                    // TODO: award player, update score/UI, play VFX/sound, etc.
                    ReplaceOrderAt(i);
                }
                return;
            }
        }

        Debug.Log($"Item {itemToRegister.itemName} does not match any available orders.");
    }

    private void ReplaceOrderAt(int index)
    {
        if (orderGenerator == null)
        {
            Debug.LogError("OrderManager: OrderGenerator is not assigned.");
            return;
        }

        Order newOrder = orderGenerator.GenerateRandomOrder();
        availableOrders[index] = newOrder;
        activeOrders[index] = new ActiveOrder(newOrder, defaultOrderDuration);

        Debug.Log($"Replaced order at slot {index} with new order {newOrder.orderId}");

        UpdateOrderUI();
    }

    private void UpdateOrderUI()
    {
        if (orderUI == null || availableOrders == null || activeOrders == null) return;

        bool[][] delivered = new bool[activeOrders.Length][];
        float[] remaining = new float[activeOrders.Length];
        float[] durations = new float[activeOrders.Length];

        for (int i = 0; i < activeOrders.Length; i++)
        {
            delivered[i] = activeOrders[i].delivered;
            remaining[i] = activeOrders[i].timeRemaining;
            durations[i] = activeOrders[i].duration;
        }

        orderUI.DisplayOrders(availableOrders, delivered, remaining, durations);
    }
}