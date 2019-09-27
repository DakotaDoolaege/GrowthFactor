using System;
using UnityEngine;

namespace Assets.Resources.Classes.Blobs
{
    public class Consumable : Blob
    {
        public ConsumableAction Action;

        public override int FoodValue
        {
            get => this.Action.FoodValue;
            set => this.Action.FoodValue = value;
        }

        // Start is called before the first frame update
        public override void Start()
        {
            this.Action = ConsumableAction.GetAction(this.BlobType);
            base.Start();
        }

        /// <summary>
        /// Gets the appropriate BlobType for the Blob
        /// </summary>
        /// <returns>
        /// The type of Blob that the current object is
        /// </returns>
        public override BlobType GetBlobType()
        {
            return BlobType.Food;
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

            float size = 0.1f + (value / maximum);
            return new Vector2(size, size);
        }


        public void OnPlayerConsume(Player player)
        {
            this.Action.OnPlayerConsumption(player);
            //Destroy(this);
        }
    }
}
