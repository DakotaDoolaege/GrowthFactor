using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Resources.Classes.Blobs
{
    /// <summary>
    /// Class <c>SpriteFactory</c> is a factory that generates and returns the
    /// proper Sprite object to use for a specific Blob type.
    /// </summary>
    public class SpriteFactory : MonoBehaviour
    {
        private void Start()
        {
        }

        // Names of individual Sprites in the sliced Sprite file
        private const string NegativeFood = "NegativeFood";
        private const string PositiveFood = "PositiveFood";
        private const string Player = "Player";
        private const string PowerUp = "PowerUp";

        // The name of the file containing the sliced Sprite objects
        private const string SpriteFile = "BayatGames/Free Platform Game Assets/GUI/png/Iconic2048x2048";

        public static Sprite BlobFactory(int value, BlobType type)
        {
            // Get the list of all sprites in the sheet
            IList<Sprite> spritesSheet =
                UnityEngine.Resources.LoadAll<Sprite>(SpriteFile);

            // Create the list of names in the exact order of the list of sprites
            IList<string> names = new List<string>();
            foreach (Sprite sprite in spritesSheet)
            {
                names.Add(sprite.name);
            }

            // Get the proper Sprite type and return
            switch (type)
            {
                case BlobType.Food:
                {
                    string foodType = (value >= 0 ? NegativeFood : PositiveFood);
                    int index = names.IndexOf(foodType);

                    return spritesSheet[index];
                }
                case BlobType.Player:
                {
                    int index = names.IndexOf("Player");
                    return spritesSheet[index];
                }
                case BlobType.PowerUp:
                {
                    int index = names.IndexOf(PowerUp);
                    return spritesSheet[index];
                }
                default:
                    return null;
            }
        }
    }
}
