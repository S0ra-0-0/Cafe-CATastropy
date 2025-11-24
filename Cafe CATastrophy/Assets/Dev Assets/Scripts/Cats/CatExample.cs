using System.Linq;
using UnityEngine;

public class CatExample : CatTesting
{
    public Transform conveyorPoint;
    public Transform fleePoint;
    private Transform itemTarget;
    private GameObject targetObject;



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

    public void SetTarget(Transform targetTransform, GameObject targetItem)
    {
        itemTarget = targetTransform;
        targetObject = targetItem;
        state = CatState.Action;

    }

    protected override void ActionUpdate()
    {
        if (state == CatState.Action)
        {
            agent.SetDestination(itemTarget.position);
            float distance = Vector3.Distance(transform.position, itemTarget.position);
            if (distance < 2f)
            {
                Debug.LogWarning("Destroying all child objects!");

                foreach (Transform child in targetObject.transform)
                {
                    Destroy(child.gameObject);
                }

                CounterFunctionality counter = targetObject.GetComponent<CounterFunctionality>();
                if (counter != null)
                {
                    counter.itemOnCounter = null;
                }

                targetObject = null;
                state = CatState.Flee;
            }
        }
    }



}
