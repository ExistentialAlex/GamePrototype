using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameGeneration.Rooms
{
    public class StairRoom : Room
    {
        /// <summary>
        /// The other stair room linked to this one.
        /// </summary>
        public StairRoom stairPair { get; set; }

        public StairRoom(Vector3 vectorPosition, List<Walls.WallTypes> walls, GameObject[,] template) : base(vectorPosition, walls, template)
        {
            this.type = RoomType.stair;
        }

        public static string[,] stair = new string[roomWidth, roomHeight]{{ "f","f","f","f","f","f","f","f","f","f","f", },
                                                                          { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                          { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                          { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                          { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                          { "f","f","f","f","f","st","f","f","f","f","f", },
                                                                          { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                          { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                          { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                          { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                          { "f","f","f","f","f","f","f","f","f","f","f", },};

        public static string[,] stair_down = new string[roomWidth, roomHeight]{{ "f","f","f","f","f","f","f","f","f","f","f", },
                                                                               { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                               { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                               { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                               { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                               { "f","f","f","f","f","std","f","f","f","f","f", },
                                                                               { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                               { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                               { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                               { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                               { "f","f","f","f","f","f","f","f","f","f","f", },};
    }
}
