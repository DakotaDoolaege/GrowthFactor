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
        public static int MinFoodValue = -100;
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
        /// Performs the appropriate actions upon a Player when that
        /// Player object consumes the Consumable object holding the
        /// current ConsumableAction object.
        /// </summary>
        /// <param name="player">The that consumes the Consumable object
        /// holding the current ConsumableAction object</param>
        public abstract void OnPlayerConsumption(Player player);

        public static ConsumableAction GetAction(BlobType type)
        {
            // Modify this for powerups later!
            return new Food();
        }
    }
}
