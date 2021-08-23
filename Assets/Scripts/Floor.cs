using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rand = UnityEngine.Random;

namespace GameGeneration
{
    public class Floor : MonoBehaviour
    {
        private Transform floor { get; set; }
        private List<Vector3> gridPositions = new List<Vector3>();
        private int floorNo { get; set; }
        private int levelNo { get; set; }
        private int floorWidth { get; set; }
        private int floorHeight { get; set; }
        private Cell[,] rooms { get; set; } // Designates a 2D array
        public int emptyCells = 3;
        public int max3Rooms = 3;
        private int current3Rooms { get; set; }
        private bool containsShop { get; set; }
        private bool containsSecret { get; set; }
        private int noSecrets { get; set; }

        #region Tiles
        public GameObject[] floorTiles;
        public GameObject[] bossTiles;
        public GameObject[] shopTiles;
        public GameObject[] secretTiles;
        public GameObject[] stairTiles;
        public GameObject emptyTile;
        public GameObject entranceTile;
        public GameObject[] wallTiles;

        #endregion

        /// <summary>
        /// Setup the parameters for the current floor
        /// </summary>
        /// <param name="floorWidth">Width of the floor</param>
        /// <param name="floorHeight">Height of the floor</param>
        /// <param name="floorNo">Current floor number</param>
        /// <param name="levelNo">Current level number</param>
        /// <param name="containsShop">Whether the floor should contain a shop</param>
        /// <param name="containsSecret">Whether the floor should contain secrets</param>
        public void SetupFloor(int floorWidth, int floorHeight, int floorNo, int levelNo, bool containsShop, bool containsSecret)
        {
            this.floorWidth = floorWidth;
            this.floorHeight = floorHeight;
            this.floorNo = floorNo;
            this.levelNo = levelNo;
            this.containsSecret = containsSecret;
            this.containsShop = containsShop;
            current3Rooms = 0;
            if (containsSecret)
            {
                noSecrets = rand.Range(1, 2); // returns either 1 or 2;
            }
            InitGrid();
            GenerateFloor();
        }

        /// <summary>
        /// Initialise the grid for the current floor
        /// </summary>
        public void InitGrid()
        {
            floor = new GameObject("Floor").transform;

            for (int x = 0; x < floorWidth * Rooms.roomWidth; x++)
            {
                for (int y = 0; y < floorWidth * Rooms.roomHeight; y++)
                {
                    gridPositions.Add(new Vector3(x, y, 0f));
                }
            }
        }

        public void PrintFloor()
        {
            // Print each floor for the level
            for (int y = 0; y < floorWidth; y++)
            {
                string row = "";
                for (int x = 0; x < floorHeight; x++)
                {
                    row += "[" + rooms[x, y].ToString().Substring(0, 2) + "]";
                }
                Debug.Log(row);
            }
            Debug.Log("\n");
        }

        /// <summary>
        /// Gets a random empty cell in the current floor
        /// </summary>
        /// <param name="x">The x coordinate of the new cell</param>
        /// <param name="y">The y coordinate of the new cell</param>
        public void GetRandomEmptyCell(out int x, out int y)
        {
            do
            {   // Use -1 here as the range is inclusive
                x = rand.Range(0, floorWidth);
                y = rand.Range(0, floorHeight);
            }
            while (rooms[x, y] != null);
        }

        /// <summary>
        /// Checks to see if a cell within the current floor is next to any adjacent cells
        /// </summary>
        /// <param name="x">The x coordinate of the cell</param>
        /// <param name="y">The y coordinate of the cell</param>
        /// <returns></returns>
        public bool AreAnyAdjacentCellsPopulated(Vector3 position)
        {
            // if < 8, then at least one cell populated
            return Rooms.GetUnpopulatedAdjacentCells(position, rooms, floorWidth, floorHeight).Count < 8;
        }

        public void GenerateFloor()
        {
            rooms = new Cell[floorWidth, floorHeight];

            // === Add Boss Room === //
            if (floorNo == levelNo)
            {
                Debug.Log("=== Adding Boss Room ===");
                int x = rand.Range(0, floorWidth - 1); // will never be last column or row
                int y = rand.Range(0, floorHeight - 1);


                AddRoom(Tuple.Create(new Vector3(x, y, 0f), Walls.WallTypes.bottom_left_corner), bossTiles[2], Cell.CellType.boss);
                AddRoom(Tuple.Create(new Vector3(x + 1, y, 0f), Walls.WallTypes.bottom_right_corner), bossTiles[3], Cell.CellType.boss);
                AddRoom(Tuple.Create(new Vector3(x, y + 1, 0f), Walls.WallTypes.top_left_corner), bossTiles[0], Cell.CellType.boss);
                AddRoom(Tuple.Create(new Vector3(x + 1, y + 1, 0f), Walls.WallTypes.top_right_corner), bossTiles[1], Cell.CellType.boss);
            }

            // === Add Entrance === //
            if (floorNo == 0)
            {
                Debug.Log("=== Adding Entrance Room ===");
                GetRandomEmptyCell(out int x, out int y);

                AddRoom(Tuple.Create(new Vector3(x, y, 0f), Walls.WallTypes.all_walls), entranceTile, Cell.CellType.entrance);
            }


            // === Add Stairs === //
            if (levelNo != 0)
            {
                Debug.Log("=== Adding Stairs ===");
                GetRandomEmptyCell(out int x, out int y);

                AddRoom(Tuple.Create(new Vector3(x, y, 0f), Walls.WallTypes.all_walls), stairTiles[0], Cell.CellType.stair);
            }

            // === Add Shop === //
            if (containsShop)
            {
                Debug.Log("=== Adding Shop ===");
                GetRandomEmptyCell(out int x, out int y);
                AddRoom(Tuple.Create(new Vector3(x, y, 0f), Walls.WallTypes.all_walls), shopTiles[0], Cell.CellType.shop);
            }

            // === Add Secrets === //
            if (containsSecret) Debug.Log("=== Adding Secret Rooms ===");
            for (int i = 0; i < noSecrets; i++)
            {
                GetRandomEmptyCell(out int x, out int y);
                AddRoom(Tuple.Create(new Vector3(x, y, 0f), Walls.WallTypes.all_walls), secretTiles[0], Cell.CellType.secret);
            }

            // === Add Empty Cells === //
            Debug.Log("=== Adding Empty Rooms ===");
            for (int i = 0; i < emptyCells; i++)
            {
                int finalX = -1;
                int finalY = -1;
                int retries = 0;
                while (finalX == -1 && finalY == -1 && retries < 3)
                {
                    GetRandomEmptyCell(out int x, out int y);
                    retries++;
                    if (!AreAnyAdjacentCellsPopulated(new Vector3(x, y)) || retries == 3)
                    {
                        finalX = x;
                        finalY = y;
                    }
                }

                AddRoom(Tuple.Create(new Vector3(finalX, finalY, 0f), Walls.WallTypes.all_walls), emptyTile, Cell.CellType.empty);
            }

            // Loop through remaining spaces to place new rooms
            Debug.Log("=== Adding Remaining Rooms ===");
            for (int x = 0; x < floorWidth; x++)
            {
                for (int y = 0; y < floorHeight; y++)
                {
                    if (rooms[x, y] == null)
                    {
                        CreateStandardRoom(new Vector3(x, y, 0f));
                    }
                }
            }
        }

        public void CreateStandardRoom(Vector3 position)
        {
            List<List<Tuple<Vector3, Walls.WallTypes>>> possibleRooms = new List<List<Tuple<Vector3, Walls.WallTypes>>>();

            // === Create a Room of Size 3 === //
            if (current3Rooms < max3Rooms)
            {
                possibleRooms.AddRange(Rooms.GetPossibleSize3Rooms(position, rooms, floorWidth, floorHeight));
            }

            if (possibleRooms.Count > 0)
            {
                // Pick a random room from our list of possible rooms
                int randomNo = rand.Range(0, possibleRooms.Count - 1);
                List<Tuple<Vector3, Walls.WallTypes>> chosenRoom = possibleRooms[randomNo];
                foreach (Tuple<Vector3, Walls.WallTypes> pos in chosenRoom)
                {
                    AddRoom(pos, floorTiles[2], Cell.CellType.room);
                    AddWall(pos);
                }

                // Increment the number of size 3 rooms
                current3Rooms++;

                return;
            }

            // === Room of Size 2 === //
            possibleRooms.AddRange(Rooms.GetPossibleSize2Rooms(position, rooms, floorWidth, floorHeight));

            if (possibleRooms.Count > 0)
            {
                // Pick a random room from our list of possible rooms
                int randomNo = rand.Range(0, possibleRooms.Count - 1);
                List<Tuple<Vector3, Walls.WallTypes>> chosenRoom = possibleRooms[randomNo];
                foreach (Tuple<Vector3, Walls.WallTypes> pos in chosenRoom)
                {
                    AddRoom(pos, floorTiles[1], Cell.CellType.room);
                    AddWall(pos);
                }

                return;
            }

            // === Create a standard room === //
            AddRoom(Tuple.Create(position, Walls.WallTypes.all_walls), floorTiles[0], Cell.CellType.room);
            return;


        }

        public void CreateRoom(int startX, int startY, string[,] roomToMake, GameObject tile)
        {
            for (int x = 0; x < roomToMake.GetLength(0); x++)
            {
                for (int y = 0; y < roomToMake.GetLength(1); y++)
                {
                    GameObject toInstantiate;
                    switch (roomToMake[x, y])
                    {
                        case "F":
                            {
                                toInstantiate = tile;
                                break;
                            }
                        default:
                            {
                                toInstantiate = tile;
                                break;
                            }
                    }

                    (Instantiate(toInstantiate, new Vector3(x + (Rooms.roomWidth * startX), y + (Rooms.roomHeight * startY), 0f), Quaternion.identity) as GameObject).transform.SetParent(floor);
                }
            }
        }

        public void AddRoom(Tuple<Vector3, Walls.WallTypes> position, GameObject toInstantiate, Cell.CellType cellType)
        {
            int x = Convert.ToInt32(position.Item1.x);
            int y = Convert.ToInt32(position.Item1.y);
            rooms[x, y] = new Cell(cellType, x, y);
            (Instantiate(toInstantiate, position.Item1, Quaternion.identity) as GameObject).transform.SetParent(floor);

            AddWall(position);
        }

        public void AddWall(Tuple<Vector3, Walls.WallTypes> wall)
        {
            (Instantiate(wallTiles[(int)wall.Item2], wall.Item1, Quaternion.identity) as GameObject).transform.SetParent(floor);
        }

        #region Rooms

        #region Size 3




        public void CreateRoomSize3(int startX, int startY, Rooms.Size3Shapes shape)
        {

        }

        #endregion Size 3


        #region Size 2

        public void CreateRoomSize2(int x, int y, Rooms.Size2Shapes shape)
        {
            string[,] roomToMake = new string[0, 0];

            switch (shape)
            {
                case Rooms.Size2Shapes.horizontal:
                    {
                        roomToMake = Rooms.size2_standard_h;
                        break;
                    }
                case Rooms.Size2Shapes.vertical:
                    {
                        roomToMake = Rooms.size2_standard_v;
                        break;
                    }
            }

            CreateRoom(x, y, roomToMake, floorTiles[1]);
        }

        #endregion Size 2

        #region Size 1


        public void CreateRoomSize1(int x, int y)
        {
            string[,] roomToMake = Rooms.size1_standard;
            CreateRoom(x, y, roomToMake, floorTiles[1]);
        }

        #endregion Size 1
        #endregion Rooms
    }
}