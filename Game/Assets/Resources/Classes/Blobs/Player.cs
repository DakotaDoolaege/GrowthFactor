using System;
using System.Collections;
using System.Diagnostics;
using UnityEditor;
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
        public const int PlayerMass = 10000;

        protected Vector2 DefaultSize = new Vector2(0.9f, 0.9f);
        private const int InitialPlayerValue = 0;
        public override int FoodValue { get; set; }
        public Vector3 StartPosition { get; set; }

        /// <summary>
        /// Start is called before the first frame to initialize the object
        /// </summary>
        public override void Start()
        {
            base.Start();
            this.FoodValue = InitialPlayerValue;
            this.RigidBody.mass = PlayerMass;
            this.StartPosition = this.transform.position;
        }

        public override void Update()
        {
            if (this.RigidBody.velocity.magnitude == 0.0f)
            {
                this.MoveToStart();
            }
            base.Update();
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
            // Set the collision instance variable
            this.OnCollisionEvent = this.ConsumeAnimation(consumable);

            // Enact the Consumable's changes to the player, which will call the
            // collision event on the player
            consumable.OnPlayerConsume(this);
            
            // Start the collision event on the consumable
            StartCoroutine(consumable.OnCollisionEvent);
        }

        // WORK ON THIS
        public void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Food")
            {
                Consumable consumable = collision.gameObject.GetComponent<Consumable>();

                if (this.OnCollisionEvent != null)
                {
                    StopCoroutine(this.OnCollisionEvent);
                }

                if (consumable.OnCollisionEvent != null)
                {
                    StopCoroutine(consumable.OnCollisionEvent);
                }
            }
        }

        /// <summary>
        /// Deal with collisions with a Player object
        /// </summary>
        /// <param name="collision">The object the Player collided with</param>
        public void OnCollisionEnter2D(Collision2D collision)
        {
            UnityEngine.Debug.Log("Player: " + this.FoodValue.ToString());
            // Deal with colliding with a Food object
            if (collision.gameObject.tag == "Food")
            {
                Consumable food = collision.gameObject.GetComponent<Consumable>();
                if (this.FoodValue + food.FoodValue > Blob.MinimumFoodValue)
                {
                    this.ConsumeFood(food);
                }
            }
            else if (collision.gameObject.tag == "PowerUp")
            {
                Consumable powerup = collision.gameObject.GetComponent<Consumable>();
                // this.ConsumePowerUp(powerup);
                /*
                 * In this function, the appropriate this.OnCollisionEvent should be
                 * set to deal with powerups. Then the Consumable object simply just
                 * calls the this.OnCollisionEvent coroutine, so we just set the appropriate
                 * stuff in there that we want to do.
                 *
                 * We need the this.ConsumePowerUp(powerup) function
                 * A Coroutine for animating the player and performing actions upon consuming
                 * the power up
                 */
            }
        }

        /// <summary>
        /// Animates a Player object's growth upon consuming a Consumable
        /// object.
        /// </summary>
        /// <param name="consumable">The object to consume</param>
        /// <param name="speed">The speed at which to grow the player</param>
        /// <returns>
        /// An IEnumerator that tells the coroutine when to stop and restart
        /// execution
        /// </returns>
        public IEnumerator ConsumeAnimation(Blob consumable, int speed = Consumable.ShrinkSpeed)
        {
            int value = consumable.FoodValue;
            if (value > 0)
            {
                while (value > 0)
                {
                    if (value - speed < 0)
                    {
                        this.Grow(value);
                        value = 0;
                        base.UpdateColliderSize();
                        yield return null;
                    }
                    else
                    {
                        this.Grow(speed);
                        value -= speed;
                        base.UpdateColliderSize();
                        yield return null;
                    }
                }
            }
            else if (value < 0)
            {
                while (value < 0)
                {
                    if (value + speed > 0)
                    {
                        this.Grow(-speed);
                        value += speed;
                        base.UpdateColliderSize();
                        yield return null;
                    }
                    else
                    {
                        this.Grow(value);
                        value = 0;
                        base.UpdateColliderSize();
                        yield return null;
                    }
                }
            }
        }

        /// <summary>
        /// Moves the player back to the start after it has been displaced
        /// </summary>
        /// <param name="speed">The speed at which to move the player
        /// back</param>
        private void MoveToStart(float speed=0.2f)
        {
            float step = speed * Time.deltaTime; // Distance to move
            this.transform.position = Vector3.MoveTowards(this.transform
                .position, this.StartPosition, step);
        }
    }
}
