using UnityEngine;

public class CatManager : MonoBehaviour
{
    [Header("Cat Spawning Settings")]

    [SerializeField] private Vector3 spawnLocation;
    [SerializeField] private GameObject[] catPrefab;
    [SerializeField] private int maxNumberOfCats = 3;
    [SerializeField] private float spawnInterval = 5f;

    private int currentNumberOfCats = 0;
    private void Update()
    {
        if (currentNumberOfCats < maxNumberOfCats)
        {
            spawnInterval -= Time.deltaTime;
            if (spawnInterval <= 0f)
            {
                SpawnCat();
                spawnInterval = 5f; // Reset spawn interval
            }
        }
    }
    private void SpawnCat()
    {
        int randomIndex = Random.Range(0, catPrefab.Length);
        Instantiate(catPrefab[randomIndex], spawnLocation, Quaternion.identity);
        currentNumberOfCats++;
    }
}
