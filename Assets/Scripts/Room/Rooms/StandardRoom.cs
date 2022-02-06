using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.GameGeneration.Rooms
{
    public class StandardRoom : Room
    {
        public StandardRoom(Vector3 vectorPosition, List<Walls.WallTypes> walls, GameObject template) : base(vectorPosition, walls, template)
        {
            this.type = RoomType.room;
        }
    }
}
