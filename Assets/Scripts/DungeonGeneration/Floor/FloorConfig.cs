namespace Prototype.GameGeneration
{
    using Prototype.GameGeneration.Rooms;
    using UnityEngine;

    /// <summary>
    /// Configuration class for a floor, accessed by unity.
    /// </summary>
    public class FloorConfig : MonoBehaviour
    {
        /// <summary>
        /// Number of empty rooms per floor.
        /// </summary>
        [SerializeField]
        private int emptyRooms;

        /// <summary>
        /// Floor Height.
        /// </summary>
        [SerializeField]
        private int floorHeight = 6;

        /// <summary>
        /// Floor Width.
        /// </summary>
        [SerializeField]
        private int floorWidth = 6;

        /// <summary>
        /// Maximum number of size 3 rooms.
        /// </summary>
        [SerializeField]
        private int max3Rooms = 3;

        /// <summary>
        /// Gets the Door configuration.
        /// </summary>
        /// <value>The door configuration.</value>
        public DoorConfig DoorConfig { get; private set; }

        /// <summary>
        /// Gets the total number of empty rooms possible for the floor.
        /// </summary>
        /// <value>The number of empty rooms.</value>
        public int EmptyRooms
        {
            get => this.emptyRooms;
            private set => this.emptyRooms = value;
        }

        /// <summary>
        /// Gets the floor height.
        /// </summary>
        /// <value>The floor height.</value>
        public int FloorHeight
        {
            get => this.floorHeight;
            private set => this.floorHeight = value;
        }

        /// <summary>
        /// Gets the floor width.
        /// </summary>
        /// <value>The floor width.</value>
        public int FloorWidth
        {
            get => this.floorWidth;
            private set => this.floorWidth = value;
        }

        /// <summary>
        /// Gets the maximum number of triple rooms.
        /// </summary>
        /// <value>The maximum number of triple rooms.</value>
        public int Max3Rooms
        {
            get => this.max3Rooms;
            private set => this.max3Rooms = value;
        }

        /// <summary>
        /// Gets the room config for the floor config.
        /// </summary>
        /// <value>The room config.</value>
        public RoomConfig RoomConfig { get; private set; }

        /// <summary>
        /// Get the Door Config Component.
        /// </summary>
        public void GetConfig()
        {
            DoorConfig = this.GetComponent<DoorConfig>();
            RoomConfig = this.GetComponent<RoomConfig>();
            RoomConfig.GetConfig();
        }
    }
}
