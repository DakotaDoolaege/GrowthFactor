using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

namespace Assets.Resources.Classes.PlayerPicker
{
    /// <summary>
    /// Class <c>PlayerPicker</c> controls a single player picker prefab
    /// </summary>
    public class PlayerPicker : MonoBehaviour
    {
        private ButtonHandler handler;
        private Image image;
        private SpriteRenderer spriteRenderer;
        private bool isActive { get; set; } = false;

        /// <summary>
        /// Start is called before the first frame
        /// </summary>
        void Start()
        {
            this.handler = this.gameObject.GetComponent<ButtonHandler>();
            this.image = this.gameObject.GetComponent<Image>();

            if (this.handler != null)
            {
                this.handler.activated = ApplicationTheme.CurrentTheme.GetActivePlayer();
                this.handler.deactivated = ApplicationTheme.CurrentTheme.GetDeactivePlayer();
            }

            if (this.image != null)
            {
                this.image.sprite = ApplicationTheme.CurrentTheme.GetDeactivePlayer();
            }

            Vector3 rescale = this.handler.deactivated.bounds.size;
            this.gameObject.transform.localScale = rescale;
        }

        /// <summary>
        /// Updates the scale of the object depending on the button handler's
        /// activated or deactivated status
        /// </summary>
        public void UpdateScale()
        {
            this.isActive = ! this.isActive;

            Vector3 rescale;

            if (this.isActive)
            {
                rescale = this.handler.activated.bounds.size;
            }
            else
            {
                rescale = this.handler.deactivated.bounds.size;
            }

            this.gameObject.transform.localScale = rescale;
        }
    }
}
