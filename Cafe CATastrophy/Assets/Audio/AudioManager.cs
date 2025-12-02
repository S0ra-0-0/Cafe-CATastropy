using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
   public static AudioManager Instance { get; private set; }
   private AudioSource audioSource;

    [SerializeField] private List<SoundEffect> soundEffects;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string soundName)
    {
        SoundEffect sEffect = soundEffects.Find(s => s.soundName == soundName);

        if (sEffect != null)
        {
            audioSource.PlayOneShot(sEffect.clip);
        }
        else
        {
            Debug.LogWarning($"SoundEffect with name {soundName} not found!");
        }
    }
}
