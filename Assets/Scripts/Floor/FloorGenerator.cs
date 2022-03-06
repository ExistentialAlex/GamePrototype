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
        /// <param name="cells">List of cells in that room.</param>
        /// <returns>The room that was added.</returns>
        public static Room AddRoom(Floor floor, Room room, List<Cell> cells)
        {
            foreach (Cell cell in cells)
            {
                // Get x & y for cell
                int x = Convert.ToInt32(cell.VectorPosition.x);
                int y = Convert.ToInt32(cell.VectorPosition.y);
                floor.Cells[x, y] = cell;
            }

            floor.Rooms.Add(room);
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
            List<Tuple<Vector3, List<Cell>, Room.RoomType>> possibleRooms = new List<Tuple<Vector3, List<Cell>, Room.RoomType>>();
            string roomId = Room.GenerateRoomId(floor.FloorNo, position);

            // Create a Room of Size 3 //
            if (Convert.ToBoolean(rand.Range(0, 2)) && floor.Current3Rooms < floor.FloorConfig.Max3Rooms)
            {
                possibleRooms.AddRange(Rooms.Rooms.GetPossibleSize3Rooms(position, floor, roomId));
            }

            if (possibleRooms.Count > 0)
            {
                Tuple<Vector3, List<Cell>, Room.RoomType> chosenRoom = Utilities.PickRandom(possibleRooms.ToArray());
                AddRoom(floor, Rooms.Rooms.CreateRoomFromType(chosenRoom.Item3, roomId, chosenRoom.Item1, floor.FloorConfig.RoomConfig), chosenRoom.Item2);

                // Increment the number of size 3 rooms
                floor.Current3Rooms++;

                return;
            }

            // Room of Size 2 //
            possibleRooms.AddRange(Rooms.Rooms.GetPossibleSize2Rooms(position, floor, roomId));

            if (possibleRooms.Count > 0)
            {
                // Pick a random room from our list of possible rooms
                Tuple<Vector3, List<Cell>, Room.RoomType> chosenRoom = Utilities.PickRandom(possibleRooms.ToArray());
                AddRoom(floor, Rooms.Rooms.CreateRoomFromType(chosenRoom.Item3, roomId, chosenRoom.Item1, floor.FloorConfig.RoomConfig), chosenRoom.Item2);

                return;
            }

            // Create a standard room //
            AddRoom(
                floor,
                new StandardRoom(
                    roomId,
                    position,
                    floor.FloorConfig.RoomConfig),
                    new List<Cell>
                    {
                        new Cell(position, roomId, Room.RoomType.singleRoom, Walls.AllWalls())
                    });

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
            return Rooms.Rooms.GetUnpopulatedAdjacentRooms(position, floor.Cells, floor.FloorConfig.FloorWidth, floor.FloorConfig.FloorHeight).Count < 8;
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
            Debug.Log("Adding Remaining Rooms");
            for (int x = 0; x < floor.FloorConfig.FloorWidth; x++)
            {
                for (int y = 0; y < floor.FloorConfig.FloorHeight; y++)
                {
                    if (floor.Cells[x, y] == null)
                    {
                        AddStandardRoom(floor, new Vector3(x, y, 0f));
                    }
                }
            }

            // Add a door to each room
            AddDoors(floor);

            AssignCellsToRooms(floor);
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
            while (floor.Cells[x, y] != null);
        }

        /// <summary>
        /// Add a boss room to the floor.
        /// </summary>
        /// <param name="floor">The floor to add the room to.</param>
        private static void AddBossRoom(Floor floor)
        {
            Debug.Log("Adding Boss Room");

            // Position in center of room.
            int x = (floor.FloorConfig.FloorWidth - 1) / 2;
            int y = (floor.FloorConfig.FloorHeight - 1) / 2;
            string roomId = Room.GenerateRoomId(floor.FloorNo, new Vector3(x, y, 0f));
            List<Cell> cells = new List<Cell>
            {
                new Cell(new Vector3(x, y, 0f), roomId, Room.RoomType.boss, Walls.BottomLeftCorner()),
                new Cell(new Vector3(x + 1, y, 0f), roomId, Room.RoomType.boss, Walls.BottomRightCorner()),
                new Cell(new Vector3(x, y + 1, 0f), roomId, Room.RoomType.boss, Walls.TopLeftCorner()),
                new Cell(new Vector3(x + 1, y + 1, 0f), roomId, Room.RoomType.boss, Walls.TopRightCorner()),
            };

            AddRoom(floor, new BossRoom(roomId, new Vector3(x, y, 0f), floor.FloorConfig.RoomConfig), cells);
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
                    if (floor.Cells[x, y].ParentRoomType != Room.RoomType.empty)
                    {
                        Door.AddDoor(floor.FloorConfig.DoorConfig, floor.Cells, x, y, floor.FloorConfig.FloorWidth - 1, floor.FloorConfig.FloorHeight - 1, rand.Range(3, 4));
                    }

                    // If it's an entrance room, we need to add 2 more doors
                    if (floor.Cells[x, y].ParentRoomType == Room.RoomType.entrance)
                    {
                        Door.AddDoor(floor.FloorConfig.DoorConfig, floor.Cells, x, y, floor.FloorConfig.FloorWidth - 1, floor.FloorConfig.FloorHeight - 1);
                        Door.AddDoor(floor.FloorConfig.DoorConfig, floor.Cells, x, y, floor.FloorConfig.FloorWidth - 1, floor.FloorConfig.FloorHeight - 1);
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
            Debug.Log("Adding Empty Rooms");
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

                Vector3 finalVector = new Vector3(finalX, finalY, 0f);
                string roomId = Room.GenerateRoomId(floor.FloorNo, finalVector);

                List<Cell> cells = new List<Cell>
                {
                    new Cell(finalVector, roomId, Room.RoomType.empty, Walls.AllWalls()),
                };

                AddRoom(floor, new EmptyRoom(roomId, finalVector, floor.FloorConfig.RoomConfig), cells);
            }
        }

        /// <summary>
        /// Add an entrance to the floor.
        /// </summary>
        /// <param name="floor">The floor to add the room to.</param>
        private static void AddEntranceRoom(Floor floor)
        {
            Debug.Log("Adding Entrance Room");
            GetRandomEmptyRoom(floor, out int x, out int y);
            Vector3 vectorPosition = new Vector3(x, y, 0f);
            string roomId = Room.GenerateRoomId(floor.FloorNo, vectorPosition);

            List<Cell> cells = new List<Cell>()
            {
                new Cell(vectorPosition, roomId, Room.RoomType.entrance, Walls.AllWalls()),
            };

            AddRoom(floor, new EntranceRoom(roomId, vectorPosition, floor.FloorConfig.RoomConfig), cells);
        }

        /// <summary>
        /// Add a secret room to the floor.
        /// </summary>
        /// <param name="floor">The floor to add the room(s) to.</param>
        private static void AddSecretRoom(Floor floor)
        {
            Debug.Log("Adding Secret Rooms");
            for (int i = 0; i < floor.NoSecrets; i++)
            {
                GetRandomEmptyRoom(floor, out int x, out int y);
                Vector3 vectorPosition = new Vector3(x, y, 0f);
                string roomId = Room.GenerateRoomId(floor.FloorNo, vectorPosition);

                List<Cell> cells = new List<Cell>()
                {
                    new Cell(vectorPosition, roomId, Room.RoomType.secret, Walls.AllWalls()),
                };

                AddRoom(floor, new SecretRoom(roomId, vectorPosition, floor.FloorConfig.RoomConfig), cells);
            }
        }

        /// <summary>
        /// Add a shop to the floor.
        /// </summary>
        /// <param name="floor">The floor to add the room to.</param>
        private static void AddShopRoom(Floor floor)
        {
            Debug.Log("Adding Shop");
            GetRandomEmptyRoom(floor, out int x, out int y);
            Vector3 vectorPosition = new Vector3(x, y, 0f);
            string roomId = Room.GenerateRoomId(floor.FloorNo, vectorPosition);

            List<Cell> cells = new List<Cell>()
            {
                new Cell(vectorPosition, roomId, Room.RoomType.shop, Walls.AllWalls()),
            };

            AddRoom(floor, new ShopRoom(roomId, vectorPosition, floor.FloorConfig.RoomConfig), cells);
        }

        /// <summary>
        /// Add a stair to the floor with a specific template.
        /// </summary>
        /// <param name="floor">The floor to add the stair to.</param>
        private static void AddStair(Floor floor)
        {
            GetRandomEmptyRoom(floor, out int x, out int y);
            Vector3 vectorPosition = new Vector3(x, y, 0f);
            string roomId = Room.GenerateRoomId(floor.FloorNo, vectorPosition);

            List<Cell> cells = new List<Cell>()
            {
                new Cell(vectorPosition, roomId, Room.RoomType.stair, Walls.AllWalls()),
            };

            floor.Stairs.Add((StairRoom)AddRoom(floor, new StairRoom(roomId, vectorPosition, floor.FloorConfig.RoomConfig), cells));
        }

        /// <summary>
        /// Add a stair down to the floor.
        /// </summary>
        /// <param name="floor">The floor to add the stair to.</param>
        private static void AddStairDown(Floor floor)
        {
            GetRandomEmptyRoom(floor, out int x, out int y);
            Vector3 vectorPosition = new Vector3(x, y, 0f);
            string roomId = Room.GenerateRoomId(floor.FloorNo, vectorPosition);

            List<Cell> cells = new List<Cell>()
            {
                new Cell(vectorPosition, roomId, Room.RoomType.stairDown, Walls.AllWalls()),
            };

            floor.Stairs.Add((StairRoom)AddRoom(floor, new StairDownRoom(roomId, vectorPosition, floor.FloorConfig.RoomConfig), cells));
        }

        /// <summary>
        /// Add stair rooms to the floor.
        /// </summary>
        /// <param name="floor">The floor to add the room(s) to.</param>
        private static void AddStairRooms(Floor floor)
        {
            Debug.Log("Adding Stairs");
            if (floor.FloorNo == 0)
            {
                AddStair(floor);
            }
            else if (floor.FloorNo == floor.LevelNo)
            {
                AddStairDown(floor);
            }
            else
            {
                // If it's a middle floor, add 2 stairs
                AddStair(floor);
                AddStairDown(floor);
            }
        }

        /// <summary>
        /// Assign the cells in the floor to the corresponding room.
        /// </summary>
        /// <param name="floor">The floor.</param>
        private static void AssignCellsToRooms(Floor floor)
        {
            foreach (Room room in floor.Rooms)
            {
                for (int x = 0; x < floor.FloorConfig.FloorWidth; x++)
                {
                    for (int y = 0; y < floor.FloorConfig.FloorWidth; y++)
                    {
                        Cell cell = floor.Cells[x, y];
                        if (cell.ParentId == room.Id)
                        {
                            room.Cells.Add(cell);
                        }
                    }
                }
            }
        }
    }
}
