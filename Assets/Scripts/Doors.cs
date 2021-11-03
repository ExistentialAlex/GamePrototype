using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rand = UnityEngine.Random;

namespace GameGeneration
{
    public class Doors
    {
        public static int doorSize = 3;

        public enum DoorPositions
        {
            top,
            left,
            right,
            bottom
        }

        public static void AddDoor(Cell[,] rooms, int roomX, int roomY, int maxX, int maxY)
        {
            Cell room = rooms[roomX, roomY];
            int doorToUse = rand.Range(0, 3);

            switch (doorToUse)
            {
                case 0:
                    {
                        if (room.vectorPosition.y == maxY)
                        {
                            break;
                        }

                        room.doors.Add(DoorPositions.top);
                        rooms[roomX, roomY + 1].doors.Add(DoorPositions.bottom);
                        break;
                    }
                case 1:
                    {
                        if (room.vectorPosition.x == 0)
                        {
                            break;
                        }

                        room.doors.Add(DoorPositions.left);
                        rooms[roomX - 1, roomY].doors.Add(DoorPositions.right);
                        break;
                    }
                case 2:
                    {
                        if (room.vectorPosition.x == maxX)
                        {
                            break;
                        }

                        room.doors.Add(DoorPositions.right);
                        rooms[roomX + 1, roomY].doors.Add(DoorPositions.left);
                        break;
                    }
                case 3:
                    {
                        if (room.vectorPosition.y == 0)
                        {
                            break;
                        }

                        room.doors.Add(DoorPositions.bottom);
                        rooms[roomX, roomY - 1].doors.Add(DoorPositions.top);
                        break;
                    }
            }
        }
    }
}
