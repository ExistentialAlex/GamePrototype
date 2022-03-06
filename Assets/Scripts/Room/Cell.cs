namespace Prototype.GameGeneration.Rooms
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Cell in a floor or room.
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="vectorPosition">The position of the cell.</param>
        /// <param name="parentId">The ID of the parent room.</param>
        /// <param name="parentRoomType">The type of parent room.</param>
        /// <param name="walls">The walls of the cell.</param>
        public Cell(Vector3 vectorPosition, string parentId, Room.RoomType parentRoomType, List<Walls.WallTypes> walls)
        {
            this.ParentId = parentId;
            this.VectorPosition = vectorPosition;
            this.ParentRoomType = parentRoomType;
            this.Doors = new List<Door>();
            this.Walls = walls;
        }

        /// <summary>
        /// Gets or sets the list of doors in the cell.
        /// </summary>
        /// <value>The list of doors.</value>
        public List<Door> Doors { get; set; }

        /// <summary>
        /// Gets the parent Id in the room.
        /// </summary>
        /// <value>The parent room's ID.</value>
        public string ParentId { get; private set; }

        /// <summary>
        /// Gets the parent room's type.
        /// </summary>
        /// <value>The parent room's type.</value>
        public Room.RoomType ParentRoomType { get; private set; }

        /// <summary>
        /// Gets or sets the cell vector position.
        /// </summary>
        /// <value>The vector Position.</value>
        public Vector3 VectorPosition { get; set; }

        /// <summary>
        /// Gets or sets the list of walls in the cell.
        /// </summary>
        /// <value>The list of walls.</value>
        public List<Walls.WallTypes> Walls { get; set; }
    }
}
