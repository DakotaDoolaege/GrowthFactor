﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Resources.Classes.Blobs
{
    /// <summary>
    /// Enum <c>PlayerActionType</c> represents the appropriate types of
    /// player actions.
    /// </summary>
    public enum PlayerActionType {Default, NoRed, Shield}

    /// <summary>
    /// Class <c>PlayerAction</c> represents actions a Player can take on the
    /// food objects.
    /// </summary>
    public abstract class PlayerAction : MonoBehaviour
    {
        public Player Player { get; set; }
        public PlayerActionType Type { get; set; }
        public bool Consuming { get; set; }
        private AudioManager Audio { get; set; }

        /// <summary>
        /// Start is called once per frame.
        /// </summary>
        public virtual void Start()
        {
            this.Audio = FindObjectOfType<AudioManager>();
        }

        /// <summary>
        /// Deals with the event of consumption of a consumable
        /// </summary>
        /// <param name="consumable">The Consumable to consumes</param>
        public abstract void Consume(Consumable consumable);

        /// <summary>
        /// A function which calculates the changes to a Player object upon
        /// consuming a consumable object.
        /// </summary>
        /// <param name="consumable">The consumable object that the Player object
        /// consumes</param>
        public virtual void ConsumeEvent(Consumable consumable)
        {
            this.Consuming = true;

            // Start the collision event
            StartCoroutine(this.Player.OnCollisionEvent);
            StartCoroutine(consumable.OnCollisionEvent);
        }

        /// <summary>
        /// Actions to take upon exiting collision
        /// </summary>
        /// <param name="consumable">The consumable object that was collided with</param>
        public virtual void StopConsumeEvent(Consumable consumable)
        {
            if (!this.Consuming)
            {
                return;
            }

            if (consumable != null && consumable.OnCollisionEvent != null)
            {
                StopCoroutine(consumable.OnCollisionEvent);
            }

            StopCoroutine(this.Player.OnCollisionEvent);


            //this.ConsumedConsumable?.Invoke(consumable);
            this.Consuming = false;
        }

        /// <summary>
        /// Sets the events to be called when a player and consumable collide
        /// </summary>
        /// <param name="consumable">The consumable being collided with</param>
        /// <param name="speed">The speed that the player engulfs the
        /// consumable with</param>
        public void SetOnCollisionEvents(Consumable consumable, int speed = Consumable.ShrinkSpeed)
        {
            if (consumable.BlobType == BlobType.Food)
            {
                consumable.OnCollisionEvent = consumable.Shrink(speed);
                this.Player.OnCollisionEvent = this.ConsumeFoodEvent(consumable, speed);

                if (consumable.FoodValue > 0) // select different sound effect if food is positive or negative
                {
                    // FindObjectOfType<AudioManager>().Play("PlayerEatPostive");
                    this.Audio.Play("PlayerEatPostive");
                }
                else if (consumable.FoodValue < 0)
                {
                    // FindObjectOfType<AudioManager>().Play("PlayerEatNegative");
                    this.Audio.Play("PlayerEatNegative");
                }

            }
            else if (consumable.BlobType == BlobType.PowerUp)
            {
                consumable.OnCollisionEvent = consumable.Shrink(100 * speed);
                //this.Player.OnCollisionEvent = this.ConsumePowerUpEvent(consumable, 100 * speed);
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
        public virtual IEnumerator ConsumeFoodEvent(Blob consumable, int speed = Consumable.ShrinkSpeed)
        {
            int value = consumable.FoodValue;

            while (value > 0)
            {
                if (value - speed < 0)
                {
                    this.Player.Grow(value);
                    value = 0;
                    yield return null;
                }
                else
                {
                    this.Player.Grow(speed);
                    value -= speed;
                    yield return null;
                }
            }
            while (value < 0)
            {
                if (value + speed > 0)
                {
                    this.Player.Grow(-speed);
                    value += speed;
                    yield return null;
                }
                else
                {
                    this.Player.Grow(value);
                    value = 0;
                    yield return null;
                }
            }
        }
    }
}
