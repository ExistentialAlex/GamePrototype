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

        private void Start()
        {
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
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.tag)
            {
                case Configuration.COMPONENT_TAGS_STAIR:
                    {
                        StartCoroutine(MovePlayerToLinkedStair(collision.gameObject, 5));
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

        private IEnumerator MovePlayerToLinkedStair(GameObject tileHit, float delay)
        {
            // Delay for a number of seconds
            yield return new WaitForSeconds(delay);

            Vector3 tilePosition = tileHit.transform.position;

            //StairRoom currentStair = GameManager.instance.currentLevel.floors.Where(floor => floor.stairs.Where(stair => stair.vectorPosition))
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
