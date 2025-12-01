using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI orderIdTextOrder1;
    [SerializeField] private TextMeshProUGUI orderIdTextOrder2;
    [SerializeField] private Image[] foodIcons;

    [SerializeField] private Image[] orderFillImages;

    // order 1 is index 0..(slotsPerOrder-1); order 2 is index slotsPerOrder..(2*slotsPerOrder-1)
    // foodIcons.Length must be divisible by orders.Length (slots per order)
    public void DisplayOrders(Order[] orders, bool[][] deliveredPerOrder, float[] remainingTimes, float[] durations)
    {
        // populate order id texts (supports exactly 2 orders; falls back if fewer)
        orderIdTextOrder1.text = (orders != null && orders.Length > 0) ? orders[0].orderId : "";
        orderIdTextOrder2.text = (orders != null && orders.Length > 1) ? orders[1].orderId : "";

        if (orders == null || orders.Length == 0 || foodIcons == null || foodIcons.Length == 0)
        {
            if (foodIcons != null)
            {
                foreach (var img in foodIcons) if (img != null) img.enabled = false;
            }
            UpdateFillImages(orders, remainingTimes, durations);
            return;
        }

        int ordersCount = Mathf.Max(1, orders.Length);
        int slotsPerOrder = foodIcons.Length / ordersCount;
        if (slotsPerOrder <= 0)
        {
            Debug.LogWarning("OrderUI: foodIcons length is smaller than orders count.");
            return;
        }

        for (int orderIndex = 0; orderIndex < ordersCount; orderIndex++)
        {
            var order = (orderIndex < orders.Length) ? orders[orderIndex] : null;
            bool[] delivered = (deliveredPerOrder != null && orderIndex < deliveredPerOrder.Length) ? deliveredPerOrder[orderIndex] : null;

            for (int slot = 0; slot < slotsPerOrder; slot++)
            {
                int iconIndex = orderIndex * slotsPerOrder + slot;
                if (iconIndex < 0 || iconIndex >= foodIcons.Length) continue;

                Image img = foodIcons[iconIndex];
                if (img == null) continue;

                if (order != null && order.requiredItems != null && slot < order.requiredItems.Length && order.requiredItems[slot] != null)
                {
                    Sprite s = order.requiredItems[slot].icon;
                    img.sprite = s;
                    img.enabled = true;

                    // preserve sprite aspect ratio; size remains controlled by RectTransform / layout
                    img.type = Image.Type.Simple;
                    img.preserveAspect = true;

                    bool isDelivered = delivered != null && slot < delivered.Length && delivered[slot];
                    img.color = isDelivered ? Color.gray : Color.white;
                }
                else
                {
                    img.enabled = false;
                }
            }
        }

        UpdateFillImages(orders, remainingTimes, durations);
    }

    private void UpdateFillImages(Order[] orders, float[] remainingTimes, float[] durations)
    {
        if (orderFillImages == null) return;

        int count = orderFillImages.Length;
        for (int i = 0; i < count; i++)
        {
            Image fill = orderFillImages[i];
            if (fill == null) continue;

            if (fill.type != Image.Type.Filled) fill.type = Image.Type.Filled;

            float fillAmount = 0f;
            if (orders != null && i < orders.Length && remainingTimes != null && durations != null && i < remainingTimes.Length && i < durations.Length && durations[i] > 0f)
            {
                fillAmount = Mathf.Clamp01(remainingTimes[i] / durations[i]);
            }

            fill.fillAmount = fillAmount;

            // optional: color from green -> red
            fill.color = Color.Lerp(Color.red, Color.green, fillAmount);
        }
    }
}