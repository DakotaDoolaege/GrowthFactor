using System.Collections;
using UnityEngine;

namespace Assets.Resources.Classes.Blobs
{
    /// <summary>
    /// Class <c>DragAndDrop</c> models a Drag-And-Drop behaviour
    /// </summary>
    public class DragAndDrop : MonoBehaviour
    {
        public Vector2 startPos;
        public Vector2 direction;
        public bool directionChosen;
        private Rigidbody2D _body;
        private float _startPosX;
        private float _startPosY;
        private bool _isHeld = false;

        [SerializeField] float _offset = 0.05f;

        private Vector2 _lastPosition;
        [SerializeField] private const float SaveDelay = 0.2f;
        [SerializeField] private const float Power = 5f;
        private bool _nextSave = true;

        private bool _following;
        private Rigidbody2D _rigidBody;
        private Vector2 _direction;
        private bool _canBePushed;


        /// <summary>
        /// Start is called before the first frame to initialize the object
        /// </summary>
        private void Start()
        {
            this._following = false;
            this._offset += 10;
            this._lastPosition = this.transform.position;
            this._rigidBody = this.GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        private void Update()
        {
            if (_isHeld)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint((mousePos));
                _isHeld = true;

               // this.gameObject.transform.localPosition = new Vector3(mousePos.x - _startPosX, mousePos.y - _startPosY, 0);
            }

            if (Input.GetMouseButtonDown(0) && (this.CalculateMagnitude()))
            {
                this._following = true;
            }

            if (Input.GetMouseButtonUp(0) && (this.CalculateMagnitude()))
            {
                this._following = false;
                this._direction = (Vector2) this.transform.position - this._lastPosition;
                this._canBePushed = true;
            }

            if (this._following)
            {
                const float midpoint = 0.5f;
               // this.transform.position = Vector2.Lerp(this.transform.position,
                   // Camera.main.ScreenToWorldPoint(Input.mousePosition), midpoint);
            }

            if (this._nextSave)
            {
                StartCoroutine("SavePosition");
            }
        }


        /// <summary>
        /// Fixed update is called by the UnityEngine to update physics.
        /// </summary>
        private void FixedUpdate()
        {
            if (this._canBePushed)
            {
                this._canBePushed = false;
                this._rigidBody.velocity = this._direction * Power;
            }

            if (Input.GetMouseButtonUp(0))
            {
                Vector2 vel = this._rigidBody.velocity;
                this._rigidBody.AddForce(vel);
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
            this._nextSave = false;
            this._lastPosition = this.transform.position;
            yield return new WaitForSeconds(SaveDelay);
            this._nextSave = true;
        }

        private bool CalculateMagnitude()
        {
            return (Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position).magnitude <= this._offset;
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

                this._startPosX = mousePos.x - this.transform.localPosition.x;
                this._startPosY = mousePos.y - this.transform.localPosition.y;

                this._isHeld = true;
            }
        }



        /// <summary>
        /// OnMouseUp handles the event where the left mouse button is realesed
        /// from an object.
        /// </summary>
        private void OnMouseUp()
        {
            _isHeld = false;
            Vector2 vel = this._rigidBody.velocity;
            this._rigidBody.AddForce(vel);
        }

    }
}
