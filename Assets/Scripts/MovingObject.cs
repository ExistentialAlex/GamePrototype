namespace Prototype.GameGeneration.Sprite
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Moving Object class.
    /// </summary>
    public abstract class MovingObject : MonoBehaviour
    {
        [SerializeField]
        private float xSpeed;

        [SerializeField]
        private float ySpeed;

        /// <summary>
        /// Gets or sets the speed to move in the x-axis.
        /// </summary>
        /// <value>The X Speed.</value>
        public float XSpeed
        {
            get => this.xSpeed;
            set => this.xSpeed = value;
        }

        /// <summary>
        /// Gets or sets the speed to move in the y-axis.
        /// </summary>
        /// <value>The Y Speed.</value>
        public float YSpeed
        {
            get => this.ySpeed;
            set => this.ySpeed = value;
        }

        /// <summary>
        /// Gets or sets the animation controller for the player.
        /// </summary>
        /// <value>The animator.</value>
        protected internal Animator Animator { get; set; }

        /// <summary>
        /// Gets or sets the box collider component of the player.
        /// </summary>
        /// <value>The object's box collider.</value>
        protected internal BoxCollider2D BoxCollider { get; set; }

        /// <summary>
        /// Gets or sets the change in movement.
        /// </summary>
        /// <value>The object's move delta.</value>
        protected internal Vector3 MoveDelta { get; set; }

        /// <summary>
        /// Gets or sets whether the object has hit something or not.
        /// </summary>
        /// <value>Whether the object has hit something.</value>
        private RaycastHit2D Hit { get; set; }

        /// <summary>
        /// Animation to run when the object is moving.
        /// </summary>
        /// <param name="movement">The vector position of the movement.</param>
        protected abstract void MoveAnimation(Vector3 movement);

        /// <inheritdoc/>
        protected virtual void Start()
        {
            // Get a component reference to this object's BoxCollider2D
            this.BoxCollider = this.GetComponent<BoxCollider2D>();
            this.Animator = this.GetComponent<Animator>();
        }

        /// <summary>
        /// Move the object to the input position.
        /// </summary>
        /// <param name="input">Vector to move to.</param>
        protected virtual void UpdateMotor(Vector3 input)
        {
            // Reset the Move Delta
            this.MoveDelta = new Vector3(input.x * this.XSpeed, input.y * this.YSpeed, 0f);

            // Swap Sprite Direction
            if (this.MoveDelta.x > 0)
            {
                this.transform.localScale = Vector3.one;
            }
            else if (this.MoveDelta.x < 0)
            {
                this.transform.localScale = new Vector3(-1, 1, 1);
            }

            // Make sure we can move in the Y direction
            this.Hit = Physics2D.BoxCast(
                this.transform.position,
                this.BoxCollider.size,
                0,
                new Vector2(0, this.MoveDelta.y),
                Mathf.Abs(this.MoveDelta.y * Time.deltaTime));

            if (this.Hit.collider == null || this.Hit.transform.gameObject.layer == LayerMask.GetMask(Convert.ToString(Configuration.SortingLayers.Floor)))
            {
                // Move
                this.transform.Translate(0, this.MoveDelta.y * Time.deltaTime, 0f);
            }

            // Make sure we can move in the X direction
            this.Hit = Physics2D.BoxCast(
                this.transform.position,
                this.BoxCollider.size,
                0,
                new Vector2(this.MoveDelta.x, 0),
                Mathf.Abs(this.MoveDelta.x * Time.deltaTime));

            if (this.Hit.collider == null || this.Hit.transform.gameObject.layer == LayerMask.GetMask(Convert.ToString(Configuration.SortingLayers.Floor)))
            {
                // Move
                this.transform.Translate(this.MoveDelta.x * Time.deltaTime, 0, 0f);
            }

            this.MoveAnimation(this.MoveDelta);
        }

        /// <inheritdoc/>
        private void Update()
        {
        }
    }
}
