using UnityEngine;

namespace Assets.Resources.Classes.PlayerPicker
{
    /// <summary>
    /// Class <c>PlayerPickerBackground</c> controls the background of the
    /// player picker scene
    /// </summary>
    public class PlayerPickerBackground : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;

        /// <summary>
        /// Start is called before the first frame
        /// </summary>
        void Start()
        {
            this.spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

            this.spriteRenderer.sprite = ApplicationTheme.CurrentTheme.GetBackground();

            ApplicationTheme.ScaleBackground(this.gameObject, ApplicationTheme.CurrentTheme.GetBackground());
        }
    }
}
