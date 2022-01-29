using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameGeneration;
using System;
using GameGeneration.Rooms;
using System.Linq;

namespace GameGeneration.Player
{
    public class Player : MonoBehaviour
    {
        private BoxCollider2D boxCollider;
        private Vector3 moveDelta;
        private RaycastHit2D hit;
        public float playerSpeed = 3; // Baseline player speed
        private Room roomToMoveTo;

        //Used to store a refrence to the Player's animator component
        private Animator animator;

        private enum AnimationTriggers
        {
            playerIdle,
            playerHit
        }

        private enum AnimationBools
        {
            playerWalk
        }

        private void Start()
        {
            //Get a component reference to the Player's animator component
            animator = GetComponent<Animator>();
            boxCollider = GetComponent<BoxCollider2D>();
        }

        /// <summary>
        /// Takes user input on the player, calculates if the player can move in that direction and moves sprite.
        /// </summary>
        private void FixedUpdate()
        {
            moveDelta = Vector3.zero;
            float playerSpeed = Time.deltaTime * this.playerSpeed;

            float x = Input.GetAxisRaw(Convert.ToString(Configuration.InputAxes.Horizontal));
            float y = Input.GetAxisRaw(Convert.ToString(Configuration.InputAxes.Vertical));

            // Reset the Move Delta
            moveDelta = new Vector3(x, y, 0f);

            // Swap Sprite Direction
            if (moveDelta.x > 0)
            {
                transform.localScale = Vector3.one;
            }
            else if (moveDelta.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            // Make sure we can move in the Y direction
            hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0,
                                    new Vector2(0, moveDelta.y),
                                    Mathf.Abs(moveDelta.y * playerSpeed));

            if (hit.collider == null || hit.transform.gameObject.layer == LayerMask.GetMask(Convert.ToString(Configuration.SortingLayers.Floor)))
            {
                // Move
                transform.Translate(0, moveDelta.y * playerSpeed, 0f);
            }

            // Make sure we can move in the X direction
            hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0,
                                    new Vector2(moveDelta.x, 0),
                                    Mathf.Abs(moveDelta.x * playerSpeed));

            if (hit.collider == null || hit.transform.gameObject.layer == LayerMask.GetMask(Convert.ToString(Configuration.SortingLayers.Floor)))
            {
                // Move
                transform.Translate(moveDelta.x * playerSpeed, 0, 0f);
            }

            // If we're actually moving (I.e. the user has a button pressed) then animate the player walking
            if (x != 0 || y != 0)
            {
                animator.SetBool(Convert.ToString(AnimationBools.playerWalk), true);
            }
            else
            {
                animator.SetBool(Convert.ToString(AnimationBools.playerWalk), false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.tag)
            {
                case Configuration.COMPONENT_TAGS_STAIR:
                    {
                        Invoke(nameof(MovePlayerToLinkedStair), 1);
                        break;
                    }
                case Configuration.COMPONENT_TAGS_STAIR_DOWN:
                    {
                        GameManager.instance.playerReady = true;
                        break;
                    }
                case Configuration.COMPONENT_TAGS_EXIT:
                    {
                        // Invoke the method 'LoadNextLevel' with a 1 second delay
                        Invoke(nameof(LoadNextLevel), 1);
                        break;
                    }
            }
        }

        private void MovePlayerToLinkedStair()
        {
            if (!GameManager.instance.playerReady)
            {
                return;
            }

            GameManager.instance.playerReady = false;

            // Get the players current position
            Vector3 currentPos = transform.position;

            Debug.Log("Moving player to next stair");

            int x = Convert.ToInt32(Math.Floor(currentPos.x));
            int y = Convert.ToInt32(Math.Floor(currentPos.y));

            Vector3 tilePosition = new Vector3(x, y, 0f);

            StairRoom currentStair = default;

            // Find the current stair
            foreach (Floor floor in GameManager.instance.currentLevel.floors)
            {
                foreach (StairRoom stair in floor.stairs)
                {
                    if (stair.innerTiles.Contains(tilePosition))
                    {
                        currentStair = stair;
                        break;
                    }
                }
            }

            // if we haven't found the stair the player stepped on then exit
            if (currentStair == null)
            {
                GameManager.instance.playerReady = true;
                return;
            }

            StairRoom nextStair = currentStair.stairPair;
            Vector3 teleportPos = nextStair.innerTiles[Convert.ToInt32(nextStair.innerTiles.Count / 2)];

            Debug.Log("Moving player to stair: " + teleportPos.x + ":" + teleportPos.y);

            transform.position = teleportPos;
        }

        /// <summary>
        /// Load the next level in the game
        /// </summary>
        private void LoadNextLevel()
        {
            // By checking if the player is active, we can stop the level from re-loading multiple times.
            if (!GameManager.instance.playerReady)
            {
                return;
            }

            GameManager.instance.FinishLevel();
        }
    }
}
