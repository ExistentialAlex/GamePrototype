namespace Prototype.GameGeneration.Rooms
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Shop room class.
    /// </summary>
    public class ShopRoom : Room
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShopRoom"/> class.
        /// </summary>
        /// <param name="id">The room Id.</param>
        /// <param name="drawFrom">The position to draw the room from.</param>
        /// <param name="roomConfig">The room configuration.</param>
        public ShopRoom(string id, Vector3 drawFrom, RoomConfig roomConfig) : base(id, drawFrom, roomConfig)
        {
        }

        /// <inheritdoc/>
        public override RoomType Type { get => RoomType.shop; }
    }
}
