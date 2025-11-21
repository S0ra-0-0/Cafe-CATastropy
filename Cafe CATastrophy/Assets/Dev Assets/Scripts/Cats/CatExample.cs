using System.Linq;
using UnityEngine;

public class CatExample : CatTesting
{
    public Transform conveyorPoint;
    public Transform fleePoint;

    private bool isPlayer1Side = false;
    private bool isPlayer2Side = false;
    private bool hasBroom = false;



    protected override Vector3 GetConveyorPoint()
    {
        if (droppedByThisPlayer == 1) { conveyorPoint = GameObject.Find("ConveyorPoint1").transform; }
        else if (droppedByThisPlayer == 2) { conveyorPoint = GameObject.Find("ConveyorPoint2").transform; }

        return conveyorPoint.position;
    }

    protected override Vector3 GetFleeTarget()
    {
        if (droppedByThisPlayer == 1) { fleePoint = GameObject.Find("FleePoint1").transform; }
        else if (droppedByThisPlayer == 2) { fleePoint = GameObject.Find("FleePoint2").transform; }

        return fleePoint.position;
    }

    protected override bool ShouldFlee()
    {
        InventoryManager inventory = FindAnyObjectByType<InventoryManager>();

        if (inventory == null)
        {
            Debug.LogError("InventoryManager not found!");
            return false;
        }


        if (droppedByThisPlayer == 1)
        {
            isPlayer1Side = inventory.transform.position.z < 0;
        }
        else
        {
            isPlayer2Side = inventory.transform.position.z > 0;
        }

        hasBroom = inventory.Items.Any(item => item.itemName == "Broom");

        if (hasBroom)
        {
            Debug.Log("Player has a broom.");
        }

        return (isPlayer2Side || isPlayer1Side) && hasBroom;

    }


}
