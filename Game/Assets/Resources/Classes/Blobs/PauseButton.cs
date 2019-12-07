using System;
using System.Collections;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Resources.Classes.Blobs
{
    /// <summary>
    /// Class <c>PauseButton</c> models an in-game pause button
    /// </summary>
    public class PauseButton : Blob
    {
        protected Vector2 DefaultSize = new Vector2(0.1f, 0.1f);
        //private const int InitialPlayerValue = 0;
        public override int FoodValue { get; set; }
        //public Vector3 StartPosition { get; set; }
        public PlayerAction Action { get; set; }
        public int Score { get; set; }


        /// <summary>
        /// Start is called before the first frame to initialize the object
        /// </summary>
        public override void Start()
        {
            // Generate the type of Blob
            this.BlobType = this.GetBlobType();

            // Generate food value and icon for blob
            //this.FoodValue = GetFoodValue();
            this.Icon = GetSprite();

            // Generate the size for the blob
            this.SizeVector = this.GetSize();
        }

        /// <summary>
        /// Pauses the game
        /// </summary>
        public void PauseGame()
        {
            GameVariables.Paused = true;
        }

        /// <summary>
        /// Gets the appropriate BlobType for the Blob
        /// </summary>
        /// <returns>
        /// The type of Blob that the current object is
        /// </returns>
        public override BlobType GetBlobType()
        {
            return BlobType.Pause;
        }

        /// <summary>
        /// Gets the size to initialize the Player with
        /// </summary>
        /// <returns></returns>
        public override Vector2 GetSize()
        {
            return this.DefaultSize;
        }
    }
}
