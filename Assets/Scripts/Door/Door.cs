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
        /// <param name="rooms">The 2D array of rooms.</param>
        /// <param name="roomX">X coordinate of the specific room.</param>
        /// <param name="roomY">Y coordinate of the specific room.</param>
        /// <param name="maxX">Maximum value of X.</param>
        /// <param name="maxY">Maximum value of Y.</param>
        /// <param name="retries">Number of times to try.</param>
        public static void AddDoor(DoorConfig config, Room[,] rooms, int roomX, int roomY, int maxX, int maxY, int retries = 3)
        {
            Room room = rooms[roomX, roomY];
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
                        if (!CheckTopDoorConstraints(room, rooms, roomX, roomY, maxX, maxY))
                        {
                            AddDoor(config, rooms, roomX, roomY, maxX, maxY, retries - 1);
                            break;
                        }

                        Door topDoor = new Door(config, DoorPositions.top);
                        room.Doors.Add(topDoor);

                        Door bottomDoor = new Door(config, DoorPositions.bottom);
                        rooms[roomX, roomY + 1].Doors.Add(bottomDoor);
                        break;
                    }

                case DoorPositions.left:
                    {
                        if (!CheckLeftDoorConstraints(room, rooms, roomX, roomY, maxX, maxY))
                        {
                            AddDoor(config, rooms, roomX, roomY, maxX, maxY, retries - 1);
                            break;
                        }

                        Door leftDoor = new Door(config, DoorPositions.left);
                        room.Doors.Add(leftDoor);

                        Door rightDoor = new Door(config, DoorPositions.right);
                        rooms[roomX - 1, roomY].Doors.Add(rightDoor);
                        break;
                    }

                case DoorPositions.right:
                    {
                        if (!CheckRightDoorConstraints(room, rooms, roomX, roomY, maxX, maxY))
                        {
                            AddDoor(config, rooms, roomX, roomY, maxX, maxY, retries - 1);
                            break;
                        }

                        Door rightDoor = new Door(config, DoorPositions.right);
                        room.Doors.Add(rightDoor);

                        Door leftDoor = new Door(config, DoorPositions.left);
                        rooms[roomX + 1, roomY].Doors.Add(leftDoor);
                        break;
                    }

                case DoorPositions.bottom:
                    {
                        if (!CheckBottomDoorConstraints(room, rooms, roomX, roomY, maxX, maxY))
                        {
                            AddDoor(config, rooms, roomX, roomY, maxX, maxY, retries - 1);
                            break;
                        }

                        Door bottomDoor = new Door(config, DoorPositions.bottom);
                        room.Doors.Add(bottomDoor);

                        Door topDoor = new Door(config, DoorPositions.top);
                        rooms[roomX, roomY - 1].Doors.Add(topDoor);
                        break;
                    }
            }
        }

        /// <summary>
        /// Check the constraints for placing a door at the bottom of a room.
        /// </summary>
        /// <param name="room">The room in question.</param>
        /// <param name="rooms">All the rooms.</param>
        /// <param name="roomX">X position of the room.</param>
        /// <param name="roomY">Y position of the room.</param>
        /// <param name="maxX">Maximum possible X position.</param>
        /// <param name="maxY">Maximum possible Y position.</param>
        /// <returns>True if all constraints are met.</returns>
        public static bool CheckBottomDoorConstraints(Room room, Room[,] rooms, int roomX, int roomY, int maxX, int maxY)
        {
            return room.VectorPosition.y > 0 &&
                   CheckGenericConstraints(room, rooms[roomX, roomY - 1]) &&
                   room.Walls.Contains(Walls.WallTypes.bottom);
        }

        /// <summary>
        /// Check the generic constraints that apply to all rooms.
        /// </summary>
        /// <param name="room">The current room.</param>
        /// <param name="adjacentRoom">The room on the other side of the door.</param>
        /// <returns>True if all constraints are met.</returns>
        public static bool CheckGenericConstraints(Room room, Room adjacentRoom)
        {
            return adjacentRoom.Type != Room.RoomType.empty &&
                   !(room.Type == Room.RoomType.entrance && adjacentRoom.Type == Room.RoomType.boss) &&
                   !(room.Type == Room.RoomType.boss && adjacentRoom.Type == Room.RoomType.entrance);
        }

        /// <summary>
        /// Check the constraints for placing a door at the left of a room.
        /// </summary>
        /// <param name="room">The room in question.</param>
        /// <param name="rooms">All the rooms.</param>
        /// <param name="roomX">X position of the room.</param>
        /// <param name="roomY">Y position of the room.</param>
        /// <param name="maxX">Maximum possible X position.</param>
        /// <param name="maxY">Maximum possible Y position.</param>
        /// <returns>True if all constraints are met.</returns>
        public static bool CheckLeftDoorConstraints(Room room, Room[,] rooms, int roomX, int roomY, int maxX, int maxY)
        {
            return room.VectorPosition.x > 0 &&
                   CheckGenericConstraints(room, rooms[roomX - 1, roomY]) &&
                   room.Walls.Contains(Walls.WallTypes.left);
        }

        /// <summary>
        /// Check the constraints for placing a door at the right of a room.
        /// </summary>
        /// <param name="room">The room in question.</param>
        /// <param name="rooms">All the rooms.</param>
        /// <param name="roomX">X position of the room.</param>
        /// <param name="roomY">Y position of the room.</param>
        /// <param name="maxX">Maximum possible X position.</param>
        /// <param name="maxY">Maximum possible Y position.</param>
        /// <returns>True if all constraints are met.</returns>
        public static bool CheckRightDoorConstraints(Room room, Room[,] rooms, int roomX, int roomY, int maxX, int maxY)
        {
            return room.VectorPosition.x < maxX &&
                   CheckGenericConstraints(room, rooms[roomX + 1, roomY]) &&
                   room.Walls.Contains(Walls.WallTypes.right);
        }

        /// <summary>
        /// Check the constraints for placing a door at the top of a room.
        /// </summary>
        /// <param name="room">The room in question.</param>
        /// <param name="rooms">All the rooms.</param>
        /// <param name="roomX">X position of the room.</param>
        /// <param name="roomY">Y position of the room.</param>
        /// <param name="maxX">Maximum possible X position.</param>
        /// <param name="maxY">Maximum possible Y position.</param>
        /// <returns>True if all constraints are met.</returns>
        public static bool CheckTopDoorConstraints(Room room, Room[,] rooms, int roomX, int roomY, int maxX, int maxY)
        {
            return room.VectorPosition.y < maxY &&
                   CheckGenericConstraints(room, rooms[roomX, roomY + 1]) &&
                   room.Walls.Contains(Walls.WallTypes.top);
        }
    }
}
