using System.Collections;
using UnityEngine;

namespace Assets.Resources.Classes.Blobs
{
    public class DefaultPlayerAction : PlayerAction
    {
        public override void Start()
        {
            this.Type = PlayerActionType.Default;
        }

        public override void Consume(Consumable consumable)
        {
            this.SetOnCollisionEvents(consumable);
            this.ConsumeEvent(consumable);
        }

        //public override IEnumerator ConsumeFoodEvent(Blob consumable, int speed = Consumable.ShrinkSpeed)
        //{
        //    float tolerance = Consumable.MinSizeTolerance;
        //    Debug.Log("Food: " + consumable.FoodValue.ToString());

        //    while ((consumable.Renderer.size.x > tolerance || consumable.Renderer.size.y > tolerance))
        //    {
        //        if (consumable.FoodValue > 0)
        //        {
        //            if (consumable.FoodValue - speed < 0)
        //            {
        //                this.Player.Grow(consumable.FoodValue);
        //                consumable.FoodValue = 0;
        //            }
        //            else
        //            {
        //                this.Player.Grow(speed);
        //                consumable.FoodValue -= speed;
        //            }
        //        }
        //        else if (consumable.FoodValue < 0)
        //        {
        //            if (consumable.FoodValue + speed > 0)
        //            {
        //                this.Player.Grow(consumable.FoodValue);
        //                consumable.FoodValue = 0;
        //            }
        //            else
        //            {
        //                this.Player.Grow(speed);
        //                consumable.FoodValue += speed;
        //            }
        //        }

        //        //decreaseValue = consumable.Renderer.size - consumable.GetSize();
        //        //consumable.Renderer.size -= decreaseValue;
        //        //consumable.UpdateColliderSize();
        //        consumable.UpdateSize();
        //        yield return null;
        //    }

        //    if (consumable.FoodValue == 0)
        //    {
        //        const float size = -(1 / (2.0f * ConsumableAction.MaxFoodValue));
        //        Vector2 decreaseValue = new Vector2(size, size);

        //        while ((consumable.Renderer.size.x >= 0 && consumable.Renderer.size.y >= 0))
        //        {
        //            consumable.UpdateSize(decreaseValue);
        //            yield return null;
        //        }
        //    }

        //    Destroy(consumable.gameObject);
        //}

        //public void ConsumePowerUpEvent()
        //{

        //}
    }
}
