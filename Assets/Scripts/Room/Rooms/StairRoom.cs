using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.GameGeneration.Rooms
{
    public class StairRoom : Room
    {
        /// <summary>
        /// The other stair room linked to this one.
        /// </summary>
        public StairRoom stairPair { get; set; }

        public StairRoom(Vector3 vectorPosition, List<Walls.WallTypes> walls, GameObject template) : base(vectorPosition, walls, template)
        {
            this.type = RoomType.stair;
        }
    }
}
