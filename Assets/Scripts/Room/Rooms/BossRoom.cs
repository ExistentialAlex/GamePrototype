using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameGeneration.Rooms
{
    public class BossRoom : Room
    {
        public BossRoom(Vector3 vectorPosition, List<Walls.WallTypes> walls, GameObject tile) : base(vectorPosition, walls, tile)
        {
            this.type = RoomType.boss;
        }
    }
}