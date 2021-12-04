using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rand = UnityEngine.Random;

namespace GameGeneration
{
    public class Room
    {
        public enum RoomType
        {
            entrance,
            boss,
            stair,
            empty,
            room,
            shop,
            secret
        }

        public RoomType type { get; }
        public Vector3 vectorPosition { get; set; }
        public List<Walls.WallTypes> walls { get; set; }
        public GameObject tile { get; set; }

        public List<Door> doors { get; set; }

        public Room(RoomType type, Vector3 vectorPosition, List<Walls.WallTypes> walls, GameObject tile)
        {
            this.type = type;
            this.vectorPosition = vectorPosition;
            this.walls = walls;
            this.tile = tile;
            this.doors = new List<Door>();
        }

        public override string ToString()
        {
            return Convert.ToString(type);
        }
    }
}
