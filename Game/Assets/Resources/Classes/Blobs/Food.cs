namespace Assets.Resources.Classes.Blobs
{
    /// <summary>
    /// Class <c>Food</c> is an action that a Consumable object may have.
    /// This class allows for a consumable object to be consumed as food,
    /// growing the player object upon consumption.
    /// </summary>
    public class Food : ConsumableAction
    {
        protected override int GenerateFoodValue()
        {
            System.Random rnd = new System.Random();
            return rnd.Next(MinFoodValue, MaxFoodValue);
        }

        public override void OnPlayerConsumption(Player player)
        {
            player.Grow(this.FoodValue);
        }
    }
}
