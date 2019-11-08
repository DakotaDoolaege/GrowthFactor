using System;
using Assets.MainMenu.Background;
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
            Camera.main.GetComponent<MainMenuBackground>().Refresh();
        }

        public void RefreshThemes()
        {
            ApplicationTheme.RefreshThemes();
            Camera.main.GetComponent<MainMenuBackground>().Refresh();
        }

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
