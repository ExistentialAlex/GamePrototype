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
        /// <param name="id">The room Id.</param>
        /// <param name="drawFrom">The position to draw the room from.</param>
        /// <param name="roomConfig">The room configuration.</param>
        public StairRoom(string id, Vector3 drawFrom, RoomConfig roomConfig) : base(id, drawFrom, roomConfig)
        {
        }

        /// <summary>
        /// Gets or sets the other stair room linked to this one.
        /// </summary>
        /// <value>The stair pair.</value>
        public StairRoom StairPair { get; set; }

        /// <inheritdoc/>
        public override RoomType Type { get => RoomType.stair; }
    }
}
