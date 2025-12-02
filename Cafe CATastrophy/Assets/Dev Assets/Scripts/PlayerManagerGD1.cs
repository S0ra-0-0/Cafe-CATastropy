using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerManagerGD1 : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public GameObject[] PlayerPrefabs;


    public void SetSpawn(PlayerInput player)
    {
        player.gameObject.transform.position = SpawnPoints[player.playerIndex].position;

        //change player prefab per player spawned in

        OrderManager.Instance.StartOrders();
        GameManager.Instance.StartTimer();
    }
}
