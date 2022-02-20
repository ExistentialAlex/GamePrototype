namespace Prototype.GameGeneration
{
    using UnityEngine;

    /// <summary>
    /// Script for moving the camera to focus on a specific transform.
    /// </summary>
    public class CameraMotor : MonoBehaviour
    {
        /// <summary>
        /// Camera boundary X axis.
        /// </summary>
        public const float BoundX = 0.15f;

        /// <summary>
        /// Camera boundary Y axis.
        /// </summary>
        public const float BoundY = 0.05f;

        /// <summary>
        /// Gets or sets the transform the camera should look at.
        /// </summary>
        private Transform LookAt { get; set; }

        /// <summary>
        /// Called every frame, makes sure the camera is looking at the player.
        /// </summary>
        private void LateUpdate()
        {
            Vector3 delta = Vector3.zero;

            float deltaX = this.LookAt.position.x - this.transform.position.x;

            // if the change in position of what we're looking at is greater than the bound
            if (deltaX > BoundX || deltaX < -BoundX)
            {
                if (this.transform.position.x < this.LookAt.position.x)
                {
                    delta.x = deltaX - BoundX;
                }
                else
                {
                    delta.x = deltaX + BoundX;
                }
            }

            float deltaY = this.LookAt.position.y - this.transform.position.y;

            if (deltaY > BoundY || deltaY < -BoundY)
            {
                if (this.transform.position.y < this.LookAt.position.y)
                {
                    delta.y = deltaY - BoundY;
                }
                else
                {
                    delta.y = deltaY + BoundY;
                }
            }

            this.transform.position += new Vector3(delta.x, delta.y, 0f);
        }

        /// <summary>
        /// Run when the camera motor script is started, but after Awake.
        /// </summary>
        private void Start()
        {
            this.LookAt = GameManager.Player.transform;
        }
    }
}
