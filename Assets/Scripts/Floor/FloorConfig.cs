using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameGeneration
{
    public class FloorConfig : MonoBehaviour
    {
        public int floorHeight = 6;
        public int floorWidth = 6;
        public int emptyCells = 3;
        public int max3Rooms = 3;
        public GameObject[] floorTiles;
        public GameObject[] bossTiles;
        public GameObject[] shopTiles;
        public GameObject[] secretTiles;
        public GameObject[] stairTiles;
        public GameObject emptyTile;
        public GameObject entranceTile;
        public GameObject exitTile;
        public GameObject[] wallTiles;
        public DoorConfig doorConfig { get; set; }

        public void GetDoorConfig()
        {
            doorConfig = GetComponent<DoorConfig>();
        }
    }
}
