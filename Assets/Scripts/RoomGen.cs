using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameGeneration
{
    public class RoomGen : MonoBehaviour
    {
        public int roomWidth = 10;
        public int roomHeight = 10;
        public GameObject player;
        public GameObject wall;
        private Vector3 playerPosition { get; set; }
        private int relativeStartX { get; set; }
        private int relativeStartY { get; set; }
        private int maxX { get; set; }
        private int maxY { get; set; }

        public void SetupRoom(Cell room, Transform floor)
        {
            relativeStartX = Convert.ToInt32(roomWidth * room.vectorPosition.x);
            relativeStartY = Convert.ToInt32(roomHeight * room.vectorPosition.y);
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

            if (room.type == Cell.CellType.entrance)
            {
                AssignPlayerPosition(relativeStartX + roomWidth / 2, relativeStartY + roomHeight / 2);
                InstantiatePlayer(floor);
            }
        }

        private void SetupWalls(Cell room, Transform floor, int x, int y)
        {
            switch (room.wallType)
            {
                case (Walls.WallTypes.top_left_corner):
                    {
                        if (room.type == Cell.CellType.room)
                        {
                            AddWall(maxX, y, floor);
                        }

                        AddLeftWall(room, x, y, floor);
                        AddTopWall(room, x, y, floor);
                        break;
                    }
                case (Walls.WallTypes.top_right_corner):
                    {
                        if (room.type == Cell.CellType.room)
                        {
                            AddWall(x, y, floor);
                        }

                        AddRightWall(room, x, y, floor);
                        AddTopWall(room, x, y, floor);
                        break;
                    }
                case (Walls.WallTypes.bottom_left_corner):
                    {
                        if (room.type == Cell.CellType.room)
                        {
                            AddWall(maxX, maxY, floor);
                        }

                        AddLeftWall(room, x, y, floor);
                        AddBottomWall(room, x, y, floor);
                        break;
                    }
                case (Walls.WallTypes.bottom_right_corner):
                    {
                        if (room.type == Cell.CellType.room)
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

        private int GetHorizontalWallCenter()
        {
            return Convert.ToInt32(Rooms.roomHeight / 2);
        }

        private int GetVerticalWallCenter()
        {
            return Convert.ToInt32(Rooms.roomWidth / 2);
        }

        private void AddVerticalDoor(int x, int y, int wallMiddle, Transform floor)
        {
            int bottomMostPoint = Convert.ToInt32(wallMiddle - Doors.doorSize / 2);

            for (int i = 0; i < bottomMostPoint; i++)
            {
                AddWall(x, i + y, floor);
            }

            for (int i = bottomMostPoint + Doors.doorSize + 1; i < roomHeight; i++)
            {
                AddWall(x, i + y, floor);
            }
        }

        private void AddHorizontalDoor(int x, int y, int wallMiddle, Transform floor)
        {
            int leftMostPoint = Convert.ToInt32(wallMiddle - Doors.doorSize / 2);

            for (int i = 0; i < leftMostPoint; i++)
            {
                AddWall(x, i + y, floor);
            }

            for (int i = leftMostPoint + Doors.doorSize + 1; i < roomWidth; i++)
            {
                AddWall(x + 1, y, floor);
            }
        }

        private void AddLeftWall(Cell room, int x, int y, Transform floor)
        {
            if (room.doors.Contains(Doors.DoorPositions.left))
            {
                AddVerticalDoor(x, y, GetVerticalWallCenter(), floor);
                return;
            }

            for (int i = 0; i < roomHeight; i++)
            {
                AddWall(x, i + y, floor); // left wall
            }
        }

        private void AddRightWall(Cell room, int x, int y, Transform floor)
        {
            if (room.doors.Contains(Doors.DoorPositions.right))
            {
                AddVerticalDoor(x, y, GetVerticalWallCenter(), floor);
                return;
            }

            for (int i = 0; i < roomHeight; i++)
            {
                AddWall(maxX, i + y, floor); // right wall
            }
        }

        private void AddTopWall(Cell room, int x, int y, Transform floor)
        {
            if (room.doors.Contains(Doors.DoorPositions.top))
            {
                AddHorizontalDoor(x, y, GetHorizontalWallCenter(), floor);
                return;
            }

            for (int i = 0; i < roomWidth; i++)
            {
                AddWall(i + x, maxY, floor); // top wall
            }
        }

        private void AddBottomWall(Cell room, int x, int y, Transform floor)
        {
            if (room.doors.Contains(Doors.DoorPositions.bottom))
            {
                AddHorizontalDoor(x, y, GetHorizontalWallCenter(), floor);
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

        public void AssignPlayerPosition(int x, int y)
        {
            playerPosition = new Vector3(x, y, 0f);
        }

        public void InstantiatePlayer(Transform floor)
        {
            player.transform.position = playerPosition;
        }
    }
}
