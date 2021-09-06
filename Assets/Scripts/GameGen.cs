using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using rand = UnityEngine.Random;
namespace GameGeneration
{
    public class GameGen : MonoBehaviour
    {
        public static GameGen instance = null;	//Static instance of GameManager which allows it to be accessed by any other script.
        public int noOfLevels = 2;
        public int floorWidth = 5;
        public int floorHeight = 5;
        public List<Level> levels { get; set; }

        void Awake()
        {
            //Check if instance already exists
            if (instance == null)
            {
                //if not, set instance to this
                instance = this;
            }

            //If instance already exists and it's not this:
            else if (instance != this)
            {
                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);
            }

            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);

            instance.SetupGame();
        }

        // static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        // {
        // }

        public void SetupGame()
        {
            Debug.Log("=== Setting up Game ===");
            Debug.Log("=== Generating Levels ===");
            levels = new List<Level>();
            int x = 0;
            int y = 0;
            for (int i = 0; i < noOfLevels; i++)
            {
                Level levelToCreate = GetComponent<Level>();
                levelToCreate.SetupLevel(i, x, y, floorWidth, floorHeight);
                levels.Add(levelToCreate);
                x += (floorWidth * (i + 1)) + (i + 1);
            }

            foreach (Level level in levels)
            {
                // TODO - Setup Logic to only instantiate the level when you've left the previous one
                level.InstantiateLevel();
            }
        }
    }
}