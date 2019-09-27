using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor;
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
        public Vector2 SizeVector;
        public Sprite Icon;
        public SpriteRenderer Renderer;
        public readonly BlobType Type;

        /// <summary>
        /// Start is called before the first frame to initialize the object
        /// </summary>
        public virtual void Start()
        {
            this.Renderer = this.gameObject.GetComponent<SpriteRenderer>();
            this.Renderer.drawMode = SpriteDrawMode.Sliced;

            // Generate the size for the blob
            this.SizeVector = this.GetSize();
            this.transform.localScale = this.SizeVector;

            // Generate food value and icon for blob
            this.FoodValue = GetFoodValue();
            this.Icon = GetSprite();
            this.Renderer.sprite = Icon;
        }

        /// <summary>
        /// Gets the appropriate starting size for the Blob
        /// </summary>
        /// <returns>
        /// The appropriate starting size for a Blob object
        /// </returns>
        public abstract Vector2 GetSize();

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
    }
}