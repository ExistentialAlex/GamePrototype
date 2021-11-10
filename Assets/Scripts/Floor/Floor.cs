using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rand = UnityEngine.Random;

namespace GameGeneration
{
    public class Floor
    {
        private Transform floor { get; set; }
        private Room[,] rooms { get; set; } // Designates a 2D array
        private List<Vector3> gridPositions = new List<Vector3>();
        private int floorNo { get; set; }
        private int levelNo { get; set; }
        private int startX { get; set; }
        private int startY { get; set; }
        private int floorWidth { get; set; }
        private int floorHeight { get; set; }
        private int current3Rooms { get; set; }
        private bool containsShop { get; set; }
        private bool containsSecret { get; set; }
        private int noSecrets { get; set; }
        private int emptyCells { get; set; }
        private int max3Rooms { get; set; }
        private DoorConfig doorConfig { get; set; }

        #region Tiles

        private GameObject[] floorTiles;
        private GameObject[] bossTiles;
        private GameObject[] shopTiles;
        private GameObject[] secretTiles;
        private GameObject[] stairTiles;
        private GameObject emptyTile;
        private GameObject entranceTile;
        private GameObject[] wallTiles;

        #endregion Tiles

        /// <summary>
        /// Setup the parameters for the current floor
        /// </summary>
        /// <param name="startX">The Start X Position</param>
        /// <param name="startY">The Start Y Position</param>
        /// <param name="floorWidth">Width of the floor</param>
        /// <param name="floorHeight">Height of the floor</param>
        /// <param name="floorNo">Current floor number</param>
        /// <param name="levelNo">Current level number</param>
        /// <param name="containsShop">Whether the floor should contain a shop</param>
        /// <param name="containsSecret">Whether the floor should contain secrets</param>
        public Floor(FloorConfig config, int startX, int startY, int floorNo, int levelNo, bool containsShop, bool containsSecret)
        {
            this.startX = startX;
            this.startY = startY;
            this.floorNo = floorNo;
            this.levelNo = levelNo;
            this.containsSecret = containsSecret;
            this.containsShop = containsShop;
            current3Rooms = 0;
            if (containsSecret)
            {
                noSecrets = rand.Range(1, 2); // returns either 1 or 2;
            }

            this.floorWidth = config.floorWidth;
            this.floorHeight = config.floorHeight;
            this.floorTiles = config.floorTiles;
            this.bossTiles = config.bossTiles;
            this.shopTiles = config.shopTiles;
            this.secretTiles = config.secretTiles;
            this.stairTiles = config.stairTiles;
            this.emptyTile = config.emptyTile;
            this.entranceTile = config.entranceTile;
            this.wallTiles = config.wallTiles;
            this.emptyCells = config.emptyCells;
            this.max3Rooms = config.max3Rooms;
            this.doorConfig = config.doorConfig;

            // Create the floor to assign everything to
            floor = new GameObject("Floor_" + levelNo + "_" + floorNo).transform;

            // Create a new room
            rooms = new Room[floorWidth, floorHeight];
        }

        /// <summary>
        /// Gets a random empty cell in the current floor
        /// </summary>
        /// <param name="cells">The cells to get a random value from</param>
        /// <param name="x">The x coordinate of the new cell</param>
        /// <param name="y">The y coordinate of the new cell</param>
        public void GetRandomEmptyCell(Room[,] cells, out int x, out int y)
        {
            do
            {
                x = rand.Range(0, floorWidth);
                y = rand.Range(0, floorHeight);
            }
            while (cells[x, y] != null);
        }

        /// <summary>
        /// Checks to see if a cell within the current floor is next to any adjacent cells
        /// </summary>
        /// <param name="position">The position to check</param>
        /// <returns></returns>
        public bool AreAnyAdjacentCellsPopulated(Vector3 position)
        {
            // if < 8, then at least one cell populated
            return Rooms.GetUnpopulatedAdjacentCells(position, rooms, floorWidth, floorHeight).Count < 8;
        }

        /// <summary>
        /// Converts the current vector3 to a relevant vector3 based on the floors start X and Y position
        /// </summary>
        /// <param name="position">The position to convert</param>
        /// <returns></returns>
        public Vector3 ConvertToRelevantCoordinate(Vector3 position)
        {
            position.x = Convert.ToInt32(position.x) + startX;
            position.y = Convert.ToInt32(position.y) + startY;
            return position;
        }

        public void GenerateFloor()
        {
            // === Add Boss Room === //
            if (floorNo == levelNo)
            {
                Debug.Log("=== Adding Boss Room ===");
                int x = rand.Range(0, floorWidth - 1); // will never be last column or row
                int y = rand.Range(0, floorHeight - 1);

                AddRoom(Tuple.Create(new Vector3(x, y, 0f), Walls.WallTypes.bottom_left_corner), bossTiles[2], Room.CellType.boss);
                AddRoom(Tuple.Create(new Vector3(x + 1, y, 0f), Walls.WallTypes.bottom_right_corner), bossTiles[3], Room.CellType.boss);
                AddRoom(Tuple.Create(new Vector3(x, y + 1, 0f), Walls.WallTypes.top_left_corner), bossTiles[0], Room.CellType.boss);
                AddRoom(Tuple.Create(new Vector3(x + 1, y + 1, 0f), Walls.WallTypes.top_right_corner), bossTiles[1], Room.CellType.boss);
            }

            // === Add Entrance === //
            if (floorNo == 0)
            {
                Debug.Log("=== Adding Entrance Room ===");
                GetRandomEmptyCell(rooms, out int x, out int y);
                AddRoom(Tuple.Create(new Vector3(x, y, 0f), Walls.WallTypes.all_walls), entranceTile, Room.CellType.entrance);
                // TODO - Make sure player is rendered after everything else
            }

            // === Add Stairs === //
            if (levelNo != 0)
            {
                if (floorNo == 0 || floorNo == levelNo)
                {
                    AddStair();
                }
                else
                {
                    // If it's a middle floor, add 2 stairs
                    AddStair();
                    AddStair();
                }
                Debug.Log("=== Adding Stairs ===");
            }

            // === Add Shop === //
            if (containsShop)
            {
                Debug.Log("=== Adding Shop ===");
                GetRandomEmptyCell(rooms, out int x, out int y);
                AddRoom(Tuple.Create(new Vector3(x, y, 0f), Walls.WallTypes.all_walls), shopTiles[0], Room.CellType.shop);
            }

            // === Add Secrets === //
            if (containsSecret) Debug.Log("=== Adding Secret Rooms ===");
            for (int i = 0; i < noSecrets; i++)
            {
                GetRandomEmptyCell(rooms, out int x, out int y);
                AddRoom(Tuple.Create(new Vector3(x, y, 0f), Walls.WallTypes.all_walls), secretTiles[0], Room.CellType.secret);
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
                    GetRandomEmptyCell(rooms, out int x, out int y);
                    retries++;
                    if (!AreAnyAdjacentCellsPopulated(new Vector3(x, y)) || retries == 3)
                    {
                        finalX = x;
                        finalY = y;
                    }
                }

                AddRoom(Tuple.Create(new Vector3(finalX, finalY, 0f), Walls.WallTypes.all_walls), emptyTile, Room.CellType.empty);
            }

            // Loop through remaining spaces to place new rooms
            Debug.Log("=== Adding Remaining Rooms ===");
            for (int x = 0; x < floorWidth; x++)
            {
                for (int y = 0; y < floorHeight; y++)
                {
                    if (rooms[x, y] == null)
                    {
                        AddStandardRoom(new Vector3(x, y, 0f));
                    }
                }
            }

            for (int x = 0; x < floorWidth; x++)
            {
                for (int y = 0; y < floorHeight; y++)
                {
                    if (rooms[x, y].type != Room.CellType.empty)
                    {
                        Door.AddDoor(doorConfig, rooms, x, y, floorWidth - 1, floorHeight - 1);
                    }
                }
            }
        }

        private void AddStair()
        {
            GetRandomEmptyCell(rooms, out int x, out int y);
            AddRoom(Tuple.Create(new Vector3(x, y, 0f), Walls.WallTypes.all_walls), stairTiles[0], Room.CellType.stair);
        }

        public void InstantiateFloor(RoomGenerator roomGenerator)
        {
            for (int x = 0; x < floorWidth; x++)
            {
                for (int y = 0; y < floorHeight; y++)
                {
                    InstantiateRoom(roomGenerator, rooms[x, y]);
                }
            }
        }

        private void InstantiateRoom(RoomGenerator roomGenerator, Room room)
        {
            roomGenerator.SetupRoom(room, floor, startX, startY);
        }

        public void AddStandardRoom(Vector3 position)
        {
            List<List<Tuple<Vector3, Walls.WallTypes>>> possibleRooms = new List<List<Tuple<Vector3, Walls.WallTypes>>>();

            // === Create a Room of Size 3 === //
            if (Convert.ToBoolean(rand.Range(0, 2)) && current3Rooms < max3Rooms)
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
                    AddRoom(pos, floorTiles[2], Room.CellType.room);
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
                    AddRoom(pos, floorTiles[1], Room.CellType.room);
                }

                return;
            }

            // === Create a standard room === //
            AddRoom(Tuple.Create(position, Walls.WallTypes.all_walls), floorTiles[0], Room.CellType.room);
            return;
        }

        /// <summary>
        /// Add a room at the specified coordinate with the specified tile
        /// </summary>
        /// <param name="position">Tuple of Vector3 and WallType to determine how to add walls</param>
        /// <param name="toInstantiate"></param>
        /// <param name="cellType"></param>
        public void AddRoom(Tuple<Vector3, Walls.WallTypes> position, GameObject toInstantiate, Room.CellType cellType)
        {
            // Get x & y for cell
            int x = Convert.ToInt32(position.Item1.x);
            int y = Convert.ToInt32(position.Item1.y);
            rooms[x, y] = new Room(cellType, position.Item1, position.Item2, toInstantiate);
        }
    }
}
