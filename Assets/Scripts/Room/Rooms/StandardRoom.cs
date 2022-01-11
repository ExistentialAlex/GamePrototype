using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameGeneration.Rooms
{
    public class StandardRoom : Room
    {
        public StandardRoom(Vector3 vectorPosition, List<Walls.WallTypes> walls, GameObject tile) : base(vectorPosition, walls, tile)
        {
            this.type = RoomType.room;
        }
    }
}
