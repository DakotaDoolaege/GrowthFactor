using Assets.Resources.Classes;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.MainMenu.Scripts
{
    public class AdminIcon : MonoBehaviour
    {
        private Image Image { get; set; }

        // Start is called before the first frame update
        void Start()
        {
            this.Image = this.gameObject.GetComponent<Image>();

            this.Image.sprite = ApplicationTheme.CurrentTheme.GetAdminIcon();
        }
    }
}
