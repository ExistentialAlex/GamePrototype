using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace GameGeneration
{
    public class RoomGenerator : MonoBehaviour
    {
        public int roomWidth = 10;
        public int roomHeight = 10;
        public GameObject wall;
        private Vector3 playerPosition { get; set; }
        private int relativeStartX { get; set; }
        private int relativeStartY { get; set; }
        private int maxX { get; set; }
        private int maxY { get; set; }

        public void SetupRoom(Room room, Transform floor, int floorStartX, int floorStartY)
        {
            relativeStartX = Convert.ToInt32(roomWidth * room.vectorPosition.x) + floorStartX;
            relativeStartY = Convert.ToInt32(roomHeight * room.vectorPosition.y) + floorStartY;
            maxX = relativeStartX + roomWidth - 1;
            maxY = relativeStartY + roomHeight - 1;

            for (int x = relativeStartX; x < relativeStartX + roomWidth; x++)
            {
                for (int y = relativeStartY; y < relativeStartY + roomHeight; y++)
                {
                    Vector3 pos = new Vector3(x, y, 0f);

                    (Instantiate(room.tile, pos, Quaternion.identity) as GameObject).transform.SetParent(floor);
                }
            }

            SetupWalls(room, floor, relativeStartX, relativeStartY);

            if (room.type == Room.CellType.entrance)
            {
                AssignPlayerPosition(relativeStartX + roomWidth / 2, relativeStartY + roomHeight / 2);
                InstantiatePlayer(floor);
            }
        }

        private void SetupWalls(Room room, Transform floor, int x, int y)
        {
            switch (room.wallType)
            {
                case (Walls.WallTypes.top_left_corner):
                    {
                        if (room.type == Room.CellType.room)
                        {
                            AddWall(maxX, y, floor);
                        }

                        AddLeftWall(room, x, y, floor);
                        AddTopWall(room, x, y, floor);
                        break;
                    }
                case (Walls.WallTypes.top_right_corner):
                    {
                        if (room.type == Room.CellType.room)
                        {
                            AddWall(x, y, floor);
                        }

                        AddRightWall(room, x, y, floor);
                        AddTopWall(room, x, y, floor);
                        break;
                    }
                case (Walls.WallTypes.bottom_left_corner):
                    {
                        if (room.type == Room.CellType.room)
                        {
                            AddWall(maxX, maxY, floor);
                        }

                        AddLeftWall(room, x, y, floor);
                        AddBottomWall(room, x, y, floor);
                        break;
                    }
                case (Walls.WallTypes.bottom_right_corner):
                    {
                        if (room.type == Room.CellType.room)
                        {
                            AddWall(x, maxY, floor);
                        }

                        AddRightWall(room, x, y, floor);
                        AddBottomWall(room, x, y, floor);
                        break;
                    }
                case (Walls.WallTypes.horizontal_open):
                    {
                        AddBottomWall(room, x, y, floor);
                        AddTopWall(room, x, y, floor);
                        break;
                    }
                case (Walls.WallTypes.vertical_open):
                    {
                        AddLeftWall(room, x, y, floor);
                        AddRightWall(room, x, y, floor);
                        break;
                    }
                case (Walls.WallTypes.top_open_side):
                    {
                        AddLeftWall(room, x, y, floor);
                        AddBottomWall(room, x, y, floor);
                        AddRightWall(room, x, y, floor);
                        break;
                    }
                case (Walls.WallTypes.right_open_side):
                    {
                        AddLeftWall(room, x, y, floor);
                        AddBottomWall(room, x, y, floor);
                        AddTopWall(room, x, y, floor);
                        break;
                    }
                case (Walls.WallTypes.bottom_open_side):
                    {
                        AddLeftWall(room, x, y, floor);
                        AddTopWall(room, x, y, floor);
                        AddRightWall(room, x, y, floor);
                        break;
                    }
                case (Walls.WallTypes.left_open_side):
                    {
                        AddBottomWall(room, x, y, floor);
                        AddTopWall(room, x, y, floor);
                        AddRightWall(room, x, y, floor);
                        break;
                    }
                case (Walls.WallTypes.all_walls):
                    {
                        AddLeftWall(room, x, y, floor);
                        AddTopWall(room, x, y, floor);
                        AddRightWall(room, x, y, floor);
                        AddBottomWall(room, x, y, floor);
                        break;
                    }
            }
        }

        private void AddLeftWall(Room room, int x, int y, Transform floor)
        {
            if (room.doors.Select(Door => Door.position).Contains(Door.DoorPositions.left))
            {
                Door door = room.doors.Where(door => door.position == Door.DoorPositions.left).First();
                AddVerticalDoor(door, x, y, GetVerticalWallCenter(), floor);
                return;
            }

            for (int i = 0; i < roomHeight; i++)
            {
                AddWall(x, i + y, floor); // left wall
            }
        }

        private void AddRightWall(Room room, int x, int y, Transform floor)
        {
            if (room.doors.Select(Door => Door.position).Contains(Door.DoorPositions.right))
            {
                Door door = room.doors.Where(door => door.position == Door.DoorPositions.right).First();
                AddVerticalDoor(door, maxX, y, GetVerticalWallCenter(), floor);
                return;
            }

            for (int i = 0; i < roomHeight; i++)
            {
                AddWall(maxX, i + y, floor); // right wall
            }
        }

        private void AddTopWall(Room room, int x, int y, Transform floor)
        {
            if (room.doors.Select(Door => Door.position).Contains(Door.DoorPositions.top))
            {
                Door door = room.doors.Where(door => door.position == Door.DoorPositions.top).First();
                AddHorizontalDoor(door, x, maxY, GetHorizontalWallCenter(), floor);
                return;
            }

            for (int i = 0; i < roomWidth; i++)
            {
                AddWall(i + x, maxY, floor); // top wall
            }
        }

        private void AddBottomWall(Room room, int x, int y, Transform floor)
        {
            if (room.doors.Select(Door => Door.position).Contains(Door.DoorPositions.bottom))
            {
                Door door = room.doors.Where(door => door.position == Door.DoorPositions.bottom).First();
                AddHorizontalDoor(door, x, y, GetHorizontalWallCenter(), floor);
                return;
            }

            for (int i = 0; i < roomWidth; i++)
            {
                AddWall(i + x, y, floor); // bottom wall
            }
        }

        private void AddWall(int x, int y, Transform floor)
        {
            (Instantiate(wall, new Vector3(x, y, 0f), Quaternion.identity) as GameObject).transform.SetParent(floor);
        }

        #region Door

        private int GetHorizontalWallCenter()
        {
            return Convert.ToInt32(Rooms.roomWidth / 2);
        }

        private int GetVerticalWallCenter()
        {
            return Convert.ToInt32(Rooms.roomHeight / 2);
        }

        private void AddVerticalDoor(Door door, int x, int y, int wallMiddle, Transform floor)
        {
            int bottomMostPoint = Convert.ToInt32(wallMiddle - door.doorSize / 2);

            for (int i = 0; i < bottomMostPoint; i++)
            {
                AddWall(x, i + y, floor);
            }

            for (int i = bottomMostPoint + door.doorSize; i < roomHeight; i++)
            {
                AddWall(x, i + y, floor);
            }
        }

        private void AddHorizontalDoor(Door door, int x, int y, int wallMiddle, Transform floor)
        {
            int leftMostPoint = Convert.ToInt32(wallMiddle - door.doorSize / 2);

            for (int i = 0; i < leftMostPoint; i++)
            {
                AddWall(x + i, y, floor);
            }

            for (int i = leftMostPoint + door.doorSize; i < roomWidth; i++)
            {
                AddWall(x + i, y, floor);
            }
        }

        #endregion Door

        public void AssignPlayerPosition(int x, int y)
        {
            playerPosition = new Vector3(x, y, 0f);
        }

        public void InstantiatePlayer(Transform floor)
        {
            GameManager.player.transform.position = playerPosition;
        }
    }
}
