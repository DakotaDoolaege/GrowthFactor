using System.Collections.Generic;
using Assets.Resources.Classes.Blobs;
using UnityEngine;


namespace Assets.Resources.Classes.Instantiator
{
    /// <summary>
    /// Class <c>PlayerInstantiator</c> instantiates players at appropriate
    /// locations.
    /// </summary>
    public class PlayerInstantiator : Instantiator
    {

        public GameObject LevelCanvas;
        public GameObject PauseButton;
        public bool paused = false;

        public const int NumStartPositions = 6;
        private const float ShiftFromEdge = 1.0f;

        /// <summary>
        /// The current number of players playing
        /// </summary>
        public int NumPlayers { get; set; }

        // Start is called before the first frame update
        public override void Start()
        {
            GameObject Canvas = Instantiate(LevelCanvas, new Vector3(0,0,0), Quaternion.identity) as GameObject;
            for (int i = 0; i < GameVariables.PlayerStations.Count; i++)
            {
                Vector3 startPosition = GameVariables.PlayerStations[i].GetPosition();
                Vector2 offset = this.GetOffset(startPosition);
                startPosition.x = startPosition.x - 1920 + offset.x;
                startPosition.y = startPosition.y - 1080 + offset.y;

                GameObject Pausebtn = Instantiate(PauseButton, startPosition, Quaternion.identity) as GameObject;
                Pausebtn.transform.SetParent(Canvas.transform, false);

                Vector3 rotation = this.GetRotation(startPosition);
                Pausebtn.transform.Rotate(rotation);
            }

            this.CurrentBlobs = new List<Blob>();
            base.Start();
        }

        /// <summary>
        /// Initializes the array of player positions
        /// </summary>
        protected override void InitializePositionArray()
        {
            this.StartPositions = new Vector3[NumStartPositions];
        }

        /// <summary>
        /// Gets the rotation for players
        /// </summary>
        /// <param name="position">The approximate position of the player</param>
        /// <returns></returns>
        private Vector3 GetRotation(Vector2 position)
        {
            if (position.y == 200 && position.x == -1600)
            {
                return new Vector3(0, 0, 270);
            }
            else if (position.y == -200 && position.x == 1600)
            {
                return new Vector3(0, 0, 90);
            }
            else if (position.y >= 500)
            {
                return new Vector3(0, 0, 180);
            }
            return new Vector3(0, 0, 0);
        }

        /// <summary>
        /// Gets the offset from the edge of the screen at which to set the
        /// player
        /// </summary>
        /// <param name="position">The approximate position of the player</param>
        /// <returns></returns>
        private Vector2 GetOffset(Vector2 position)
        {
            if (position.x <= 1920 && position.y < 1080)
            {
                return new Vector2(-200, 0);
            }
            else if (position.x >= 1920 && position.y < 1080)
            {
                return new Vector2(-200, 0);
            }
            else if (position.x <= 1920 && position.y > 1080)
            {
                return new Vector2(200, 0);
            }
            else if (position.x >= 1920 && position.y > 1080)
            {
                return new Vector2(200, 0);
            }
            else if (position.x >= 1029 && position.y == 1080)
            {
                return new Vector2(0, -200);
            }
            return new Vector2(0, 200);
        }

        /// <summary>
        /// Sets the appropriate start positions for players
        /// </summary>
        protected override void SetStartPositions()
        {
            float x = Camera.main.transform.position.x;
            float y = Camera.main.transform.position.y;
            float z = 0f;

            float height = Camera.main.orthographicSize * 2.0f;
            float width = Camera.main.aspect * height;

            this.StartPositions[0] = new Vector3(x + (width / 2) - ShiftFromEdge, y, z);
            this.StartPositions[1] = new Vector3(x - (width / 2) + ShiftFromEdge, y, z);

            this.StartPositions[2] = new Vector3(x + (width / 4), y + (height / 2) - ShiftFromEdge, z);
            this.StartPositions[3] = new Vector3(x - (width / 4), y + (height / 2) - ShiftFromEdge, z);

            this.StartPositions[4] = new Vector3(x + (width / 4), y - (height / 2) + ShiftFromEdge, z);
            this.StartPositions[5] = new Vector3(x - (width / 4), y - (height / 2) + ShiftFromEdge, z);
        }

        /// <summary>
        /// Adds a player to the current game with position from list of chosen players
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
            Vector3 startPosition = (Camera.main.ScreenToWorldPoint(GameVariables.PlayerStations[this.CurrentBlobs.Count].GetPosition()));
            startPosition.z = 0;

            GameObject player = Instantiate(this.Prefab, startPosition, Quaternion.identity);
            startPosition.y = startPosition.y - 2;

            this.CurrentBlobs.Add(player.GetComponent<Player>());

            // Rotate players
            Vector2 position = new Vector2(startPosition.x, startPosition.y);

            if (position.x >= 0 && position.y >= 0)
            {
                player.transform.Rotate(new Vector3(0, 0, 180));

            }
            else if (position.x >= 0 && position.y >= -2 && position.y <= 2)
            {
                player.transform.Rotate(new Vector3(0, 0, 90));
            }
            else if (position.x <= 0 && position.y >= -2 && position.y <= 2)
            {
                player.transform.Rotate(new Vector3(0, 0, -90));
            }
            else if (position.x <= 0 && position.y >= 0)
            {
                player.transform.Rotate(new Vector3(0, 0, 180));
            }

            return player;
        }
    }
}
