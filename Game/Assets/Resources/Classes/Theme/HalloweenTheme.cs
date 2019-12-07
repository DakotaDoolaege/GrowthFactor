using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Assets.Resources.Classes.Theme
{
    /// <summary>
    /// Class <c>KnightTheme</c> is a class that holds the Knight theme
    /// for the game
    /// </summary>
    public class HalloweenTheme : GameTheme
    {
        // Set GUI elements for theme
        public override int DefaultLargeButtonIndex { get; } = 5;
        public override int DefaultSoundIconIndex { get; } = 2;
        public override int DefaultMuteIconIndex { get; } = 14;
        public override int DefaultAdminIconIndex { get; } = 6;
        public override int DefaultBackButtonIndex { get; } = 82;
        public override int DefaultForwardButtonIndex { get; } = 70;

        public override int DefaultScoresButtonIndex { get; } = 140;
        public override int DefaultTutorialButtonIndex { get; } = 30;

        // Backgrounds
        public override string MainMenuBackgroundPrefab { get; set; }
        private const string BACKGROUND_FILE = "_game_background";
        private Sprite background;

        // In-game sprites
        private IList<Sprite> NegativeFoods { get; }
        private IList<Sprite> PositiveFoods { get; }

        // Random number generator for randomly selecting backgrounds and players
        private readonly System.Random _random = new System.Random();


        // Set the active and deactive player for the player picker
        public string ActivePlayer { get; } = "Halloween/CandyGameItems/PNG/ico/24";
        public override string DeactivePlayer { get; } = "Halloween/CandyGameItems/PNG/ico/30";

        /// <summary>
        /// Constructor
        /// </summary>
        public HalloweenTheme() : base()
        {
            // Set the main menu's background
            this.SetMainMenuBackground();

            // Set the in-game background
            this.SetBackground();

            // Set up the negative foods
            this.NegativeFoods = new List<Sprite>();

            // Generate the negative foods
            string[] fileNames = {"25", "26"};
            string prefix = "Halloween/CandyGameItems/PNG/ico/";

            foreach (string file in fileNames)
            {
                Sprite sprite = UnityEngine.Resources.Load<Sprite>(prefix + file);
                this.NegativeFoods.Add(sprite);
            }

            // Set up the positive foods
            this.PositiveFoods = new List<Sprite>();

            prefix = "Halloween/CandyGameItems/PNG/ico/";
            string[] positiveFoodNames = { "3", "7", "14", "16", "17", "22", "23", "24", "27", "28", "29"};

            foreach (string name in positiveFoodNames)
            {
                Sprite sprite = UnityEngine.Resources.Load<Sprite>(prefix + name);
                this.PositiveFoods.Add(sprite);
            }
        }

        /// <summary>
        /// Sets the background to use.
        ///
        /// Note: A specific background is set so that the background is
        /// consistent between the game and the player picker screen.
        /// </summary>
        private void SetBackground()
        {
            int numBackgrounds = 4; // the number of backgrounds in the path
            string path = "Halloween/HalloweenBackgrouds/PNG/Images/";

            // Get a randomly selected background from the path
            int file = this._random.Next(1, numBackgrounds + 1);

            string backgroundFile = path + file + BACKGROUND_FILE;

            //Debug.Log(backgroundFile);

            this.background = UnityEngine.Resources.Load<Sprite>(backgroundFile);
        }

        /// <summary>
        /// Gets the Sprite to use for the background
        /// </summary>
        /// <returns>
        /// The Sprite to use for the background
        /// </returns>
        public override Sprite GetBackground()
        {
            return this.background;
        }

        /// <summary>
        /// Gets the Sprite for the Player object
        /// </summary>
        /// <returns>
        /// The Sprite to use as a Player
        /// </returns>
        public override Sprite GetPlayer()
        {
            const int playerFiles = 40;
            const string playerSheet = "Halloween/Monsters/PNG/";
            int playerNumber = this._random.Next(1, playerFiles + 1);

            return UnityEngine.Resources.Load<Sprite>(playerSheet + playerNumber);
        }

        /// <summary>
        /// Gets the active player sprite
        /// </summary>
        /// <returns>The icon for the active player</returns>
        public override Sprite GetActivePlayer()
        {
            return UnityEngine.Resources.Load<Sprite>(this.ActivePlayer);
        }

        /// <summary>
        /// Gets the Sprite for the positive Consumable object
        /// </summary>
        /// <returns>
        /// The Sprite to use as the positive food
        /// </returns>
        public override Sprite GetPositiveFood()
        {
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
            return this.NegativeFoods[ind];
        }

        /// <summary>
        /// Refreshes the theme, so that if a theme has multiple backgrounds
        /// we can update the current one each time the theme selector button
        /// is pressed
        /// </summary>
        public override void Refresh()
        {
            this.SetBackground();
            this.SetMainMenuBackground();
        }

        /// <summary>
        /// Sets the main meny background
        /// </summary>
        public void SetMainMenuBackground()
        {
            this.MainMenuBackgroundPrefab = "MainMenuBackgrounds/HalloweenBackground" + this._random.Next(1, 4);
        }
    }
}
