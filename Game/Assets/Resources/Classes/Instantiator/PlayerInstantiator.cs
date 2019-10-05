using System.Collections.Generic;
using Assets.Resources.Classes.Blobs;
using UnityEngine;

namespace Assets.Resources.Classes.Instantiator
{
    public class PlayerInstantiator : MonoBehaviour
    {
        public const int MaxPlayers = 6;
        private Vector3[] _startPositions = new Vector3[MaxPlayers];
        public IList<Blob> CurrentPlayers;
        public GameObject Prefab;
        private const float ShiftFromEdge = 1.0f;

        public int Count => this.CurrentPlayers.Count;

        /// <summary>
        /// The current number of players playing
        /// </summary>
        public int NumPlayers { get; set; }

        // Start is called before the first frame update
        public void Start()
        {
            this.CurrentPlayers = new List<Blob>();   
            this.SetStartPositions();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                this.AddPlayer();
            }
        }

        private void SetStartPositions()
        {
            float x = Camera.main.transform.position.x;
            float y = Camera.main.transform.position.y;
            float z = 0f;

            float height = Camera.main.orthographicSize * 2.0f;
            float width = Camera.main.aspect * height;

            Debug.Log(height);
            Debug.Log(width);

            this._startPositions[0] = new Vector3(x + (width / 2) - ShiftFromEdge, y, z);
            this._startPositions[1] = new Vector3(x - (width / 2) + ShiftFromEdge, y, z);

            this._startPositions[2] = new Vector3(x + (width / 4), y + (height / 2) - ShiftFromEdge, z);
            this._startPositions[3] = new Vector3(x - (width / 4), y + (height / 2) - ShiftFromEdge, z);

            this._startPositions[4] = new Vector3(x + (width / 4), y - (height / 2) + ShiftFromEdge, z);
            this._startPositions[5] = new Vector3(x - (width / 4), y - (height / 2) + ShiftFromEdge, z);
        }

        /// <summary>
        /// Adds a player to the current game
        /// </summary>
        /// <returns>
        /// The player GameObject that was added to the game.
        /// </returns>
        public GameObject AddPlayer()
        {
            if (this.Count == 6)
            {
                return null;
            }
            Vector3 startPosition = this._startPositions[this.CurrentPlayers.Count];
            Debug.Log(startPosition);
            GameObject player = Instantiate(this.Prefab, startPosition, Quaternion.identity);
            this.CurrentPlayers.Add(player.GetComponent<Player>());

            return player;
        }
    }
}
