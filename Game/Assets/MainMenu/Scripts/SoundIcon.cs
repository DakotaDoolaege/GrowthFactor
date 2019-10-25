using UnityEngine;
using UnityEngine.UI;

namespace Assets.MainMenu.Scripts
{
    public class SoundIcon : MonoBehaviour
    {
        public Sprite MutedIcon;

        public Sprite SoundOnIcon;

        private bool isMuted = false;
        private Image image;

        /// <summary>
        /// Start is called before the first frame
        /// </summary>
        void Start()
        {
            this.image = this.gameObject.GetComponent<Image>();
        }

        /// <summary>
        /// Switches the image for the sound icon upon clicking from the
        /// "playing sound" icon to the "muted" icon.
        /// </summary>
        public void SwitchImage()
        {
            if (! this.isMuted && this.MutedIcon != null && this.image != null)
            {
                this.image.sprite = MutedIcon;
            }

            else if (this.isMuted && this.SoundOnIcon != null && this.image != null)
            {
                this.image.sprite = SoundOnIcon;
            }

            this.isMuted = !this.isMuted;
        }
    }
}
