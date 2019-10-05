using System;
using System.Collections.Generic;
using Assets.Resources.Classes.Blobs;
using UnityEngine;

namespace Assets.Resources.Classes.Instantiator
{
    /*
     * TODO
     * - Implement observable pattern so that the instantiator
     * can register with Consumables so that when they are
     * consumed, we can update in a more appropriate manner.
     *
     * The instantiator should be the observer, and the
     * observable should be the PlayerAction, which decides
     * when a Consumable is consumed.
     */
    public class ConsumableInstantiator : Instantiator
    {
        public const int NumStartPositions = 5; // Override NumStartPositions
        public int Level { get; set; } = 1;
        private System.Random _rnd;

        /// <summary>
        /// Start is called before the first frame
        /// </summary>
        public override void Start()
        {
            base.Start();
            this._rnd = new System.Random();
            //this.SetStartPositions();

            for (int i = 0; i < NumStartPositions; i++)
            {
                this.GenerateBlob();
            }
        }

        protected override void InitializePositionArray()
        {
            this.StartPositions = new Vector3[NumStartPositions];
        }
        
        /// <summary>
        /// Update is called once per frame
        /// </summary>
        void Update()
        {
            this.GenerateBlob();
        }

        /// <summary>
        /// Action to perform when a blob is consumed
        /// </summary>
        public void ConsumeBlob(Consumable consumable)
        {
            //GameObject[] food = GameObject.FindGameObjectsWithTag("Food");
            //GameObject[] powerup = GameObject.FindGameObjectsWithTag("PowerUp");
            //this.Count = food.Length + powerup.Length;

            if (consumable.FoodValue == 0)
            {
                this.CurrentBlobs.Remove((Blob) consumable);
            }
            //this.Count = this.CurrentBlobs.Count;
        }

        /// <summary>
        /// Generates the blobs in the start positions randomly
        /// </summary>
        public override GameObject GenerateBlob()
        {
            int index = this._rnd.Next(0, this.StartPositions.Length);
            Vector3 spawnPoint = this.StartPositions[index];

            if (this.CheckPositionEmpty(spawnPoint) && this.Count < this.CalculateMaxFood())
            {
                GameObject blob = Instantiate(this.Prefab, spawnPoint, Quaternion.identity);

                float xVelocity = (float) (Math.Pow(-1, this._rnd.Next(1, 3))) * (this._rnd.Next(0, 100) / 100.0f);
                float yVelocity = (float) (Math.Pow(-1, this._rnd.Next(1, 3))) * (this._rnd.Next(0, 100) / 100.0f);
                blob.gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(xVelocity, yVelocity);

                //this.Count++;
                this.CurrentBlobs.Add(blob.gameObject.GetComponent<Blob>());
                return blob;
            }

            return null;
        }

        /// <summary>
        /// Sets the start positions on the screen
        /// </summary>
        protected override void SetStartPositions()
        {
            float x = Camera.main.transform.position.x;
            float y = Camera.main.transform.position.y;

            float height = Camera.main.orthographicSize * 2.0f;
            float width = Camera.main.aspect * height;

            float shift = height / (2 * NumStartPositions);

            for (int i = 1; i <= NumStartPositions; i++)
            {
                float xPos = (float) (x);
                float yPos = (float) (y - (height / 2) + ((height * i) / NumStartPositions)) - shift;

                Vector3 vector = new Vector3(xPos, yPos, 0);
                this.StartPositions[i - 1] = vector;
            }
        }

        /// <summary>
        /// Calculates the maximum allowed food for the level
        /// </summary>
        /// <returns>
        /// The maximum allowable food on the level at any given point
        /// </returns>
        public int CalculateMaxFood()
        {
            return this.Level % 3 + 9;
        }

        /// <summary>
        /// Checks if any Blob exists within the radius of the given point
        /// </summary>
        /// <param name="position">The position to check</param>
        /// <param name="radius">The radius around the position to check</param>
        /// <returns>
        /// Whether or not the circle bounded by the argument radius around the
        /// argument position has any Blob object on or within it
        /// </returns>
        private bool CheckPositionEmpty(Vector3 position, float radius=1.0f)
        {
            GameObject[] allFood = GameObject.FindGameObjectsWithTag("Food");
            GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject food in allFood)
            {
                float norm = (food.transform.position - position).magnitude;
                if (norm <= radius)
                {
                    return false;
                }
            }

            foreach (GameObject player in Players)
            {
                float norm = (player.transform.position - position).magnitude;
                if (norm <= radius)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
