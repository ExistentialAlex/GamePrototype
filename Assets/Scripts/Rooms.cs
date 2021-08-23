using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameGeneration
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

        public static List<Vector3> GetUnpopulatedAdjacentCells(Vector3 position, Cell[,] rooms, int floorWidth, int floorHeight)
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
        public static List<List<Tuple<Vector3, Walls.WallTypes>>> GetPossibleSize3Rooms(Vector3 position, Cell[,] rooms, int floorWidth, int floorHeight)
        {
            int x = Convert.ToInt32(position.x);
            int y = Convert.ToInt32(position.y);

            List<List<Tuple<Vector3, Walls.WallTypes>>> possibleRooms = new List<List<Tuple<Vector3, Walls.WallTypes>>>();

            // Room of shape
            // [ ][ ]
            // [s] 

            if (x < floorWidth - 1 && y < floorHeight - 1 && rooms[x, y + 1] == null && rooms[x + 1, y + 1] == null)
            {
                possibleRooms.Add(new List<Tuple<Vector3, Walls.WallTypes>>(){
                        Tuple.Create(new Vector3(x, y, 0f), Walls.WallTypes.top_open_side),
                        Tuple.Create(new Vector3(x, y + 1, 0f), Walls.WallTypes.top_left_corner),
                        Tuple.Create(new Vector3(x + 1, y + 1, 0f), Walls.WallTypes.left_open_side),
                    });
            }

            // Room of shape
            // [s][ ]
            //    [ ]

            else if (x < floorWidth - 1 && y > 0 && rooms[x + 1, y] == null && rooms[x + 1, y - 1] == null)
            {
                possibleRooms.Add(new List<Tuple<Vector3, Walls.WallTypes>>(){
                        Tuple.Create(new Vector3(x, y, 0f), Walls.WallTypes.right_open_side),
                        Tuple.Create(new Vector3(x + 1, y, 0f), Walls.WallTypes.top_right_corner),
                        Tuple.Create(new Vector3(x + 1, y - 1, 0f), Walls.WallTypes.top_open_side),
                    });
            }

            // Room of shape
            //    [ ]
            // [s][ ] 

            else if (x < floorWidth - 1 && y < floorHeight - 1 && rooms[x + 1, y] == null && rooms[x + 1, y + 1] == null)
            {
                possibleRooms.Add(new List<Tuple<Vector3, Walls.WallTypes>>(){
                        Tuple.Create(new Vector3(x, y, 0f), Walls.WallTypes.right_open_side),
                        Tuple.Create(new Vector3(x + 1, y, 0f), Walls.WallTypes.bottom_right_corner),
                        Tuple.Create(new Vector3(x + 1, y + 1, 0f), Walls.WallTypes.bottom_open_side),
                    });
            }

            // Room of shape
            // [ ]
            // [s][ ]

            else if (x < floorWidth - 1 && y < floorHeight - 1 && rooms[x, y + 1] == null && rooms[x + 1, y] == null)
            {
                possibleRooms.Add(new List<Tuple<Vector3, Walls.WallTypes>>(){
                        Tuple.Create(new Vector3(x, y, 0f), Walls.WallTypes.bottom_left_corner),
                        Tuple.Create(new Vector3(x, y + 1, 0f), Walls.WallTypes.bottom_open_side),
                        Tuple.Create(new Vector3(x + 1, y, 0f), Walls.WallTypes.left_open_side),
                    });
            }

            // Room of shape
            // [ ]
            // [ ]
            // [s]

            else if (y < floorHeight - 2 && rooms[x, y + 1] == null && rooms[x, y + 2] == null)
            {
                possibleRooms.Add(new List<Tuple<Vector3, Walls.WallTypes>>(){
                        Tuple.Create(new Vector3(x, y, 0f), Walls.WallTypes.top_open_side),
                        Tuple.Create(new Vector3(x, y + 1, 0f), Walls.WallTypes.vertical_open),
                        Tuple.Create(new Vector3(x, y + 2, 0f), Walls.WallTypes.bottom_open_side),
                    });
            }

            // Room of shape 
            // [s][ ][ ]

            else if (x < floorWidth - 2 && rooms[x + 1, y] == null && rooms[x + 2, y] == null)
            {
                possibleRooms.Add(new List<Tuple<Vector3, Walls.WallTypes>>(){
                        Tuple.Create(new Vector3(x, y, 0f), Walls.WallTypes.right_open_side),
                        Tuple.Create(new Vector3(x + 1, y, 0f), Walls.WallTypes.horizontal_open),
                        Tuple.Create(new Vector3(x + 2, y, 0f), Walls.WallTypes.left_open_side),
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
        public static List<List<Tuple<Vector3, Walls.WallTypes>>> GetPossibleSize2Rooms(Vector3 initialPosition, Cell[,] rooms, int floorWidth, int floorHeight)
        {
            List<List<Tuple<Vector3, Walls.WallTypes>>> possibleRooms = new List<List<Tuple<Vector3, Walls.WallTypes>>>();
            List<Vector3> unpopulatedCells = GetUnpopulatedAdjacentCells(initialPosition, rooms, floorWidth, floorHeight);

            foreach (Vector3 pos in unpopulatedCells)
            {
                // Room of shape
                // [ ]
                // [s]
                if (pos.x == initialPosition.x && pos.y == (initialPosition.y + 1))
                {
                    possibleRooms.Add(new List<Tuple<Vector3, Walls.WallTypes>> {
                        Tuple.Create(initialPosition, Walls.WallTypes.top_open_side),
                        Tuple.Create(pos, Walls.WallTypes.bottom_open_side)
                    });
                }
                // Room of shape
                // [s][ ]
                if (pos.x == (initialPosition.x + 1) && pos.y == initialPosition.y)
                {
                    possibleRooms.Add(new List<Tuple<Vector3, Walls.WallTypes>> {
                        Tuple.Create(initialPosition, Walls.WallTypes.right_open_side),
                        Tuple.Create(pos, Walls.WallTypes.left_open_side)
                    });
                }
            }

            return possibleRooms;
        }


        public static readonly string[,] size2_standard_h = new string[roomWidth * 2, roomHeight]{{"F","F","F","F","F","F","F","F","F","F"},
                                                                                                     {"F","F","F","F","F","F","F","F","F","F"},
                                                                                                     {"F","F","F","F","F","F","F","F","F","F"},
                                                                                                     {"F","F","F","F","F","F","F","F","F","F"},
                                                                                                     {"F","F","F","F","F","F","F","F","F","F"},
                                                                                                     {"F","F","F","F","F","F","F","F","F","F"},
                                                                                                     {"F","F","F","F","F","F","F","F","F","F"},
                                                                                                     {"F","F","F","F","F","F","F","F","F","F"},
                                                                                                     {"F","F","F","F","F","F","F","F","F","F"},
                                                                                                     {"F","F","F","F","F","F","F","F","F","F"},
                                                                                                    // === Bottom half will appear to right === //
                                                                                                     {"F","F","F","F","F","F","F","F","F","F"},
                                                                                                     {"F","F","F","F","F","F","F","F","F","F"},
                                                                                                     {"F","F","F","F","F","F","F","F","F","F"},
                                                                                                     {"F","F","F","F","F","F","F","F","F","F"},
                                                                                                     {"F","F","F","F","F","F","F","F","F","F"},
                                                                                                     {"F","F","F","F","F","F","F","F","F","F"},
                                                                                                     {"F","F","F","F","F","F","F","F","F","F"},
                                                                                                     {"F","F","F","F","F","F","F","F","F","F"},
                                                                                                     {"F","F","F","F","F","F","F","F","F","F"},
                                                                                                     {"F","F","F","F","F","F","F","F","F","F"}};

        public static readonly string[,] size2_standard_v = new string[roomWidth, roomHeight * 2]{{"F","F","F","F","F","F","F","F","F","F",/* Right */"F","F","F","F","F","F","F","F","F","F"},
                                                                                                     {"F","F","F","F","F","F","F","F","F","F",/* Side  */"F","F","F","F","F","F","F","F","F","F"},
                                                                                                     {"F","F","F","F","F","F","F","F","F","F",/* Will  */"F","F","F","F","F","F","F","F","F","F"},
                                                                                                     {"F","F","F","F","F","F","F","F","F","F",/* Be    */"F","F","F","F","F","F","F","F","F","F"},
                                                                                                     {"F","F","F","F","F","F","F","F","F","F",/* Above */"F","F","F","F","F","F","F","F","F","F"},
                                                                                                     {"F","F","F","F","F","F","F","F","F","F",/*       */"F","F","F","F","F","F","F","F","F","F"},
                                                                                                     {"F","F","F","F","F","F","F","F","F","F",/*       */"F","F","F","F","F","F","F","F","F","F"},
                                                                                                     {"F","F","F","F","F","F","F","F","F","F",/*       */"F","F","F","F","F","F","F","F","F","F"},
                                                                                                     {"F","F","F","F","F","F","F","F","F","F",/*       */"F","F","F","F","F","F","F","F","F","F"},
                                                                                                     {"F","F","F","F","F","F","F","F","F","F",/*       */"F","F","F","F","F","F","F","F","F","F"}};


        public static readonly string[,] size1_standard = new string[roomWidth, roomHeight]{{"F","F","F","F","F","F","F","F","F","F"},
                                                                                            {"F","F","F","F","F","F","F","F","F","F"},
                                                                                            {"F","F","F","F","F","F","F","F","F","F"},
                                                                                            {"F","F","F","F","F","F","F","F","F","F"},
                                                                                            {"F","F","F","F","F","F","F","F","F","F"},
                                                                                            {"F","F","F","F","F","F","F","F","F","F"},
                                                                                            {"F","F","F","F","F","F","F","F","F","F"},
                                                                                            {"F","F","F","F","F","F","F","F","F","F"},
                                                                                            {"F","F","F","F","F","F","F","F","F","F"},
                                                                                            {"F","F","F","F","F","F","F","F","F","F"}};
    }
}