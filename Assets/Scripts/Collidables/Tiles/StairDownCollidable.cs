using Prototype.Common;
using Prototype.GameGeneration;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.Collidables.Tiles
{
    public class StairDownCollidable : Collidable
    {
        protected override void OnCollide(Collider2D collider)
        {
            // When we enter the stair down set the player state to true.
            if (IsCollidingWithPlayer(collider))
            {
                DungeonGenerator.Instance.PlayerReady = true;
            }
        }
    }
}