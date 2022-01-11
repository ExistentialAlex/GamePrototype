using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameGeneration.Rooms
{
    public class SecretRoom : Room
    {
        public SecretRoom(Vector3 vectorPosition, List<Walls.WallTypes> walls, GameObject tile) : base(vectorPosition, walls, tile)
        {
            this.type = RoomType.secret;
        }
    }
}
