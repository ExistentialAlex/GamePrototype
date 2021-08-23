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
        private int xPosition { get; set; }
        private int yPosition { get; set; }
        public Cell(CellType type, int xPosition, int yPosition)
        {
            this.type = type;
            this.xPosition = xPosition;
            this.yPosition = yPosition;
        }

        public override string ToString()
        {
            return Convert.ToString(type);
        }
    }
}
