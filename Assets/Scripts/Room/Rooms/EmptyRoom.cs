using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameGeneration.Rooms
{
    public class EmptyRoom : Room
    {
        public EmptyRoom(Vector3 vectorPosition, List<Walls.WallTypes> walls, GameObject[,] template) : base(vectorPosition, walls, template)
        {
            this.type = RoomType.empty;
        }

        public static string[,] empty_template = new string[roomWidth, roomHeight]{{ "em","em","em","em","em","em","em","em","em","em","em", },
                                                                                   { "em","em","em","em","em","em","em","em","em","em","em", },
                                                                                   { "em","em","em","em","em","em","em","em","em","em","em", },
                                                                                   { "em","em","em","em","em","em","em","em","em","em","em", },
                                                                                   { "em","em","em","em","em","em","em","em","em","em","em", },
                                                                                   { "em","em","em","em","em","em","em","em","em","em","em", },
                                                                                   { "em","em","em","em","em","em","em","em","em","em","em", },
                                                                                   { "em","em","em","em","em","em","em","em","em","em","em", },
                                                                                   { "em","em","em","em","em","em","em","em","em","em","em", },
                                                                                   { "em","em","em","em","em","em","em","em","em","em","em", },
                                                                                   { "em","em","em","em","em","em","em","em","em","em","em", },};
    }
}
