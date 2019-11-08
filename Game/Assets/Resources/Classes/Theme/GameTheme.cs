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
        public virtual string MainMenuBackgroundPrefab { get; set; } = "MainMenuBackgrounds/DefaultBackground";
        public Sprite Player { get; set; }
        public Sprite PositiveFood { get; set; }
        public Sprite NegativeFood { get; set; }

        public Sprite Background { get; set; }

        // Set the deactive player for the player picker
        public virtual string DeactivePlayer { get; } = "";

        // PowerUps can be added here
        //public Sprite PowerUp { get; set; }

        public GameTheme() { }

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

        // Once PowerUps are added, uncomment the following line
        //public abstract Sprite CreatePowerUp()
    }
}
