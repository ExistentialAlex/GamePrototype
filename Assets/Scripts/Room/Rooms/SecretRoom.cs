using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameGeneration.Rooms
{
    public class SecretRoom : Room
    {
        public SecretRoom(Vector3 vectorPosition, List<Walls.WallTypes> walls, GameObject[,] template) : base(vectorPosition, walls, template)
        {
            this.type = RoomType.secret;
        }

        public static string[,] secret_template = new string[roomWidth, roomHeight]{{ "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","se","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },
                                                                                   { "f","f","f","f","f","f","f","f","f","f","f", },};
    }
}
