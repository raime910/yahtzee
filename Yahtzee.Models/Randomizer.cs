using System;

namespace Yahtzee.Models
{
    public class Randomizer
    {
        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static Randomizer Instance => _instance ?? (_instance = new Randomizer());

        /// <summary>
        /// The instance
        /// </summary>
        private static Randomizer _instance;

        /// <summary>
        /// Prevents a default instance of the <see cref="Randomizer"/> class from being created.
        /// </summary>
        private Randomizer() { }

        /// <summary>
        /// The random generator.
        /// </summary>
        private readonly Random _random = new Random();

        /// <summary>
        /// Gets the next.
        /// </summary>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns></returns>
        public int GenerateValue(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}