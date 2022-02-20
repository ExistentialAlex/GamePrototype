namespace Prototype.GameGeneration
{
    using UnityEngine;

    /// <summary>
    /// Configuration class for a floor, accessed by unity.
    /// </summary>
    public class FloorConfig : MonoBehaviour
    {
        /// <summary>
        /// Templates for the boss room.
        /// </summary>
        [SerializeField]
        private GameObject[] bossRoomTemplates;

        /// <summary>
        /// Templates for the double standard room.
        /// </summary>
        [SerializeField]
        private GameObject[] doubleStandardRoomTemplates;

        /// <summary>
        /// Number of empty rooms per floor.
        /// </summary>
        [SerializeField]
        private int emptyRooms;

        /// <summary>
        /// Templates for the empty rooms.
        /// </summary>
        [SerializeField]
        private GameObject[] emptyRoomTemplates;

        /// <summary>
        /// Templates for the entrance rooms.
        /// </summary>
        [SerializeField]
        private GameObject[] entranceRoomTemplates;

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
        /// Templates for the secret rooms.
        /// </summary>
        [SerializeField]
        private GameObject[] secretRoomTemplates;

        /// <summary>
        /// Templates for the shop rooms.
        /// </summary>
        [SerializeField]
        private GameObject[] shopRoomTemplates;

        /// <summary>
        /// Templates for the single standard rooms.
        /// </summary>
        [SerializeField]
        private GameObject[] singleStandardRoomTemplates;

        /// <summary>
        /// Templates for the stair down rooms.
        /// </summary>
        [SerializeField]
        private GameObject[] stairDownRoomTemplates;

        /// <summary>
        /// Templates for the stair rooms.
        /// </summary>
        [SerializeField]
        private GameObject[] stairRoomTemplates;

        /// <summary>
        /// Templates for the triple standard rooms.
        /// </summary>
        [SerializeField]
        private GameObject[] tripleStandardRoomTemplates;

        /// <summary>
        /// Gets the boss room templates.
        /// </summary>
        /// <value>Boss room templates.</value>
        public GameObject[] BossRoomTemplates
        {
            get => this.bossRoomTemplates;
            private set => this.bossRoomTemplates = value;
        }

        /// <summary>
        /// Gets the Door configuration.
        /// </summary>
        /// <value>The door configuration.</value>
        public DoorConfig DoorConfig { get; private set; }

        /// <summary>
        /// Gets the double standard room templates.
        /// </summary>
        /// <value>The double standard room templates.</value>
        public GameObject[] DoubleStandardRoomTemplates
        {
            get => this.doubleStandardRoomTemplates;
            private set => this.doubleStandardRoomTemplates = value;
        }

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
        /// Gets the empty room templates.
        /// </summary>
        /// <value>The empty room templates.</value>
        public GameObject[] EmptyRoomTemplates
        {
            get => this.emptyRoomTemplates;
            private set => this.emptyRoomTemplates = value;
        }

        /// <summary>
        /// Gets the entrance room templates.
        /// </summary>
        /// <value>The entrance room templates.</value>
        public GameObject[] EntranceRoomTemplates
        {
            get => this.entranceRoomTemplates;
            private set => this.entranceRoomTemplates = value;
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
        /// Gets the secret room templates.
        /// </summary>
        /// <value>The secret room templates.</value>
        public GameObject[] SecretRoomTemplates
        {
            get => this.secretRoomTemplates;
            private set => this.secretRoomTemplates = value;
        }

        /// <summary>
        /// Gets the shop room templates.
        /// </summary>
        /// <value>The shop room templates.</value>
        public GameObject[] ShopRoomTemplates
        {
            get => this.shopRoomTemplates;
            private set => this.shopRoomTemplates = value;
        }

        /// <summary>
        /// Gets the single standard room templates.
        /// </summary>
        /// <value>The single standard room templates.</value>
        public GameObject[] SingleStandardRoomTemplates
        {
            get => this.singleStandardRoomTemplates;
            private set => this.singleStandardRoomTemplates = value;
        }

        /// <summary>
        /// Gets the stair down room templates.
        /// </summary>
        /// <value>The stair down room templates.</value>
        public GameObject[] StairDownRoomTemplates
        {
            get => this.stairDownRoomTemplates;
            private set => this.stairDownRoomTemplates = value;
        }

        /// <summary>
        /// Gets the stair room templates.
        /// </summary>
        /// <value>The stair room templates.</value>
        public GameObject[] StairRoomTemplates
        {
            get => this.stairRoomTemplates;
            private set => this.stairRoomTemplates = value;
        }

        /// <summary>
        /// Gets the triple standard room templates.
        /// </summary>
        /// <value>The triple standard room templates.</value>
        public GameObject[] TripleStandardRoomTemplates
        {
            get => this.tripleStandardRoomTemplates;
            private set => this.tripleStandardRoomTemplates = value;
        }

        /// <summary>
        /// Get the Door Config Component.
        /// </summary>
        public void GetDoorConfig()
        {
            DoorConfig = this.GetComponent<DoorConfig>();
        }
    }
}
