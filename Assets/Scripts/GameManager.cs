using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using rand = UnityEngine.Random;

namespace GameGeneration
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;	//Static instance of GameManager which allows it to be accessed by any other script.
        public static GameObject player = null;
        public GameObject playerPrefab;
        public int noOfLevels = 2;
        private Level currentLevel { get; set; }
        private int levelNo { get; set; }
        private FloorConfig floorConfig;
        private RoomGenerator roomGenerator;

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

        // static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        // {
        // }

        public void FinishLevel()
        {
            Debug.Log($"Level {levelNo} finished, loading next level");
            levelNo++;
            SceneManager.LoadScene(0);
        }

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
