using System;

namespace Automation.Framework.Utilities
{
    /// <summary>
    /// Helper class for math computations.
    /// </summary>
    public static class MathHelper
    {

        /// <summary>
        /// Gets a random number within a range with 0 index and exludes the max.
        /// <param name="max">Max is not inclusive.</param>
        /// </summary>
        public static int GetRandomNumber(int max)
        {
            return new Random().Next(0, max);
        }

        /// <summary>
        /// Gets a random number within a range that includes the min and excludes the max.
        /// <param name="min">Min is inclusive.</param>
        /// <param name="max">Max is not inclusive.</param>
        /// </summary>
        public static int GetRandomNumber(int min, int max)
        {
            return new Random().Next(min, max);
        }

    }
}
