using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameGeneration.Rooms
{
    public class BossRoom : Room
    {
        public BossRoom(Vector3 vectorPosition, List<Walls.WallTypes> walls, GameObject[,] template) : base(vectorPosition, walls, template)
        {
            this.type = RoomType.boss;
        }

        public static string[,] bossBottomLeft = new string[roomWidth, roomHeight]{{ "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","b_bl", },};

        public static string[,] bossBottomRight = new string[roomWidth, roomHeight]{{ "f","f","f","f","f","f","f","f","f","f","b_br", },
                                                                                   { "f","f","f","f","f","f","f","f","f","ex","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },};

        public static string[,] bossTopLeft = new string[roomWidth, roomHeight]{{ "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "b_tl","f","f","f","f","f","f","f","f","f","f", },};

        public static string[,] bossTopRight = new string[roomWidth, roomHeight]{{ "b_tr","f","f","f","f","f","f","f","f","f","f", },
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
    }
}
