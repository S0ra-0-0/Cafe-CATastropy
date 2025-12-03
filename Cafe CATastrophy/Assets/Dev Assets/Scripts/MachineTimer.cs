using UnityEngine;

public class MachineTimer : MonoBehaviour
{
    // Timer for all machines (like cooking water, oven working etc)

    public float totalTime = 1f;
    public bool isFinished = false;

    public void StartTimer()
    {
        if (totalTime > 0)
        {
            totalTime -= Time.deltaTime;
            isFinished = false;
        }

        if (totalTime <= 0)
        {
            isFinished = true;
            totalTime = 1f;
        }
    }
}
