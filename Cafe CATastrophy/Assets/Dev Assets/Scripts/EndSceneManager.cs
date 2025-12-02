using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndSceneManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Score;
    [SerializeField] private TextMeshProUGUI ordersCompleted;
    [SerializeField] private TextMeshProUGUI ordersFailed;

    [Tooltip("0 - Bronze, 1 - Silver, 2 - Gold")]
    [SerializeField] private Image[] medals; // 0 - Bronze, 1 - Silver, 2 - Gold

    private void Start()
    {
        int completedOrders = GameManager.Instance.OrdersCompleted;
        int failedOrders = GameManager.Instance.OrdersExpired;
        ordersCompleted.text = "Completed: " + completedOrders;
        ordersFailed.text = "Failed: " + failedOrders;
        int score = (completedOrders * 10) - (failedOrders * 5);
        Score.text += score;

        if (score >= 100)
        {
            medals[2].gameObject.SetActive(true); // Gold
        }
        if (score >= 50)
        {
            medals[1].gameObject.SetActive(true); // Silver
        }
        if (score >= 10)
        {
            medals[0].gameObject.SetActive(true); // Bronze
        }
    }
}

