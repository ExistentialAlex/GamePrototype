using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameGeneration.Rooms
{
    public class EntranceRoom : Room
    {
        public EntranceRoom(Vector3 vectorPosition, List<Walls.WallTypes> walls, GameObject[,] template) : base(vectorPosition, walls, template)
        {
            this.type = RoomType.entrance;
        }

        public static string[,] entrance_template = new string[roomWidth, roomHeight]{{ "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","e","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },};
    }
}
