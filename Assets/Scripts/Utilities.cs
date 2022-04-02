namespace Prototype.Utilities
{
    using System;
    using System.Linq;
    using rand = UnityEngine.Random;

    /// <summary>
    /// Utility methods used throughout the game.
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Create a save string from the input items.
        /// </summary>
        /// <param name="items">Items to save.</param>
        /// <returns>The save string.</returns>
        public static string CreateSaveString(params string[] items)
        {
            string s = "";

            if (!items.Any() || items == null)
            {
                throw new Exception("No items provided.");
            }

            if (items.Length == 1)
            {
                return Convert.ToString(items[0]);
            }

            for (int i = 0; i < items.Length; i++)
            {
                s += items[i];
                if (i < items.Length - 1)
                {
                    s += "|";
                }
            }

            return s;
        }

        /// <summary>
        /// Pick a random item from an array of provided items.
        /// </summary>
        /// <typeparam name="T">The type of item to sort.</typeparam>
        /// <param name="items">List of items to pick from.</param>
        /// <returns>The randomly picked item.</returns>
        public static T PickRandom<T>(params T[] items)
        {
            if (!items.Any() || items == null)
            {
                throw new Exception("No items provided.");
            }

            if (items.Length == 1)
            {
                return items[0];
            }

            return items[rand.Range(0, items.Length)];
        }
    }
}
