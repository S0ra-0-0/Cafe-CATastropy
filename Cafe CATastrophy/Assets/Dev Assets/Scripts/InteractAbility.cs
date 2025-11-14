using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractAbility : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayer;
    public PlayerInput playerInputObj;

    public ItemController itemController;
    public InventoryManager inventoryManager;

    private void Awake()
    {
        itemController = GetComponent<ItemController>();
        inventoryManager = GameObject.Find("Player").GetComponent<InventoryManager>();
        playerInputObj = GameObject.Find("Player").GetComponent<PlayerInput>();
    }
    public void DoInteract(InputAction.CallbackContext context)
    {
        if (context.performed && itemController.inRange)
        {
            inventoryManager.AddItem(itemController.inventoryItem);
        }
    }
}
