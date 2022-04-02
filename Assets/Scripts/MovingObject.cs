namespace Prototype.Units
{
    using Prototype.Common;
    using Prototype.GameGeneration;
    using System;
    using UnityEngine;

    /// <summary>
    /// Moving Object class.
    /// </summary>
    public abstract class MovingObject : Collidable
    {
        [SerializeField]
        private float speed;


        /// <summary>
        /// Gets or sets the speed to move.
        /// </summary>
        /// <value>The Speed.</value>
        public float Speed
        {
            get => this.speed;
            set => this.speed = value;
        }

        /// <summary>
        /// Gets or sets the animation controller for the player.
        /// </summary>
        /// <value>The animator.</value>
        protected internal Animator Animator { get; set; }

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
        protected override void Start()
        {
            base.Start();

            // Get a component reference to this object's BoxCollider2D
            this.Animator = this.GetComponent<Animator>();
        }

        /// <inheritdoc/>
        protected override void Update()
        {
            base.Update();
        }

        /// <summary>
        /// Move the object to the input position.
        /// </summary>
        /// <param name="input">Vector to move to.</param>
        protected virtual void UpdateMotor(Vector3 input, float distance = 1f)
        {
            // Reset the Move Delta
            this.MoveDelta = new Vector3(input.x, input.y, 0f);

            // Swap Sprite Direction
            if (this.MoveDelta.x > 0)
            {
                this.transform.localScale = Vector3.one;
            }
            else if (this.MoveDelta.x < 0)
            {
                this.transform.localScale = new Vector3(-1, 1, 1);
            }


            // if we can't move diagonally
            bool canMove = this.CanMove(this.MoveDelta, distance * this.Speed);

            if (!canMove)
            {
                this.MoveDelta = new Vector3(input.x, 0f, 0f);
                canMove = this.MoveDelta.x != 0f && this.CanMove(this.MoveDelta, distance * this.Speed);
                if (!canMove)
                {
                    this.MoveDelta = new Vector3(0f, input.y, 0f);
                    canMove = this.MoveDelta.y != 0f && this.CanMove(this.MoveDelta, distance * this.Speed);
                }
            }

            if (canMove)
            {
                // Move
                this.transform.Translate(this.MoveDelta.x * distance * this.Speed * Time.deltaTime, this.MoveDelta.y * distance * this.Speed * Time.deltaTime, 0f);
                this.MoveAnimation(this.MoveDelta);
            }
        }

        private bool CanMove(Vector3 dir, float distance)
        {
            this.Hit = Physics2D.BoxCast(this.transform.position, this.BoxCollider.size, 0, dir, Mathf.Abs(distance * Time.deltaTime));
            return this.Hit.collider == null || this.Hit.transform.gameObject.layer == LayerMask.GetMask(Convert.ToString(Configuration.SortingLayers.Floor));
        }
    }
}
