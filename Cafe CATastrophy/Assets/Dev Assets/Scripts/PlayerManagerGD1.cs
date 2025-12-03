using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManagerGD1 : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public GameObject[] PlayerPrefabs;

    public void SetSpawn(PlayerInput _player)
    {
        Debug.Log(_player.playerIndex + " spawned at " + SpawnPoints[_player.playerIndex].position);


        _player.gameObject.transform.position = SpawnPoints[_player.playerIndex].position;

        OrderManager.Instance.StartOrders();
        GameManager.Instance.StartTimer();
    }
}
