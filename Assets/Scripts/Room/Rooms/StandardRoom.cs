namespace Prototype.GameGeneration.Rooms
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Standard Room class.
    /// </summary>
    public class StandardRoom : Room
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StandardRoom"/> class.
        /// </summary>
        /// <param name="vectorPosition">Position of the room.</param>
        /// <param name="walls">List of walls the room has.</param>
        /// <param name="template">The prefab template of the room.</param>
        public StandardRoom(Vector3 vectorPosition, List<Walls.WallTypes> walls, GameObject template) : base(vectorPosition, walls, template)
        {
            this.Type = RoomType.room;
        }
    }
}
