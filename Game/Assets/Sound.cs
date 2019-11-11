using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip soundClip;

    [Range(0f, 1f)]
    public float soundVolume;

    public bool soundLoop;

    [HideInInspector]
    public AudioSource soundSource;
}
