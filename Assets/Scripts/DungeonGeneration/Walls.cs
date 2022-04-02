using System.Collections.Generic;

/// <summary>
/// Walls class containing method.
/// </summary>
public static class Walls
{
    /// <summary>
    /// Types of possible walls on rooms.
    /// </summary>
    public enum WallTypes
    {
        top,
        left,
        right,
        bottom,
    }

    /// <summary>
    /// Add all walls to the room.
    /// </summary>
    /// <returns>List of walls.</returns>
    public static List<WallTypes> AllWalls()
    {
        return new List<WallTypes>() { WallTypes.top, WallTypes.left, WallTypes.right, WallTypes.bottom };
    }

    /// <summary>
    /// Add left and bottom walls to the room.
    /// </summary>
    /// <returns>List of walls.</returns>
    public static List<WallTypes> BottomLeftCorner()
    {
        return new List<WallTypes>() { WallTypes.left, WallTypes.bottom };
    }

    /// <summary>
    /// Add top, left and right walls to the room.
    /// </summary>
    /// <returns>List of walls.</returns>
    public static List<WallTypes> BottomOpenSide()
    {
        return new List<WallTypes>() { WallTypes.top, WallTypes.left, WallTypes.right };
    }

    /// <summary>
    /// Add right and bottom walls to the room.
    /// </summary>
    /// <returns>List of walls.</returns>
    public static List<WallTypes> BottomRightCorner()
    {
        return new List<WallTypes>() { WallTypes.right, WallTypes.bottom };
    }

    /// <summary>
    /// Add top and bottom walls to the room.
    /// </summary>
    /// <returns>List of walls.</returns>
    public static List<WallTypes> HorizontalOpen()
    {
        return new List<WallTypes>() { WallTypes.top, WallTypes.bottom };
    }

    /// <summary>
    /// Add top, right and bottom walls to the room.
    /// </summary>
    /// <returns>List of walls.</returns>
    public static List<WallTypes> LeftOpenSide()
    {
        return new List<WallTypes>() { WallTypes.top, WallTypes.right, WallTypes.bottom };
    }

    /// <summary>
    /// Add top, left and bottom walls to the room.
    /// </summary>
    /// <returns>List of walls.</returns>
    public static List<WallTypes> RightOpenSide()
    {
        return new List<WallTypes>() { WallTypes.top, WallTypes.left, WallTypes.bottom };
    }

    /// <summary>
    /// Add top and left walls to the room.
    /// </summary>
    /// <returns>List of walls.</returns>
    public static List<WallTypes> TopLeftCorner()
    {
        return new List<WallTypes>() { WallTypes.top, WallTypes.left };
    }

    /// <summary>
    /// Add left, right and bottom walls to the room.
    /// </summary>
    /// <returns>List of walls.</returns>
    public static List<WallTypes> TopOpenSide()
    {
        return new List<WallTypes>() { WallTypes.left, WallTypes.right, WallTypes.bottom };
    }

    /// <summary>
    /// Add top and right walls to the room.
    /// </summary>
    /// <returns>List of walls.</returns>
    public static List<WallTypes> TopRightCorner()
    {
        return new List<WallTypes>() { WallTypes.top, WallTypes.right };
    }

    /// <summary>
    /// Add left and right walls to the room.
    /// </summary>
    /// <returns>List of walls.</returns>
    public static List<WallTypes> VerticalOpen()
    {
        return new List<WallTypes>() { WallTypes.left, WallTypes.right };
    }
}
