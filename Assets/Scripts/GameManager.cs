namespace Prototype.GameGeneration
{
    using Prototype.Common;
    using Prototype.GameGeneration.Rooms;
    using Prototype.Units;
    using System;
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
        private static Player player = null;

        /// <summary>
        /// The player prefab, containing sprite info and scripts.
        /// </summary>
        [SerializeField]
        private GameObject playerPrefab;

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
        public static Player Player
        {
            get => player;
            set => player = value;
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
            }
            else if (Instance != this)
            {
                // If instance already exists and it's not this.
                // Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                GameManager.Destroy(this.gameObject);
            }

            // Sets this to not be destroyed when reloading scene
            GameManager.DontDestroyOnLoad(this.gameObject);

            // Ensure the player is loaded on every scene.
            SceneManager.sceneLoaded += LoadPlayer;
            SceneManager.sceneLoaded += LoadState;

            SceneManager.LoadScene(Convert.ToString(Configuration.Scenes.Dungeon));
        }

        private void LoadPlayer(Scene s, LoadSceneMode mode)
        {
            if (Player == null)
            {
                Player = GameManager.Instantiate(this.PlayerPrefab, new Vector3(0, 0, 0f), Quaternion.identity).GetComponent<Player>();
            }

            Player.name = Convert.ToString(Configuration.Collidables.Player);

            GameManager.DontDestroyOnLoad(Player);
        }

        private void LoadState(Scene s, LoadSceneMode mode)
        {
            Debug.Log("LoadState");
        }

        private void SaveState()
        {
            Debug.Log("SaveState");
        }
    }
}
