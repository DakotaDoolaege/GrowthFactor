using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class takes care of the sound effects for button clicks and hover
/// </summary>
public class buttonSoundEffects : MonoBehaviour
{
    public AudioSource soundSource;
    public AudioClip clickSound;
    public AudioClip hoverSound;

    /// <summary>
    /// Plays click sound effect 
    /// </summary>
    public void ClickSoundEffect()
    {
        soundSource.PlayOneShot(clickSound);
    }

    /// <summary>
    /// Play hover sound effect
    /// </summary>
    public void HoverSoundEffect() {
        soundSource.PlayOneShot(hoverSound);
    }
}
