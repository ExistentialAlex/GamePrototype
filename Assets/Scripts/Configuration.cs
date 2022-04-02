namespace Prototype.Common
{
    /// <summary>
    /// Configuration class containing constants and enumerations.
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// Exit Tag used for collision.
        /// </summary>
        public const string COMPONENT_TAGS_EXIT = "Exit";

        /// <summary>
        /// Stairs Tag used for collision.
        /// </summary>
        public const string COMPONENT_TAGS_STAIR = "Stair";

        /// <summary>
        /// Stairs Down Tag used for collision.
        /// </summary>
        public const string COMPONENT_TAGS_STAIR_DOWN = "Stair_Down";

        public enum Collidables
        {
            Player,
            Stair,
            Stair_Down,
            Exit
        }

        /// <summary>
        /// Input axes for use in movement.
        /// </summary>
        public enum InputAxes
        {
            Horizontal,
            Vertical
        }

        /// <summary>
        /// Layers available in unity.
        /// </summary>
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

        public enum Scenes
        {
            Dungeon,
            Town
        }

        /// <summary>
        /// Sorting layers available in unity.
        /// </summary>
        public enum SortingLayers
        {
            Default,
            Floor,
            Items,
            Units,
            Walls
        }
    }
}
