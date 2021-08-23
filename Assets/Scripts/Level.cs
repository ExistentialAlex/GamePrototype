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

        public void SetupLevel(int levelNo, int startX, int startY, int floorWidth, int floorHeight)
        {
            noFloors = levelNo;
            floors = new List<Floor>();
            GenerateFloors(startX, startY, floorWidth, floorHeight);
        }

        public void GenerateFloors(int startX, int startY, int floorWidth, int floorHeight)
        {
            Debug.Log("=== Generating Floors for Level " + noFloors + " ===");
            int x = startX;
            int y = startY;
            for (int i = 0; i < noFloors + 1; i++)
            {
                bool secret = Convert.ToBoolean(rand.Range(0, 2));
                bool shop = Convert.ToBoolean(rand.Range(0, 2));
                Floor floorToCreate = GetComponent<Floor>();
                floorToCreate.SetupFloor(x, y, floorWidth, floorHeight, i, noFloors, shop, secret);
                floors.Add(floorToCreate);
                x += floorWidth + 1;
            }
        }
    }
}