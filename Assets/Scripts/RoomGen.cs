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
        private Vector3 playerPosition { get; set; }

        private int relativeStartX { get; set; }
        private int relativeStartY { get; set; }

        public GameObject wall;

        public void SetupRoom(Cell room, Transform floor, GameObject[] wallTiles)
        {
            relativeStartX = Convert.ToInt32(roomWidth * room.vectorPosition.x);
            relativeStartY = Convert.ToInt32(roomHeight * room.vectorPosition.y);

            for (int x = relativeStartX; x < relativeStartX + roomWidth; x++)
            {
                for (int y = relativeStartY; y < relativeStartY + roomHeight; y++)
                {
                    Vector3 pos = new Vector3(x, y, 0f);

                    (Instantiate(room.tile, pos, Quaternion.identity) as GameObject).transform.SetParent(floor);
                }
            }

            SetupWalls(room, floor, wallTiles, relativeStartX, relativeStartY);
            if (room.type == Cell.CellType.entrance)
            {
                AssignPlayerPosition(relativeStartX + roomWidth / 2, relativeStartY + roomHeight / 2);
                InstantiatePlayer(floor);
            }
        }

        private void SetupWalls(Cell room, Transform floor, GameObject[] wallTiles, int x, int y)
        {
            switch (room.wallType)
            {
                case (Walls.WallTypes.top_left_corner):
                    {
                        for (int i = 0; i < roomWidth; i++)
                        {
                            (Instantiate(wall, new Vector3(x, i + y, 0f), Quaternion.identity) as GameObject).transform.SetParent(floor); // left wall
                            (Instantiate(wall, new Vector3(i + x, y + roomHeight - 1, 0f), Quaternion.identity) as GameObject).transform.SetParent(floor); // top wall
                        }
                    }
                    break;
                case (Walls.WallTypes.top_right_corner):
                    {
                        for (int i = 0; i < roomWidth; i++)
                        {
                            (Instantiate(wall, new Vector3(x + roomWidth - 1, i + y, 0f), Quaternion.identity) as GameObject).transform.SetParent(floor); // right wall
                            (Instantiate(wall, new Vector3(i + x, y + roomHeight - 1, 0f), Quaternion.identity) as GameObject).transform.SetParent(floor); // top wall
                        }
                        break;
                    }
                case (Walls.WallTypes.bottom_left_corner):
                    {
                        for (int i = 0; i < roomWidth; i++)
                        {
                            (Instantiate(wall, new Vector3(x, i + y, 0f), Quaternion.identity) as GameObject).transform.SetParent(floor); // left wall
                            (Instantiate(wall, new Vector3(i + x, y, 0f), Quaternion.identity) as GameObject).transform.SetParent(floor); // bottom wall
                        }
                        break;
                    }
                case (Walls.WallTypes.bottom_right_corner):
                    {
                        for (int i = 0; i < roomWidth; i++)
                        {
                            (Instantiate(wall, new Vector3(x + roomWidth - 1, i + y, 0f), Quaternion.identity) as GameObject).transform.SetParent(floor); // right wall
                            (Instantiate(wall, new Vector3(i + x, y, 0f), Quaternion.identity) as GameObject).transform.SetParent(floor); // bottom wall
                        }
                        break;
                    }
                case (Walls.WallTypes.horizontal_open):
                    {
                        for (int i = 0; i < roomWidth; i++)
                        {
                            (Instantiate(wall, new Vector3(i + x, y, 0f), Quaternion.identity) as GameObject).transform.SetParent(floor);
                            (Instantiate(wall, new Vector3(i + x, y + roomHeight - 1, 0f), Quaternion.identity) as GameObject).transform.SetParent(floor);
                        }
                        break;
                    }
                case (Walls.WallTypes.vertical_open):
                    {
                        for (int i = 0; i < roomWidth; i++)
                        {
                            (Instantiate(wall, new Vector3(x, i + y, 0f), Quaternion.identity) as GameObject).transform.SetParent(floor);
                            (Instantiate(wall, new Vector3(x + roomWidth - 1, i + y, 0f), Quaternion.identity) as GameObject).transform.SetParent(floor);
                        }
                        break;
                    }
                case (Walls.WallTypes.top_open_side):
                    {
                        for (int i = 0; i < roomWidth; i++)
                        {
                            (Instantiate(wall, new Vector3(x, i + y, 0f), Quaternion.identity) as GameObject).transform.SetParent(floor);
                            (Instantiate(wall, new Vector3(i + x, y, 0f), Quaternion.identity) as GameObject).transform.SetParent(floor);
                            (Instantiate(wall, new Vector3(x + roomWidth - 1, i + y, 0f), Quaternion.identity) as GameObject).transform.SetParent(floor);
                        }
                        break;
                    }
                case (Walls.WallTypes.right_open_side):
                    {
                        for (int i = 0; i < roomWidth; i++)
                        {
                            (Instantiate(wall, new Vector3(x, i + y, 0f), Quaternion.identity) as GameObject).transform.SetParent(floor);
                            (Instantiate(wall, new Vector3(i + x, y, 0f), Quaternion.identity) as GameObject).transform.SetParent(floor);
                            (Instantiate(wall, new Vector3(i + x, y + roomHeight - 1, 0f), Quaternion.identity) as GameObject).transform.SetParent(floor);
                        }
                        break;
                    }
                case (Walls.WallTypes.bottom_open_side):
                    {
                        for (int i = 0; i < roomWidth; i++)
                        {
                            (Instantiate(wall, new Vector3(x, i + y, 0f), Quaternion.identity) as GameObject).transform.SetParent(floor); // left wall
                            (Instantiate(wall, new Vector3(i + x, y + roomHeight - 1, 0f), Quaternion.identity) as GameObject).transform.SetParent(floor); // top wall
                            (Instantiate(wall, new Vector3(x + roomWidth - 1, i + y, 0f), Quaternion.identity) as GameObject).transform.SetParent(floor); // right wall
                        }
                        break;
                    }
                case (Walls.WallTypes.left_open_side):
                    {
                        for (int i = 0; i < roomWidth; i++)
                        {
                            (Instantiate(wall, new Vector3(i + x, y, 0f), Quaternion.identity) as GameObject).transform.SetParent(floor); // bottom wall
                            (Instantiate(wall, new Vector3(i + x, y + roomHeight - 1, 0f), Quaternion.identity) as GameObject).transform.SetParent(floor); // top wall
                            (Instantiate(wall, new Vector3(x + roomWidth - 1, i + y, 0f), Quaternion.identity) as GameObject).transform.SetParent(floor); // right wall
                        }
                        break;
                    }
                case (Walls.WallTypes.all_walls):
                    {
                        for (int i = 0; i < roomWidth; i++)
                        {
                            (Instantiate(wall, new Vector3(x, i + y, 0f), Quaternion.identity) as GameObject).transform.SetParent(floor); // left wall
                            (Instantiate(wall, new Vector3(i + x, y + roomHeight - 1, 0f), Quaternion.identity) as GameObject).transform.SetParent(floor); // top wall
                            (Instantiate(wall, new Vector3(x + roomWidth - 1, i + y, 0f), Quaternion.identity) as GameObject).transform.SetParent(floor); // right wall
                            (Instantiate(wall, new Vector3(i + x, y, 0f), Quaternion.identity) as GameObject).transform.SetParent(floor); // bottom wall
                        }
                        break;
                    }
            }
        }


        public void AssignPlayerPosition(int x, int y)
        {
            playerPosition = new Vector3(x, y, 0f);
        }

        public void InstantiatePlayer(Transform floor)
        {
            (Instantiate(player, playerPosition, Quaternion.identity) as GameObject).transform.SetParent(floor);
        }
    }

}
