using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameGeneration {
public class RoomGen : MonoBehaviour
{
    public int roomWidth = 10;
    public int roomHeight = 10;

    private int relativeStartX {get;set;}
    private int relativeStartY {get;set;}

    public void SetupRoom(Cell room, Transform floor, GameObject[] wallTiles){
        relativeStartX = Convert.ToInt32(roomWidth * room.vectorPosition.x);
        relativeStartY = Convert.ToInt32(roomHeight * room.vectorPosition.y);

        for (int x = relativeStartX; x < relativeStartX + roomWidth; x ++) {
            for (int y = relativeStartY; y < relativeStartY + roomHeight; y ++) {
                
                Vector3 pos = new Vector3(x, y, 0f);
            (Instantiate(room.tile, pos, Quaternion.identity) as GameObject).transform.SetParent(floor);
            // (Instantiate(wallTiles[(int)room.wallType], pos, Quaternion.identity) as GameObject).transform.SetParent(floor);
        }   
        }
    }
}

}
