using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public bool isMuted = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Mutes or Unmutes game sound
    /// </summary>
    public void Mute()
    {
        if (isMuted == false)
        {
            AudioListener.volume = 0;
            isMuted = true;
        }
        else {
            AudioListener.volume = 0.4f;
            isMuted = false;
        }
    }
    
}
