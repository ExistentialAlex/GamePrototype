namespace Prototype.GameGeneration
{
    using System;
    using System.Collections.Generic;
    using Prototype.GameGeneration.Rooms;
    using UnityEngine;
    using rand = UnityEngine.Random;

    /// <summary>
    /// Level generator for generating levels.
    /// </summary>
    public static class LevelGenerator
    {
        /// <summary>
        /// Generate the input level based on the floor configuration provided.
        /// </summary>
        /// <param name="level">The level to generate.</param>
        /// <param name="floorConfig">The floor configuration.</param>
        public static void GenerateLevel(Level level, FloorConfig floorConfig)
        {
            GenerateFloors(level, floorConfig);
            LinkStairs(level);
        }

        /// <summary>
        /// Generate the floors for the level.
        /// </summary>
        /// <param name="level">The level to generate.</param>
        /// <param name="floorConfig">The floor configuration.</param>
        private static void GenerateFloors(Level level, FloorConfig floorConfig)
        {
            Debug.Log("=== Generating Floors for Level " + level.NoFloors + " ===");
            int x = 0;
            int y = 0;
            for (int i = 0; i <= level.NoFloors; i++)
            {
                bool includeSecret = Convert.ToBoolean(rand.Range(0, 2));
                bool includeShop = Convert.ToBoolean(rand.Range(0, 2));
                Floor floorToCreate = new Floor(floorConfig, x, y, i, level.NoFloors, includeShop, includeSecret);
                FloorGenerator.GenerateFloor(floorToCreate);
                level.Floors.Add(floorToCreate);
                x = x + (Room.RoomWidth * floorConfig.FloorWidth) + Room.RoomWidth;
            }
        }

        /// <summary>
        /// Links the stairs between floors together.
        /// </summary>
        /// <param name="level">The level to link the stairs in.</param>
        private static void LinkStairs(Level level)
        {
            if (level.Floors.Count < 2)
            {
                // We don't need to link stairs if there's less than 2 floors.
                return;
            }

            List<StairRoom> stairs = new List<StairRoom>();

            foreach (Floor floor in level.Floors)
            {
                stairs.AddRange(floor.Stairs);
            }

            for (int i = 0; i < stairs.Count; i += 2)
            {
                StairRoom stair1 = stairs[i];
                StairRoom stair2 = stairs[i + 1];

                stair1.StairPair = stair2;
                stair2.StairPair = stair1;
            }
        }
    }
}
