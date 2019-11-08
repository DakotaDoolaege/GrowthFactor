using System.Runtime.CompilerServices;
using Assets.Resources.Classes;
using Assets.Resources.Classes.Theme;
using UnityEngine;

namespace Assets.MainMenu.Background
{
    public class DELETELATER : MonoBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {
            GameObject background = ApplicationTheme.CurrentTheme.GetMainMenuBackground();
            GameObject.Instantiate(background);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
