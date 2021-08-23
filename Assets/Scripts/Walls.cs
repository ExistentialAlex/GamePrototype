using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    public enum WallTypes
    {
        top_left_corner,
        top_right_corner,
        bottom_left_corner,
        bottom_right_corner,
        horizontal_open,
        vertical_open,
        top_open_side,
        right_open_side,
        bottom_open_side,
        left_open_side,
        all_walls
    }
}
