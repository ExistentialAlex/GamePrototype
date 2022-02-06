using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.GameGeneration.Rooms
{
    public class BossRoom : Room
    {
        public BossRoom(Vector3 vectorPosition, List<Walls.WallTypes> walls, GameObject template) : base(vectorPosition, walls, template)
        {
            this.type = RoomType.boss;
        }
    }
}
