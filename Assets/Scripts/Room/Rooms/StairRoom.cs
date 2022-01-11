using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameGeneration.Rooms
{
    public class StairRoom : Room
    {
        /// <summary>
        /// The other stair room linked to this one.
        /// </summary>
        public StairRoom stairPair { get; set; }

        public StairRoom(Vector3 vectorPosition, List<Walls.WallTypes> walls, GameObject tile) : base(vectorPosition, walls, tile)
        {
            this.type = RoomType.stair;
        }
    }
}
