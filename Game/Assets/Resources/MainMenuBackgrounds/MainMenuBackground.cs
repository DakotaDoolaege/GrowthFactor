using System.Collections;
using System.Runtime.CompilerServices;
using Assets.Resources.Classes;
using Assets.Resources.Classes.Theme;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.MainMenu.Background
{
    public class MainMenuBackground : MonoBehaviour
    {
        public GameObject MenuBackground { get; set; }

        // Start is called before the first frame update
        void Start()
        {
            // Instantiate the background and set it
            this.MenuBackground = ApplicationTheme.CurrentTheme.GetMainMenuBackground();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void Refresh()
        {
            GameObject oldBackground = this.MenuBackground;
            this.MenuBackground = ApplicationTheme.CurrentTheme.GetMainMenuBackground();
            
            GameObject.Destroy(oldBackground);
        }
    }
}
