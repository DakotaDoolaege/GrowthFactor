using System.Collections;
using UnityEngine;

namespace Assets.Resources.Classes.Blobs
{
    /// <summary>
    /// Class <c>ConsumableAction</c> represents an action that a Consumable
    /// object may have. This class allow Consumable objects to change their
    /// actions through the Strategy Pattern.
    /// </summary>
    public abstract class ConsumableAction
    {
        public const int MaxFoodValue = 100;
        public static int MinFoodValue = -50;
        public int FoodValue { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        protected ConsumableAction()
        {
            this.FoodValue = this.GenerateFoodValue();
        }

        /// <summary>
        /// Generates the food value for the Consumable object
        /// </summary>
        /// <returns></returns>
        protected virtual int GenerateFoodValue() => 0;

        /// <summary>
        /// Handles the event where a Player consumes A Consumable object
        /// holding the current ConsumableAction
        /// </summary>
        /// <param name="player">The Player consuming the Consumable
        /// object</param>
        public virtual void OnPlayerConsumption(Player player)
        {
            // The only action the food has is to perform the collision
            // event on the player
            player.StartCoroutine(player.OnCollisionEvent);
        }

        public static ConsumableAction GetAction(BlobType type)
        {
            // Modify this for powerups later!
            return new Food();
        }
    }
}
