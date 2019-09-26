using UnityEngine;

namespace Assets.Resources.Classes.Blobs
{
    /// <summary>
    /// Class <c>Player</c> models a player in the game
    /// </summary>
    public class Player : Blob
    {
        public new readonly BlobType Type = BlobType.Player;

        /// <summary>
        /// Start is called before the first frame to initialize the object
        /// </summary>
        public override void Start()
        {
            base.Start();
        }

        /// <summary>
        /// Gets and returns the appropriate sprite to use for a player object
        /// </summary>
        /// <returns></returns>
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

            // Example showing that the growing function works
            if (Input.GetMouseButtonDown(0))
            {
                this.Grow(0.2f);
            }
        }

        /// <summary>
        /// Grows the sprite by the specified amount
        /// </summary>
        /// <param name="amount">The amount to grow the sprite</param>
        private void Grow(float amount)
        {
            this.Renderer.size += new Vector2(amount, amount);
        }

        /// <summary>
        /// A function which calculates the changes to a Player object upon
        /// consuming a food object.
        /// </summary>
        /// <param name="food">The food object that the Player object
        /// consumes</param>
        public void ConsumeFood(Food food)
        {
            this.FoodValue += food.FoodValue;

            int amount = food.FoodValue / Food.MaxFoodValue; 
            this.Grow(amount);
        }
    }
}
