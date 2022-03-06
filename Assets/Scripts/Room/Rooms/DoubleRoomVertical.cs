namespace Prototype.GameGeneration.Rooms
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Double Room Vertical class.
    /// </summary>
    public class DoubleRoomVertical : Room
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleRoomVertical"/> class.
        /// </summary>
        /// <param name="id">The room Id.</param>
        /// <param name="drawFrom">The position to draw the room from.</param>
        /// <param name="roomConfig">The room configuration.</param>
        public DoubleRoomVertical(string id, Vector3 drawFrom, RoomConfig roomConfig) : base(id, drawFrom, roomConfig)
        {
        }

        /// <inheritdoc/>
        public override RoomType Type { get => RoomType.doubleVertical; }
    }
}
