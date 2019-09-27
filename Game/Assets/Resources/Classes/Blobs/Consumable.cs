using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.Resources.Classes.Blobs
{
    public class Consumable : Blob
    {
        public new readonly BlobType Type = BlobType.Food;
        public static int MaxFoodValue = 100;
        public static int MinFoodValue = -100;

        /// <summary>
        /// Start is called before any frame to initialize the object
        /// </summary>
        public override void Start()
        {
            base.Start();
        }

        /// <summary>
        /// Gets the value of the food
        /// </summary>
        /// <returns>
        /// The value that a food object should be initialized with
        /// </returns>
        public override int GetFoodValue()
        {
            System.Random rnd = new System.Random();
            return rnd.Next(MinFoodValue, MaxFoodValue);
        }

        /// <summary>
        /// Generates the icon for the sprite
        /// </summary>
        /// <returns>
        /// The icon that the sprite should be initialized with
        /// </returns>
        public override Sprite GetSprite()
        {
            return Blob.BlobFactory(this.FoodValue, this.Type);
        }

        /// <summary>
        /// Update is called once per frame to update the object
        /// </summary>
        public override void Update()
        {
            base.Update();
            // If food need additional update functionality that the player
            // does not need, add it here. Otherwise, remove this method
        }

    }
}
