using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Assets.Resources.Classes.Theme
{
    /// <summary>
    /// Class <c>DefaultGameTheme</c> is a class that holds the default
    /// theme for the game
    /// </summary>
    public class DefaultGameTheme : GameTheme
    {
        // The name of the Player Sprite in the Sprite TileSheet
        private const string PlayerName = "Player";

        // The name of the Positive Food Sprite in the Sprite TileSheet
        private const string PositiveFoodName = "PositiveFood";

        // The name of the Negative Food Sprite in the Sprite TileSheet
        private const string NegativeFoodName = "NegativeFood";

        // The name of the image to use as the background
        private const string BackgroundFile = "DefaultTheme/DefaultBG";

        // The name of the file containing the sliced Sprite objects
        private const string SpriteFile = "BayatGames/Free Platform Game Assets/GUI/png/Iconic2048x2048";

        // Get the list of all sprites in the sheet
        private static readonly IList<Sprite> SpritesSheet = UnityEngine.Resources.LoadAll<Sprite>(SpriteFile);

        // Create the list of names in the exact order of the list of sprites
        private static readonly IList<string> Names = (from sprite in SpritesSheet select sprite.name).ToList();

        public override string DeactivePlayer { get; } = "Iconic2048x2048_136";

        /// <summary>
        /// Gets the deactive player for the player picker prefab
        /// </summary>
        /// <returns>
        /// The deactive player for the player picker prefab
        /// </returns>
        public override Sprite GetDeactivePlayer()
        {
            int index = Names.IndexOf(DeactivePlayer);

            return SpritesSheet[index];
        }

        /// <summary>
        /// Gets the Sprite to use for the background
        /// </summary>
        /// <returns>
        /// The Sprite to use for the background
        /// </returns>
        public override Sprite GetBackground()
        {
            return UnityEngine.Resources.Load<Sprite>(BackgroundFile);
        }

        /// <summary>
        /// Gets the Sprite for the Player object
        /// </summary>
        /// <returns>
        /// The Sprite to use as a Player
        /// </returns>
        public override Sprite GetPlayer()
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
        public override Sprite GetPositiveFood()
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
        public override Sprite GetNegativeFood()
        {
            int index = Names.IndexOf(NegativeFoodName);

            return SpritesSheet[index];
        }
    }
}
