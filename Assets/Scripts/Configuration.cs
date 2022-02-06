using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.GameGeneration
{
    public static class Configuration
    {
        public enum Layers
        {
            Default = 0,
            TransparentFx = 1,
            IgnoreRaycast = 2,
            Water = 4,
            UI = 5,
            BlockingLayer = 8,
            Walls = 9
        }

        public enum SortingLayers
        {
            Default,
            Floor,
            Items,
            Units,
            Walls
        }

        public enum InputAxes
        {
            Horizontal,
            Vertical
        }

        public const string COMPONENT_TAGS_STAIR = "Stair";
        public const string COMPONENT_TAGS_STAIR_DOWN = "Stair_Down";
        public const string COMPONENT_TAGS_EXIT = "Exit";
    }
}
