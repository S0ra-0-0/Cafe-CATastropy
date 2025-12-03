using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerManagerGD1 : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public GameObject[] PlayerPrefabs;


    public void SetSpawn(PlayerInput player)
    {
        player.gameObject.transform.position = SpawnPoints[player.playerIndex].transform.position;

        OrderManager.Instance.StartOrders();
        GameManager.Instance.StartTimer();
    }
}
