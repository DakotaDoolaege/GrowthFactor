using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.Resources.Classes.Blobs
{
    /// <summary>
    /// Class <c>SpriteFactory</c> is a factory that generates and returns the
    /// proper Sprite object to use for a specific Blob type.
    /// </summary>
    public class SpriteFactory : MonoBehaviour
    {
        public static Driver.Driver GameDriver = FindObjectOfType<Driver.Driver>();

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
            IList<string> names = spritesSheet.Select(sprite => sprite.name).ToList();

            // Get the proper Sprite type and return
            switch (type)
            {
                case BlobType.Food:
                    {
                        return value < 0 ? GameDriver.GameTheme.GetNegativeFood() :
                            GameDriver.GameTheme.GetPositiveFood();
                    }
                case BlobType.Player:
                    {
                        return GameDriver.GameTheme.GetPlayer();
                    }
                case BlobType.Pause:
                    {
                        IList<Sprite> SpritesSheet = UnityEngine.Resources.LoadAll<Sprite>("BayatGames/Free Platform Game Assets/GUI/png/Iconic2048x2048.png");
                        //return UnityEngine.Resources.Load<Sprite>("BayatGames/Free Platform Game Assets/GUI/png/Iconic2048x2048.png");
                        return spritesSheet[56];
                    }

                // When PowerUps added, add a case for the BlobType.PowerUp Enum

                default:
                    return null;
            }
        }
    }
}
