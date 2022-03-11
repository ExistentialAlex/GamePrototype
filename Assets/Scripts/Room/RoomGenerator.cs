namespace Prototype.GameGeneration.Rooms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using rand = UnityEngine.Random;

    /// <summary>
    /// The room generator class.
    /// </summary>
    public class RoomGenerator : MonoBehaviour
    {
        /// <summary>
        /// The tile used for doors.
        /// </summary>
        [SerializeField]
        private GameObject doorTile;

        /// <summary>
        /// The tile used for walls.
        /// </summary>
        [SerializeField]
        private GameObject wall;

        /// <summary>
        /// Gets or sets the door tile.
        /// </summary>
        /// <value>The door tile.</value>
        public GameObject DoorTile
        {
            get => this.doorTile;
            set => this.doorTile = value;
        }

        /// <summary>
        /// Gets or sets the wall tile.
        /// </summary>
        /// <value>The wall tile.</value>
        public GameObject Wall
        {
            get => this.wall;
            set => this.wall = value;
        }

        /// <summary>
        /// Gets or sets the player position.
        /// </summary>
        /// <value>The player position.</value>
        private Vector3 PlayerPosition { get; set; }

        /// <summary>
        /// Gets or sets the relative X value of the start position.
        /// </summary>
        /// <value>The X value of the relative start position.</value>
        private int RelativeStartX { get; set; }

        /// <summary>
        /// Gets or sets the relative Y value of the start position.
        /// </summary>
        /// <value>The Y value of the relative start position.</value>
        private int RelativeStartY { get; set; }

        /// <summary>
        /// Gets the wall width.
        /// </summary>
        /// <value>The wall width.</value>
        private int WallWidth
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// Assign the player to the x, y position.
        /// </summary>
        /// <param name="x">X Value to assign player to.</param>
        /// <param name="y">Y Value to assign player to.</param>
        public void AssignPlayerPosition(int x, int y)
        {
            this.PlayerPosition = new Vector3(x, y, 0f);
        }

        /// <summary>
        /// Move the player to the new player position.
        /// </summary>
        public void InstantiatePlayer()
        {
            GameManager.Player.transform.position = this.PlayerPosition;
            GameManager.Instance.PlayerReady = true;
        }

        /// <summary>
        /// Setup the room in the game world.
        /// </summary>
        /// <param name="room">The room to setup.</param>
        /// <param name="transform">The transform of the floor.</param>
        /// <param name="floorStartX">The global X start position.</param>
        /// <param name="floorStartY">The global Y start position.</param>
        public void SetupRoom(Room room, Transform transform, int floorStartX, int floorStartY)
        {
            Vector3 relativeStartVector = this.GetRelativeVectorPosition(room.DrawFrom, floorStartX, floorStartY);

            this.RelativeStartX = Convert.ToInt32(relativeStartVector.x);
            this.RelativeStartY = Convert.ToInt32(relativeStartVector.y);

            room.GlobalVectorPosition = relativeStartVector;

            GameObject roomObject = Instantiate(room.Template, room.GlobalVectorPosition, Quaternion.identity);

            roomObject.transform.SetParent(transform);

            foreach (Cell cell in room.Cells)
            {
                Vector3 relativeCellVector = this.GetRelativeVectorPosition(cell.VectorPosition, floorStartX, floorStartY);
                int relativeCellX = Convert.ToInt32(relativeCellVector.x);
                int relativeCellY = Convert.ToInt32(relativeCellVector.y);

                int maxX = relativeCellX + Room.RoomWidth - 1;
                int maxY = relativeCellY + Room.RoomHeight - 1;

                this.SetupWalls(cell, roomObject.transform, relativeCellX, relativeCellY, maxX, maxY);
                this.PlaceEnemies(cell, roomObject.transform, relativeCellX, relativeCellY, maxX, maxY);
            }

            if (room.Type == Room.RoomType.entrance)
            {
                this.AssignPlayerPosition(this.RelativeStartX + (Room.RoomWidth / 2), this.RelativeStartY + (Room.RoomHeight / 2));
                this.InstantiatePlayer();
            }
        }

        /// <summary>
        /// Add a wall at the bottom of the room.
        /// </summary>
        /// <param name="cell">The cell to add the wall to.</param>
        /// <param name="x">The relative x position of the cell.</param>
        /// <param name="y">The relative y position of the cell.</param>
        /// <param name="transform">The transform to append it to.</param>
        private void AddBottomWall(Cell cell, int x, int y, Transform transform)
        {
            if (cell.Doors.Select(Door => Door.Position).Contains(Door.DoorPositions.bottom))
            {
                Door door = cell.Doors.Where(door => door.Position == Door.DoorPositions.bottom).First();
                this.AddHorizontalDoor(door, x, y - 1, this.GetHorizontalWallCenter(), transform);
                return;
            }

            for (int i = -1; i < Room.RoomWidth; i++)
            {
                this.AddWall(i + x, y - 1, transform); // bottom wall
            }
        }

        /// <summary>
        /// Add a wall at the left side of the cell.
        /// </summary>
        /// <param name="cell">The cell to add the wall to.</param>
        /// <param name="x">The relative x position of the cell.</param>
        /// <param name="y">The relative y position of the cell.</param>
        /// <param name="transform">The transform to append it to.</param>
        private void AddLeftWall(Cell cell, int x, int y, Transform transform)
        {
            if (cell.Doors.Select(Door => Door.Position).Contains(Door.DoorPositions.left))
            {
                Door door = cell.Doors.Where(door => door.Position == Door.DoorPositions.left).First();
                this.AddVerticalDoor(door, x - 1, y, this.GetVerticalWallCenter(), transform);
                return;
            }

            for (int i = -1; i < Room.RoomHeight; i++)
            {
                this.AddWall(x - 1, i + y, transform); // left wall
            }
        }

        /// <summary>
        /// Add a wall at the right side of the cell.
        /// </summary>
        /// <param name="cell">The cell to add the wall to.</param>
        /// <param name="x">The relative x position of the cell.</param>
        /// <param name="y">The relative y position of the cell.</param>
        /// <param name="transform">The transform to append it to.</param>
        private void AddRightWall(Cell cell, int x, int y, Transform transform)
        {
            if (cell.Doors.Select(Door => Door.Position).Contains(Door.DoorPositions.right))
            {
                Door door = cell.Doors.Where(door => door.Position == Door.DoorPositions.right).First();
                this.AddVerticalDoor(door, x + 1, y, this.GetVerticalWallCenter(), transform);
                return;
            }

            for (int i = -1; i < Room.RoomHeight; i++)
            {
                this.AddWall(x + 1, i + y, transform); // right wall
            }
        }

        /// <summary>
        /// Add a wall at the top of the cell.
        /// </summary>
        /// <param name="cell">The cell to add the wall to.</param>
        /// <param name="x">The relative x position of the cell.</param>
        /// <param name="y">The relative y position of the cell.</param>
        /// <param name="transform">The transform to append it to.</param>
        private void AddTopWall(Cell cell, int x, int y, Transform transform)
        {
            if (cell.Doors.Select(Door => Door.Position).Contains(Door.DoorPositions.top))
            {
                Door door = cell.Doors.Where(door => door.Position == Door.DoorPositions.top).First();
                this.AddHorizontalDoor(door, x, y + 1, this.GetHorizontalWallCenter(), transform);
                return;
            }

            for (int i = -1; i < Room.RoomWidth; i++)
            {
                this.AddWall(i + x, y + 1, transform); // top wall
            }
        }

        /// <summary>
        /// Add a wall at the specified position.
        /// </summary>
        /// <param name="x">The relative x position of the cell.</param>
        /// <param name="y">The relative y position of the cell.</param>
        /// <param name="transform">The transform to append it to.</param>
        private void AddWall(int x, int y, Transform transform)
        {
            Instantiate(this.Wall, new Vector3(x, y, 0f), Quaternion.identity).transform.SetParent(transform);
        }

        /// <summary>
        /// Get the relative vector position based on the room width, height and wall width and the start position of the floor.
        /// </summary>
        /// <param name="pos">The position to convert.</param>
        /// <param name="floorStartX">Start x position of the floor.</param>
        /// <param name="floorStartY">Start y position of the floor.</param>
        /// <returns>The relative vector position.</returns>
        private Vector3 GetRelativeVectorPosition(Vector3 pos, int floorStartX, int floorStartY)
        {
            int relativeStartX = Convert.ToInt32((Room.RoomWidth + this.WallWidth) * pos.x) + floorStartX;
            int relativeStartY = Convert.ToInt32((Room.RoomHeight + this.WallWidth) * pos.y) + floorStartY;

            return new Vector3(relativeStartX, relativeStartY, 0f);
        }

        /// <summary>
        /// Place the enemies in the cell.
        /// </summary>
        /// <param name="cell">The cell to place enemies in.</param>
        /// <param name="transform">The parent room transform.</param>
        /// <param name="x">The relative start x position.</param>
        /// <param name="y">The relative start y position.</param>
        /// <param name="maxX">The maximum x position.</param>
        /// <param name="maxY">The maximum y position.</param>
        private void PlaceEnemies(Cell cell, Transform transform, int x, int y, int maxX, int maxY)
        {
            List<int> occupiedX = new List<int>();
            List<int> occupiedY = new List<int>();

            foreach (GameObject enemy in cell.Enemies)
            {
                int randX;
                int randY;
                do
                {
                    randX = rand.Range(x, maxX);
                    randY = rand.Range(y, maxY);
                }
                while (occupiedX.Contains(randX) && occupiedY.Contains(randY));

                GameObject instantiatedEnemy = Instantiate(enemy, new Vector3(randX, randY, 0f), Quaternion.identity);
                instantiatedEnemy.transform.SetParent(transform);
            }
        }

        /// <summary>
        /// Set up walls for the specified room.
        /// </summary>
        /// <param name="cell">The cell to setup the walls for.</param>
        /// <param name="transform">The floor transform.</param>
        /// <param name="x">The relative X position of the floor.</param>
        /// <param name="y">The relative Y position of the floor.</param>
        /// <param name="maxX">The max X position of the floor.</param>
        /// <param name="maxY">The max Y position of the floor.</param>
        private void SetupWalls(Cell cell, Transform transform, int x, int y, int maxX, int maxY)
        {
            foreach (Walls.WallTypes wall in cell.Walls)
            {
                switch (wall)
                {
                    case Walls.WallTypes.top:
                        {
                            this.AddTopWall(cell, x, maxY, transform);
                            break;
                        }

                    case Walls.WallTypes.left:
                        {
                            this.AddLeftWall(cell, x, y, transform);
                            break;
                        }

                    case Walls.WallTypes.right:
                        {
                            this.AddRightWall(cell, maxX, y, transform);
                            break;
                        }

                    case Walls.WallTypes.bottom:
                        {
                            this.AddBottomWall(cell, x, y, transform);
                            break;
                        }
                }
            }
        }

        #region Door

        /// <summary>
        /// Add a door tile at the specified position.
        /// </summary>
        /// <param name="x">The x position to add the tile too.</param>
        /// <param name="y">The y position to add the tile too.</param>
        /// <param name="transform">The transform to assign the tile to.</param>
        private void AddDoor(int x, int y, Transform transform)
        {
            Instantiate(this.DoorTile, new Vector3(x, y, 0f), Quaternion.identity).transform.SetParent(transform);
        }

        /// <summary>
        /// Add a horizontal door at the specified position.
        /// </summary>
        /// <param name="door">The door to add.</param>
        /// <param name="x">The relative x position.</param>
        /// <param name="y">The relative y position.</param>
        /// <param name="wallMiddle">The middle of the wall.</param>
        /// <param name="transform">The transform to assign the door to.</param>
        private void AddHorizontalDoor(Door door, int x, int y, int wallMiddle, Transform transform)
        {
            int leftMostPoint = Convert.ToInt32(wallMiddle - (door.DoorSize / 2));

            for (int i = -1; i < leftMostPoint; i++)
            {
                this.AddWall(x + i, y, transform);
            }

            for (int i = leftMostPoint; i < (leftMostPoint + door.DoorSize); i++)
            {
                this.AddDoor(x + i, y, transform);
            }

            for (int i = leftMostPoint + door.DoorSize; i <= Room.RoomWidth; i++)
            {
                this.AddWall(x + i, y, transform);
            }
        }

        /// <summary>
        /// Add a vertical door at the specified position.
        /// </summary>
        /// <param name="door">The door to add.</param>
        /// <param name="x">The relative x position.</param>
        /// <param name="y">The relative y position.</param>
        /// <param name="wallMiddle">The middle of the wall.</param>
        /// <param name="transform">The transform to assign the door to.</param>
        private void AddVerticalDoor(Door door, int x, int y, int wallMiddle, Transform transform)
        {
            int bottomMostPoint = Convert.ToInt32(wallMiddle - (door.DoorSize / 2));

            for (int i = -1; i < bottomMostPoint; i++)
            {
                this.AddWall(x, i + y, transform);
            }

            for (int i = bottomMostPoint; i < (bottomMostPoint + door.DoorSize); i++)
            {
                this.AddDoor(x, i + y, transform);
            }

            for (int i = bottomMostPoint + door.DoorSize; i <= Room.RoomHeight; i++)
            {
                this.AddWall(x, i + y, transform);
            }
        }

        /// <summary>
        /// Get the center of a horizontal wall.
        /// </summary>
        /// <returns>The center of a horizontal wall.</returns>
        private int GetHorizontalWallCenter()
        {
            return Convert.ToInt32(Room.RoomWidth / 2);
        }

        /// <summary>
        /// Get the center of a vertical wall.
        /// </summary>
        /// <returns>The center of a vertical wall.</returns>
        private int GetVerticalWallCenter()
        {
            return Convert.ToInt32(Room.RoomHeight / 2);
        }

        #endregion Door
    }
}
