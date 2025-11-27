using UnityEngine;

public class KillCat : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
        {
            Destroy(other.gameObject);

            if (other.CompareTag("Cat"))
            {
                CatManager catManager = FindFirstObjectByType<CatManager>();
                if (catManager != null)
                {
                    catManager.CatDestroyed();
                }
            }
        }
    }
}

