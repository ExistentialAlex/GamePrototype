using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rand = UnityEngine.Random;

namespace GameGeneration
{
    public class Cell
    {
        public enum CellType
        {
            entrance,
            boss,
            stair,
            empty,
            room,
            shop,
            secret
        }

        public CellType type { get; }
        public Vector3 vectorPosition { get; set; }
        public Walls.WallTypes wallType { get; set; }
        public GameObject tile { get; set; }

        public List<Door> doors { get; set; }

        public Cell(CellType type, Vector3 vectorPosition, Walls.WallTypes wallType, GameObject tile)
        {
            this.type = type;
            this.vectorPosition = vectorPosition;
            this.wallType = wallType;
            this.tile = tile;
            this.doors = new List<Door>();
        }

        public override string ToString()
        {
            return Convert.ToString(type);
        }
    }
}
