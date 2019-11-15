using System.Collections.Generic;
using System.Linq;
using Assets.Resources.Classes;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.MainMenuIcons
{
    public class TextButton : MonoBehaviour
    {
        /**
         * TODO:
         * - Make TextButton get the Text child object and set its text to 
         *   the text specified in this class
         */
        public string Text;
        private Image Image { get; set; }

        // Start is called before the first frame update
        void Start()
        {
            this.Image = this.gameObject.GetComponent<Image>();

            this.Image.sprite = ApplicationTheme.CurrentTheme.GetButtonLarge();
        }
    }
}
