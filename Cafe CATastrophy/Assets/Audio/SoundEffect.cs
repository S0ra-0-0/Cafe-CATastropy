using UnityEngine;

[CreateAssetMenu(fileName = "SoundEffecct", menuName = "Scriptable Objects/SoundEffecct")]
public class SoundEffect : ScriptableObject
{
    public string soundName;
    public AudioClip clip;
}
