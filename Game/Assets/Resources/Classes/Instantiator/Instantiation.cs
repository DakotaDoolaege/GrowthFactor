using UnityEngine;

namespace Assets.Resources.Classes.Instantiator
{
    /*
     * TODO
     *
     * - When a player consumes a Blob, the this.Count should be decremented
     */
    public class Instantiation : MonoBehaviour
    {
        public GameObject Prefab;
        public int Level { get; set; } = 1;
        public int Count { get; set; } = 0;

        /// <summary>
        /// Start is called before the first frame
        /// </summary>
        void Start()
        {
        }
        
        /// <summary>
        /// Update is called once per frame
        /// </summary>
        void Update()
        {
            if (this.CheckPositionEmpty(new Vector3(0, 0, 0)) && this.Count < this.CalculateMaxFood())
            {
                Instantiate(Prefab, new Vector2(0, 0), Quaternion.identity);
                this.Count++;
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
            return this.Level * 5 + 5;
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
