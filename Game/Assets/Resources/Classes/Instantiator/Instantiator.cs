using System.Collections.Generic;
using Assets.Resources.Classes.Blobs;
using UnityEngine;

namespace Assets.Resources.Classes.Instantiator
{
    /// <summary>
    /// Class <c>Instantiator</c> instantiates blobs
    /// </summary>
    public abstract class Instantiator : MonoBehaviour
    {
        public GameObject Prefab;
        protected Vector3[] StartPositions;
        public IList<Blob> CurrentBlobs;
        public int Count => this.CurrentBlobs.Count;

        // Start is called before the first frame update
        public virtual void Start()
        {
            this.CurrentBlobs = new List<Blob>();
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
