using System;
using System.Linq;
using rand = UnityEngine.Random;

namespace Prototype.Utilities
{
    public static class Utilities
    {
        /// <summary>
        /// Pick a random item from an array of provided items
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
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

            int randomNo = rand.Range(0, items.Length - 1);

            return items[randomNo];
        }
    }
}
