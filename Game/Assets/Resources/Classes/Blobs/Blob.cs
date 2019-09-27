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


    /// <summary>
    /// Class <c>Blob</c> parent to all blob objects in the game. A Blob
    /// may be a player, power up, or food.
    /// </summary>
    public abstract class Blob : MonoBehaviour
    {
        // public int FoodValue { get; set; }
        public Vector2 SizeVector;
        public Sprite Icon;
        public SpriteRenderer Renderer;
        public CircleCollider2D Collider;
        protected BlobType BlobType;
        public abstract int FoodValue { get; set; }
        
        /// <summary>
        /// Start is called before the first frame to initialize the object
        /// </summary>
        public virtual void Start()
        {
            this.Renderer = this.gameObject.GetComponent<SpriteRenderer>();
            this.Renderer.drawMode = SpriteDrawMode.Sliced;

            // Generate the type of Blob
            this.BlobType = this.GetBlobType();

            // Generate food value and icon for blob
            //this.FoodValue = GetFoodValue();
            this.Icon = GetSprite();
            this.Renderer.sprite = Icon;

            // Generate the size for the blob
            this.SizeVector = this.GetSize();
            this.transform.localScale = this.SizeVector;

            // Adjust the collider
            this.Collider = this.GetComponent<CircleCollider2D>();
            UpdateColliderSize();
        }

        /// <summary>
        /// Updates the collider radius for the Blob object's collider
        /// </summary>
        protected void UpdateColliderSize()
        {
            Vector3 spriteHalfSize = this.Renderer.sprite.bounds.extents;
            this.Collider.radius = spriteHalfSize.x > spriteHalfSize.y ? spriteHalfSize.x : spriteHalfSize.y;
        }

        /// <summary>
        /// Gets the appropriate BlobType for the Blob
        /// </summary>
        /// <returns>
        /// The type of Blob that the current object is
        /// </returns>
        public abstract BlobType GetBlobType();

        /// <summary>
        /// Gets the appropriate starting size for the Blob
        /// </summary>
        /// <returns>
        /// The appropriate starting size for a Blob object
        /// </returns>
        public abstract Vector2 GetSize();


        /// <summary>
        /// Generates the icon for the sprite
        /// </summary>
        /// <returns>
        /// The icon that the sprite should be initialized with
        /// </returns>
        public virtual Sprite GetSprite()
        {
            return SpriteFactory.BlobFactory(this.FoodValue, this.BlobType);
        }

        /// <summary>
        /// Called once per frame to update the object
        /// </summary>
        public virtual void Update(){}
    }
}