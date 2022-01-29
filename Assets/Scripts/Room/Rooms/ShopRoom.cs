using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameGeneration.Rooms
{
    public class ShopRoom : Room
    {
        public ShopRoom(Vector3 vectorPosition, List<Walls.WallTypes> walls, GameObject[,] template) : base(vectorPosition, walls, template)
        {
            this.type = RoomType.shop;
        }

        public static string[,] shop_template = new string[roomWidth, roomHeight]{{ "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","sh","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },};
    }
}
