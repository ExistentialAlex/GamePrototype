using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameGeneration.Rooms
{
    public class ShopRoom : Room
    {
        public ShopRoom(Vector3 vectorPosition, List<Walls.WallTypes> walls, GameObject tile) : base(vectorPosition, walls, tile)
        {
            this.type = RoomType.shop;
        }
    }
}
