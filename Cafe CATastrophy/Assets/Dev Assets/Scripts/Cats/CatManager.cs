using UnityEngine;
using System.Collections;

public class CatManager : MonoBehaviour
{
    [Header("Cat Spawning Settings")]
    [SerializeField] private Vector3 spawnLocation;
    [SerializeField] private GameObject[] catPrefab;
    [SerializeField] private int maxNumberOfCats = 3;
    [SerializeField] private float spawnInterval = 4f;
    private bool playerFound = false;

    private int currentNumberOfCats = 0;

    private void Start()
    {
        StartCoroutine(FindPlayerCoroutine());
        StartCoroutine(SpawnCatsCoroutine());
    }

    private IEnumerator FindPlayerCoroutine()
    {
        yield return new WaitUntil(() => FindAnyObjectByType<PlayerGD1>() != null);
        playerFound = true;
        Debug.Log("Player found!");
    }

    private IEnumerator SpawnCatsCoroutine()
    {
        while (true)
        {
            if (currentNumberOfCats < maxNumberOfCats && playerFound)
            {
                SpawnCat();
                yield return new WaitForSeconds(spawnInterval);
            }
            else
            {
                yield return null;
            }
        }
    }

    private void SpawnCat()
    {
        int randomIndex = Random.Range(0, catPrefab.Length);
        Instantiate(catPrefab[randomIndex], spawnLocation, Quaternion.identity);
        currentNumberOfCats++;
    }

    public void CatDestroyed()
    {
        currentNumberOfCats--;
    }
}
