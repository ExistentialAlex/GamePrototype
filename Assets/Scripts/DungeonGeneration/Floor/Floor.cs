namespace Prototype.GameGeneration
{
    using System;
    using System.Collections.Generic;
    using Prototype.GameGeneration.Rooms;
    using UnityEngine;

    using rand = UnityEngine.Random;

    /// <summary>
    /// Class defining the structure of a floor.
    /// </summary>
    public class Floor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Floor"/> class.
        /// </summary>
        /// <param name="config">Floor configuration.</param>
        /// <param name="startX">Global starting X coordinate.</param>
        /// <param name="startY">Global starting Y coordinate.</param>
        /// <param name="floorNo">The floor number.</param>
        /// <param name="levelNo">The level number.</param>
        /// <param name="containsShop">Whether the floor contains a shop.</param>
        /// <param name="containsSecret">Whether the floor contains secrets.</param>
        public Floor(FloorConfig config, int startX, int startY, int floorNo, int levelNo, bool containsShop, bool containsSecret)
        {
            this.FloorConfig = config;

            this.StartX = startX;
            this.StartY = startY;
            this.FloorNo = floorNo;
            this.LevelNo = levelNo;
            this.ContainsSecret = containsSecret;
            this.ContainsShop = containsShop;

            this.Current3Rooms = 0;
            this.NoSecrets = containsSecret ? rand.Range(1, 2) : 0; // returns either 1 or 2;

            // Create the floor to assign everything to
            this.Transform = new GameObject("Floor_" + levelNo + "_" + floorNo).transform;

            // Create a new room
            this.Cells = new Cell[this.FloorConfig.FloorWidth, this.FloorConfig.FloorHeight];
            this.Rooms = new List<Room>();
            this.Stairs = new List<StairRoom>();
        }

        /// <summary>
        /// Gets the 2D array of rooms in the floor.
        /// </summary>
        /// <value>The 2D array of rooms.</value>
        public Cell[,] Cells { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the floor contains a secret.
        /// </summary>
        /// <value>Whether the floor contains a secret.</value>
        public bool ContainsSecret { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the floor contains a shop.
        /// </summary>
        /// <value>Whether the floor contains a shop.</value>
        public bool ContainsShop { get; private set; }

        /// <summary>
        /// Gets the door config.
        /// </summary>
        /// <value>The floors door configuration.</value>
        public DoorConfig DoorConfig { get; private set; }

        /// <summary>
        /// Gets or sets the floor configuration.
        /// </summary>
        /// <value>The floor configuration.</value>
        public FloorConfig FloorConfig { get; set; }

        /// <summary>
        /// Gets the floor number.
        /// </summary>
        /// <value>The floor number.</value>
        public int FloorNo { get; private set; }

        /// <summary>
        /// Gets the level number.
        /// </summary>
        /// <value>The level number.</value>
        public int LevelNo { get; private set; }

        /// <summary>
        /// Gets the number of secrets for the floor.
        /// </summary>
        /// <value>The number of secrets.</value>
        public int NoSecrets { get; private set; }

        /// <summary>
        /// Gets the list of rooms to build.
        /// </summary>
        /// <value>The rooms to build.</value>
        public List<Room> Rooms { get; private set; }

        /// <summary>
        /// Gets the list of stairs in the floor.
        /// </summary>
        /// <value>The list of stairs.</value>
        public List<StairRoom> Stairs { get; private set; }

        /// <summary>
        /// Gets the starting X coordinate of the floor.
        /// </summary>
        /// <value>The starting X coordinate.</value>
        public int StartX { get; private set; }

        /// <summary>
        /// Gets the starting Y coordinate of the floor.
        /// </summary>
        /// <value>The starting Y coordinate.</value>
        public int StartY { get; private set; }

        /// <summary>
        /// Gets the transform for the floor.
        /// </summary>
        /// <value>The transform.</value>
        public Transform Transform { get; private set; }

        /// <summary>
        /// Gets or sets the number of size 3 rooms.
        /// </summary>
        /// <value>The number of size 3 rooms currently in the floor.</value>
        internal int Current3Rooms { get; set; }

        /// <summary>
        /// Instantiate the floor and place it on the screen.
        /// </summary>
        /// <param name="roomGenerator">The room generator.</param>
        public void InstantiateFloor(RoomGenerator roomGenerator)
        {
            foreach (Room room in this.Rooms)
            {
                this.InstantiateRoom(roomGenerator, room);
            }
        }

        /// <summary>
        /// Instantiate the room and place it on the screen.
        /// </summary>
        /// <param name="roomGenerator">The room generator.</param>
        /// <param name="room">The room to generate.</param>
        private void InstantiateRoom(RoomGenerator roomGenerator, Room room)
        {
            roomGenerator.SetupRoom(room, this.Transform, this.StartX, this.StartY);
        }
    }
}
