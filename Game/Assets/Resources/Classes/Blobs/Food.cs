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

        ///// <summary>
        ///// Handles the event where a Player consumes A Consumable object
        ///// holding the current ConsumableAction
        ///// </summary>
        ///// <param name="player">The Player consuming the Consumable
        ///// object</param>
        //public override void OnPlayerConsumption(Player player)
        //{
        //    // The only action the food has is to perform the collision
        //    // event on the player.
        //    player.StartCoroutine(player.OnCollisionEvent);
        //}
    }
}
