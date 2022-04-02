using Prototype.Common;
using System;
using UnityEngine;

namespace Prototype.Collectables
{
    public class Collectable : Collidable
    {
        protected bool Collected { get; set; }

        protected virtual void OnCollect()
        {
            this.Collected = true;
        }

        protected override void OnCollide(Collider2D collider)
        {
            base.OnCollide(collider);
            if (collider.name == Convert.ToString(Configuration.Collidables.Player))
            {
                this.OnCollect();
            }
        }

        protected override void Start()
        {
            base.Start();
            this.Collected = false;
        }
    }
}
