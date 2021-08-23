using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using rand = UnityEngine.Random;
using System;
namespace GameGeneration
{
    public class GameGen : MonoBehaviour
    {
        public static GameGen instance = null;				//Static instance of GameManager which allows it to be accessed by any other script.
        public int noOfLevels = 2;
        public List<Level> levels { get; set; }

        void Awake()
        {
            //Check if instance already exists
            if (instance == null)

                //if not, set instance to this
                instance = this;

            //If instance already exists and it's not this:
            else if (instance != this)

                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);

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
            for (int i = 0; i < noOfLevels; i++)
            {
                Level levelToCreate = GetComponent<Level>();
                levelToCreate.SetupLevel(i);
                levels.Add(levelToCreate);
            }
        }


    }

    // [Serializable]
    // public class Room
    // {
    //     public Cell[,] cells { get; set; }
    // }
}