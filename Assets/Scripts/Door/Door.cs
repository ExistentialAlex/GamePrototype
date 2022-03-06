namespace Prototype.GameGeneration
{
    using System;
    using Prototype.GameGeneration.Rooms;
    using UnityEngine;
    using rand = UnityEngine.Random;

    /// <summary>
    /// Describes the structure of a door and the attributes of that door.
    /// </summary>
    public class Door
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Door"/> class.
        /// </summary>
        /// <param name="config">Door configuration.</param>
        /// <param name="position">The position the door is in in the room.</param>
        /// <param name="doorSize">Size of the door.</param>
        public Door(DoorConfig config, DoorPositions position, int doorSize = 0)
        {
            this.Position = position;

            if (doorSize == 0)
            {
                this.DoorSize = config.DefaultDoorSize;
            }
            else
            {
                this.DoorSize = doorSize;
            }
        }

        /// <summary>
        /// Door Positions.
        /// Defines which wall the door appears in.
        /// </summary>
        public enum DoorPositions
        {
            top,
            left,
            right,
            bottom
        }

        /// <summary>
        /// Gets the Door Size.
        /// </summary>
        /// <value>The Door Size.</value>
        public int DoorSize { get; private set; }

        /// <summary>
        /// Gets the door position.
        /// </summary>
        /// <value>The Door Position.</value>
        public DoorPositions Position { get; private set; }

        /// <summary>
        /// Randomly chooses whether to add a door to a room based on the rooms around it.
        /// Tries a number of times per room before giving up.
        /// </summary>
        /// <param name="config">Door configuration.</param>
        /// <param name="cells">The 2D array of cells.</param>
        /// <param name="cellX">X coordinate of the specific cell.</param>
        /// <param name="cellY">Y coordinate of the specific room.</param>
        /// <param name="maxX">Maximum value of X.</param>
        /// <param name="maxY">Maximum value of Y.</param>
        /// <param name="retries">Number of times to try.</param>
        public static void AddDoor(DoorConfig config, Cell[,] cells, int cellX, int cellY, int maxX, int maxY, int retries = 4)
        {
            Cell cell = cells[cellX, cellY];
            int doorToUse = rand.Range(0, 3);

            if (!Enum.TryParse(Convert.ToString(doorToUse), out DoorPositions position))
            {
                Debug.Log("Could not get door position from random number");
            }

            if (retries == 0)
            {
                return;
            }

            switch (position)
            {
                case DoorPositions.top:
                    {
                        if (!CheckTopDoorConstraints(cell, cells, cellX, cellY, maxX, maxY))
                        {
                            AddDoor(config, cells, cellX, cellY, maxX, maxY, retries - 1);
                            break;
                        }

                        Door topDoor = new Door(config, DoorPositions.top);
                        cell.Doors.Add(topDoor);

                        Door bottomDoor = new Door(config, DoorPositions.bottom);
                        cells[cellX, cellY + 1].Doors.Add(bottomDoor);
                        break;
                    }

                case DoorPositions.left:
                    {
                        if (!CheckLeftDoorConstraints(cell, cells, cellX, cellY, maxX, maxY))
                        {
                            AddDoor(config, cells, cellX, cellY, maxX, maxY, retries - 1);
                            break;
                        }

                        Door leftDoor = new Door(config, DoorPositions.left);
                        cell.Doors.Add(leftDoor);

                        Door rightDoor = new Door(config, DoorPositions.right);
                        cells[cellX - 1, cellY].Doors.Add(rightDoor);
                        break;
                    }

                case DoorPositions.right:
                    {
                        if (!CheckRightDoorConstraints(cell, cells, cellX, cellY, maxX, maxY))
                        {
                            AddDoor(config, cells, cellX, cellY, maxX, maxY, retries - 1);
                            break;
                        }

                        Door rightDoor = new Door(config, DoorPositions.right);
                        cell.Doors.Add(rightDoor);

                        Door leftDoor = new Door(config, DoorPositions.left);
                        cells[cellX + 1, cellY].Doors.Add(leftDoor);
                        break;
                    }

                case DoorPositions.bottom:
                    {
                        if (!CheckBottomDoorConstraints(cell, cells, cellX, cellY, maxX, maxY))
                        {
                            AddDoor(config, cells, cellX, cellY, maxX, maxY, retries - 1);
                            break;
                        }

                        Door bottomDoor = new Door(config, DoorPositions.bottom);
                        cell.Doors.Add(bottomDoor);

                        Door topDoor = new Door(config, DoorPositions.top);
                        cells[cellX, cellY - 1].Doors.Add(topDoor);
                        break;
                    }
            }
        }

        /// <summary>
        /// Check the constraints for placing a door at the bottom of a cell.
        /// </summary>
        /// <param name="cell">The room in question.</param>
        /// <param name="cells">All the rooms.</param>
        /// <param name="cellX">X position of the cell.</param>
        /// <param name="cellY">Y position of the cell.</param>
        /// <param name="maxX">Maximum possible X position.</param>
        /// <param name="maxY">Maximum possible Y position.</param>
        /// <returns>True if all constraints are met.</returns>
        public static bool CheckBottomDoorConstraints(Cell cell, Cell[,] cells, int cellX, int cellY, int maxX, int maxY)
        {
            return cell.VectorPosition.y > 0 &&
                   CheckGenericConstraints(cell, cells[cellX, cellY - 1]) &&
                   cell.Walls.Contains(Walls.WallTypes.bottom);
        }

        /// <summary>
        /// Check the generic constraints that apply to all cells.
        /// </summary>
        /// <param name="cell">The current cell.</param>
        /// <param name="adjacentCell">The cell on the other side of the door.</param>
        /// <returns>True if all constraints are met.</returns>
        public static bool CheckGenericConstraints(Cell cell, Cell adjacentCell)
        {
            return adjacentCell.ParentRoomType != Room.RoomType.empty &&
                   !(cell.ParentRoomType == Room.RoomType.entrance && adjacentCell.ParentRoomType == Room.RoomType.boss) &&
                   !(cell.ParentRoomType == Room.RoomType.boss && adjacentCell.ParentRoomType == Room.RoomType.entrance);
        }

        /// <summary>
        /// Check the constraints for placing a door at the left of a cell.
        /// </summary>
        /// <param name="cell">The cell in question.</param>
        /// <param name="cells">All the cells.</param>
        /// <param name="cellX">X position of the cell.</param>
        /// <param name="cellY">Y position of the cell.</param>
        /// <param name="maxX">Maximum possible X position.</param>
        /// <param name="maxY">Maximum possible Y position.</param>
        /// <returns>True if all constraints are met.</returns>
        public static bool CheckLeftDoorConstraints(Cell cell, Cell[,] cells, int cellX, int cellY, int maxX, int maxY)
        {
            return cell.VectorPosition.x > 0 &&
                   CheckGenericConstraints(cell, cells[cellX - 1, cellY]) &&
                   cell.Walls.Contains(Walls.WallTypes.left);
        }

        /// <summary>
        /// Check the constraints for placing a door at the right of a cell.
        /// </summary>
        /// <param name="cell">The cell in question.</param>
        /// <param name="cells">All the cells.</param>
        /// <param name="cellX">X position of the cell.</param>
        /// <param name="cellY">Y position of the cell.</param>
        /// <param name="maxX">Maximum possible X position.</param>
        /// <param name="maxY">Maximum possible Y position.</param>
        /// <returns>True if all constraints are met.</returns>
        public static bool CheckRightDoorConstraints(Cell cell, Cell[,] cells, int cellX, int cellY, int maxX, int maxY)
        {
            return cell.VectorPosition.x < maxX &&
                   CheckGenericConstraints(cell, cells[cellX + 1, cellY]) &&
                   cell.Walls.Contains(Walls.WallTypes.right);
        }

        /// <summary>
        /// Check the constraints for placing a door at the top of a cell.
        /// </summary>
        /// <param name="cell">The cell in question.</param>
        /// <param name="cells">All the cells.</param>
        /// <param name="cellX">X position of the cell.</param>
        /// <param name="cellY">Y position of the cell.</param>
        /// <param name="maxX">Maximum possible X position.</param>
        /// <param name="maxY">Maximum possible Y position.</param>
        /// <returns>True if all constraints are met.</returns>
        public static bool CheckTopDoorConstraints(Cell cell, Cell[,] cells, int cellX, int cellY, int maxX, int maxY)
        {
            return cell.VectorPosition.y < maxY &&
                   CheckGenericConstraints(cell, cells[cellX, cellY + 1]) &&
                   cell.Walls.Contains(Walls.WallTypes.top);
        }
    }
}
