using Assets.Resources.Classes;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.MainMenu.Scripts
{
    public class TutorialIcon: MonoBehaviour
    {
        private Image image;

        // Start is called before the first frame update
        void Start()
        {
            this.image = this.gameObject.GetComponent<Image>();
            this.image.sprite = ApplicationTheme.CurrentTheme.GetTutorialIcon();
        }
    }
}
