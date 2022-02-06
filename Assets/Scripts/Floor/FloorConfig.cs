using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.GameGeneration
{
    public class FloorConfig : MonoBehaviour
    {
        public int floorHeight = 6;
        public int floorWidth = 6;
        public int emptyCells = 3;
        public int max3Rooms = 3;

        public GameObject[] singleStandardRoomTemplates;
        public GameObject[] doubleStandardRoomTemplates;
        public GameObject[] tripleStandardRoomTemplates;
        public GameObject[] shopRoomTemplates;
        public GameObject[] secretRoomTemplates;
        public GameObject[] bossRoomTemplates;
        public GameObject[] stairRoomTemplates;
        public GameObject[] stairDownRoomTemplates;
        public GameObject[] emptyRoomTemplates;
        public GameObject[] entranceRoomTemplates;
        public DoorConfig doorConfig { get; set; }

        public void GetDoorConfig()
        {
            doorConfig = GetComponent<DoorConfig>();
        }
    }
}
