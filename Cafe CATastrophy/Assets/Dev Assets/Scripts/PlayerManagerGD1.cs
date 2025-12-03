using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManagerGD1 : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public GameObject[] PlayerPrefabs;

    public void SetSpawn(PlayerInput player)
    {
        int index = player.playerIndex;

        GameObject playerObj = Instantiate(PlayerPrefabs[index], SpawnPoints[index].position, SpawnPoints[index].rotation);

        OrderManager.Instance.StartOrders();
        GameManager.Instance.StartTimer();
    }
}
