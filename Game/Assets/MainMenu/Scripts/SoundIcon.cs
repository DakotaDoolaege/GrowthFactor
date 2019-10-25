using UnityEngine;
using UnityEngine.UI;

namespace Assets.MainMenu.Scripts
{
    public class SoundIcon : MonoBehaviour
    {
        public Sprite MutedIcon;

        public Sprite SoundOnIcon;
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
            float tol = 0.05f;
            bool isMuted = AudioListener.volume > tol;

            if (! isMuted && this.MutedIcon != null && this.image != null)
            {
                this.image.sprite = MutedIcon;
            }

            else if (isMuted && this.SoundOnIcon != null && this.image != null)
            {
                this.image.sprite = SoundOnIcon;
            }
        }
    }
}
