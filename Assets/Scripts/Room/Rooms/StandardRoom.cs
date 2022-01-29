using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameGeneration.Rooms
{
    public class StandardRoom : Room
    {
        public StandardRoom(Vector3 vectorPosition, List<Walls.WallTypes> walls, GameObject[,] template) : base(vectorPosition, walls, template)
        {
            this.type = RoomType.room;
        }

        public static string[,] standard_template = new string[roomWidth, roomHeight]{{ "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },};

        public static string[,] standard_template2 = new string[roomWidth, roomHeight]{{ "f2","f2","f2","f2","f2","f2","f2","f2","f2","f2","f2", },
                                                                                   { "f2","f2","f2","f2","f2","f2","f2","f2","f2","f2","f2", },
                                                                                   { "f2","f2","f2","f2","f2","f2","f2","f2","f2","f2","f2", },
                                                                                   { "f2","f2","f2","f2","f2","f2","f2","f2","f2","f2","f2", },
                                                                                   { "f2","f2","f2","f2","f2","f2","f2","f2","f2","f2","f2", },
                                                                                   { "f2","f2","f2","f2","f2","f2","f2","f2","f2","f2","f2", },
                                                                                   { "f2","f2","f2","f2","f2","f2","f2","f2","f2","f2","f2", },
                                                                                   { "f2","f2","f2","f2","f2","f2","f2","f2","f2","f2","f2", },
                                                                                   { "f2","f2","f2","f2","f2","f2","f2","f2","f2","f2","f2", },
                                                                                   { "f2","f2","f2","f2","f2","f2","f2","f2","f2","f2","f2", },
                                                                                   { "f2","f2","f2","f2","f2","f2","f2","f2","f2","f2","f2", },};

        public static string[,] standard_template3 = new string[roomWidth, roomHeight]{{ "f3","f3","f3","f3","f3","f3","f3","f3","f3","f3","f3", },
                                                                                   { "f3","f3","f3","f3","f3","f3","f3","f3","f3","f3","f3", },
                                                                                   { "f3","f3","f3","f3","f3","f3","f3","f3","f3","f3","f3", },
                                                                                   { "f3","f3","f3","f3","f3","f3","f3","f3","f3","f3","f3", },
                                                                                   { "f3","f3","f3","f3","f3","f3","f3","f3","f3","f3","f3", },
                                                                                   { "f3","f3","f3","f3","f3","f3","f3","f3","f3","f3","f3", },
                                                                                   { "f3","f3","f3","f3","f3","f3","f3","f3","f3","f3","f3", },
                                                                                   { "f3","f3","f3","f3","f3","f3","f3","f3","f3","f3","f3", },
                                                                                   { "f3","f3","f3","f3","f3","f3","f3","f3","f3","f3","f3", },
                                                                                   { "f3","f3","f3","f3","f3","f3","f3","f3","f3","f3","f3", },
                                                                                   { "f3","f3","f3","f3","f3","f3","f3","f3","f3","f3","f3", },};
    }
}
