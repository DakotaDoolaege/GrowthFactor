using Assets.Resources.Classes;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.MainMenu.Scripts
{
    public class SoundIcon : MonoBehaviour
    {
        private Image image;

        /// <summary>
        /// Start is called before the first frame
        /// </summary>
        void Start()
        {
            this.image = this.gameObject.GetComponent<Image>();
            this.image.sprite = ApplicationTheme.CurrentTheme.GetSoundIcon();
        }

        /// <summary>
        /// Switches the image for the sound icon upon clicking from the
        /// "playing sound" icon to the "muted" icon.
        /// </summary>
        public void SwitchImage()
        {
            float tol = 0.05f;
            bool isMuted = AudioListener.volume > tol;

            if (! isMuted && this.image != null)
            {
                this.image.sprite = ApplicationTheme.CurrentTheme.GetMuteIcon();
            }

            else if (isMuted && this.image != null)
            {
                this.image.sprite = ApplicationTheme.CurrentTheme.GetSoundIcon();
            }
        }
    }
}
