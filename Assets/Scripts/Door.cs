using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rand = UnityEngine.Random;
using System;

namespace GameGeneration
{
    public class Door : MonoBehaviour
    {
        public int defaultDoorSize = 3;

        public DoorPositions position { get; set; }
        public int doorSize { get; set; }

        public enum DoorPositions
        {
            top,
            left,
            right,
            bottom
        }

        public Door(DoorPositions position, int doorSize = 0)
        {
            this.position = position;

            if (doorSize == 0)
            {
                this.doorSize = defaultDoorSize;
            }
            else
            {
                this.doorSize = doorSize;
            }
        }

        public static void AddDoor(Cell[,] rooms, int roomX, int roomY, int maxX, int maxY)
        {
            Cell room = rooms[roomX, roomY];
            int doorToUse = rand.Range(0, 3);

            if (!Enum.TryParse(Convert.ToString(doorToUse), out DoorPositions position))
            {
                Debug.Log("Could not get door position from random number");
            }

            switch (position)
            {
                case DoorPositions.top:
                    {
                        if (room.vectorPosition.y == maxY)
                        {
                            AddDoor(rooms, roomX, roomY, maxX, maxY);
                            break;
                        }

                        Door topDoor = new Door(DoorPositions.top);
                        room.doors.Add(topDoor);

                        Door bottomDoor = new Door(DoorPositions.bottom);
                        rooms[roomX, roomY + 1].doors.Add(bottomDoor);
                        break;
                    }
                case DoorPositions.left:
                    {
                        if (room.vectorPosition.x == 0)
                        {
                            AddDoor(rooms, roomX, roomY, maxX, maxY);
                            break;
                        }

                        Door leftDoor = new Door(DoorPositions.left);
                        room.doors.Add(leftDoor);

                        Door rightDoor = new Door(DoorPositions.right);
                        rooms[roomX - 1, roomY].doors.Add(rightDoor);
                        break;
                    }
                case DoorPositions.right:
                    {
                        if (room.vectorPosition.x == maxX)
                        {
                            AddDoor(rooms, roomX, roomY, maxX, maxY);
                            break;
                        }

                        Door rightDoor = new Door(DoorPositions.right);
                        room.doors.Add(rightDoor);

                        Door leftDoor = new Door(DoorPositions.left);
                        rooms[roomX + 1, roomY].doors.Add(leftDoor);
                        break;
                    }
                case DoorPositions.bottom:
                    {
                        if (room.vectorPosition.y == 0)
                        {
                            AddDoor(rooms, roomX, roomY, maxX, maxY);
                            break;
                        }

                        Door bottomDoor = new Door(DoorPositions.bottom);
                        room.doors.Add(bottomDoor);

                        Door topDoor = new Door(DoorPositions.top);
                        rooms[roomX, roomY - 1].doors.Add(topDoor);
                        break;
                    }
            }
        }
    }
}
