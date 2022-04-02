namespace Prototype.Units
{
    using Prototype.GameGeneration;
    using UnityEngine;

    /// <summary>
    /// Enemy class.
    /// </summary>
    public class Enemy : Unit
    {
        /// <summary>
        /// The distance an enemy will chase the player.
        /// </summary>
        [SerializeField]
        private float chaseLength = 25;

        /// <summary>
        /// Whether the enemy is currently chasing the player.
        /// </summary>
        private bool chasing;

        /// <summary>
        /// Whether the enemy is current colliding with the player.
        /// </summary>
        private bool collidingWithPlayer;

        /// <summary>
        /// The player's current transform.
        /// </summary>
        private Transform playerTransform;

        /// <summary>
        /// Starting position of the enemy.
        /// </summary>
        private Vector3 startingPosition;

        /// <summary>
        /// The distance an enemy is triggered by.
        /// </summary>
        [SerializeField]
        private float triggerLength = 5;

        /// <summary>
        /// Gets or sets the distance an enemy will chase.
        /// </summary>
        /// <value>The distance an enemy will chase.</value>
        public float ChaseLength { get => this.chaseLength; set => this.chaseLength = value; }

        /// <summary>
        /// Gets or sets the distance an enemy is triggered from.
        /// </summary>
        /// <value>The distance an enemy is triggered from.</value>
        public float TriggerLength { get => this.triggerLength; set => this.triggerLength = value; }

        /// <inheritdoc/>
        protected override void MoveAnimation(Vector3 movement)
        {
        }

        protected override void OnCollide(Collider2D collider)
        {
            if (collider.name == "Player")
            {
                this.collidingWithPlayer = true;
            }
            else
            {
                this.collidingWithPlayer = false;
            }
        }

        /// <inheritdoc/>
        protected override void Start()
        {
            base.Start();
            this.playerTransform = GameManager.Player.transform;
            this.startingPosition = transform.position;
            this.collidingWithPlayer = false;
        }

        /// <inheritdoc/>
        private void FixedUpdate()
        {
            if (Vector3.Distance(this.playerTransform.position, this.startingPosition) < this.ChaseLength)
            {
                if (Vector3.Distance(this.playerTransform.position, this.startingPosition) < this.TriggerLength)
                {
                    this.chasing = true;
                }

                if (this.chasing)
                {
                    if (!this.collidingWithPlayer)
                    {
                        this.UpdateMotor((this.playerTransform.position - this.transform.position).normalized);
                    }
                }
                else
                {
                    this.UpdateMotor(this.startingPosition - this.transform.position);
                }
            }
            else
            {
                this.UpdateMotor(this.startingPosition - this.transform.position);
                this.chasing = false;
            }
        }
    }
}
