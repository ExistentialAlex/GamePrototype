using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using rand = UnityEngine.Random;

namespace GameGeneration
{
    public class Level : MonoBehaviour
    {
        public int noFloors { get; set; }
        private List<Floor> floors { get; set; }

        public void SetupLevel(int levelNo, int startX, int startY, int floorWidth, int floorHeight)
        {
            noFloors = levelNo;
            floors = new List<Floor>();
            GenerateFloors(startX, startY, floorWidth, floorHeight);
        }

        public List<Floor> GenerateFloors(int startX, int startY, int floorWidth, int floorHeight)
        {
            List<Floor> generatedFloors = new List<Floor>();
            Debug.Log("=== Generating Floors for Level " + noFloors + " ===");
            int x = startX;
            int y = startY;
            for (int i = 0; i <= noFloors; i++)
            {
                bool includeSecret = Convert.ToBoolean(rand.Range(0, 2));
                bool includeShop = Convert.ToBoolean(rand.Range(0, 2));
                Floor floorToCreate = GetComponent<Floor>();
                floorToCreate.SetupFloor(x, y, floorWidth, floorHeight, i, noFloors, includeShop, includeSecret);
                floorToCreate.GenerateFloor();
                floorToCreate.InstantiateFloor();
                x += floorWidth + 1;
            }

            return generatedFloors;
        }

        public void InstantiateLevel()
        {
        }
    }
}
