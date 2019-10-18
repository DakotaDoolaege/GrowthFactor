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

        // PowerUps can be added here
        //public Sprite PowerUp { get; set; }

        //public virtual void Start()
        //{
        //    this.Player = this.CreatePlayer();
        //    this.PositiveFood = this.CreatePositiveFood();
        //    this.NegativeFood = this.CreateNegativeFood();
        //    this.Background = this.CreateBackground();
        //}

        public GameTheme()
        {
            this.Player = this.CreatePlayer();
            this.PositiveFood = this.CreatePositiveFood();
            this.NegativeFood = this.CreateNegativeFood();
            this.Background = this.CreateBackground();
        }

        /// <summary>
        /// Gets the Sprite to use for the background
        /// </summary>
        /// <returns>
        /// The Sprite to use for the background
        /// </returns>
        public abstract Sprite CreateBackground();

        /// <summary>
        /// Gets the Sprite for the Player object
        /// </summary>
        /// <returns>
        /// The Sprite to use as a Player
        /// </returns>
        public abstract Sprite CreatePlayer();

        /// <summary>
        /// Gets the Sprite for the positive Consumable object
        /// </summary>
        /// <returns>
        /// The Sprite to use as the positive food
        /// </returns>
        public abstract Sprite CreatePositiveFood();

        /// <summary>
        /// Gets the Sprite for the negative Consumable object
        /// </summary>
        /// <returns>
        /// The Sprite to use as the negative food
        /// </returns>
        public abstract Sprite CreateNegativeFood();

        // Once PowerUps are added, uncomment the following line
        //public abstract Sprite CreatePowerUp()
    }
}
