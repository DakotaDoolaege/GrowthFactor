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
        /// Factory method pattern for getting a specific ConsumableAction
        /// </summary>
        /// <param name="type">The enum denoting the type of the action</param>
        /// <returns>
        /// A concrete action reflecting the argument enum
        /// </returns>
        public static ConsumableAction GetAction(BlobType type)
        {
            if (type == BlobType.Food)
            {
                return new Food();
            }

            // Powerups may be added here
            return new Food();
        }
    }
}
