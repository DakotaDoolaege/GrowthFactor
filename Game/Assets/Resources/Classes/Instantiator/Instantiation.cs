using System.Collections.Generic;
using UnityEngine;

namespace Assets.Resources.Classes.Instantiator
{
    public class Instantiation : MonoBehaviour
    {
        public GameObject Prefab;
        public int Level { get; set; } = 1;
        public int Count { get; set; } = 0;
        public IList<Vector3> StartPositions { get; set; }
        public int NumStartPositions { get; set; } = 5;
        private System.Random _rnd;

        /// <summary>
        /// Start is called before the first frame
        /// </summary>
        void Start()
        {
            StartPositions = new List<Vector3>();
            this._rnd = new System.Random();
            this.SetStartPositions();

            for (int i = 0; i < this.NumStartPositions; i++)
            {
                this.GenerateBlobs();
            }
        }
        
        /// <summary>
        /// Update is called once per frame
        /// </summary>
        void Update()
        {
            this.GenerateBlobs();
        }

        /// <summary>
        /// Action to perform when a blob is consumed
        /// </summary>
        public void ConsumeBlob()
        {
            this.Count--;
        }

        private void GenerateBlobs()
        {
            int index = this._rnd.Next(0, this.StartPositions.Count);
            Vector3 spawnPoint = this.StartPositions[index];

            if (this.CheckPositionEmpty(spawnPoint) && this.Count < this.CalculateMaxFood())
            {
                Debug.Log(spawnPoint);
                GameObject a = Instantiate(this.Prefab, spawnPoint, Quaternion.identity);
                this.Count++;
            }
        }

        public void SetStartPositions()
        {
            float x = Camera.main.transform.position.x;
            float y = Camera.main.transform.position.y;

            float height = Camera.main.orthographicSize * 2.0f;
            float width = Camera.main.aspect * height;

            Debug.Log("X: " + x);
            Debug.Log("Y: " + y);
            Debug.Log(Camera.main.rect);

            float shift = height / (2 * this.NumStartPositions);

            for (int i = 1; i <= this.NumStartPositions; i++)
            {
                float xPos = (float) (x);
                float yPos = (float) (y - (height / 2) + ((height * i) / this.NumStartPositions)) - shift;


                Debug.Log(yPos);

                Vector3 vector = new Vector3(xPos, yPos, 0);
                this.StartPositions.Add(vector);
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
            return this.Level % 3 + 5;
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
