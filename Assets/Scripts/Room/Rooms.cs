namespace Prototype.GameGeneration.Rooms
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Class containing methods for generating rooms.
    /// </summary>
    public static class Rooms
    {
        /// <summary>
        /// Create a room type from the type of room.
        /// </summary>
        /// <param name="roomType">Room type.</param>
        /// <param name="roomId">ID of the room to create.</param>
        /// <param name="drawFrom">Position to draw the room from.</param>
        /// <param name="roomConfig">Room config.</param>
        /// <returns>A new instance of the room based on the type.</returns>
        public static Room CreateRoomFromType(Room.RoomType roomType, string roomId, Vector3 drawFrom, RoomConfig roomConfig)
        {
            Dictionary<Room.RoomType, Room> dict = new Dictionary<Room.RoomType, Room>
            {
                { Room.RoomType.boss, new BossRoom(roomId, drawFrom, roomConfig) },
                { Room.RoomType.empty, new EmptyRoom(roomId, drawFrom, roomConfig) },
                { Room.RoomType.entrance, new EntranceRoom(roomId, drawFrom, roomConfig) },
                { Room.RoomType.secret, new SecretRoom(roomId, drawFrom, roomConfig) },
                { Room.RoomType.shop, new ShopRoom(roomId, drawFrom, roomConfig) },
                { Room.RoomType.stair, new StairRoom(roomId, drawFrom, roomConfig) },
                { Room.RoomType.stairDown, new StairDownRoom(roomId, drawFrom, roomConfig) },
                { Room.RoomType.singleRoom, new StandardRoom(roomId, drawFrom, roomConfig) },
                { Room.RoomType.doubleHorizontal, new DoubleRoomHorizontal(roomId, drawFrom, roomConfig) },
                { Room.RoomType.doubleVertical, new DoubleRoomVertical(roomId, drawFrom, roomConfig) },
                { Room.RoomType.tripleBottomLeft, new TripleRoomBottomLeft(roomId, drawFrom, roomConfig) },
                { Room.RoomType.tripleBottomRight, new TripleRoomBottomRight(roomId, drawFrom, roomConfig) },
                { Room.RoomType.tripleHorizontal, new TripleRoomHorizontal(roomId, drawFrom, roomConfig) },
                { Room.RoomType.tripleTopLeft, new TripleRoomTopLeft(roomId, drawFrom, roomConfig) },
                { Room.RoomType.tripleTopRight, new TripleRoomTopRight(roomId, drawFrom, roomConfig) },
                { Room.RoomType.tripleVertical, new TripleRoomVertical(roomId, drawFrom, roomConfig) },
            };

            if (dict.TryGetValue(roomType, out Room room))
            {
                return room;
            }

            throw new Exception("Could not create a room for the specified Room Type: '" + Convert.ToString(roomType) + "'");
        }

        /// <summary>
        /// Generates rooms of size 2, from bottom left most position.
        /// Excluded are rooms that can't be generated based on cell parse order.
        /// (empty rooms to left or below position).
        /// </summary>
        /// <param name="initialPosition">Starting position from which to check.</param>
        /// <param name="floor">The floor to check.</param>
        /// <param name="roomId">The room ID.</param>
        /// <returns>A list of possible size 2 rooms.</returns>
        public static List<Tuple<Vector3, List<Cell>, Room.RoomType>> GetPossibleSize2Rooms(Vector3 initialPosition, Floor floor, string roomId)
        {
            List<Tuple<Vector3, List<Cell>, Room.RoomType>> possibleRooms = new List<Tuple<Vector3, List<Cell>, Room.RoomType>>();
            List<Vector3> unpopulatedCells = GetUnpopulatedAdjacentRooms(initialPosition, floor.Cells, floor.FloorConfig.FloorWidth, floor.FloorConfig.FloorHeight);

            foreach (Vector3 pos in unpopulatedCells)
            {
                // Room of shape
                // [ ]
                // [s]
                if (pos.x == initialPosition.x && pos.y == (initialPosition.y + 1))
                {
                    possibleRooms.Add(Tuple.Create(
                        initialPosition,
                        new List<Cell>
                        {
                            new Cell(initialPosition, roomId, Room.RoomType.doubleVertical, Walls.TopOpenSide()),
                            new Cell(pos, roomId, Room.RoomType.doubleVertical, Walls.BottomOpenSide()),
                        },
                        Room.RoomType.doubleVertical));
                }

                // Room of shape
                // [s][ ]
                if (pos.x == (initialPosition.x + 1) && pos.y == initialPosition.y)
                {
                    possibleRooms.Add(Tuple.Create(
                        initialPosition,
                        new List<Cell>
                        {
                            new Cell(initialPosition, roomId, Room.RoomType.doubleHorizontal, Walls.RightOpenSide()),
                            new Cell(pos, roomId, Room.RoomType.doubleHorizontal, Walls.LeftOpenSide()),
                        },
                        Room.RoomType.doubleHorizontal));
                }
            }

            return possibleRooms;
        }

        /// <summary>
        /// Generates rooms of size 3, from bottom left most position.
        /// Excluded are rooms that can't be generated based on cell parse order.
        /// (empty rooms to left or below position).
        /// </summary>
        /// <param name="position">Starting position from which to check.</param>
        /// <param name="floor">The floor to check.</param>
        /// <param name="roomId">The room ID.</param>
        /// <returns>A list of possible size 3 rooms.</returns>
        public static List<Tuple<Vector3, List<Cell>, Room.RoomType>> GetPossibleSize3Rooms(Vector3 position, Floor floor, string roomId)
        {
            int x = Convert.ToInt32(position.x);
            int y = Convert.ToInt32(position.y);

            List<Tuple<Vector3, List<Cell>, Room.RoomType>> possibleRooms = new List<Tuple<Vector3, List<Cell>, Room.RoomType>>();
            Cell[,] cells = floor.Cells;
            int floorWidth = floor.FloorConfig.FloorWidth;
            int floorHeight = floor.FloorConfig.FloorHeight;

            // Room of shape
            // [ ][ ]
            // [s]
            if (x < floorWidth - 1 && y < floorHeight - 1 && cells[x, y + 1] == null && cells[x + 1, y + 1] == null)
            {
                possibleRooms.Add(Tuple.Create(
                    new Vector3(x, y, 0f),
                    new List<Cell>
                    {
                        new Cell(new Vector3(x, y, 0f), roomId, Room.RoomType.tripleTopLeft, Walls.TopOpenSide()),
                        new Cell(new Vector3(x, y + 1, 0f), roomId, Room.RoomType.tripleTopLeft, Walls.TopLeftCorner()),
                        new Cell(new Vector3(x + 1, y + 1, 0f), roomId, Room.RoomType.tripleTopLeft, Walls.LeftOpenSide())
                    },
                    Room.RoomType.tripleTopLeft));
            }

            // Room of shape
            // [s][ ]
            //    [ ]
            if (x < floorWidth - 1 && y > 0 && cells[x + 1, y] == null && cells[x + 1, y - 1] == null)
            {
                possibleRooms.Add(Tuple.Create(
                    new Vector3(x, y - 1, 0f),
                    new List<Cell>
                    {
                        new Cell(new Vector3(x, y, 0f), roomId, Room.RoomType.tripleTopRight, Walls.RightOpenSide()),
                        new Cell(new Vector3(x + 1, y, 0f), roomId, Room.RoomType.tripleTopRight, Walls.TopRightCorner()),
                        new Cell(new Vector3(x + 1, y - 1, 0f), roomId, Room.RoomType.tripleTopRight, Walls.TopOpenSide())
                    },
                    Room.RoomType.tripleTopRight));
            }

            // Room of shape
            //    [ ]
            // [s][ ]
            if (x < floorWidth - 1 && y < floorHeight - 1 && cells[x + 1, y] == null && cells[x + 1, y + 1] == null)
            {
                possibleRooms.Add(Tuple.Create(
                    new Vector3(x, y, 0f),
                    new List<Cell>
                    {
                        new Cell(new Vector3(x, y, 0f), roomId, Room.RoomType.tripleBottomRight, Walls.RightOpenSide()),
                        new Cell(new Vector3(x + 1, y, 0f), roomId, Room.RoomType.tripleBottomRight, Walls.BottomRightCorner()),
                        new Cell(new Vector3(x + 1, y + 1, 0f), roomId, Room.RoomType.tripleBottomRight, Walls.BottomOpenSide())
                    },
                    Room.RoomType.tripleBottomRight));
            }

            // Room of shape
            // [ ]
            // [s][ ]
            if (x < floorWidth - 1 && y < floorHeight - 1 && cells[x, y + 1] == null && cells[x + 1, y] == null)
            {
                possibleRooms.Add(Tuple.Create(
                    new Vector3(x, y, 0f),
                    new List<Cell>
                    {
                        new Cell(new Vector3(x, y, 0f), roomId, Room.RoomType.tripleBottomLeft, Walls.BottomLeftCorner()),
                        new Cell(new Vector3(x, y + 1, 0f), roomId, Room.RoomType.tripleBottomLeft, Walls.BottomOpenSide()),
                        new Cell(new Vector3(x + 1, y, 0f), roomId, Room.RoomType.tripleBottomLeft, Walls.LeftOpenSide())
                    },
                    Room.RoomType.tripleBottomLeft));
            }

            // Room of shape
            // [ ]
            // [ ]
            // [s]
            if (y < floorHeight - 2 && cells[x, y + 1] == null && cells[x, y + 2] == null)
            {
                possibleRooms.Add(Tuple.Create(
                    new Vector3(x, y, 0f),
                    new List<Cell>
                    {
                        new Cell(new Vector3(x, y, 0f), roomId, Room.RoomType.tripleVertical, Walls.TopOpenSide()),
                        new Cell(new Vector3(x, y + 1, 0f), roomId, Room.RoomType.tripleVertical, Walls.VerticalOpen()),
                        new Cell(new Vector3(x, y + 2, 0f), roomId, Room.RoomType.tripleVertical, Walls.BottomOpenSide())
                    },
                    Room.RoomType.tripleVertical));
            }

            // Room of shape
            // [s][ ][ ]
            if (x < floorWidth - 2 && cells[x + 1, y] == null && cells[x + 2, y] == null)
            {
                possibleRooms.Add(Tuple.Create(
                    new Vector3(x, y, 0f),
                    new List<Cell>
                    {
                        new Cell(new Vector3(x, y, 0f), roomId, Room.RoomType.tripleHorizontal, Walls.RightOpenSide()),
                        new Cell(new Vector3(x + 1, y, 0f), roomId, Room.RoomType.tripleHorizontal, Walls.HorizontalOpen()),
                        new Cell(new Vector3(x + 2, y, 0f), roomId, Room.RoomType.tripleHorizontal, Walls.LeftOpenSide())
                    },
                    Room.RoomType.tripleHorizontal));
            }

            return possibleRooms;
        }

        /// <summary>
        /// Gets the list of adjacent rooms that are unpopulated.
        /// </summary>
        /// <param name="position">The position to check.</param>
        /// <param name="cells">List of cells.</param>
        /// <param name="floorWidth">Width of the floor.</param>
        /// <param name="floorHeight">Height of the floor.</param>
        /// <returns>List of unpopulated vector positions.</returns>
        public static List<Vector3> GetUnpopulatedAdjacentRooms(Vector3 position, Cell[,] cells, int floorWidth, int floorHeight)
        {
            List<Vector3> unpopulatedCells = new List<Vector3>();

            int x = Convert.ToInt32(position.x);
            int y = Convert.ToInt32(position.y);
            int xMin = -1;
            int xMax = 1;
            int yMin = -1;
            int yMax = 1;

            if (x == floorWidth - 1)
            {
                xMax = 0;
            }
            else if (x == 0)
            {
                xMin = 0;
            }

            if (y == floorHeight - 1)
            {
                yMax = 0;
            }
            else if (y == 0)
            {
                yMin = 0;
            }

            for (int i = xMin; i <= xMax; i++)
            {
                for (int j = yMin; j <= yMax; j++)
                {
                    // Don't check the cell itself
                    if (x + i == x && y + j == y)
                    {
                        continue;
                    }

                    if (cells[x + i, y + j] == null)
                    {
                        unpopulatedCells.Add(new Vector3(x + i, y + j));
                    }
                }
            }

            return unpopulatedCells;
        }
    }
}
