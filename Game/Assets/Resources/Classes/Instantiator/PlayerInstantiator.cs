using System.Collections.Generic;
using Assets.Resources.Classes.Blobs;
using UnityEngine;

namespace Assets.Resources.Classes.Instantiator
{
    public class PlayerInstantiator : Instantiator
    {
        public const int NumStartPositions = 6;
        //public IList<Blob> CurrentBlobs;
        private const float ShiftFromEdge = 1.0f;

        protected override void InitializePositionArray()
        {
            this.StartPositions = new Vector3[NumStartPositions];
        }

        /// <summary>
        /// The current number of players playing
        /// </summary>
        public int NumPlayers { get; set; }

        // Start is called before the first frame update
        public override void Start()
        {
            this.CurrentBlobs = new List<Blob>();   
            //this.SetStartPositions();
            base.Start();
        }

        // Update is called once per frame
        void Update()
        {
            //if (Input.GetMouseButtonDown(0))
            //{
            //    this.GenerateBlob();
            //}
        }

        protected override void SetStartPositions()
        {
            float x = Camera.main.transform.position.x;
            float y = Camera.main.transform.position.y;
            float z = 0f;

            float height = Camera.main.orthographicSize * 2.0f;
            float width = Camera.main.aspect * height;

            Debug.Log(height);
            Debug.Log(width);

            this.StartPositions[0] = new Vector3(x + (width / 2) - ShiftFromEdge, y, z);
            this.StartPositions[1] = new Vector3(x - (width / 2) + ShiftFromEdge, y, z);

            this.StartPositions[2] = new Vector3(x + (width / 4), y + (height / 2) - ShiftFromEdge, z);
            this.StartPositions[3] = new Vector3(x - (width / 4), y + (height / 2) - ShiftFromEdge, z);

            this.StartPositions[4] = new Vector3(x + (width / 4), y - (height / 2) + ShiftFromEdge, z);
            this.StartPositions[5] = new Vector3(x - (width / 4), y - (height / 2) + ShiftFromEdge, z);
        }

        /// <summary>
        /// Adds a player to the current game
        /// </summary>
        /// <returns>
        /// The player GameObject that was added to the game.
        /// </returns>
        public override GameObject GenerateBlob()
        {
            if (this.Count == 6)
            {
                return null;
            }
            Vector3 startPosition = this.StartPositions[this.CurrentBlobs.Count];
            Debug.Log("START POS:" +startPosition);
            GameObject player = Instantiate(this.Prefab, startPosition, Quaternion.identity);
            this.CurrentBlobs.Add(player.GetComponent<Player>());

            return player;
        }
    }
}
