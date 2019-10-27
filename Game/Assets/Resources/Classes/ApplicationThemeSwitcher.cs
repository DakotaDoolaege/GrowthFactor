using UnityEngine;

namespace Assets.Resources.Classes
{
    public class ApplicationThemeSwitcher : MonoBehaviour
    {
        public int Index = 0;
        public void Start()
        {

        }

        public void SwitchTheme()
        {
            ApplicationTheme.SwitchTheme(this.Index);
        }

        public void RefreshThemes()
        {
            ApplicationTheme.RefreshThemes();
        }
    }
}
