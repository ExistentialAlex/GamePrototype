using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using rand = UnityEngine.Random;
using Prototype.GameGeneration.Rooms;

namespace Prototype.GameGeneration
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

        public void InstantiateLevel()
        {
        }
    }

    public static class LevelGenerator
    {
        public static void GenerateLevel(Level level, FloorConfig floorConfig, RoomGenerator roomGenerator)
        {
            GenerateFloors(level, floorConfig, roomGenerator);
            LinkStairs(level, floorConfig);
        }

        private static void GenerateFloors(Level level, FloorConfig floorConfig, RoomGenerator roomGenerator)
        {
            Debug.Log("=== Generating Floors for Level " + level.noFloors + " ===");
            int x = 0;
            int y = 0;
            for (int i = 0; i <= level.noFloors; i++)
            {
                bool includeSecret = Convert.ToBoolean(rand.Range(0, 2));
                bool includeShop = Convert.ToBoolean(rand.Range(0, 2));
                Floor floorToCreate = new Floor(floorConfig, x, y, i, level.noFloors, includeShop, includeSecret);
                FloorGenerator.GenerateFloor(floorToCreate);
                level.floors.Add(floorToCreate);
                x = x + (Room.roomWidth * floorConfig.floorWidth) + Room.roomWidth;
            }
        }

        /// <summary>
        /// Links the stairs between floors together
        /// </summary>
        /// <param name="level"></param>
        private static void LinkStairs(Level level, FloorConfig floorConfig)
        {
            if (level.floors.Count < 2)
            {
                // We don't need to link stairs if there's less than 2 floors.
                return;
            }

            List<StairRoom> stairs = new List<StairRoom>();

            foreach (Floor floor in level.floors)
            {
                stairs.AddRange(floor.stairs);
            }

            for (int i = 0; i < stairs.Count; i += 2)
            {
                StairRoom stair1 = stairs[i];
                StairRoom stair2 = stairs[i + 1];

                stair1.stairPair = stair2;
                stair2.stairPair = stair1;
            }
        }
    }
}
