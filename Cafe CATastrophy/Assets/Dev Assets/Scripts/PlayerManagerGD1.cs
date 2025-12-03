using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManagerGD1 : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public GameObject[] PlayerPrefabs;

    public PlayerInputManager playerInputManager;
    private int playerIndex;

    public void SetSpawn(PlayerInput _player)
    {
        playerIndex = _player.playerIndex;
        _player.gameObject.transform.position = SpawnPoints[_player.playerIndex].position;

        OrderManager.Instance.StartOrders();
        GameManager.Instance.StartTimer();
        playerInputManager.playerPrefab = PlayerPrefabs[1];
    }
}
