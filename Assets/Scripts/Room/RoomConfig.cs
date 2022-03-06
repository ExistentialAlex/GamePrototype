namespace Prototype.GameGeneration.Rooms
{
    using UnityEngine;

    /// <summary>
    /// The room configuration class.
    /// </summary>
    public class RoomConfig : MonoBehaviour
    {
        /// <summary>
        /// Templates for the boss room.
        /// </summary>
        [SerializeField]
        private GameObject[] bossRoomTemplates;

        /// <summary>
        /// Templates for the double standard horizontal room.
        /// </summary>
        [SerializeField]
        private GameObject[] doubleStandardHorizontalRoomTemplates;

        /// <summary>
        /// Templates for the double standard vertical room.
        /// </summary>
        [SerializeField]
        private GameObject[] doubleStandardVerticalRoomTemplates;

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
        /// Templates for the triple bottom left standard rooms.
        /// </summary>
        [SerializeField]
        private GameObject[] tripleStandardBottomLeftRoomTemplates;

        /// <summary>
        /// Templates for the triple bottom right standard rooms.
        /// </summary>
        [SerializeField]
        private GameObject[] tripleStandardBottomRightRoomTemplates;

        /// <summary>
        /// Templates for the triple horizontal standard rooms.
        /// </summary>
        [SerializeField]
        private GameObject[] tripleStandardHorizontalRoomTemplates;

        /// <summary>
        /// Templates for the triple top left standard rooms.
        /// </summary>
        [SerializeField]
        private GameObject[] tripleStandardTopLeftRoomTemplates;

        /// <summary>
        /// Templates for the triple top right standard rooms.
        /// </summary>
        [SerializeField]
        private GameObject[] tripleStandardTopRightRoomTemplates;

        /// <summary>
        /// Templates for the triple vertical standard rooms.
        /// </summary>
        [SerializeField]
        private GameObject[] tripleStandardVerticalRoomTemplates;

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
        /// Gets the double standard horizontal room templates.
        /// </summary>
        /// <value>The double standard horizontal room templates.</value>
        public GameObject[] DoubleStandardHorizontalRoomTemplates
        {
            get => this.doubleStandardHorizontalRoomTemplates;
            private set => this.doubleStandardHorizontalRoomTemplates = value;
        }

        /// <summary>
        /// Gets the double standard vertical room templates.
        /// </summary>
        /// <value>The double standard vertical room templates.</value>
        public GameObject[] DoubleStandardVerticalRoomTemplates
        {
            get => this.doubleStandardVerticalRoomTemplates;
            private set => this.doubleStandardVerticalRoomTemplates = value;
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
        /// Gets the triple standard bottom left room templates.
        /// </summary>
        /// <value>The triple standard bottom left room templates.</value>
        public GameObject[] TripleStandardBottomLeftRoomTemplates
        {
            get => this.tripleStandardBottomLeftRoomTemplates;
            private set => this.tripleStandardBottomLeftRoomTemplates = value;
        }

        /// <summary>
        /// Gets the triple standard bottom right room templates.
        /// </summary>
        /// <value>The triple standard bottom right room templates.</value>
        public GameObject[] TripleStandardBottomRightRoomTemplates
        {
            get => this.tripleStandardBottomRightRoomTemplates;
            private set => this.tripleStandardBottomRightRoomTemplates = value;
        }

        /// <summary>
        /// Gets the triple standard horizontal room templates.
        /// </summary>
        /// <value>The triple standard horizontal room templates.</value>
        public GameObject[] TripleStandardHorizontalRoomTemplates
        {
            get => this.tripleStandardHorizontalRoomTemplates;
            private set => this.tripleStandardHorizontalRoomTemplates = value;
        }

        /// <summary>
        /// Gets the triple standard top left room templates.
        /// </summary>
        /// <value>The triple standard top left room templates.</value>
        public GameObject[] TripleStandardTopLeftRoomTemplates
        {
            get => this.tripleStandardTopLeftRoomTemplates;
            private set => this.tripleStandardTopLeftRoomTemplates = value;
        }

        /// <summary>
        /// Gets the triple standard top right room templates.
        /// </summary>
        /// <value>The triple standard top right room templates.</value>
        public GameObject[] TripleStandardTopRightRoomTemplates
        {
            get => this.tripleStandardTopRightRoomTemplates;
            private set => this.tripleStandardTopRightRoomTemplates = value;
        }

        /// <summary>
        /// Gets the triple standard vertical room templates.
        /// </summary>
        /// <value>The triple standard vertical room templates.</value>
        public GameObject[] TripleStandardVerticalRoomTemplates
        {
            get => this.tripleStandardVerticalRoomTemplates;
            private set => this.tripleStandardVerticalRoomTemplates = value;
        }
    }
}
