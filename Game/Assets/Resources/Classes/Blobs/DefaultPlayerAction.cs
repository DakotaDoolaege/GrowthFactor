using System.Collections;
using UnityEngine;

namespace Assets.Resources.Classes.Blobs
{
    /// <summary>
    /// Class <c>DefaultPlayerAction</c> represents the actions a player may
    /// take by default.
    /// </summary>
    public class DefaultPlayerAction : PlayerAction
    {
        public override void Start()
        {
            this.Type = PlayerActionType.Default;
            base.Start();
        }

        /// <summary>
        /// Consumes a consumable object
        /// </summary>
        /// <param name="consumable">The object to consume</param>
        public override void Consume(Consumable consumable)
        {
            this.SetOnCollisionEvents(consumable);
            this.ConsumeEvent(consumable);
        }
    }
}
