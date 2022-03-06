namespace Prototype.GameGeneration
{
    using Prototype.GameGeneration.Rooms;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// Game Manager used for monitoring and maintaining game state.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// Static instance of GameManager which allows it to be accessed by any other script.
        /// </summary>
        private static GameManager instance = null;

        /// <summary>
        /// Static instance of the player which allows it to be accessed by any other script.
        /// </summary>
        private static GameObject player = null;

        /// <summary>
        /// The number of levels for this game.
        /// </summary>
        [SerializeField]
        private int noOfLevels;

        /// <summary>
        /// The player prefab, containing sprite info and scripts.
        /// </summary>
        [SerializeField]
        private GameObject playerPrefab;

        /// <summary>
        /// The room generator.
        /// </summary>
        private RoomGenerator roomGenerator;

        /// <summary>
        /// Gets or sets the game manager instance.
        /// </summary>
        /// <value>The current instance of the game manager.</value>
        public static GameManager Instance
        {
            get => instance;
            set => instance = value;
        }

        /// <summary>
        /// Gets or sets the player instance.
        /// </summary>
        /// <value>The current instance of the player.</value>
        public static GameObject Player
        {
            get => player;
            set => player = value;
        }

        /// <summary>
        /// Gets or sets the current level in the game.
        /// </summary>
        /// <value>The current level.</value>
        [HideInInspector]
        public Level CurrentLevel { get; set; }

        /// <summary>
        /// Gets or sets the number of levels.
        /// </summary>
        /// <value>The number of levels in the game.</value>
        public int NoOfLevels
        {
            get => this.noOfLevels;
            set => this.noOfLevels = value;
        }

        /// <summary>
        /// Gets or sets the player prefab.
        /// </summary>
        /// <value>The player prefab.</value>
        public GameObject PlayerPrefab
        {
            get => this.playerPrefab;
            set => this.playerPrefab = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is ready to be moved.
        /// </summary>
        /// <value>The player's ready state.</value>
        public bool PlayerReady { get; set; }

        /// <summary>
        /// Gets or sets the floor configuration.
        /// </summary>
        /// <value>The floor configuration.</value>
        private FloorConfig FloorConfig { get; set; }

        /// <summary>
        /// Gets or sets the current level number.
        /// </summary>
        /// <value>The current level number.</value>
        private int LevelNo { get; set; }

        /// <summary>
        /// Increments the level number and loads the next level.
        /// </summary>
        public void FinishLevel()
        {
            Debug.Log($"Level {this.LevelNo} finished, loading next level");
            this.LevelNo++;
            Instance.PlayerReady = false;

            // if you've completed the requisit levels then end the game.
            if (this.LevelNo == this.NoOfLevels)
            {
                this.GameOver();
                return;
            }

            SceneManager.LoadScene(0);
        }

        /// <summary>
        /// Method run when the game is over. Sets the instance of the game manager to disabled.
        /// TODO - Will eventually show a screen which will return the player to the hub world/title screen.
        /// </summary>
        public void GameOver()
        {
            Debug.Log("Game Over!");
            this.enabled = false;
            this.PlayerReady = false;
            return;
        }

        /// <summary>
        /// Set up the game based on the current level number.
        /// </summary>
        public void SetupGame()
        {
            Debug.Log("=== Setting up Game ===");
            this.CurrentLevel = new Level(this.LevelNo);
            LevelGenerator.GenerateLevel(this.CurrentLevel, this.FloorConfig);

            foreach (Floor floor in this.CurrentLevel.Floors)
            {
                floor.InstantiateFloor(this.roomGenerator);
            }
        }

        /// <summary>
        /// Called when the game is started. Will create a new instance of the game manager that is
        /// used to manage state throughout the game.
        /// </summary>
        private void Awake()
        {
            // Check if instance already exists
            if (Instance == null)
            {
                // if not, set instance to this
                Instance = this;
                Instance.roomGenerator = this.GetComponent<RoomGenerator>();
                Instance.FloorConfig = this.GetComponent<FloorConfig>();
                Instance.FloorConfig.GetDoorConfig();
                Instance.LevelNo = 0;
            }
            else if (Instance != this)
            {
                // If instance already exists and it's not this.
                // Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                GameManager.Destroy(this.gameObject);
            }

            if (Player == null)
            {
                Player = GameManager.Instantiate(this.PlayerPrefab, new Vector3(0, 0, 0f), Quaternion.identity);
            }

            // Sets this to not be destroyed when reloading scene
            GameManager.DontDestroyOnLoad(this.gameObject);

            Instance.SetupGame();
        }
    }
}
