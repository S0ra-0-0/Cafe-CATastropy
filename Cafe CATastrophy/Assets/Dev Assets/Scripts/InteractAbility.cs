using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractAbility : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayer;
    public PlayerInput playerInputObj;

    public InventoryManager inventoryManager;

    public bool isInteracting;

    private void Awake()
    {
        inventoryManager = GetComponent<InventoryManager>();
        playerInputObj = GetComponent<PlayerInput>();
    }
    public void DoInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isInteracting = true;
        }
        if (context.canceled)
        {
            isInteracting = false;
        }
    }
}
