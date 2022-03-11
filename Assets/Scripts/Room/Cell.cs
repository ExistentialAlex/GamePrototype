namespace Prototype.GameGeneration.Rooms
{
    using System.Collections.Generic;
    using Prototype.Utilities;
    using UnityEngine;

    /// <summary>
    /// Cell in a floor or room.
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="cellConfig">The cell configuration.</param>
        /// <param name="vectorPosition">The position of the cell.</param>
        /// <param name="parentId">The ID of the parent room.</param>
        /// <param name="parentRoomType">The type of parent room.</param>
        /// <param name="walls">The walls of the cell.</param>
        public Cell(CellConfig cellConfig, Vector3 vectorPosition, string parentId, Room.RoomType parentRoomType, List<Walls.WallTypes> walls)
        {
            this.ParentId = parentId;
            this.VectorPosition = vectorPosition;
            this.ParentRoomType = parentRoomType;
            this.Doors = new List<Door>();
            this.Walls = walls;
            this.Enemies = new List<GameObject>();
            this.AddEnemies(cellConfig);
        }

        /// <summary>
        /// Gets or sets the list of doors in the cell.
        /// </summary>
        /// <value>The list of doors.</value>
        public List<Door> Doors { get; set; }

        /// <summary>
        /// Gets the list of enemies in the cell.
        /// </summary>
        /// <value>List of enemy game objects.</value>
        public List<GameObject> Enemies { get; private set; }

        /// <summary>
        /// Gets the parent Id in the room.
        /// </summary>
        /// <value>The parent room's ID.</value>
        public string ParentId { get; private set; }

        /// <summary>
        /// Gets the parent room's type.
        /// </summary>
        /// <value>The parent room's type.</value>
        public Room.RoomType ParentRoomType { get; private set; }

        /// <summary>
        /// Gets or sets the cell vector position.
        /// </summary>
        /// <value>The vector Position.</value>
        public Vector3 VectorPosition { get; set; }

        /// <summary>
        /// Gets or sets the list of walls in the cell.
        /// </summary>
        /// <value>The list of walls.</value>
        public List<Walls.WallTypes> Walls { get; set; }

        /// <summary>
        /// Gets the list of rooms enemies are excluded from.
        /// </summary>
        /// <value>The list of enemy excluded rooms.</value>
        private List<Room.RoomType> EnemyExcludedRooms
        {
            get
            {
                return new List<Room.RoomType>
                {
                    Room.RoomType.empty,
                    Room.RoomType.entrance,
                    Room.RoomType.stair,
                    Room.RoomType.stairDown,
                    Room.RoomType.boss,
                    Room.RoomType.shop,
                };
            }
        }

        /// <summary>
        /// Add enemies to the cell.
        /// </summary>
        /// <param name="cellConfig">The cell configuration.</param>
        private void AddEnemies(CellConfig cellConfig)
        {
            for (int i = 0; i < Random.Range(cellConfig.MinEnemies, cellConfig.MaxEnemies); i++)
            {
                if (!this.EnemyExcludedRooms.Contains(this.ParentRoomType))
                {
                    this.Enemies.Add(Utilities.PickRandom(GameManager.Instance.EnemyPrefabs));
                }
            }
        }
    }
}
