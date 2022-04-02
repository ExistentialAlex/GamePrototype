using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.Units
{
    public class Unit : MovingObject
    {
        protected int Health { get; set; }

        protected override void MoveAnimation(Vector3 movement)
        {
        }

        protected void OnDeath()
        {
        }

        /// <inheritdoc/>
        protected override void Start()
        {
            base.Start();
        }
    }
}
