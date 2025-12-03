using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManagerGD1 : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public GameObject[] PlayerPrefabs;


    public void SetSpawn(PlayerInput player)
    {
        OrderManager.Instance.StartOrders();
        GameManager.Instance.StartTimer();
        player.gameObject.transform.position = SpawnPoints[player.playerIndex].transform.position;


    }
}
