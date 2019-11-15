using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;

namespace Assets.Resources.Classes.Theme
{
    /*
     * TODO
     * - Make factory get icons from theme
     */

    /// <summary>
    /// Class <c>GameTheme</c> represents a theme for the game.
    /// </summary>
    public abstract class GameTheme
    {
        // Set GUI elements for theme
        public readonly IList<Sprite> SpritesSheet;
        public readonly IList<Sprite> IconicSpritesSheet;
        public virtual int DefaultLargeButtonIndex { get; } = 2;
        public virtual int DefaultSoundIconIndex { get; } = 0;
        public virtual int DefaultMuteIconIndex { get; } = 12;
        public virtual int DefaultAdminIconIndex { get; } = 4;
        public virtual int DefaultBackButtonIndex { get; } = 80;
        public virtual int DefaultScoresButtonIndex { get; } = 138;

        // Backgrounds
        public virtual string MainMenuBackgroundPrefab { get; set; } = "MainMenuBackgrounds/DefaultBackground";
        public Sprite Background { get; set; }

        // In-game sprites
        public Sprite Player { get; set; }
        public Sprite PositiveFood { get; set; }
        public Sprite NegativeFood { get; set; }


        // Set the deactive player for the player picker
        public virtual string DeactivePlayer { get; } = "";

        // PowerUps can be added here
        //public Sprite PowerUp { get; set; }

        public GameTheme()
        {
            this.SpritesSheet =
                UnityEngine.Resources.LoadAll<Sprite>("BayatGames/Free Platform Game Assets/GUI/png/Empty1024x1024");
            this.IconicSpritesSheet =
                UnityEngine.Resources.LoadAll<Sprite>("BayatGames/Free Platform Game Assets/GUI/png/Iconic2048x2048");
        }

        /// <summary>
        /// Gets the deactive player for the player picker prefab
        /// </summary>
        /// <returns>
        /// The deactive player for the player picker prefab
        /// </returns>
        public virtual Sprite GetDeactivePlayer()
        {
            return UnityEngine.Resources.Load<Sprite>(DeactivePlayer);
        }

        /// <summary>
        /// Gets the active player for the player picker prefab
        /// </summary>
        /// <returns>
        /// The active player for the player picker prefab
        /// </returns>
        public virtual Sprite GetActivePlayer()
        {
            return ApplicationTheme.CurrentTheme.GetPlayer();
        }

        /// <summary>
        /// Gets the Sprite to use for the background
        /// </summary>
        /// <returns>
        /// The Sprite to use for the background
        /// </returns>
        public abstract Sprite GetBackground();

        /// <summary>
        /// Gets the Sprite for the Player object
        /// </summary>
        /// <returns>
        /// The Sprite to use as a Player
        /// </returns>
        public abstract Sprite GetPlayer();

        /// <summary>
        /// Gets the Sprite for the positive Consumable object
        /// </summary>
        /// <returns>
        /// The Sprite to use as the positive food
        /// </returns>
        public abstract Sprite GetPositiveFood();

        /// <summary>
        /// Gets the Sprite for the negative Consumable object
        /// </summary>
        /// <returns>
        /// The Sprite to use as the negative food
        /// </returns>
        public abstract Sprite GetNegativeFood();

        /// <summary>
        /// Refreshes the theme, so that if a theme has multiple elements,
        /// we can update the current one each time the theme selector button
        /// is pressed
        /// </summary>
        public virtual void Refresh() { }

        public virtual GameObject GetMainMenuBackground()
        {
            GameObject background = UnityEngine.Resources.Load<GameObject>(this.MainMenuBackgroundPrefab);
            background = GameObject.Instantiate(background);
            return background;
        }

        public virtual Sprite GetButtonLarge()
        {
            return this.SpritesSheet[this.DefaultLargeButtonIndex];
        }

        public virtual Sprite GetSoundIcon()
        {
            return this.IconicSpritesSheet[this.DefaultSoundIconIndex];
        }

        public virtual Sprite GetMuteIcon()
        {
            return this.IconicSpritesSheet[this.DefaultMuteIconIndex];
        }

        public virtual Sprite GetAdminIcon()
        {
            return this.IconicSpritesSheet[this.DefaultAdminIconIndex];
        }

        public virtual Sprite GetBackIcon()
        {
            return this.IconicSpritesSheet[this.DefaultBackButtonIndex];
        }

        public virtual Sprite GetScoresIcon()
        {
            return this.IconicSpritesSheet[this.DefaultScoresButtonIndex];
        }

        // Once PowerUps are added, uncomment the following line
        //public abstract Sprite CreatePowerUp()
    }
}
