using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AudioManager : MonoBehaviour
{
    
    // FindObjectOfType<AudioManager>().Play("SoundName")
    public bool isMuted = false; // game muted

    public Sound[] listOfSounds;

    // avoid creating new audio manager in every scene loaded
    public static AudioManager audioManagerInstance;

    /// <summary>
    /// Gets all the audio sources at the start of the game
    /// </summary>
    private void Awake()
    {
        // if there is no audio manager in scene
        if (audioManagerInstance == null)
        {
            AudioManager audioManager = this;
            audioManagerInstance = audioManager;
        }
        else // already have audio manager
        {
            Destroy(gameObject);
        }

        // audio manager persists across scenes
        DontDestroyOnLoad(gameObject);        

        //set volume, clip, and loop for each sound
        foreach (Sound sound in listOfSounds)
        {
            sound.soundSource = gameObject.AddComponent<AudioSource>();
            sound.soundSource.volume = sound.soundVolume;
            sound.soundSource.clip = sound.soundClip;
            sound.soundSource.loop = sound.soundLoop;
        }      
    }

    /// <summary>
    /// Play Game theme
    /// </summary>
    private void Start()
    {
        Play("GameTheme");
    }

    /// <summary>
    /// Looks for the sound that matches the name provided and if it finds it, it will play it
    /// </summary>
    /// <param name="specificSoundName">name of sound that the game wants to play</param>
    public void Play(string specificSoundName)
    {
        Sound givenSound = null;

        // find the sound in the sounds array that is equal to to the name provided
        foreach (Sound sound in listOfSounds)
        {
            if (sound.soundName == specificSoundName)
            {
                givenSound = sound;
            }
        }

        if (givenSound == null) // if sound was not found, it won't give null pointer exception
        {
            Debug.LogWarning(specificSoundName + " sound was not found");
            return;
        }

        givenSound.soundSource.Play(); // play the sound provided
    }

    /// <summary>
    /// Mutes or Unmutes game sound
    /// </summary>
    public void Mute()
    {
        // checks if audio manager is already muted; if it is, unmutes; if not, mutes
        if (audioManagerInstance.isMuted == false)
        {
            AudioListener.volume = 0;
            audioManagerInstance.isMuted = true;
        }

        else 
        {
            AudioListener.volume = 1f;
            audioManagerInstance.isMuted = false;

        }
    }
}
