using UnityEngine;

namespace Assets.Resources.Classes
{
    public class ApplicationThemeSwitcher : MonoBehaviour
    {
        public void Start()
        {

        }

        public void SwitchTheme()
        {
            ApplicationTheme.Next();
        }
    }
}
