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

        public List<Vector3> innerTiles { get; set; }

        public Room(Vector3 vectorPosition, List<Walls.WallTypes> walls, GameObject tile)
        {
            this.vectorPosition = vectorPosition;
            this.walls = walls;
            this.tile = tile;
            this.doors = new List<Door>();
            this.innerTiles = new List<Vector3>();
        }

        public override string ToString()
        {
            return Convert.ToString(type);
        }
    }
}
