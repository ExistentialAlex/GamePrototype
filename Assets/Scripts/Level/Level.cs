using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using rand = UnityEngine.Random;

namespace GameGeneration
{
    public class Level
    {
        public int noFloors { get; set; }
        public List<Floor> floors { get; set; }
        private int x { get; set; }
        private int y { get; set; }

        public Level(int levelNo, int startX, int startY)
        {
            noFloors = levelNo;
            floors = new List<Floor>();
            this.x = startX;
            this.y = startY;
        }

        public void GenerateLevel(FloorConfig floorConfig)
        {
            GenerateFloors(floorConfig, x, y);
        }

        private void GenerateFloors(FloorConfig floorConfig, int startX, int startY)
        {
            Debug.Log("=== Generating Floors for Level " + noFloors + " ===");
            int x = startX;
            int y = startY;
            for (int i = 0; i <= noFloors; i++)
            {
                bool includeSecret = Convert.ToBoolean(rand.Range(0, 2));
                bool includeShop = Convert.ToBoolean(rand.Range(0, 2));
                Floor floorToCreate = new Floor(floorConfig, x, y, i, noFloors, includeShop, includeSecret);
                floorToCreate.GenerateFloor();
                floors.Add(floorToCreate);
            }
        }

        public void InstantiateLevel()
        {
        }
    }
}
