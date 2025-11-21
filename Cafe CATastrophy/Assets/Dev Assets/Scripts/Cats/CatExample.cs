using System.Linq;
using UnityEngine;

public class CatExample : CatTesting
{
    public Transform conveyorPoint;
    public Transform fleePoint;



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
        return false;
    }

    protected override bool PerformAction()
    {
        return false;
    }

    protected override void ActionUpdate()
    {


        base.ActionUpdate();
    }


}
