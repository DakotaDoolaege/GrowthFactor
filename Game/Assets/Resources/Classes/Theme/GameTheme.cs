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

        // Once PowerUps are added, uncomment the following line
        //public abstract Sprite CreatePowerUp()
    }
}
