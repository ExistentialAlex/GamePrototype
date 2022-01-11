using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameGeneration.Rooms
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

        public RoomType type { get; set; }
        public Vector3 vectorPosition { get; set; }
        public List<Walls.WallTypes> walls { get; set; }
        public GameObject tile { get; set; }

        public List<Door> doors { get; set; }

        public Room(Vector3 vectorPosition, List<Walls.WallTypes> walls, GameObject tile)
        {
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
