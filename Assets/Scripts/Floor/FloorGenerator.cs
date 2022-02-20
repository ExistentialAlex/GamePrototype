namespace Prototype.GameGeneration
{
    using System;
    using System.Collections.Generic;
    using Prototype.GameGeneration.Rooms;
    using Prototype.Utilities;
    using UnityEngine;

    using rand = UnityEngine.Random;

    /// <summary>
    /// Generates a floor based on the input parameters.
    /// </summary>
    public static class FloorGenerator
    {
        /// <summary>
        /// Add a room to the floor.
        /// </summary>
        /// <param name="floor">The floor to add the room to.</param>
        /// <param name="room">The room to add.</param>
        /// <returns>The room that was added.</returns>
        public static Room AddRoom(Floor floor, Room room)
        {
            // Get x & y for cell
            int x = Convert.ToInt32(room.VectorPosition.x);
            int y = Convert.ToInt32(room.VectorPosition.y);
            floor.Rooms[x, y] = room;
            return room;
        }

        /// <summary>
        /// Add a standard room to the floor, the overall room can be made of 1, 2 or 3 rooms and
        /// it's position will be based on the starting vector.
        /// </summary>
        /// <param name="floor">The floor to add the room to.</param>
        /// <param name="position">The starting position.</param>
        public static void AddStandardRoom(Floor floor, Vector3 position)
        {
            List<List<Tuple<Vector3, List<Walls.WallTypes>>>> possibleRooms = new List<List<Tuple<Vector3, List<Walls.WallTypes>>>>();

            // === Create a Room of Size 3 === //
            if (Convert.ToBoolean(rand.Range(0, 2)) && floor.Current3Rooms < floor.FloorConfig.Max3Rooms)
            {
                possibleRooms.AddRange(Rooms.Rooms.GetPossibleSize3Rooms(position, floor.Rooms, floor.FloorConfig.FloorWidth, floor.FloorConfig.FloorHeight));
            }

            if (possibleRooms.Count > 0)
            {
                // Pick a random room from our list of possible rooms
                int randomNo = rand.Range(0, possibleRooms.Count - 1);
                List<Tuple<Vector3, List<Walls.WallTypes>>> chosenRoom = possibleRooms[randomNo];
                foreach (Tuple<Vector3, List<Walls.WallTypes>> pos in chosenRoom)
                {
                    AddRoom(floor, new StandardRoom(pos.Item1, pos.Item2, Utilities.PickRandom(floor.FloorConfig.TripleStandardRoomTemplates)));
                }

                // Increment the number of size 3 rooms
                floor.Current3Rooms++;

                return;
            }

            // === Room of Size 2 === //
            possibleRooms.AddRange(Rooms.Rooms.GetPossibleSize2Rooms(position, floor.Rooms, floor.FloorConfig.FloorWidth, floor.FloorConfig.FloorHeight));

            if (possibleRooms.Count > 0)
            {
                // Pick a random room from our list of possible rooms
                int randomNo = rand.Range(0, possibleRooms.Count - 1);
                List<Tuple<Vector3, List<Walls.WallTypes>>> chosenRoom = possibleRooms[randomNo];
                foreach (Tuple<Vector3, List<Walls.WallTypes>> pos in chosenRoom)
                {
                    AddRoom(floor, new StandardRoom(pos.Item1, pos.Item2, Utilities.PickRandom(floor.FloorConfig.DoubleStandardRoomTemplates)));
                }

                return;
            }

            // === Create a standard room === //
            AddRoom(floor, new StandardRoom(position, Walls.AllWalls(), Utilities.PickRandom(floor.FloorConfig.SingleStandardRoomTemplates)));
            return;
        }

        /// <summary>
        /// Checks to see if a room within the current floor is next to any populated room.
        /// </summary>
        /// <param name="floor">The floor to check.</param>
        /// <param name="position">The position to check.</param>
        /// <returns>True if at least one adjacent room is populated.</returns>
        public static bool AreAnyAdjacentRoomsPopulated(Floor floor, Vector3 position)
        {
            // if < 8, then at least one room populated
            return Rooms.Rooms.GetUnpopulatedAdjacentRooms(position, floor.Rooms, floor.FloorConfig.FloorWidth, floor.FloorConfig.FloorHeight).Count < 8;
        }

        /// <summary>
        /// Generate a floor containing an array of rooms. This will be based on the supplied floor
        /// config when the floor was instantiated.
        /// </summary>
        /// <param name="floor">The floor to generate.</param>
        public static void GenerateFloor(Floor floor)
        {
            // Add Boss Room
            if (floor.FloorNo == floor.LevelNo)
            {
                AddBossRoom(floor);
            }

            // Add Entrance
            if (floor.FloorNo == 0)
            {
                AddEntranceRoom(floor);
            }

            // Add Stairs
            if (floor.LevelNo != 0)
            {
                AddStairRooms(floor);
            }

            // Add Shop
            if (floor.ContainsShop)
            {
                AddShopRoom(floor);
            }

            // Add Secrets
            if (floor.ContainsSecret)
            {
                AddSecretRoom(floor);
            }

            // Add Empty Cells
            AddEmptyRooms(floor);

            // Loop through remaining spaces to place new rooms
            Debug.Log("=== Adding Remaining Rooms ===");
            for (int x = 0; x < floor.FloorConfig.FloorWidth; x++)
            {
                for (int y = 0; y < floor.FloorConfig.FloorHeight; y++)
                {
                    if (floor.Rooms[x, y] == null)
                    {
                        AddStandardRoom(floor, new Vector3(x, y, 0f));
                    }
                }
            }

            // Add a door to each room
            AddDoors(floor);
        }

        /// <summary>
        /// Gets a random empty room in the current floor.
        /// </summary>
        /// <param name="floor">The floor to get a random value from.</param>
        /// <param name="x">The x coordinate of the new cell.</param>
        /// <param name="y">The y coordinate of the new cell.</param>
        public static void GetRandomEmptyRoom(Floor floor, out int x, out int y)
        {
            do
            {
                x = rand.Range(0, floor.FloorConfig.FloorWidth);
                y = rand.Range(0, floor.FloorConfig.FloorHeight);
            }
            while (floor.Rooms[x, y] != null);
        }

        /// <summary>
        /// Add a boss room to the floor.
        /// </summary>
        /// <param name="floor">The floor to add the room to.</param>
        private static void AddBossRoom(Floor floor)
        {
            Debug.Log("=== Adding Boss Room ===");
            int x = rand.Range(0, floor.FloorConfig.FloorWidth - 1); // will never be last column or row
            int y = rand.Range(0, floor.FloorConfig.FloorHeight - 1);

            // TODO - Update boss rooms to pick from list of full boss rooms.
            AddRoom(floor, new BossRoom(new Vector3(x, y, 0f), Walls.BottomLeftCorner(), floor.FloorConfig.BossRoomTemplates[0]));
            AddRoom(floor, new BossRoom(new Vector3(x + 1, y, 0f), Walls.BottomRightCorner(), floor.FloorConfig.BossRoomTemplates[1]));
            AddRoom(floor, new BossRoom(new Vector3(x, y + 1, 0f), Walls.TopLeftCorner(), floor.FloorConfig.BossRoomTemplates[2]));
            AddRoom(floor, new BossRoom(new Vector3(x + 1, y + 1, 0f), Walls.TopRightCorner(), floor.FloorConfig.BossRoomTemplates[3]));
        }

        /// <summary>
        /// Add a door to every room in the floor.
        /// </summary>
        /// <param name="floor">The floor to add the doors to.</param>
        private static void AddDoors(Floor floor)
        {
            for (int x = 0; x < floor.FloorConfig.FloorWidth; x++)
            {
                for (int y = 0; y < floor.FloorConfig.FloorHeight; y++)
                {
                    if (floor.Rooms[x, y].Type != Room.RoomType.empty)
                    {
                        Door.AddDoor(floor.FloorConfig.DoorConfig, floor.Rooms, x, y, floor.FloorConfig.FloorWidth - 1, floor.FloorConfig.FloorHeight - 1);
                    }

                    // If it's an entrance room, we need to add 2 more doors
                    if (floor.Rooms[x, y].Type == Room.RoomType.entrance)
                    {
                        Door.AddDoor(floor.FloorConfig.DoorConfig, floor.Rooms, x, y, floor.FloorConfig.FloorWidth - 1, floor.FloorConfig.FloorHeight - 1);
                        Door.AddDoor(floor.FloorConfig.DoorConfig, floor.Rooms, x, y, floor.FloorConfig.FloorWidth - 1, floor.FloorConfig.FloorHeight - 1);
                    }
                }
            }
        }

        /// <summary>
        /// Add empty rooms to the floor, this helps to shake up the layout.
        /// </summary>
        /// <param name="floor">The floor to add the room(s) to.</param>
        private static void AddEmptyRooms(Floor floor)
        {
            Debug.Log("=== Adding Empty Rooms ===");
            for (int i = 0; i < floor.FloorConfig.EmptyRooms; i++)
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

                AddRoom(floor, new EmptyRoom(new Vector3(finalX, finalY, 0f), Walls.AllWalls(), Utilities.PickRandom(floor.FloorConfig.EmptyRoomTemplates)));
            }
        }

        /// <summary>
        /// Add an entrance to the floor.
        /// </summary>
        /// <param name="floor">The floor to add the room to.</param>
        private static void AddEntranceRoom(Floor floor)
        {
            Debug.Log("=== Adding Entrance Room ===");
            GetRandomEmptyRoom(floor, out int x, out int y);
            AddRoom(floor, new EntranceRoom(new Vector3(x, y, 0f), Walls.AllWalls(), Utilities.PickRandom(floor.FloorConfig.EntranceRoomTemplates)));
        }

        /// <summary>
        /// Add a secret room to the floor.
        /// </summary>
        /// <param name="floor">The floor to add the room(s) to.</param>
        private static void AddSecretRoom(Floor floor)
        {
            Debug.Log("=== Adding Secret Rooms ===");
            for (int i = 0; i < floor.NoSecrets; i++)
            {
                GetRandomEmptyRoom(floor, out int x, out int y);
                AddRoom(floor, new SecretRoom(new Vector3(x, y, 0f), Walls.AllWalls(), Utilities.PickRandom(floor.FloorConfig.SecretRoomTemplates)));
            }
        }

        /// <summary>
        /// Add a shop to the floor.
        /// </summary>
        /// <param name="floor">The floor to add the room to.</param>
        private static void AddShopRoom(Floor floor)
        {
            Debug.Log("=== Adding Shop ===");
            GetRandomEmptyRoom(floor, out int x, out int y);
            AddRoom(floor, new ShopRoom(new Vector3(x, y, 0f), Walls.AllWalls(), Utilities.PickRandom(floor.FloorConfig.ShopRoomTemplates)));
        }

        /// <summary>
        /// Add a stair to the floor with a specific template.
        /// </summary>
        /// <param name="floor">The floor to add the stair to.</param>
        /// <param name="template">The template to use when instantiating the stair.</param>
        private static void AddStair(Floor floor, GameObject template)
        {
            GetRandomEmptyRoom(floor, out int x, out int y);
            floor.Stairs.Add((StairRoom)AddRoom(floor, new StairRoom(new Vector3(x, y, 0f), Walls.AllWalls(), template)));
        }

        /// <summary>
        /// Add stair rooms to the floor.
        /// </summary>
        /// <param name="floor">The floor to add the room(s) to.</param>
        private static void AddStairRooms(Floor floor)
        {
            Debug.Log("=== Adding Stairs ===");
            if (floor.FloorNo == 0)
            {
                AddStair(floor, Utilities.PickRandom(floor.FloorConfig.StairRoomTemplates));
            }
            else if (floor.FloorNo == floor.LevelNo)
            {
                AddStair(floor, Utilities.PickRandom(floor.FloorConfig.StairDownRoomTemplates));
            }
            else
            {
                // If it's a middle floor, add 2 stairs
                AddStair(floor, Utilities.PickRandom(floor.FloorConfig.StairRoomTemplates));
                AddStair(floor, Utilities.PickRandom(floor.FloorConfig.StairDownRoomTemplates));
            }
        }
    }
}
