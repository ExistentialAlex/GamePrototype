namespace Prototype.GameGeneration.Rooms
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Triple Room Top Right class.
    /// </summary>
    public class TripleRoomTopRight : Room
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TripleRoomTopRight"/> class.
        /// </summary>
        /// <param name="id">The room Id.</param>
        /// <param name="drawFrom">The position to draw the room from.</param>
        /// <param name="roomConfig">The room configuration.</param>
        public TripleRoomTopRight(string id, Vector3 drawFrom, RoomConfig roomConfig) : base(id, drawFrom, roomConfig)
        {
        }

        /// <inheritdoc/>
        public override RoomType Type { get => RoomType.tripleTopRight; }
    }
}
