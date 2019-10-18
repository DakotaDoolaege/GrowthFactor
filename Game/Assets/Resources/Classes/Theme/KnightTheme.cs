using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Resources.Classes.Theme
{
    public class KnightTheme : GameTheme
    {
        // The name of the Player Sprite in the Sprite TileSheet
        private const string PlayerName = "Complete";

        // The name of the Positive Food Sprite in the Sprite TileSheet
        private const string PositiveFoodName = "Complete";

        // The name of the Negative Food Sprite in the Sprite TileSheet
        private const string NegativeFoodName = "Complete";

        // The name of the image to use as the background
        private const string BackgroundFile = "LevelResources/Backgrounds/Space2-4k";

        // The name of the file containing the sliced Sprite objects
        private const string SpriteFile = "Low_Swordman/1.Sprite/Complete";

        // Get the list of all sprites in the sheet
        private static readonly IList<Sprite> SpritesSheet =
            UnityEngine.Resources.LoadAll<Sprite>(SpriteFile);

        // Create the list of names in the exact order of the list of sprites
        private static readonly IList<string> Names = (from sprite in
            SpritesSheet select sprite.name).ToList();

        public override Sprite CreateBackground()
        {
            return UnityEngine.Resources.Load<Sprite>(BackgroundFile);
        }

        /// <summary>
        /// Gets the Sprite for the Player object
        /// </summary>
        /// <returns>
        /// The Sprite to use as a Player
        /// </returns>
        public override Sprite CreatePlayer()
        {
            int index = Names.IndexOf(PlayerName);
            return SpritesSheet[index];
        }

        /// <summary>
        /// Gets the Sprite for the positive Consumable object
        /// </summary>
        /// <returns>
        /// The Sprite to use as the positive food
        /// </returns>
        public override Sprite CreatePositiveFood()
        {
            int index = Names.IndexOf(PositiveFoodName);

            return SpritesSheet[index];
        }

        /// <summary>
        /// Gets the Sprite for the negative Consumable object
        /// </summary>
        /// <returns>
        /// The Sprite to use as the negative food
        /// </returns>
        public override Sprite CreateNegativeFood()
        {
            int index = Names.IndexOf(NegativeFoodName);

            return SpritesSheet[index];
        }
    }
}
