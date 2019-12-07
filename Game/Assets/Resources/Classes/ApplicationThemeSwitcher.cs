using System;
using Assets.MainMenu.Background;
using UnityEngine;

namespace Assets.Resources.Classes
{
    /// <summary>
    /// Class <c>ApplicationThemeSwitcher</c> is responsible for switching the
    /// currently active application theme
    /// </summary>
    public class ApplicationThemeSwitcher : MonoBehaviour
    {
        public int Index = 0;

        /// <summary>
        /// Switches the theme to the index specified by the Index instance
        /// variable
        /// </summary>
        public void SwitchTheme()
        {
            ApplicationTheme.SwitchTheme(this.Index);
            Camera.main.GetComponent<MainMenuBackground>().Refresh();
        }

        /// <summary>
        /// Refreshes the currently loaded application themes
        /// </summary>
        public void RefreshThemes()
        {
            ApplicationTheme.RefreshThemes();
            Camera.main.GetComponent<MainMenuBackground>().Refresh();
        }

        /// <summary>
        /// Refreshes GUI elements upon theme change
        /// </summary>
        public void RefreshGuiElements()
        {
            GameObject[] refreshItems = GameObject.FindGameObjectsWithTag("RefreshOnThemeUpdate");

            foreach(GameObject refreshObject in refreshItems)
            {
                try
                {
                    BackButton back = refreshObject.GetComponent<BackButton>();
                    back.Refresh();
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }
    }
}
