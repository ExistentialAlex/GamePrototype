namespace Prototype.GameGeneration.Player
{
    using System;
    using Prototype.GameGeneration.Rooms;
    using UnityEngine;

    /// <summary>
    /// Script controlling the behaviors of the player sprite.
    /// </summary>
    public class Player : MonoBehaviour
    {
        /// <summary>
        /// The animation controller for the player.
        /// </summary>
        private Animator animator;

        /// <summary>
        /// The box collider component of the player.
        /// </summary>
        private BoxCollider2D boxCollider;

        /// <summary>
        /// Whether the player has hit something or not.
        /// </summary>
        private RaycastHit2D hit;

        /// <summary>
        /// The change in movement.
        /// </summary>
        private Vector3 moveDelta;

        /// <summary>
        /// The player's speed.
        /// </summary>
        [SerializeField]
        private float playerSpeed;

        /// <summary>
        /// Booleans defined in the animation pane.
        /// </summary>
        private enum AnimationBools
        {
            playerWalk
        }

        /// <summary>
        /// Triggers defined in the animation pane.
        /// </summary>
        private enum AnimationTriggers
        {
            playerIdle,
            playerHit
        }

        /// <summary>
        /// Gets or sets the player speed.
        /// </summary>
        /// <value>The player's speed.</value>
        public float PlayerSpeed
        {
            get => this.playerSpeed;
            set => this.playerSpeed = value;
        }

        /// <summary>
        /// Takes user input on the player, calculates if the player can move in that direction and
        /// moves sprite.
        /// </summary>
        private void FixedUpdate()
        {
            this.moveDelta = Vector3.zero;
            float playerSpeed = Time.deltaTime * this.PlayerSpeed;

            float x = Input.GetAxisRaw(Convert.ToString(Configuration.InputAxes.Horizontal));
            float y = Input.GetAxisRaw(Convert.ToString(Configuration.InputAxes.Vertical));

            // Reset the Move Delta
            this.moveDelta = new Vector3(x, y, 0f);

            // Swap Sprite Direction
            if (this.moveDelta.x > 0)
            {
                this.transform.localScale = Vector3.one;
            }
            else if (this.moveDelta.x < 0)
            {
                this.transform.localScale = new Vector3(-1, 1, 1);
            }

            // Make sure we can move in the Y direction
            this.hit = Physics2D.BoxCast(
                this.transform.position,
                this.boxCollider.size,
                0,
                new Vector2(0, this.moveDelta.y),
                Mathf.Abs(this.moveDelta.y * playerSpeed));

            if (this.hit.collider == null || this.hit.transform.gameObject.layer == LayerMask.GetMask(Convert.ToString(Configuration.SortingLayers.Floor)))
            {
                // Move
                this.transform.Translate(0, this.moveDelta.y * playerSpeed, 0f);
            }

            // Make sure we can move in the X direction
            this.hit = Physics2D.BoxCast(
                this.transform.position,
                this.boxCollider.size,
                0,
                new Vector2(this.moveDelta.x, 0),
                Mathf.Abs(this.moveDelta.x * playerSpeed));

            if (this.hit.collider == null || this.hit.transform.gameObject.layer == LayerMask.GetMask(Convert.ToString(Configuration.SortingLayers.Floor)))
            {
                // Move
                this.transform.Translate(this.moveDelta.x * playerSpeed, 0, 0f);
            }

            // If we're actually moving (I.e. the user has a button pressed) then animate the player walking
            if (x != 0 || y != 0)
            {
                this.animator.SetBool(Convert.ToString(AnimationBools.playerWalk), true);
            }
            else
            {
                this.animator.SetBool(Convert.ToString(AnimationBools.playerWalk), false);
            }
        }

        /// <summary>
        /// Load the next level in the game.
        /// </summary>
        private void LoadNextLevel()
        {
            // By checking if the player is active, we can stop the level from re-loading multiple times.
            if (!GameManager.Instance.PlayerReady)
            {
                return;
            }

            GameManager.Instance.FinishLevel();
        }

        /// <summary>
        /// Move the player to the next stair room based on the current stair room.
        /// </summary>
        /// <param name="collision">The tile that the player collided with.</param>
        private void MovePlayerToLinkedStair(Collider2D collision)
        {
            if (!GameManager.Instance.PlayerReady)
            {
                return;
            }

            GameManager.Instance.PlayerReady = false;

            GameObject parent = collision.gameObject.transform.parent.gameObject;
            Vector3 parentVector = parent.transform.position;

            StairRoom currentStair = default;

            // Find the current stair
            foreach (Floor floor in GameManager.Instance.CurrentLevel.Floors)
            {
                foreach (StairRoom stair in floor.Stairs)
                {
                    if (stair.GlobalVectorPosition == parentVector)
                    {
                        currentStair = stair;
                        break;
                    }
                }
            }

            // if we haven't found the stair the player stepped on then exit
            if (currentStair == null)
            {
                GameManager.Instance.PlayerReady = true;
                return;
            }

            StairRoom nextStair = currentStair.StairPair;
            Vector3 teleportPos = new Vector3(nextStair.GlobalVectorPosition.x + (Room.RoomWidth / 2), nextStair.GlobalVectorPosition.y + (Room.RoomHeight / 2));

            Debug.Log("Moving player to stair: " + teleportPos.x + ":" + teleportPos.y);

            transform.position = teleportPos;
        }

        /// <summary>
        /// When the player collides with another game object, depending on the object perform an action.
        /// </summary>
        /// <param name="collision">The collision made by the player.</param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.tag)
            {
                case Configuration.COMPONENT_TAGS_STAIR:
                    {
                        this.MovePlayerToLinkedStair(collision);
                        break;
                    }

                case Configuration.COMPONENT_TAGS_STAIR_DOWN:
                    {
                        GameManager.Instance.PlayerReady = true;
                        break;
                    }

                case Configuration.COMPONENT_TAGS_EXIT:
                    {
                        // Invoke the method 'LoadNextLevel' with a 1 second delay
                        this.Invoke(nameof(this.LoadNextLevel), 1);
                        break;
                    }
            }
        }

        /// <summary>
        /// Run when the player script starts.
        /// Gets instances of the animator and box collider components.
        /// </summary>
        private void Start()
        {
            this.animator = this.GetComponent<Animator>();
            this.boxCollider = this.GetComponent<BoxCollider2D>();
        }
    }
}
