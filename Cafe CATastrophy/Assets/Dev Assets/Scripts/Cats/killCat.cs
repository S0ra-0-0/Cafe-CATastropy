using UnityEngine;

public class killCat : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cat"))
        {
            Destroy(other.gameObject);
            CatManager catManager = FindFirstObjectByType<CatManager>();
            if (catManager != null)
            {
                catManager.currentNumberOfCats--;
            }
        }
    }
}
