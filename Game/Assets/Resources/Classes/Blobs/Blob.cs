using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.Resources.Classes.Blobs
{
    /// <summary>
    /// Enum <c>BlobType</c> represents each type that a blob may be
    /// </summary>
    public enum BlobType {Food, Player, PowerUp}


    /**
     * TODO
     *
     * - Set instance variable that determines initial size of the
     *   object
     */

    /// <summary>
    /// Class <c>Blob</c> parent to all blob objects in the game. A Blob
    /// may be a player, power up, or food.
    /// </summary>
    public abstract class Blob : MonoBehaviour
    {
        public int FoodValue { get; set; }
        public Sprite Icon;
        public SpriteRenderer Renderer;
        public readonly BlobType Type;
        public IList<Sprite> SpritesSheet;

        /// <summary>
        /// Start is called before the first frame to initialize the object
        /// </summary>
        public virtual void Start()
        {
            this.Renderer = this.gameObject.GetComponent<SpriteRenderer>();
            this.Renderer.drawMode = SpriteDrawMode.Sliced;

            this.SpritesSheet =
                UnityEngine.Resources.LoadAll<Sprite>("BayatGames/Free Platform Game Assets/GUI/png/Iconic1024x1024");

            this.FoodValue = GetFoodValue();
            this.Icon = GetSprite();
            this.Renderer.sprite = Icon;
        }

        /// <summary>
        /// Gets the appropriate starting food value for a Blob
        /// </summary>
        /// <returns>
        /// The appropriate starting food value for a Blob
        /// </returns>
        public virtual int GetFoodValue()
        {
            return 0;
        }

        /// <summary>
        /// Gets the appropriate sprite to use for the Blob's icon
        /// </summary>
        /// <returns>
        /// The sprite to use for the Blob's icon
        /// </returns>
        public abstract Sprite GetSprite();

        /// <summary>
        /// Called once per frame to update the object
        /// </summary>
        public virtual void Update(){}

        /// <summary>
        /// Factor that generates the proper sprite icon to use
        /// </summary>
        /// <param name="value">The value that the Blob object contains</param>
        /// <param name="type">The type of the Blob object</param>
        /// <returns>
        /// A sprite icon for the inputted Blob type
        /// </returns>
        public static Sprite BlobFactory(int value, BlobType type) => 
            SpriteFactory.BlobFactory(value, type);
    }
}