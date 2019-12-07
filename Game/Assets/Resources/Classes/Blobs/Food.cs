using System.Collections;
using UnityEngine;

namespace Assets.Resources.Classes.Blobs
{
    /// <summary>
    /// Class <c>Food</c> is an action that a Consumable object may have.
    /// This class allows for a consumable object to be consumed as food,
    /// growing the player object upon consumption.
    /// </summary>
    public class Food : ConsumableAction
    {
        /// <summary>
        /// Generates a random food value for the Consumable object
        /// </summary>
        /// <returns>
        /// A random integer representing the Food value of a Consumable object
        /// </returns>
        protected override int GenerateFoodValue()
        {
            System.Random rnd = new System.Random();
            return rnd.Next(MinFoodValue, MaxFoodValue);
        }
    }
}
