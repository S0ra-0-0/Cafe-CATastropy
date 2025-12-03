using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public AudioManager audioManager;

    public void Awake()
    {
        audioManager = AudioManager.Instance;
        audioManager.PlaySound("StartScreen");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void Credits()
    {
        Debug.Log("Loading Credits Scene");
        SceneManager.LoadScene("Credits");
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene("Start");
    }

    public void ExitGame()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }
}
