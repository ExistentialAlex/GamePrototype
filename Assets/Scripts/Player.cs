namespace Prototype.GameGeneration.Sprite
{
    using System;
    using Prototype.GameGeneration.Rooms;
    using UnityEngine;

    /// <summary>
    /// Script controlling the behaviors of the player sprite.
    /// </summary>
    public class Player : MovingObject
    {
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

        /// <inheritdoc/>
        protected override void MoveAnimation(Vector3 movement)
        {
            // If we're actually moving (I.e. the user has a button pressed) then animate the player walking
            if (movement.x != 0 || movement.y != 0)
            {
                this.Animator.SetBool(Convert.ToString(AnimationBools.playerWalk), true);
            }
            else
            {
                this.Animator.SetBool(Convert.ToString(AnimationBools.playerWalk), false);
            }
        }

        /// <summary>
        /// Takes user input on the player, calculates if the player can move in that direction and
        /// moves sprite.
        /// </summary>
        private void FixedUpdate()
        {
            float x = Input.GetAxisRaw(Convert.ToString(Configuration.InputAxes.Horizontal));
            float y = Input.GetAxisRaw(Convert.ToString(Configuration.InputAxes.Vertical));

            this.UpdateMotor(new Vector3(x, y, 0f));
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
    }
}
