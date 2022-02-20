namespace Prototype.GameGeneration.Rooms
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// The room structure class.
    /// </summary>
    public class Room
    {
        /// <summary>
        /// The height of a room.
        /// </summary>
        public static readonly int RoomHeight = 10;

        /// <summary>
        /// The width of a room.
        /// </summary>
        public static readonly int RoomWidth = 10;

        /// <summary>
        /// Initializes a new instance of the <see cref="Room"/> class.
        /// </summary>
        /// <param name="vectorPosition">The position of the room in 3D space.</param>
        /// <param name="walls">List of walls in the room.</param>
        /// <param name="template">The template or prefab the room should use.</param>
        public Room(Vector3 vectorPosition, List<Walls.WallTypes> walls, GameObject template)
        {
            this.VectorPosition = vectorPosition;
            this.Walls = walls;
            this.Doors = new List<Door>();
            this.Template = template;
        }

        /// <summary>
        /// The type of room.
        /// </summary>
        public enum RoomType
        {
            entrance,
            boss,
            stair,
            empty,
            room,
            shop,
            secret
        }

        /// <summary>
        /// Gets or sets the list of doors in the room.
        /// </summary>
        /// <value>List of doors in the room.</value>
        public List<Door> Doors { get; set; }

        /// <summary>
        /// Gets or sets the global vector position of the room.
        /// </summary>
        /// <value>The global vector position of the room.</value>
        public Vector3 GlobalVectorPosition { get; set; }

        /// <summary>
        /// Gets or sets the template/prefab of the room.
        /// </summary>
        /// <value>The template/prefab of the room.</value>
        public GameObject Template { get; set; }

        /// <summary>
        /// Gets or sets the room type.
        /// </summary>
        /// <value>The room type.</value>
        public RoomType Type { get; set; }

        /// <summary>
        /// Gets or sets the local vector position.
        /// </summary>
        /// <value>The local vector position.</value>
        public Vector3 VectorPosition { get; set; }

        /// <summary>
        /// Gets or sets the list of walls in the room.
        /// </summary>
        /// <value>The list of wall in the room.</value>
        public List<Walls.WallTypes> Walls { get; set; }

        /// <summary>
        /// Converts the type of room to a string.
        /// </summary>
        /// <returns>The room type as a string.</returns>
        public override string ToString()
        {
            return Convert.ToString(this.Type);
        }
    }
}
