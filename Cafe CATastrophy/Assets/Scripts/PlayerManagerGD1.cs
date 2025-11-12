using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManagerGD1 : MonoBehaviour
{
    public Transform[] SpawnPoints;

    public void SetSpawn(PlayerInput _player)
    {
        _player.gameObject.transform.position = SpawnPoints[_player.playerIndex].position;
    }
}
