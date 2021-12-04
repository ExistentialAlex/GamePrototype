using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Walls
{
    public enum WallTypes
    {
        top,
        left,
        right,
        bottom,
        extra_top_left,
        extra_top_right,
        extra_bottom_left,
        extra_bottom_right
    }

    public static List<WallTypes> TopLeftCorner()
    {
        return new List<WallTypes>() { WallTypes.top, WallTypes.left, WallTypes.extra_bottom_right };
    }

    public static List<WallTypes> TopRightCorner()
    {
        return new List<WallTypes>() { WallTypes.top, WallTypes.right, WallTypes.extra_bottom_left };
    }

    public static List<WallTypes> BottomLeftCorner()
    {
        return new List<WallTypes>() { WallTypes.left, WallTypes.bottom, WallTypes.extra_top_right };
    }

    public static List<WallTypes> BottomRightCorner()
    {
        return new List<WallTypes>() { WallTypes.right, WallTypes.bottom, WallTypes.extra_top_left };
    }

    public static List<WallTypes> HorizontalOpen()
    {
        return new List<WallTypes>() { WallTypes.top, WallTypes.bottom };
    }

    public static List<WallTypes> VerticalOpen()
    {
        return new List<WallTypes>() { WallTypes.left, WallTypes.right };
    }

    public static List<WallTypes> TopOpenSide()
    {
        return new List<WallTypes>() { WallTypes.left, WallTypes.right, WallTypes.bottom };
    }

    public static List<WallTypes> LeftOpenSide()
    {
        return new List<WallTypes>() { WallTypes.top, WallTypes.right, WallTypes.bottom };
    }

    public static List<WallTypes> RightOpenSide()
    {
        return new List<WallTypes>() { WallTypes.top, WallTypes.left, WallTypes.bottom };
    }

    public static List<WallTypes> BottomOpenSide()
    {
        return new List<WallTypes>() { WallTypes.top, WallTypes.left, WallTypes.right };
    }

    public static List<WallTypes> AllWalls()
    {
        return new List<WallTypes>() { WallTypes.top, WallTypes.left, WallTypes.right, WallTypes.bottom };
    }
}
