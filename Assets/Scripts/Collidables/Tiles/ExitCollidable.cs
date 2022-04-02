using Prototype.Common;
using Prototype.GameGeneration;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.Collidables.Tiles
{

    public class ExitCollidable : Collidable
    {
        protected override void OnCollide(Collider2D collider)
        {
            if (IsCollidingWithPlayer(collider))
            {
                DungeonGenerator.Instance.LoadNextLevel();
            }
        }
    }
}