using System;
using System.Diagnostics;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace Assets.Resources.Classes.Blobs
{
    /// <summary>
    /// Class <c>Player</c> models a player in the game
    /// </summary>
    public class Player : Blob
    {
        protected Vector2 DefaultSize = new Vector2(0.9f, 0.9f);
        private readonly int initialPlayerValue = 0;
        public override int FoodValue { get; set; }

        /// <summary>
        /// Start is called before the first frame to initialize the object
        /// </summary>
        public override void Start()
        {
            base.Start();
            this.FoodValue = this.initialPlayerValue;
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
        /// Update is called once per frame to update the object
        /// </summary>
        public override void Update()
        {
            base.Update();

            // Example showing that the growing function works
            if (Input.GetMouseButtonDown(0))
            {
                this.Grow(20);
            }
        }

        /// <summary>
        /// Grows the sprite by the specified amount
        /// </summary>
        /// <param name="amount">The amount to grow the sprite</param>
        public void Grow(int amount)
        {
            float size = (float) amount / 
                         (float) ConsumableAction.MaxFoodValue;

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
            consumable.OnPlayerConsume(this);
            base.UpdateColliderSize();
        }

        /// <summary>
        /// Deal with collisions with a Player object
        /// </summary>
        /// <param name="collision">The object the Player collided with</param>
        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Food")
            {
                UnityEngine.Debug.Log("Collided with a food object");
                Consumable food = collision.gameObject.GetComponent<Consumable>();
                this.ConsumeFood(food);
                Destroy(collision.gameObject);
            }
        }
    }
}
