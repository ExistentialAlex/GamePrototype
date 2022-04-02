using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.Collectables
{
    public class Soda : HealthCollectable
    {
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            this.HealthValue = 5;
        }
    }
}
