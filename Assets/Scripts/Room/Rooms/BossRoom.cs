namespace Prototype.GameGeneration.Rooms
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Boss Room class.
    /// </summary>
    public class BossRoom : Room
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BossRoom"/> class.
        /// </summary>
        /// <param name="vectorPosition">Position of the room.</param>
        /// <param name="walls">List of walls the room has.</param>
        /// <param name="template">The prefab template of the room.</param>
        public BossRoom(Vector3 vectorPosition, List<Walls.WallTypes> walls, GameObject template) : base(vectorPosition, walls, template)
        {
            this.Type = RoomType.boss;
        }
    }
}
