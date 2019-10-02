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
        public ConsumableAction Action;
        public const float MinSizeTolerance = 0.6f;
        public const int ShrinkSpeed = 5;

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

        public override void Update()
        {
            if (this.FoodValue == 0)
            {
                StartCoroutine(this.Shrink());
            }
        }

        /// <summary>
        /// Gets the appropriate BlobType for the Blob
        /// </summary>
        /// <returns>
        /// The type of Blob that the current object is
        /// </returns>
        public override BlobType GetBlobType()
        {
            if (this.gameObject.tag == "Food")
            {
                return BlobType.Food;
            }
            return BlobType.PowerUp;
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

            float size = MinSizeTolerance + (value / (2.0f * maximum));
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
            // Shrink the consumable
            if (this.BlobType == BlobType.Food || this.BlobType == BlobType.PowerUp)
            {
                this.OnCollisionEvent = this.Shrink();
            }
            //player.SetOnCollisionEvent(this);

            // Call the action's action on the player, and start shrinking the
            // consumable
            this.Action.OnPlayerConsumption(player);
            //StartCoroutine(this.OnCollisionEvent);
        }

        public IEnumerator Shrink(int speed = ShrinkSpeed, 
                                  float tolerance = MinSizeTolerance)
        {
            Debug.Log("Food: " + this.FoodValue.ToString());

            Vector2 decreaseValue;

            while ((this.Renderer.size.x > tolerance || this.Renderer.size.y > tolerance))
            {
                if (this.FoodValue > 0)
                {
                    if (this.FoodValue - speed < 0)
                    {
                        //player.Grow(this.FoodValue);
                        this.FoodValue = 0;
                    }
                    else
                    {
                        //player.Grow(speed);
                        this.FoodValue -= speed;
                    }
                }
                else if (this.FoodValue < 0)
                {
                    if (this.FoodValue + speed > 0)
                    {
                        //player.Grow(this.FoodValue);
                        this.FoodValue = 0;
                    }
                    else
                    {
                        //player.Grow(speed);
                        this.FoodValue += speed;
                    }
                }

                decreaseValue = this.Renderer.size - this.GetSize();
                this.Renderer.size -= decreaseValue;
                this.UpdateColliderSize();
                yield return null;
            }

            if (this.FoodValue == 0)
            {
                const float size = (1 / (2.0f * ConsumableAction.MaxFoodValue));
                decreaseValue = new Vector2(size, size);
                while ((this.Renderer.size.x >= 0 && this.Renderer.size.y >= 0))
                {
                    this.Renderer.size -= decreaseValue;
                    //this.FoodValue -= 1;
                    this.UpdateColliderSize();
                    yield return null;
                }
            }

            Destroy(this.gameObject);
        }
    }
}
