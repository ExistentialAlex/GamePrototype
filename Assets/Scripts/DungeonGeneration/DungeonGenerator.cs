using Prototype.Common;
using Prototype.GameGeneration.Rooms;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prototype.GameGeneration
{
    public class DungeonGenerator : MonoBehaviour
    {
        /// <summary>
        /// Static instance of DungeonGenerator which allows it to be accessed by any other script.
        /// </summary>
        private static DungeonGenerator instance = null;

        /// <summary>
        /// The enemy prefabs, containing sprint info and scripts.
        /// </summary>
        [SerializeField]
        private GameObject[] enemyPrefabs;

        /// <summary>
        /// The number of levels for this game.
        /// </summary>
        [SerializeField]
        private int noOfLevels;

        /// <summary>
        /// The room generator.
        /// </summary>
        private RoomGenerator roomGenerator;

        /// <summary>
        /// Gets or sets the game manager instance.
        /// </summary>
        /// <value>The current instance of the game manager.</value>
        public static DungeonGenerator Instance
        {
            get => instance;
            set => instance = value;
        }

        /// <summary>
        /// Gets or sets the current level in the game.
        /// </summary>
        /// <value>The current level.</value>
        [HideInInspector]
        public Level CurrentLevel { get; set; }

        /// <summary>
        /// Gets or sets the array of enemy prefabs.
        /// </summary>
        /// <value>The enemy prefabs.</value>
        public GameObject[] EnemyPrefabs
        {
            get => this.enemyPrefabs;
            set => this.enemyPrefabs = value;
        }

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
            Debug.Log($"Level {Instance.LevelNo} finished");
            Instance.LevelNo++;
            Instance.PlayerReady = false;

            // if you've completed the requisit levels then end the game.
            if (Instance.LevelNo == Instance.NoOfLevels)
            {
                Instance.GameOver();
                return;
            }

            Debug.Log("Loading next level");

            SceneManager.LoadScene(Convert.ToString(Configuration.Scenes.Dungeon));
        }

        /// <summary>
        /// Method run when the game is over. Sets the instance of the game manager to disabled.
        /// TODO - Will eventually show a screen which will return the player to the hub world/title screen.
        /// </summary>
        public void GameOver()
        {
            Debug.Log("Game Over!");
            Instance.enabled = false;
            Instance.PlayerReady = false;

            // Destroy the instance of the dungeon generator and load the town.
            Destroy(Instance);
            SceneManager.LoadScene(Convert.ToString(Configuration.Scenes.Town));

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
        /// Load the next level in the game.
        /// </summary>
        public void LoadNextLevel()
        {
            // By checking if the player is active, we can stop the level from re-loading multiple times.
            if (!Instance.PlayerReady)
            {
                return;
            }

            Instance.FinishLevel();
        }

        private void Awake()
        {
            // Check if instance already exists
            if (Instance == null)
            {
                // if not, set instance to this
                Instance = this;
                Instance.roomGenerator = this.GetComponent<RoomGenerator>();
                Instance.FloorConfig = this.GetComponent<FloorConfig>();
                Instance.FloorConfig.GetConfig();
                Instance.LevelNo = 0;
            }
            else if (Instance != this)
            {
                // If instance already exists and it's not this.
                // Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                GameManager.Destroy(this.gameObject);
            }

            // Sets this to not be destroyed when reloading scene
            GameManager.DontDestroyOnLoad(this.gameObject);

            Instance.SetupGame();
        }
    }
}
