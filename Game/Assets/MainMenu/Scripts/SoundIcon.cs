using Assets.Resources.Classes;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.MainMenu.Scripts
{
    public class SoundIcon : MonoBehaviour
    {
        private Image image;
        private AudioManager Manager { get; set; }

        /// <summary>
        /// Start is called before the first frame
        /// </summary>
        void Start()
        {
            this.Manager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            this.image = this.gameObject.GetComponent<Image>();

            if (this.Manager.isMuted)
            {
                this.image.sprite = ApplicationTheme.CurrentTheme.GetMuteIcon();
            }
            else
            {
                this.image.sprite = ApplicationTheme.CurrentTheme.GetSoundIcon();
            }
        }

        /// <summary>
        /// Switches the image for the sound icon upon clicking from the
        /// "playing sound" icon to the "muted" icon.
        /// </summary>
        public void SwitchImage()
        {
            bool isMuted = this.Manager.isMuted;

            if (isMuted && this.image != null)
            {
                this.image.sprite = ApplicationTheme.CurrentTheme.GetMuteIcon();
            }

            else if (! isMuted && this.image != null)
            {
                this.image.sprite = ApplicationTheme.CurrentTheme.GetSoundIcon();
            }
        }

        /// <summary>
        /// Mutes the sound of the application and switches the sound icon to the
        /// appropriate sound or mute icon
        /// </summary>
        public void ToggleSound()
        {
            this.Manager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

            this.Manager.Mute();
            this.SwitchImage();
        }
    }
}
