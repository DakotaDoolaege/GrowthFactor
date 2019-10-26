using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public bool isMuted = false;


    /// <summary>
    /// Mutes or Unmutes game sound
    /// </summary>
    public void Mute()
    {
        if (this.isMuted == false)
        {
            AudioListener.volume = 0;
            this.isMuted = true;
        }

        else 
        {
            AudioListener.volume = 0.7f;
            this.isMuted = false;

        }
    }
}
