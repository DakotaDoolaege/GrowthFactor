using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Assets.Resources.Classes.Instantiator;
using UnityEditor;
using UnityEngine;

namespace Assets.Resources.Classes.Blobs
{
    /// <summary>
    /// Enum <c>BlobType</c> represents each type that a blob may be
    /// </summary>
    public enum BlobType {Food, Player, PowerUp, Pause}


    /// <summary>
    /// Class <c>Blob</c> parent to all blob objects in the game. A Blob
    /// may be a player, power up, or food.
    /// </summary>
    public abstract class Blob : MonoBehaviour
    {
        public Rigidbody2D RigidBody;
        public Vector2 SizeVector;
        public Sprite Icon;
        public SpriteRenderer Renderer;
        public CircleCollider2D Collider;
        public BlobType BlobType;
        public abstract int FoodValue { get; set; }
        public const int MinimumFoodValue = 0;
        public Vector2 LastVelocity;
        private const float BaseMass = 35.0f;
        private const float MassMultiplier = 15.2f;
        public float Mass { get; set; }

        public ConsumableInstantiator Instantiator { get; set; }

        public IEnumerator OnCollisionEvent { get; set; }

        /// <summary>
        /// Sets the instantiator instance variable
        /// </summary>
        private void SetInstantiator()
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Driver");
            this.Instantiator = obj.gameObject.GetComponent<ConsumableInstantiator>();
        }

        /// <summary>
        /// Start is called before the first frame to initialize the object
        /// </summary>
        public virtual void Start()
        {
            this.LastVelocity = new Vector2(0, 0);

            this.Renderer = this.gameObject.GetComponent<SpriteRenderer>();
            this.Renderer.drawMode = SpriteDrawMode.Sliced;
            this.RigidBody = this.gameObject.GetComponent<Rigidbody2D>();

            // Generate the type of Blob
            this.BlobType = this.GetBlobType();

            // Generate food value and icon for blob
            this.Icon = GetSprite();
            this.Renderer.sprite = Icon;

            // Generate the size for the blob
            this.SizeVector = this.GetSize();
            this.Renderer.size = SizeVector;

            // Adjust the collider
            this.Collider = this.GetComponent<CircleCollider2D>();
            UpdateColliderSize();

            // Update the mass for the RigidBody to the FoodValue
            float mass = Math.Abs(BaseMass + ((MassMultiplier * this.FoodValue)
                                               / ConsumableAction.MaxFoodValue));
            this.RigidBody.mass = mass;
            this.Mass = mass;

            // Set instantiation variable
            this.SetInstantiator();
        }

        /// <summary>
        /// Updates the collider radius for the Blob object's collider
        /// </summary>
        public void UpdateColliderSize()
        {
            Vector2 spriteHalfSize = this.Renderer.size / 2.0f;
            this.Collider.radius = spriteHalfSize.x > spriteHalfSize.y ?
                spriteHalfSize.x : spriteHalfSize.y;
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

        public void UpdateSize(Vector2? decrease = null)
        {
            Vector2 decreaseValue;
            if (decrease == null)
            {
                decreaseValue = this.GetSize() - this.Renderer.size;
            }
            else
            {
                decreaseValue = (Vector2) decrease;
            }
            this.Renderer.size += decreaseValue;
            this.UpdateColliderSize();
        }

        /// <summary>
        /// Called once per frame to update the object
        /// </summary>
        public virtual void Update(){}
    }
}