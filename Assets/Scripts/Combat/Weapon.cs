using Prototype.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : Collidable
{
    [SerializeField]
    private int baseDamage;

    public int Damage
    {
        get
        {
            return Convert.ToInt32(this.baseDamage * this.Multipliers.Aggregate((a, b) => a * b));
        }
    }

    public List<float> Multipliers { get; private set; }

    protected override void OnCollide(Collider2D collider)
    {
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        // Initialise with 1 here as we need at least one element to aggregate.
        this.Multipliers = new List<float> { 1 };
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
    }
}
