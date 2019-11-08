using UnityEngine;

namespace Assets.Resources.Classes.Theme
{
    /// <summary>
    /// Class <c>BackgroundTheme</c> themes the background of the game
    /// main menu
    /// </summary>
    public class BackgroundTheme
    {
        /// <summary>
        /// The layers for the parallax background
        /// </summary>
        public Sprite Layer0 { get; set; }
        public Sprite Layer1 { get; set; }
        public  Sprite Layer2 { get; set; }
        public Sprite Layer3 { get; set; }
        public Sprite Layer4 { get; set; }

        /// <summary>
        /// Whether or not to treat the background as a parallax
        /// </summary>
        public bool IsParallax { get; set; } = false;

    }
}
