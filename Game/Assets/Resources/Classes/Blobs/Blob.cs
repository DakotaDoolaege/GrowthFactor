using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

namespace Assets.Resources.Classes.Blobs
{
    /*
     * TODO:
     *
     * - Move consuming into just the Consumable class, so that on collision the
     *   Consumable class takes care of all the consuming. It calls its action, which
     *   calls the appropriate functionality on the Player, and then it calls the
     *   appropriate functionality on itself.
     */


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
        public Rigidbody2D RigidBody;
        public Vector2 SizeVector;
        public Sprite Icon;
        public SpriteRenderer Renderer;
        public CircleCollider2D Collider;
        protected BlobType BlobType;
        public abstract int FoodValue { get; set; }
        public const int MinimumFoodValue = 0;
        public Vector2 LastVelocity;
        public Vector2 Acceleration;

        public IEnumerator OnCollisionEvent { get; set; }

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
            //this.FoodValue = GetFoodValue();
            this.Icon = GetSprite();
            this.Renderer.sprite = Icon;

            // Generate the size for the blob
            this.SizeVector = this.GetSize();
            this.Renderer.size = SizeVector;
            //this.transform.localScale = this.SizeVector;

            // Adjust the collider
            this.Collider = this.GetComponent<CircleCollider2D>();
            UpdateColliderSize();

            // Update the mass for the RigidBody to the FoodValue
            this.RigidBody.mass = Math.Abs(this.FoodValue);
            if (this.FoodValue == 0)
            {
                this.RigidBody.mass = 1;
            }
        }

        /// <summary>
        /// Updates the collider radius for the Blob object's collider
        /// </summary>
        protected void UpdateColliderSize()
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

        /// <summary>
        /// Called once per frame to update the object
        /// </summary>
        public virtual void Update(){}

        ///// <summary>
        ///// Calculates the changes in the x-coordinate and y-coordinate of
        ///// the force vector upon collision with some object. The changes in
        ///// forces are calculated as follows:
        /////
        ///// The x coordinate changes by mass * cos(theta) where mass is the
        ///// mass of the object being collided with and theta is the angle
        ///// between the positive x-axis and the vector of the velocity of
        ///// the collided with object.
        /////
        ///// The y coordinate changes by mass * sin(theta) where mass is the
        ///// mass of the object being collided with and theta is the angle
        ///// between the positive x-axis and the vector of the velocity of
        ///// the collided with object.
        ///// </summary>
        ///// <param name="collision">The object being collided with</param>
        //public void CalculateCollision2D(Collision2D collision)
        //{
        //    if (collision.gameObject.tag == "Food" || collision.gameObject.tag == "Player")
        //    {
        //        Rigidbody2D rigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
        //        float mass = rigidbody.mass;
        //        Blob blob = collision.gameObject.GetComponent<Blob>();

        //        // Get the vectors of velocity and positive x-axis
        //        Vector2 velocity = rigidbody.velocity;
        //        Vector2 xAxis = rigidbody.centerOfMass + new Vector2(1, 0);

        //        // Calculate the angle between the two
        //        float theta = Vector2.Angle(xAxis, velocity);

        //        // Calculate the changes in the x and y coordinates to be
        //        // applied to the current object
        //        float x = (float) (mass * blob.Acceleration.x * Math.Cos(theta));
        //        float y = (float) (mass * blob.Acceleration.y * Math.Sin(theta));
        //        Vector2 force = new Vector2(x, y);

        //        // Change the current object's velocity by the calculate force
        //        this.RigidBody.velocity -= force;
        //    }
        //}

        /// <summary>
        /// FixedUpdate() is called a by the UnityEngine to deal with physics
        /// </summary>
        public void FixedUpdate()
        {
            //// Since this method deals with physics, it is a safe option to 
            //// calculate the object's acceleration here
            //this.Acceleration = (LastVelocity - this.RigidBody.velocity)
            //                    / Time.fixedDeltaTime;
            //this.LastVelocity = this.RigidBody.velocity;
        }
    }
}