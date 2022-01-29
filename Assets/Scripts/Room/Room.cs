using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameGeneration.Rooms
{
    public class Room
    {
        public const int roomWidth = 11;
        public const int roomHeight = 11;

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

        public List<string[,]> templates { get; set; }
        public GameObject[,] template { get; set; }

        public Room(Vector3 vectorPosition, List<Walls.WallTypes> walls, GameObject[,] template)
        {
            this.vectorPosition = vectorPosition;
            this.walls = walls;
            this.tile = tile;
            this.doors = new List<Door>();
            this.innerTiles = new List<Vector3>();
            this.templates = new List<string[,]>();
            this.template = template;
        }

        public override string ToString()
        {
            return Convert.ToString(type);
        }
    }
}
