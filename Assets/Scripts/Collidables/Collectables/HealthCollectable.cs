using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.Collectables
{
    public class HealthCollectable : Collectable
    {
        protected int HealthValue { get; set; }

        protected override void OnCollect()
        {
            if (this.Collected)
            {
                return;
            }

            base.OnCollect();
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            this.HealthValue = 0;
        }
    }
}
