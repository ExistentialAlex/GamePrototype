using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Prototype.GameGeneration.Rooms
{
    public class RoomGenerator : MonoBehaviour
    {
        public GameObject wall;
        public GameObject doorTile;

        private int wallWidth
        { get { return 1; } }

        private Vector3 playerPosition { get; set; }
        private int relativeStartX { get; set; }
        private int relativeStartY { get; set; }
        private int maxX { get; set; }
        private int maxY { get; set; }

        public void SetupRoom(Room room, Transform floor, int floorStartX, int floorStartY)
        {
            relativeStartX = Convert.ToInt32((Room.roomWidth + wallWidth) * room.vectorPosition.x) + floorStartX;
            relativeStartY = Convert.ToInt32((Room.roomHeight + wallWidth) * room.vectorPosition.y) + floorStartY;

            room.globalVectorPosition = new Vector3(relativeStartX, relativeStartY, 0f);

            maxX = relativeStartX + Room.roomWidth - 1;
            maxY = relativeStartY + Room.roomHeight - 1;

            GameObject roomObject = Instantiate(room.template, room.globalVectorPosition, Quaternion.identity);

            roomObject.transform.SetParent(floor);

            SetupWalls(room, roomObject.transform, relativeStartX, relativeStartY);

            if (room.type == Room.RoomType.entrance)
            {
                AssignPlayerPosition(relativeStartX + Room.roomWidth / 2, relativeStartY + Room.roomHeight / 2);
                InstantiatePlayer(floor);
            }
        }

        private void SetupWalls(Room room, Transform transform, int x, int y)
        {
            foreach (Walls.WallTypes wall in room.walls)
            {
                switch (wall)
                {
                    case Walls.WallTypes.top:
                        {
                            AddTopWall(room, x, y, transform);
                            break;
                        }
                    case Walls.WallTypes.left:
                        {
                            AddLeftWall(room, x, y, transform);
                            break;
                        }
                    case Walls.WallTypes.right:
                        {
                            AddRightWall(room, x, y, transform);
                            break;
                        }
                    case Walls.WallTypes.bottom:
                        {
                            AddBottomWall(room, x, y, transform);
                            break;
                        }
                }
            }
        }

        private void AddLeftWall(Room room, int x, int y, Transform transform)
        {
            if (room.doors.Select(Door => Door.position).Contains(Door.DoorPositions.left))
            {
                Door door = room.doors.Where(door => door.position == Door.DoorPositions.left).First();
                AddVerticalDoor(door, x - 1, y, GetVerticalWallCenter(), transform);
                return;
            }

            for (int i = -1; i < Room.roomHeight; i++)
            {
                AddWall(x - 1, i + y, transform); // left wall
            }
        }

        private void AddRightWall(Room room, int x, int y, Transform transform)
        {
            if (room.doors.Select(Door => Door.position).Contains(Door.DoorPositions.right))
            {
                Door door = room.doors.Where(door => door.position == Door.DoorPositions.right).First();
                AddVerticalDoor(door, maxX + 1, y, GetVerticalWallCenter(), transform);
                return;
            }

            for (int i = -1; i < Room.roomHeight; i++)
            {
                AddWall(maxX + 1, i + y, transform); // right wall
            }
        }

        private void AddTopWall(Room room, int x, int y, Transform transform)
        {
            if (room.doors.Select(Door => Door.position).Contains(Door.DoorPositions.top))
            {
                Door door = room.doors.Where(door => door.position == Door.DoorPositions.top).First();
                AddHorizontalDoor(door, x, maxY + 1, GetHorizontalWallCenter(), transform);
                return;
            }

            for (int i = -1; i < Room.roomWidth; i++)
            {
                AddWall(i + x, maxY + 1, transform); // top wall
            }
        }

        private void AddBottomWall(Room room, int x, int y, Transform transform)
        {
            if (room.doors.Select(Door => Door.position).Contains(Door.DoorPositions.bottom))
            {
                Door door = room.doors.Where(door => door.position == Door.DoorPositions.bottom).First();
                AddHorizontalDoor(door, x, y - 1, GetHorizontalWallCenter(), transform);
                return;
            }

            for (int i = -1; i < Room.roomWidth; i++)
            {
                AddWall(i + x, y - 1, transform); // bottom wall
            }
        }

        private void AddWall(int x, int y, Transform transform)
        {
            Instantiate(wall, new Vector3(x, y, 0f), Quaternion.identity).transform.SetParent(transform);
        }

        #region Door

        private void AddDoor(int x, int y, Transform transform)
        {
            Instantiate(doorTile, new Vector3(x, y, 0f), Quaternion.identity).transform.SetParent(transform);
        }

        private int GetHorizontalWallCenter()
        {
            return Convert.ToInt32(Room.roomWidth / 2);
        }

        private int GetVerticalWallCenter()
        {
            return Convert.ToInt32(Room.roomHeight / 2);
        }

        private void AddVerticalDoor(Door door, int x, int y, int wallMiddle, Transform transform)
        {
            int bottomMostPoint = Convert.ToInt32(wallMiddle - door.doorSize / 2);

            for (int i = -1; i < bottomMostPoint; i++)
            {
                AddWall(x, i + y, transform);
            }

            for (int i = bottomMostPoint; i < (bottomMostPoint + door.doorSize); i++)
            {
                AddDoor(x, i + y, transform);
            }

            for (int i = bottomMostPoint + door.doorSize; i <= Room.roomHeight; i++)
            {
                AddWall(x, i + y, transform);
            }
        }

        private void AddHorizontalDoor(Door door, int x, int y, int wallMiddle, Transform transform)
        {
            int leftMostPoint = Convert.ToInt32(wallMiddle - door.doorSize / 2);

            for (int i = -1; i < leftMostPoint; i++)
            {
                AddWall(x + i, y, transform);
            }

            for (int i = leftMostPoint; i < (leftMostPoint + door.doorSize); i++)
            {
                AddDoor(x + i, y, transform);
            }

            for (int i = leftMostPoint + door.doorSize; i <= Room.roomWidth; i++)
            {
                AddWall(x + i, y, transform);
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
            GameManager.instance.playerReady = true;
        }
    }
}
