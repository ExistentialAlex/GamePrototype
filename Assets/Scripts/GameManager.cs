using UnityEngine;
using UnityEngine.SceneManagement;
using Prototype.GameGeneration.Rooms;

namespace Prototype.GameGeneration
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;	//Static instance of GameManager which allows it to be accessed by any other script.
        public static GameObject player = null;
        public GameObject playerPrefab;
        public int noOfLevels = 2;

        [HideInInspector]
        public Level currentLevel { get; set; }

        private int levelNo { get; set; }
        private FloorConfig floorConfig;
        private RoomGenerator roomGenerator;
        public bool playerReady { get; set; }

        /// <summary>
        /// Called when the game is started.
        /// Will create a new instance of the game manager that is used to manage state throughout the game.
        /// </summary>
        private void Awake()
        {
            //Check if instance already exists
            if (instance == null)
            {
                //if not, set instance to this
                instance = this;
                instance.roomGenerator = GetComponent<RoomGenerator>();
                instance.floorConfig = GetComponent<FloorConfig>();
                instance.floorConfig.GetDoorConfig();
                instance.levelNo = 0;
            }

            //If instance already exists and it's not this:
            else if (instance != this)
            {
                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);
            }

            if (player == null)
            {
                player = Instantiate(playerPrefab, new Vector3(0, 0, 0f), Quaternion.identity);
            }

            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);

            instance.SetupGame();
        }

        /// <summary>
        /// Increments the level number and loads the next level.
        /// </summary>
        public void FinishLevel()
        {
            Debug.Log($"Level {levelNo} finished, loading next level");
            levelNo++;
            instance.playerReady = false;

            // if you've completed the requisit levels then end the game.
            if (levelNo == noOfLevels)
            {
                GameOver();
            }

            SceneManager.LoadScene(0);
        }

        public void GameOver()
        {
            enabled = false;
        }

        /// <summary>
        /// Set up the game based on the current level number.
        /// </summary>
        public void SetupGame()
        {
            Debug.Log("=== Setting up Game ===");
            currentLevel = new Level(levelNo);
            LevelGenerator.GenerateLevel(currentLevel, floorConfig, roomGenerator);

            foreach (Floor floor in currentLevel.floors)
            {
                floor.InstantiateFloor(roomGenerator);
            }
        }
    }
}
