using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rand = UnityEngine.Random;

namespace GameGeneration
{
    public class Level : MonoBehaviour
    {
        public int noFloors { get; set; }
        List<Floor> floors { get; set; }
        public int floorWidth = 5;
        public int floorHeight = 5;

        public void SetupLevel(int levelNo)
        {
            noFloors = levelNo;
            floors = new List<Floor>();
            GenerateFloors();
        }

        public void GenerateFloors()
        {
            Debug.Log("=== Generating Floors for Level " + noFloors + " ===");
            for (int i = 0; i < noFloors + 1; i++)
            {
                bool secret = Convert.ToBoolean(rand.Range(0, 2));
                bool shop = Convert.ToBoolean(rand.Range(0, 2));
                Floor floorToCreate = GetComponent<Floor>();
                floorToCreate.SetupFloor(floorWidth, floorHeight, i, noFloors, shop, secret);
                floors.Add(floorToCreate);
            }
        }
    }
}