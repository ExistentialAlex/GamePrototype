using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rand = UnityEngine.Random;
using System;

namespace GameGeneration
{
    public class Door
    {
        public DoorPositions position { get; set; }
        public int doorSize { get; set; }

        public enum DoorPositions
        {
            top,
            left,
            right,
            bottom
        }

        public Door(DoorConfig config, DoorPositions position, int doorSize = 0)
        {
            this.position = position;

            if (doorSize == 0)
            {
                this.doorSize = config.defaultDoorSize;
            }
            else
            {
                this.doorSize = doorSize;
            }
        }

        public static void AddDoor(DoorConfig config, Room[,] rooms, int roomX, int roomY, int maxX, int maxY, int retries = 0)
        {
            Room room = rooms[roomX, roomY];
            int doorToUse = rand.Range(0, 3);

            if (!Enum.TryParse(Convert.ToString(doorToUse), out DoorPositions position))
            {
                Debug.Log("Could not get door position from random number");
            }

            if (retries == 3)
            {
                return;
            }

            switch (position)
            {
                case DoorPositions.top:
                    {
                        if (CheckTopDoorConstraints(room, rooms, roomX, roomY, maxX, maxY))
                        {
                            AddDoor(config, rooms, roomX, roomY, maxX, maxY, retries + 1);
                            break;
                        }

                        Door topDoor = new Door(config, DoorPositions.top);
                        room.doors.Add(topDoor);

                        Door bottomDoor = new Door(config, DoorPositions.bottom);
                        rooms[roomX, roomY + 1].doors.Add(bottomDoor);
                        break;
                    }
                case DoorPositions.left:
                    {
                        if (CheckLeftDoorConstraints(room, rooms, roomX, roomY, maxX, maxY))
                        {
                            AddDoor(config, rooms, roomX, roomY, maxX, maxY, retries + 1);
                            break;
                        }

                        Door leftDoor = new Door(config, DoorPositions.left);
                        room.doors.Add(leftDoor);

                        Door rightDoor = new Door(config, DoorPositions.right);
                        rooms[roomX - 1, roomY].doors.Add(rightDoor);
                        break;
                    }
                case DoorPositions.right:
                    {
                        if (CheckRightDoorConstraints(room, rooms, roomX, roomY, maxX, maxY))
                        {
                            AddDoor(config, rooms, roomX, roomY, maxX, maxY, retries + 1);
                            break;
                        }

                        Door rightDoor = new Door(config, DoorPositions.right);
                        room.doors.Add(rightDoor);

                        Door leftDoor = new Door(config, DoorPositions.left);
                        rooms[roomX + 1, roomY].doors.Add(leftDoor);
                        break;
                    }
                case DoorPositions.bottom:
                    {
                        if (CheckBottomDoorConstraints(room, rooms, roomX, roomY, maxX, maxY))
                        {
                            AddDoor(config, rooms, roomX, roomY, maxX, maxY, retries + 1);
                            break;
                        }

                        Door bottomDoor = new Door(config, DoorPositions.bottom);
                        room.doors.Add(bottomDoor);

                        Door topDoor = new Door(config, DoorPositions.top);
                        rooms[roomX, roomY - 1].doors.Add(topDoor);
                        break;
                    }
            }
        }

        public static bool CheckGenericConstraints(Room room, Room adjacentRoom)
        {
            return adjacentRoom.type == Room.CellType.empty ||
                   (room.type == Room.CellType.entrance && adjacentRoom.type == Room.CellType.boss) ||
                   (room.type == Room.CellType.boss && adjacentRoom.type == Room.CellType.entrance);
        }

        public static bool CheckTopDoorConstraints(Room room, Room[,] rooms, int roomX, int roomY, int maxX, int maxY)
        {
            return room.vectorPosition.y == maxY || CheckGenericConstraints(room, rooms[roomX, roomY + 1]);
        }

        public static bool CheckLeftDoorConstraints(Room room, Room[,] rooms, int roomX, int roomY, int maxX, int maxY)
        {
            return room.vectorPosition.x == 0 || CheckGenericConstraints(room, rooms[roomX - 1, roomY]);
        }

        public static bool CheckRightDoorConstraints(Room room, Room[,] rooms, int roomX, int roomY, int maxX, int maxY)
        {
            return room.vectorPosition.x == maxX || CheckGenericConstraints(room, rooms[roomX + 1, roomY]);
        }

        public static bool CheckBottomDoorConstraints(Room room, Room[,] rooms, int roomX, int roomY, int maxX, int maxY)
        {
            return room.vectorPosition.y == 0 || CheckGenericConstraints(room, rooms[roomX, roomY - 1]);
        }
    }
}
