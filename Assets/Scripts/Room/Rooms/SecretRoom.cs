using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.GameGeneration.Rooms
{
    public class SecretRoom : Room
    {
        public SecretRoom(Vector3 vectorPosition, List<Walls.WallTypes> walls, GameObject template) : base(vectorPosition, walls, template)
        {
            this.type = RoomType.secret;
        }
    }
}
