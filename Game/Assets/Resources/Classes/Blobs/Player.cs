using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;
using Debug = System.Diagnostics.Debug;

namespace Assets.Resources.Classes.Blobs
{
    /// <summary>
    /// Class <c>Player</c> models a player in the game
    /// </summary>
    public class Player : Blob
    {
        protected Vector2 DefaultSize = new Vector2(0.9f, 0.9f);
        private const int InitialPlayerValue = 0;
        public override int FoodValue { get; set; }

        /// <summary>
        /// Start is called before the first frame to initialize the object
        /// </summary>
        public override void Start()
        {
            base.Start();
            this.FoodValue = InitialPlayerValue;
        }

        /// <summary>
        /// Gets the appropriate BlobType for the Blob
        /// </summary>
        /// <returns>
        /// The type of Blob that the current object is
        /// </returns>
        public override BlobType GetBlobType()
        {
            return BlobType.Player;
        }

        /// <summary>
        /// Gets the size to initialize the Player with
        /// </summary>
        /// <returns></returns>
        public override Vector2 GetSize()
        {
            return this.DefaultSize;
        }

        /// <summary>
        /// Grows the sprite by the specified amount
        /// </summary>
        /// <param name="amount">The amount to grow the sprite</param>
        /// <param name="growthFactor">The speed at which the object's growing
        /// should be slowed down by. A higher growthFactor means a slower
        /// growing speed.</param>
        public void Grow(int amount, float growthFactor=3.0f)
        {
            float size = (float)amount /
                         (growthFactor * ConsumableAction.MaxFoodValue);

            this.Renderer.size += new Vector2(size, size);
            this.FoodValue += amount;
        }

        /// <summary>
        /// A function which calculates the changes to a Player object upon
        /// consuming a consumable object.
        /// </summary>
        /// <param name="consumable">The consumable object that the Player object
        /// consumes</param>
        public void ConsumeFood(Consumable consumable)
        {
            // Enact the Consumable's changes to the player
            consumable.OnPlayerConsume(this);
            
            // Run the animation to make the player grow or shrink
            StartCoroutine(ConsumeAnimation(consumable));
        }

        /// <summary>
        /// Deal with collisions with a Player object
        /// </summary>
        /// <param name="collision">The object the Player collided with</param>
        public void OnCollisionEnter2D(Collision2D collision)
        {
            //base.CalculateCollision2D(collision);

            // Deal with colliding with a Food object
            if (collision.gameObject.tag == "Food")
            {
                UnityEngine.Debug.Log("Collided with a food object");
                Consumable food = collision.gameObject.GetComponent<Consumable>();
                if (this.FoodValue + food.FoodValue > Blob.MinimumFoodValue)
                {
                    this.ConsumeFood(food);
                    Destroy(collision.gameObject);
                }
            }
        }

        /// <summary>
        /// Animates a Player object's growth upon consuming a Consumable
        /// object.
        /// </summary>
        /// <param name="consumable">The object to consume</param>
        /// <returns>
        /// An IEnumerator that tells the coroutine when to stop and restart
        /// execution
        /// </returns>
        private IEnumerator ConsumeAnimation(Blob consumable)
        {
            int value = consumable.FoodValue;
            if (value > 0)
            {
                while (value > 0)
                {
                    this.Grow(1);
                    value -= 1;
                    base.UpdateColliderSize();
                    yield return null;
                }
            }
            else if (value < 0)
            {
                while (value < 0)
                {
                    this.Grow(-1);
                    value += 1;
                    base.UpdateColliderSize();
                    yield return null;
                }
            }
        }
    }
}
