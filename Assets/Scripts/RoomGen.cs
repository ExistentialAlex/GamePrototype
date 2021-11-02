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
                        AddLeftWall(x, y, floor);
                        AddTopWall(x, y, floor);

                        if (room.type == Cell.CellType.room)
                        {
                            AddWall(maxX, y, floor);
                        }
                        break;
                    }
                case (Walls.WallTypes.top_right_corner):
                    {
                        AddRightWall(x, y, floor);
                        AddTopWall(x, y, floor);

                        if (room.type == Cell.CellType.room)
                        {
                            AddWall(x, y, floor);
                        }
                        break;
                    }
                case (Walls.WallTypes.bottom_left_corner):
                    {
                        AddLeftWall(x, y, floor);
                        AddBottomWall(x, y, floor);

                        if (room.type == Cell.CellType.room)
                        {
                            AddWall(maxX, maxY, floor);
                        }
                        break;
                    }
                case (Walls.WallTypes.bottom_right_corner):
                    {
                        AddRightWall(x, y, floor);
                        AddBottomWall(x, y, floor);

                        if (room.type == Cell.CellType.room)
                        {
                            AddWall(x, maxY, floor);
                        }
                        break;
                    }
                case (Walls.WallTypes.horizontal_open):
                    {
                        AddBottomWall(x, y, floor);
                        AddTopWall(x, y, floor);
                        break;
                    }
                case (Walls.WallTypes.vertical_open):
                    {
                        AddLeftWall(x, y, floor);
                        AddRightWall(x, y, floor);
                        break;
                    }
                case (Walls.WallTypes.top_open_side):
                    {
                        AddLeftWall(x, y, floor);
                        AddBottomWall(x, y, floor);
                        AddRightWall(x, y, floor);
                        break;
                    }
                case (Walls.WallTypes.right_open_side):
                    {
                        AddLeftWall(x, y, floor);
                        AddBottomWall(x, y, floor);
                        AddTopWall(x, y, floor);
                        break;
                    }
                case (Walls.WallTypes.bottom_open_side):
                    {
                        AddLeftWall(x, y, floor);
                        AddTopWall(x, y, floor);
                        AddRightWall(x, y, floor);
                        break;
                    }
                case (Walls.WallTypes.left_open_side):
                    {
                        AddBottomWall(x, y, floor);
                        AddTopWall(x, y, floor);
                        AddRightWall(x, y, floor);
                        break;
                    }
                case (Walls.WallTypes.all_walls):
                    {
                        AddLeftWall(x, y, floor);
                        AddTopWall(x, y, floor);
                        AddRightWall(x, y, floor);
                        AddBottomWall(x, y, floor);
                        break;
                    }
            }
        }

        private void AddLeftWall(int x, int y, Transform floor)
        {
            for (int i = 0; i < roomHeight; i++)
            {
                AddWall(x, i + y, floor); // left wall
            }
        }

        private void AddRightWall(int x, int y, Transform floor)
        {
            for (int i = 0; i < roomHeight; i++)
            {
                AddWall(maxX, i + y, floor); // right wall
            }
        }

        private void AddTopWall(int x, int y, Transform floor)
        {
            for (int i = 0; i < roomWidth; i++)
            {
                AddWall(i + x, maxY, floor); // top wall
            }
        }

        private void AddBottomWall(int x, int y, Transform floor)
        {
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
