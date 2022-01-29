using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameGeneration.Rooms
{
    public static class Rooms
    {
        public const int roomWidth = 10;
        public const int roomHeight = 10;

        public enum Size3Shapes
        {
            // [ ][ ]
            //    [ ]
            up_left,

            // [ ][ ]
            // [ ]
            up_right,

            // [ ]
            // [ ][ ]
            l_shaped,

            //    [ ]
            // [ ][ ]
            right_up
        }

        public enum Size2Shapes
        {
            horizontal,
            vertical
        }

        public static List<Vector3> GetUnpopulatedAdjacentRooms(Vector3 position, Room[,] rooms, int floorWidth, int floorHeight)
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

                    if (rooms[x + i, y + j] == null)
                    {
                        unpopulatedCells.Add(new Vector3(x + i, y + j));
                    }
                }
            }

            return unpopulatedCells;
        }

        /// <summary>
        /// Generates rooms of size 3, from bottom left most position.
        /// Excluded are rooms that can't be generated based on cell parse order
        /// (empty rooms to left or below position)
        /// </summary>
        /// <param name="position">Starting position from which to check</param>
        /// <param name="rooms">Current arrangement of rooms</param>
        /// <param name="floorWidth">Width of the current floor</param>
        /// <param name="floorHeight">Height of the current floor</param>
        /// <returns></returns>
        public static List<List<Tuple<Vector3, List<Walls.WallTypes>>>> GetPossibleSize3Rooms(Vector3 position, Room[,] rooms, int floorWidth, int floorHeight)
        {
            int x = Convert.ToInt32(position.x);
            int y = Convert.ToInt32(position.y);

            List<List<Tuple<Vector3, List<Walls.WallTypes>>>> possibleRooms = new List<List<Tuple<Vector3, List<Walls.WallTypes>>>>();

            // Room of shape
            // [ ][ ]
            // [s]

            if (x < floorWidth - 1 && y < floorHeight - 1 && rooms[x, y + 1] == null && rooms[x + 1, y + 1] == null)
            {
                possibleRooms.Add(new List<Tuple<Vector3, List<Walls.WallTypes>>>(){
                        Tuple.Create(new Vector3(x, y, 0f), Walls.TopOpenSide()),
                        Tuple.Create(new Vector3(x, y + 1, 0f), Walls.TopLeftCorner()),
                        Tuple.Create(new Vector3(x + 1, y + 1, 0f), Walls.LeftOpenSide()),
                    });
            }

            // Room of shape
            // [s][ ]
            //    [ ]
            else if (x < floorWidth - 1 && y > 0 && rooms[x + 1, y] == null && rooms[x + 1, y - 1] == null)
            {
                possibleRooms.Add(new List<Tuple<Vector3, List<Walls.WallTypes>>>(){
                        Tuple.Create(new Vector3(x, y, 0f), Walls.RightOpenSide()),
                        Tuple.Create(new Vector3(x + 1, y, 0f), Walls.TopRightCorner()),
                        Tuple.Create(new Vector3(x + 1, y - 1, 0f), Walls.TopOpenSide()),
                    });
            }

            // Room of shape
            //    [ ]
            // [s][ ]
            else if (x < floorWidth - 1 && y < floorHeight - 1 && rooms[x + 1, y] == null && rooms[x + 1, y + 1] == null)
            {
                possibleRooms.Add(new List<Tuple<Vector3, List<Walls.WallTypes>>>(){
                        Tuple.Create(new Vector3(x, y, 0f), Walls.RightOpenSide()),
                        Tuple.Create(new Vector3(x + 1, y, 0f), Walls.BottomRightCorner()),
                        Tuple.Create(new Vector3(x + 1, y + 1, 0f), Walls.BottomOpenSide()),
                    });
            }

            // Room of shape
            // [ ]
            // [s][ ]
            else if (x < floorWidth - 1 && y < floorHeight - 1 && rooms[x, y + 1] == null && rooms[x + 1, y] == null)
            {
                possibleRooms.Add(new List<Tuple<Vector3, List<Walls.WallTypes>>>(){
                        Tuple.Create(new Vector3(x, y, 0f), Walls.BottomLeftCorner()),
                        Tuple.Create(new Vector3(x, y + 1, 0f), Walls.BottomOpenSide()),
                        Tuple.Create(new Vector3(x + 1, y, 0f), Walls.LeftOpenSide()),
                    });
            }

            // Room of shape
            // [ ]
            // [ ]
            // [s]
            else if (y < floorHeight - 2 && rooms[x, y + 1] == null && rooms[x, y + 2] == null)
            {
                possibleRooms.Add(new List<Tuple<Vector3, List<Walls.WallTypes>>>(){
                        Tuple.Create(new Vector3(x, y, 0f), Walls.TopOpenSide()),
                        Tuple.Create(new Vector3(x, y + 1, 0f), Walls.VerticalOpen()),
                        Tuple.Create(new Vector3(x, y + 2, 0f), Walls.BottomOpenSide()),
                    });
            }

            // Room of shape
            // [s][ ][ ]
            else if (x < floorWidth - 2 && rooms[x + 1, y] == null && rooms[x + 2, y] == null)
            {
                possibleRooms.Add(new List<Tuple<Vector3, List<Walls.WallTypes>>>(){
                        Tuple.Create(new Vector3(x, y, 0f), Walls.RightOpenSide()),
                        Tuple.Create(new Vector3(x + 1, y, 0f), Walls.HorizontalOpen()),
                        Tuple.Create(new Vector3(x + 2, y, 0f), Walls.LeftOpenSide()),
                    });
            }

            return possibleRooms;
        }

        /// <summary>
        /// Generates rooms of size 2, from bottom left most position.
        /// Excluded are rooms that can't be generated based on cell parse order
        /// (empty rooms to left or below position)
        /// </summary>
        /// <param name="initialPosition">Starting position from which to check</param>
        /// <param name="rooms">Current arrangement of rooms</param>
        /// <param name="floorWidth">Width of the current floor</param>
        /// <param name="floorHeight">Height of the current floor</param>
        /// <returns></returns>
        public static List<List<Tuple<Vector3, List<Walls.WallTypes>>>> GetPossibleSize2Rooms(Vector3 initialPosition, Room[,] rooms, int floorWidth, int floorHeight)
        {
            List<List<Tuple<Vector3, List<Walls.WallTypes>>>> possibleRooms = new List<List<Tuple<Vector3, List<Walls.WallTypes>>>>();
            List<Vector3> unpopulatedCells = GetUnpopulatedAdjacentRooms(initialPosition, rooms, floorWidth, floorHeight);

            foreach (Vector3 pos in unpopulatedCells)
            {
                // Room of shape
                // [ ]
                // [s]
                if (pos.x == initialPosition.x && pos.y == (initialPosition.y + 1))
                {
                    possibleRooms.Add(new List<Tuple<Vector3, List<Walls.WallTypes>>> {
                        Tuple.Create(initialPosition, Walls.TopOpenSide()),
                        Tuple.Create(pos, Walls.BottomOpenSide())
                    });
                }
                // Room of shape
                // [s][ ]
                if (pos.x == (initialPosition.x + 1) && pos.y == initialPosition.y)
                {
                    possibleRooms.Add(new List<Tuple<Vector3, List<Walls.WallTypes>>> {
                        Tuple.Create(initialPosition, Walls.RightOpenSide()),
                        Tuple.Create(pos, Walls.LeftOpenSide())
                    });
                }
            }

            return possibleRooms;
        }
    }
}
