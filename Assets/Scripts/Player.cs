using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameGeneration;
using System;

namespace GameGeneration.Player
{
    public class Player : MonoBehaviour
    {
        private BoxCollider2D boxCollider;
        private Vector3 moveDelta;
        private RaycastHit2D hit;
        public float playerSpeed = 1;

        private void Start()
        {
            boxCollider = GetComponent<BoxCollider2D>();
        }

        private void FixedUpdate()
        {
            moveDelta = Vector3.zero;

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
                                    Mathf.Abs(moveDelta.y * Time.deltaTime),
                                    LayerMask.GetMask(Convert.ToString(Configuration.SortingLayers.Units), Convert.ToString(Configuration.SortingLayers.Walls)));

            if (hit.collider == null)
            {
                // Move
                transform.Translate(0, moveDelta.y * Time.deltaTime * playerSpeed, 0f);
            }

            // Make sure we can move in the X direction
            hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0,
                                    new Vector2(moveDelta.x, 0),
                                    Mathf.Abs(moveDelta.x * Time.deltaTime),
                                    LayerMask.GetMask(Convert.ToString(Configuration.SortingLayers.Units), Convert.ToString(Configuration.SortingLayers.Walls)));

            if (hit.collider == null)
            {
                // Move
                transform.Translate(moveDelta.x * Time.deltaTime * playerSpeed, 0, 0f);
            }
        }
    }
}
