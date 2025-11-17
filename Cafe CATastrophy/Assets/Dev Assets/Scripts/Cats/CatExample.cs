using UnityEngine;

public class CatExample : CatTesting
{
    public Transform conveyorPoint;
    public Transform fleePoint;

    private float wanderTime = 0f;

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
        // Flee after 10 seconds of wandering
        wanderTime += Time.deltaTime;

        return wanderTime > 15f;
    }
}
