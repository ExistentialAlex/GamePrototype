using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rand = UnityEngine.Random;
using GameGeneration.Rooms;

namespace GameGeneration
{
    public class Floor
    {
        public Transform transform { get; set; }
        public Room[,] rooms { get; set; } // [,] Designates a 2D array
        public List<StairRoom> stairs { get; set; }
        public int floorNo { get; set; }
        public int levelNo { get; set; }
        public int startX { get; set; }
        public int startY { get; set; }
        public int floorWidth { get; set; }
        public int floorHeight { get; set; }
        public int current3Rooms { get; set; }
        public bool containsShop { get; set; }
        public bool containsSecret { get; set; }
        public int noSecrets { get; set; }
        public int emptyCells { get; set; }
        public int max3Rooms { get; set; }
        public DoorConfig doorConfig { get; set; }

        #region Tiles

        public GameObject[] floorTiles;
        public GameObject[] bossTiles;
        public GameObject[] shopTiles;
        public GameObject[] secretTiles;
        public GameObject[] stairTiles;
        public GameObject emptyTile;
        public GameObject entranceTile;
        public GameObject[] wallTiles;

        #endregion Tiles

        /// <summary>
        /// Setup the parameters for the current floor
        /// </summary>
        /// <param name="startX">The Start X Position</param>
        /// <param name="startY">The Start Y Position</param>
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

            current3Rooms = 0;
            noSecrets = containsSecret ? rand.Range(1, 2) : 0; // returns either 1 or 2;

            // Create the floor to assign everything to
            transform = new GameObject("Floor_" + levelNo + "_" + floorNo).transform;

            // Create a new room
            rooms = new Room[floorWidth, floorHeight];
            stairs = new List<StairRoom>();
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
            roomGenerator.SetupRoom(room, transform, startX, startY);
        }
    }

    public static class FloorGenerator
    {
        /// <summary>
        /// Gets a random empty cell in the current floor
        /// </summary>
        /// <param name="cells">The cells to get a random value from</param>
        /// <param name="x">The x coordinate of the new cell</param>
        /// <param name="y">The y coordinate of the new cell</param>
        public static void GetRandomEmptyRoom(Floor floor, out int x, out int y)
        {
            do
            {
                x = rand.Range(0, floor.floorWidth);
                y = rand.Range(0, floor.floorHeight);
            }
            while (floor.rooms[x, y] != null);
        }

        /// <summary>
        /// Checks to see if a cell within the current floor is next to any adjacent cells
        /// </summary>
        /// <param name="position">The position to check</param>
        /// <returns></returns>
        public static bool AreAnyAdjacentRoomsPopulated(Floor floor, Vector3 position)
        {
            // if < 8, then at least one cell populated
            return Rooms.Rooms.GetUnpopulatedAdjacentRooms(position, floor.rooms, floor.floorWidth, floor.floorHeight).Count < 8;
        }

        /// <summary>
        /// Converts the current vector3 to a relevant vector3 based on the floors start X and Y position
        /// </summary>
        /// <param name="position">The position to convert</param>
        /// <returns></returns>
        public static Vector3 ConvertToRelevantCoordinate(Floor floor, Vector3 position)
        {
            position.x = Convert.ToInt32(position.x) + floor.startX;
            position.y = Convert.ToInt32(position.y) + floor.startY;
            return position;
        }

        public static void GenerateFloor(Floor floor)
        {
            // === Add Boss Room === //
            if (floor.floorNo == floor.levelNo)
            {
                Debug.Log("=== Adding Boss Room ===");
                int x = rand.Range(0, floor.floorWidth - 1); // will never be last column or row
                int y = rand.Range(0, floor.floorHeight - 1);

                AddRoom(floor, new BossRoom(new Vector3(x, y, 0f), Walls.BottomLeftCorner(), floor.bossTiles[2]));
                AddRoom(floor, new BossRoom(new Vector3(x + 1, y, 0f), Walls.BottomRightCorner(), floor.bossTiles[3]));
                AddRoom(floor, new BossRoom(new Vector3(x, y + 1, 0f), Walls.TopLeftCorner(), floor.bossTiles[0]));
                AddRoom(floor, new BossRoom(new Vector3(x + 1, y + 1, 0f), Walls.TopRightCorner(), floor.bossTiles[1]));
            }

            // === Add Entrance === //
            if (floor.floorNo == 0)
            {
                Debug.Log("=== Adding Entrance Room ===");
                GetRandomEmptyRoom(floor, out int x, out int y);
                AddRoom(floor, new EntranceRoom(new Vector3(x, y, 0f), Walls.AllWalls(), floor.entranceTile));
            }

            // === Add Stairs === //
            if (floor.levelNo != 0)
            {
                if (floor.floorNo == 0 || floor.floorNo == floor.levelNo)
                {
                    AddStair(floor);
                }
                else
                {
                    // If it's a middle floor, add 2 stairs
                    AddStair(floor);
                    AddStair(floor);
                }
                Debug.Log("=== Adding Stairs ===");
            }

            // === Add Shop === //
            if (floor.containsShop)
            {
                Debug.Log("=== Adding Shop ===");
                GetRandomEmptyRoom(floor, out int x, out int y);
                AddRoom(floor, new ShopRoom(new Vector3(x, y, 0f), Walls.AllWalls(), floor.shopTiles[0]));
            }

            // === Add Secrets === //
            if (floor.containsSecret) Debug.Log("=== Adding Secret Rooms ===");
            for (int i = 0; i < floor.noSecrets; i++)
            {
                GetRandomEmptyRoom(floor, out int x, out int y);
                AddRoom(floor, new SecretRoom(new Vector3(x, y, 0f), Walls.AllWalls(), floor.secretTiles[0]));
            }

            // === Add Empty Cells === //
            Debug.Log("=== Adding Empty Rooms ===");
            for (int i = 0; i < floor.emptyCells; i++)
            {
                int finalX = -1;
                int finalY = -1;
                int retries = 0;
                while (finalX == -1 && finalY == -1 && retries < 3)
                {
                    GetRandomEmptyRoom(floor, out int x, out int y);
                    retries++;
                    if (!AreAnyAdjacentRoomsPopulated(floor, new Vector3(x, y)) || retries == 3)
                    {
                        finalX = x;
                        finalY = y;
                    }
                }

                AddRoom(floor, new EmptyRoom(new Vector3(finalX, finalY, 0f), Walls.AllWalls(), floor.emptyTile));
            }

            // Loop through remaining spaces to place new rooms
            Debug.Log("=== Adding Remaining Rooms ===");
            for (int x = 0; x < floor.floorWidth; x++)
            {
                for (int y = 0; y < floor.floorHeight; y++)
                {
                    if (floor.rooms[x, y] == null)
                    {
                        AddStandardRoom(floor, new Vector3(x, y, 0f));
                    }
                }
            }

            // Add a door to each room
            for (int x = 0; x < floor.floorWidth; x++)
            {
                for (int y = 0; y < floor.floorHeight; y++)
                {
                    if (floor.rooms[x, y].type != Room.RoomType.empty)
                    {
                        Door.AddDoor(floor.doorConfig, floor.rooms, x, y, floor.floorWidth - 1, floor.floorHeight - 1);
                    }

                    // If it's an entrance room, we need to add 2 more doors
                    if (floor.rooms[x, y].type == Room.RoomType.entrance)
                    {
                        Door.AddDoor(floor.doorConfig, floor.rooms, x, y, floor.floorWidth - 1, floor.floorHeight - 1);
                        Door.AddDoor(floor.doorConfig, floor.rooms, x, y, floor.floorWidth - 1, floor.floorHeight - 1);
                    }
                }
            }
        }

        private static void AddStair(Floor floor)
        {
            GetRandomEmptyRoom(floor, out int x, out int y);
            floor.stairs.Add((StairRoom)AddRoom(floor, new StairRoom(new Vector3(x, y, 0f), Walls.AllWalls(), floor.stairTiles[0])));
        }

        public static void AddStandardRoom(Floor floor, Vector3 position)
        {
            List<List<Tuple<Vector3, List<Walls.WallTypes>>>> possibleRooms = new List<List<Tuple<Vector3, List<Walls.WallTypes>>>>();

            // === Create a Room of Size 3 === //
            if (Convert.ToBoolean(rand.Range(0, 2)) && floor.current3Rooms < floor.max3Rooms)
            {
                possibleRooms.AddRange(Rooms.Rooms.GetPossibleSize3Rooms(position, floor.rooms, floor.floorWidth, floor.floorHeight));
            }

            if (possibleRooms.Count > 0)
            {
                // Pick a random room from our list of possible rooms
                int randomNo = rand.Range(0, possibleRooms.Count - 1);
                List<Tuple<Vector3, List<Walls.WallTypes>>> chosenRoom = possibleRooms[randomNo];
                foreach (Tuple<Vector3, List<Walls.WallTypes>> pos in chosenRoom)
                {
                    AddRoom(floor, new StandardRoom(pos.Item1, pos.Item2, floor.floorTiles[2]));
                }

                // Increment the number of size 3 rooms
                floor.current3Rooms++;

                return;
            }

            // === Room of Size 2 === //
            possibleRooms.AddRange(Rooms.Rooms.GetPossibleSize2Rooms(position, floor.rooms, floor.floorWidth, floor.floorHeight));

            if (possibleRooms.Count > 0)
            {
                // Pick a random room from our list of possible rooms
                int randomNo = rand.Range(0, possibleRooms.Count - 1);
                List<Tuple<Vector3, List<Walls.WallTypes>>> chosenRoom = possibleRooms[randomNo];
                foreach (Tuple<Vector3, List<Walls.WallTypes>> pos in chosenRoom)
                {
                    AddRoom(floor, new StandardRoom(pos.Item1, pos.Item2, floor.floorTiles[1]));
                }

                return;
            }

            // === Create a standard room === //
            AddRoom(floor, new StandardRoom(position, Walls.AllWalls(), floor.floorTiles[0]));
            return;
        }

        /// <summary>
        /// Add a room at the specified coordinate with the specified tile
        /// </summary>
        /// <param name="position">Tuple of Vector3 and WallType to determine how to add walls</param>
        /// <param name="toInstantiate"></param>
        /// <param name="cellType"></param>
        public static Room AddRoom(Floor floor, Room room)
        {
            // Get x & y for cell
            int x = Convert.ToInt32(room.vectorPosition.x);
            int y = Convert.ToInt32(room.vectorPosition.y);
            floor.rooms[x, y] = room;
            return room;
        }
    }
}
