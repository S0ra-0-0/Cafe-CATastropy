using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int gameTimer = 300;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI addTimerText;
    [SerializeField] private TextMeshProUGUI removeTimerText;

    public int playerCount = 0;

    public List<InventoryItems> allItems = new List<InventoryItems>();

    public int OrdersCompleted = 0;
    public int OrdersExpired = 0;
    public static GameManager Instance { get; private set; }


    private float currentTime;
    private bool timerIsRunning = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        currentTime = gameTimer;
        timerIsRunning = false;
    }

    public void StartTimer()
    {
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                UpdateTimerDisplay(currentTime);
            }
            else
            {
                Debug.Log("Time has run out!");
                currentTime = 0;
                SceneManager.LoadScene("EndScene");
                timerIsRunning = false;
            }
        }
    }

    private void UpdateTimerDisplay(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("Time left: {0:00}:{1:00}", minutes, seconds);
    }
    public void AddTime(int seconds)
    {
        currentTime += seconds;
        var addedTextObj = Instantiate(addTimerText, timerText.transform.position + new Vector3(0, 30, 0), Quaternion.identity, timerText.transform.parent);
        addedTextObj.text = "order completed" + "+" + seconds.ToString() + "s";
        StartCoroutine(DestroyTextAfterSeconds(addedTextObj, .8f));
        StartCoroutine(ChangeColour(2, Color.green));
    }

    public void RemoveTime(int seconds)
    {
        currentTime -= seconds;
        var removedTextObj = Instantiate(removeTimerText, timerText.transform.position + new Vector3(0, 30, 0), Quaternion.identity, timerText.transform.parent);
        removedTextObj.text = "order failed" + "-" + seconds.ToString() + "s";
        StartCoroutine(DestroyTextAfterSeconds(removedTextObj, .8f));
        StartCoroutine(ChangeColour(2, Color.red));
    }

    private IEnumerator DestroyTextAfterSeconds(TextMeshProUGUI textElement, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(textElement.gameObject);
    }

    private IEnumerator ChangeColour(int seconds, Color color)
    {
        timerText.color = color;
        yield return new WaitForSeconds(seconds);
        timerText.color = Color.black;
    }

    public void IncrementOrdersCompleted(int amount)
    {
        OrdersCompleted = +amount;
    }

    public void IncrementOrdersExpired(int amount)
    {
        OrdersExpired = +amount;
    }

    internal void OnSceneLoaded(EndSceneManager endSceneManager)
    {
        throw new NotImplementedException();
    }






    /// <summary>
    /// Gets an inventory item from the list by its name
    /// </summary>
    /// <param name="name">Name of the item</param>
    /// <returns>returns an ScriptableObject inventory item</returns>
    public InventoryItems GetItem(string name)
    {
        // korte versie
        return allItems.Find(item => item.itemName == name);

        // lange versie
        /*
        foreach (InventoryItems items in allItems)
        {
            if (items.itemName == name)
            {
                return items;
            }
        }
        */
    }
}
