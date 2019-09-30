using System;
using System.Collections;
using UnityEngine;

namespace Assets.Resources.Classes.Blobs
{
    /// <summary>
    /// Class <c>Consumable</c> is a class that represents objects that may
    /// be consumed by a Player object. These include PowerUps and Food.
    /// </summary>
    public class Consumable : Blob
    {
        /*
         * TODO
         *
         * - Add animation for when a Consumable gets consumed
         */
        public ConsumableAction Action;

        public override int FoodValue
        {
            get => this.Action.FoodValue;
            set => this.Action.FoodValue = value;
        }

        /// <summary>
        /// Start is called before the first frame to initialize the object
        /// </summary>
        public override void Start()
        {
            this.Action = ConsumableAction.GetAction(this.BlobType);
            base.Start();
        }

        /// <summary>
        /// Gets the appropriate BlobType for the Blob
        /// </summary>
        /// <returns>
        /// The type of Blob that the current object is
        /// </returns>
        public override BlobType GetBlobType()
        {
            return BlobType.Food;
        }

        /// <summary>
        /// Gets the appropriate starting size for the Blob
        /// </summary>
        /// <returns>
        /// The appropriate starting size for a Blob object
        /// </returns>
        public override Vector2 GetSize()
        {
            float maximum = (float) ConsumableAction.MaxFoodValue;
            float value = Math.Abs((float) this.FoodValue);

            float size = 0.3f + (value / (2.0f * maximum));
            return new Vector2(size, size);
        }

        /// <summary>
        /// Performs the appropriate action upon the player object when
        /// the player object consumes the current Consumable object.
        /// </summary>
        /// <param name="player">The Player object that consumes the
        /// Consumable object</param>
        public void OnPlayerConsume(Player player)
        {
            this.Action.OnPlayerConsumption(player);
            //StartCoroutine("Shrink");
        }

        /// <summary>
        /// Deals with collisions between some Object and a Consumable object
        /// </summary>
        /// <param name="collision">The object colliding with</param>
        public void OnCollisionEnter2D(Collision2D collision)
        {
            base.CalculateCollision2D(collision);
        }
    }
}
