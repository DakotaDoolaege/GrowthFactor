using System.Collections;
using UnityEngine;

namespace Assets.Resources.Classes.Blobs
{
    /// <summary>
    /// Class <c>DragAndDrop</c> models a Drag-And-Drop behaviour
    ///
    /// Note that this file is deprecated and is only kept so that we can
    /// test the game with a mouse and are not forced to test with a touch
    /// screen.
    /// </summary>
    public class DragAndDrop : MonoBehaviour
    {
        public Vector2 StartPos;
        public Vector2 Direction;
        public bool DirectionChosen;
        private Rigidbody2D body;
        private float startPosX;
        private float startPosY;
        private bool isHeld = false;

        [SerializeField] float offset = 0.05f;

        private Vector2 lastPosition;
        [SerializeField] private const float SAVE_DELAY = 0.2f;
        [SerializeField] private const float POWER = 5f;
        private bool nextSave = true;

        private bool following;
        private Rigidbody2D rigidBody;
        private Vector2 direction;
        private bool canBePushed;


        /// <summary>
        /// Start is called before the first frame to initialize the object
        /// </summary>
        private void Start()
        {
            this.following = false;
            this.offset += 10;
            this.lastPosition = this.transform.position;
            this.rigidBody = this.GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        private void Update()
        {
            if (isHeld)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint((mousePos));
                isHeld = true;
                //this.gameObject.transform.localPosition = new Vector3(mousePos.x - _startPosX, mousePos.y - _startPosY, 0);
            }

            if (Input.GetMouseButtonDown(0) && (this.CalculateMagnitude()))
            {
                this.following = true;
            }

            if (Input.GetMouseButtonUp(0) && (this.CalculateMagnitude()))
            {
                this.following = false;
                this.direction = (Vector2) this.transform.position - this.lastPosition;
                this.canBePushed = true;
            }

            if (this.nextSave)
            {
                StartCoroutine("SavePosition");
            }
        }


        /// <summary>
        /// Fixed update is called by the UnityEngine to update physics.
        /// </summary>
        private void FixedUpdate()
        {
            if (this.canBePushed)
            {
                this.canBePushed = false;
                this.rigidBody.velocity = this.direction * POWER;
            }

            if (Input.GetMouseButtonUp(0))
            {
                Vector2 vel = this.rigidBody.velocity;
                this.rigidBody.AddForce(vel);
            }
        }

        /// <summary>
        /// Every SaveDelay seconds, transform the last position to the current
        /// position.
        ///
        /// See https://docs.unity3d.com/Manual/Coroutines.html for more details.
        /// </summary>
        /// <returns>
        /// An IEnumerator object that tells the coroutine when to stop execution
        /// until the SaveDelay seconds have passed.
        /// </returns>
        private IEnumerator SavePosition()
        {
            this.nextSave = false;
            this.lastPosition = this.transform.position;
            yield return new WaitForSeconds(SAVE_DELAY);
            this.nextSave = true;
        }

        /// <summary>
        /// Ensures the movement magnitude is acceptable
        /// </summary>
        /// <returns>Whether the movement is acceptable to make</returns>
        private bool CalculateMagnitude()
        {
            return (Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position).magnitude <= this.offset;
        }

        /// <summary>
        /// OnMouseDown handles the event where the left mouse button is used to
        /// grab an object.
        /// </summary>
        private void OnMouseDown()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint((mousePos));

                this.startPosX = mousePos.x - this.transform.localPosition.x;
                this.startPosY = mousePos.y - this.transform.localPosition.y;

                this.isHeld = true;
            }
        }

        /// <summary>
        /// OnMouseUp handles the event where the left mouse button is realesed
        /// from an object.
        /// </summary>
        private void OnMouseUp()
        {
            if (this.rigidBody != null)
            {
                isHeld = false;
                Vector2 vel = this.rigidBody.velocity;
                this.rigidBody.AddForce(vel);
            }
        }

    }
}
