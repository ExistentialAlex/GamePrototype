namespace Prototype.Units
{
    using System;
    using Prototype.Common;
    using Prototype.Utilities;
    using Prototype.GameGeneration;
    using Prototype.GameGeneration.Rooms;
    using UnityEngine;
    using System.Collections;

    /// <summary>
    /// Script controlling the behaviors of the player sprite.
    /// </summary>
    public class Player : Unit
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

        public int Experience { get; set; }
        public int ExpToNextLevel { get; set; }
        public int Level { get; set; }
        public int Money { get; set; }


        private float MoveX { get; set; }
        private float MoveY { get; set; }


        public string SaveState()
        {
            return Utilities.CreateSaveString(Convert.ToString(this.Level), Convert.ToString(this.Experience), Convert.ToString(this.Money));
        }

        protected override void Start()
        {
            base.Start();
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

        protected override void Update()
        {
            base.Update();

            this.MoveX = Input.GetAxisRaw(Convert.ToString(Configuration.InputAxes.Horizontal));
            this.MoveY = Input.GetAxisRaw(Convert.ToString(Configuration.InputAxes.Vertical));
        }

        /// <summary>
        /// Takes user input on the player, calculates if the player can move in that direction and
        /// moves sprite.
        /// </summary>
        private void FixedUpdate()
        {

            this.UpdateMotor(new Vector3(this.MoveX, this.MoveY, 0f));
        }
    }
}
