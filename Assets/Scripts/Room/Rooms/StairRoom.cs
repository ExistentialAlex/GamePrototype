namespace Prototype.GameGeneration.Rooms
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Stair Room class.
    /// </summary>
    public class StairRoom : Room
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StairRoom"/> class.
        /// </summary>
        /// <param name="vectorPosition">Position of the room.</param>
        /// <param name="walls">List of walls the room has.</param>
        /// <param name="template">The prefab template of the room.</param>
        public StairRoom(Vector3 vectorPosition, List<Walls.WallTypes> walls, GameObject template) : base(vectorPosition, walls, template)
        {
            this.Type = RoomType.stair;
        }

        /// <summary>
        /// Gets or sets the other stair room linked to this one.
        /// </summary>
        /// <value>The stair pair.</value>
        public StairRoom StairPair { get; set; }
    }
}
