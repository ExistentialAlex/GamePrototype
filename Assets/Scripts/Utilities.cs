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

            int randomNo = rand.Range(0, items.Length);

            return items[randomNo];
        }
    }
}
