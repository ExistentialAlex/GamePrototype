namespace Prototype.GameGeneration.Rooms
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Utilities;

    /// <summary>
    /// The room structure class.
    /// </summary>
    public class Room
    {
        /// <summary>
        /// The height of a room.
        /// </summary>
        public static readonly int RoomHeight = 10;

        /// <summary>
        /// The width of a room.
        /// </summary>
        public static readonly int RoomWidth = 10;

        /// <summary>
        /// Initializes a new instance of the <see cref="Room"/> class.
        /// </summary>
        /// <param name="id">The room Id.</param>
        /// <param name="drawFrom">The position to draw the room from.</param>
        /// <param name="roomConfig">The room configuration.</param>
        public Room(string id, Vector3 drawFrom, RoomConfig roomConfig)
        {
            this.Id = id;
            this.Cells = new List<Cell>();
            this.Template = Utilities.PickRandom(this.GetTemplatesFromRoomType(roomConfig));
            this.DrawFrom = drawFrom;
        }

        /// <summary>
        /// The type of room.
        /// </summary>
        public enum RoomType
        {
            entrance,
            boss,
            stair,
            stairDown,
            empty,
            shop,
            secret,
            singleRoom,
            doubleHorizontal,
            doubleVertical,
            tripleBottomLeft,
            tripleBottomRight,
            tripleHorizontal,
            tripleTopLeft,
            tripleTopRight,
            tripleVertical,
        }

        /// <summary>
        /// Gets or sets the list of cells in the room.
        /// </summary>
        /// <value>The list of cells.</value>
        public List<Cell> Cells { get; set; }

        /// <summary>
        /// Gets or sets the position to draw the room from.
        /// </summary>
        /// <value>The position to draw from.</value>
        public Vector3 DrawFrom { get; set; }

        /// <summary>
        /// Gets or sets the global vector position of the room.
        /// </summary>
        /// <value>The global vector position of the room.</value>
        public Vector3 GlobalVectorPosition { get; set; }

        /// <summary>
        /// Gets the ID of the room.
        /// </summary>
        /// <value>The room ID.</value>
        public string Id { get; private set; }

        /// <summary>
        /// Gets or sets the template/prefab of the room.
        /// </summary>
        /// <value>The template/prefab of the room.</value>
        public GameObject Template { get; set; }

        /// <summary>
        /// Gets the room type.
        /// </summary>
        /// <value>The room type.</value>
        public virtual RoomType Type { get; }

        /// <summary>
        /// Generate a room ID based on the floor number and position of the room.
        /// </summary>
        /// <param name="floorNo">Floor number.</param>
        /// <param name="position">Position of the room.</param>
        /// <returns>The generated ID.</returns>
        public static string GenerateRoomId(int floorNo, Vector3 position)
        {
            return string.Join("_", Convert.ToString(floorNo), Convert.ToInt32(position.x), Convert.ToInt32(position.y));
        }

        /// <summary>
        /// Gets the templates for a room based on the type of room.
        /// </summary>
        /// <param name="roomConfig">The room config.</param>
        /// <returns>The array of templates for that room.</returns>
        public GameObject[] GetTemplatesFromRoomType(RoomConfig roomConfig)
        {
            Dictionary<RoomType, GameObject[]> dict = new Dictionary<RoomType, GameObject[]>
            {
                { RoomType.boss, roomConfig.BossRoomTemplates },
                { RoomType.empty, roomConfig.EmptyRoomTemplates },
                { RoomType.entrance, roomConfig.EntranceRoomTemplates },
                { RoomType.secret, roomConfig.SecretRoomTemplates },
                { RoomType.shop, roomConfig.ShopRoomTemplates },
                { RoomType.stair, roomConfig.StairRoomTemplates },
                { RoomType.stairDown, roomConfig.StairDownRoomTemplates },
                { RoomType.singleRoom, roomConfig.SingleStandardRoomTemplates },
                { RoomType.doubleHorizontal, roomConfig.DoubleStandardHorizontalRoomTemplates },
                { RoomType.doubleVertical, roomConfig.DoubleStandardVerticalRoomTemplates },
                { RoomType.tripleBottomLeft, roomConfig.TripleStandardBottomLeftRoomTemplates },
                { RoomType.tripleBottomRight, roomConfig.TripleStandardBottomRightRoomTemplates },
                { RoomType.tripleHorizontal, roomConfig.TripleStandardHorizontalRoomTemplates },
                { RoomType.tripleTopLeft, roomConfig.TripleStandardTopLeftRoomTemplates },
                { RoomType.tripleTopRight, roomConfig.TripleStandardTopRightRoomTemplates },
                { RoomType.tripleVertical, roomConfig.TripleStandardVerticalRoomTemplates },
            };

            if (dict.TryGetValue(this.Type, out GameObject[] templates))
            {
                return templates;
            }

            throw new Exception("Could not get templates for specified Room Type: '" + Convert.ToString(this.Type) + "'");
        }

        /// <summary>
        /// Converts the type of room to a string.
        /// </summary>
        /// <returns>The room type as a string.</returns>
        public override string ToString()
        {
            return Convert.ToString(this.Type);
        }
    }
}
