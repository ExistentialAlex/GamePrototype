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

        public Level(int levelNo)
        {
            noFloors = levelNo;
            floors = new List<Floor>();
        }

        public void GenerateLevel(FloorConfig floorConfig, RoomGenerator roomGenerator)
        {
            GenerateFloors(floorConfig, roomGenerator);
        }

        private void GenerateFloors(FloorConfig floorConfig, RoomGenerator roomGenerator)
        {
            Debug.Log("=== Generating Floors for Level " + noFloors + " ===");
            int x = 0;
            int y = 0;
            for (int i = 0; i <= noFloors; i++)
            {
                bool includeSecret = Convert.ToBoolean(rand.Range(0, 2));
                bool includeShop = Convert.ToBoolean(rand.Range(0, 2));
                Floor floorToCreate = new Floor(floorConfig, x, y, i, noFloors, includeShop, includeSecret);
                floorToCreate.GenerateFloor();
                floors.Add(floorToCreate);
                x = x + (roomGenerator.roomWidth * floorConfig.floorWidth) + 2;
            }
        }

        public void InstantiateLevel()
        {
        }
    }
}
