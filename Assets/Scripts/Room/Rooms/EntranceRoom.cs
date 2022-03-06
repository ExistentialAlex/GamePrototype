namespace Prototype.GameGeneration.Rooms
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// The entrance room class.
    /// </summary>
    public class EntranceRoom : Room
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntranceRoom"/> class.
        /// </summary>
        /// <param name="id">The room Id.</param>
        /// <param name="drawFrom">The position to draw the room from.</param>
        /// <param name="roomConfig">The room configuration.</param>
        public EntranceRoom(string id, Vector3 drawFrom, RoomConfig roomConfig) : base(id, drawFrom, roomConfig)
        {
        }

        /// <inheritdoc/>
        public override RoomType Type { get => RoomType.entrance; }
    }
}
