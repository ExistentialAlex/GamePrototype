using System;
using UnityEngine;

namespace Prototype.Common
{
    public class Collidable : MonoBehaviour
    {
        public ContactFilter2D Filter { get; set; }

        protected BoxCollider2D BoxCollider { get; set; }

        private Collider2D[] Hits { get; set; }

        protected virtual void OnCollide(Collider2D collider)
        {
            Debug.Log("OnCollider not implemented for " + collider.name);
        }

        protected virtual void Start()
        {
            this.BoxCollider = this.GetComponent<BoxCollider2D>();
            this.Hits = new Collider2D[10];
        }

        protected virtual void Update()
        {
            this.BoxCollider.OverlapCollider(this.Filter, this.Hits);
            for (int i = 0; i < this.Hits.Length; i++)
            {
                if (this.Hits[i] == null)
                {
                    continue;
                }

                this.OnCollide(this.Hits[i]);

                // Cleanup the array.
                this.Hits[i] = null;
            }
        }

        protected bool IsCollidingWithPlayer(Collider2D collider)
        {
            return collider.name == Convert.ToString(Configuration.Collidables.Player);
        }
    }
}
