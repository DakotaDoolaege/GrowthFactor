using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Resources.Classes.Theme
{
    /// <summary>
    /// Class <c>KnightTheme</c> is a class that holds the Knight theme
    /// for the game
    /// </summary>
    public class KnightTheme : GameTheme
    {
        private IList<Sprite> NegativeFoods { get; }
        private IList<Sprite> PositiveFoods { get; }

        private readonly System.Random _random = new System.Random();

        // The name of the image to use as the background
        private const string BACKGROUND_FILE = "Painted HQ 2D Forest Medieval Background/Day";

        // Set the deactive player for the player picker
        public override string DeactivePlayer { get; } = "Low_Swordman/1.Sprite/Hat-Helmet";

        /// <summary>
        /// Constructor
        /// </summary>
        public KnightTheme() : base()
        {
            // Set up the negative foods
            this.NegativeFoods = new List<Sprite>();

            string negativeFoodName =
                "Match 3 - Dark Monsters - Infinite Monster Builder/Monsters/Monster_";

            for (int i = 1; i < 4; i++)
            {
                Sprite sprite = UnityEngine.Resources.Load<Sprite>(negativeFoodName + i.ToString());
                this.NegativeFoods.Add(sprite);
            }

            // Set up the positive foods
            this.PositiveFoods = new List<Sprite>();

            string prefix = "MagicpotionsFree/";
            string[] positiveFoodNames = { "potionblue04", "potionyellow04", "potionred04" };

            foreach (string name in positiveFoodNames)
            {
                Sprite sprite = UnityEngine.Resources.Load<Sprite>(prefix + name);
                this.PositiveFoods.Add(sprite);
            }
        }

        /// <summary>
        /// Gets the Sprite to use for the background
        /// </summary>
        /// <returns>
        /// The Sprite to use for the background
        /// </returns>
        public override Sprite GetBackground()
        {
            return UnityEngine.Resources.Load<Sprite>(BACKGROUND_FILE);
        }

        /// <summary>
        /// Gets the Sprite for the Player object
        /// </summary>
        /// <returns>
        /// The Sprite to use as a Player
        /// </returns>
        public override Sprite GetPlayer()
        {
            const string playerSheet = "Low_Swordman/1.Sprite/Complete";

            return UnityEngine.Resources.Load<Sprite>(playerSheet);
        }

        /// <summary>
        /// Gets the Sprite for the positive Consumable object
        /// </summary>
        /// <returns>
        /// The Sprite to use as the positive food
        /// </returns>
        public override Sprite GetPositiveFood()
        {
            //const string positiveFoodSheet = "MagicpotionsFree/potionblue04";

            //return UnityEngine.Resources.Load<Sprite>(positiveFoodSheet);
            int ind = this._random.Next() % this.PositiveFoods.Count;
            return this.PositiveFoods[ind];
        }

        /// <summary>
        /// Gets the Sprite for the negative Consumable object
        /// </summary>
        /// <returns>
        /// The Sprite to use as the negative food
        /// </returns>
        public override Sprite GetNegativeFood()
        {
            int ind = this._random.Next() % this.NegativeFoods.Count;
            Debug.Log("Random: " + ind);

            return this.NegativeFoods[ind];
        }
    }
}
