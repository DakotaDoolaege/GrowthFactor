using UnityEngine;

namespace Assets.Resources.Classes.Instantiator
{
    public abstract class Instantiator : MonoBehaviour
    {
        public GameObject Prefab;
        protected Vector3[] StartPositions;

        public abstract int Count { get; set; }

        // Start is called before the first frame update
        public virtual void Start()
        {
            this.InitializePositionArray();
            this.SetStartPositions();
        }

        /// <summary>
        /// Generates a Blob object at an appropriate position
        /// </summary>
        /// <returns>
        /// The Blob object generated
        /// </returns>
        public abstract GameObject GenerateBlob();

        /// <summary>
        /// Initializes the array of possible positions for the Blobs
        /// </summary>
        protected abstract void InitializePositionArray();

        /// <summary>
        /// Sets the start positions in the position array
        /// </summary>
        protected abstract void SetStartPositions();
    }
}
