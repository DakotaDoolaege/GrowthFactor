using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AudioManager : MonoBehaviour
{
    
    // FindObjectOfType<AudioManager>().Play("SoundName")
    public bool isMuted = false; // game muted

    public Sound[] listOfSounds; // list of game sounds

    public Slider volumeSlider; // volume slider in admin menu

    public float levelBeforeMute; // keeps the audio level value before the game was muted

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
            sound.soundSource.volume = volumeSlider.value;

            sound.soundSource.clip = sound.soundClip;
            sound.soundSource.loop = sound.soundLoop;
        }

        //volumeSlider.value = 1f;
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

        givenSound.soundVolume = volumeSlider.value;
        Debug.LogWarning(givenSound.soundVolume);
        givenSound.soundSource.Play(); // play the sound provided
    }


    public void SetVolume() {

        foreach (Sound sound in listOfSounds)
        {
            sound.soundSource.volume = volumeSlider.value;

            if (volumeSlider.value == 0f)
            {
                audioManagerInstance.isMuted = true;
            }
        }
    }

    /// <summary>
    /// Mutes or Unmutes game sound
    /// </summary>
    public void Mute()
    {
        // checks if audio manager is already muted; if it is, unmutes; if not, mutes
        if (audioManagerInstance.isMuted == false) // if audio not currently muted, mute
        {
            levelBeforeMute = volumeSlider.value;
            AudioListener.volume = 0;
            volumeSlider.value = 0;
            audioManagerInstance.isMuted = true;
            
        }

        else 
        {
            AudioListener.volume = levelBeforeMute;
            volumeSlider.value = levelBeforeMute;
            audioManagerInstance.isMuted = false;

        }
    }
}
