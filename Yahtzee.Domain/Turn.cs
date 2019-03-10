using System;
using System.Collections.Generic;
using System.Linq;

namespace Yahtzee.Domain
{
    /// <summary>
    /// Represents a single turn that encapsulates multiple rolls.
    /// </summary>
    public class Turn
    {
        #region Constants

        /// <summary>
        /// The maximum number rolls per turn.
        /// </summary>
        private const int _MAX_ROLLS_PER_TURN = 2;

        #endregion Constants

        #region Fields

        /// <summary>
        /// The results per roll.
        /// </summary>
        private readonly Dictionary<int, IEnumerable<int>> _resultsPerRoll;

        /// <summary>
        /// The dice collection.
        /// </summary>
        private readonly List<Die> _dice;

        #endregion Fields

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="Turn"/> class.
        /// </summary>
        public Turn()
        {
            this._dice = new List<Die> {
                new Die(),
                new Die(),
                new Die(),
                new Die(),
                new Die()
            };
            this._resultsPerRoll = new Dictionary<int, IEnumerable<int>>();
        }

        #endregion Ctor

        #region Properties

        /// <summary>
        /// Gets the dice.
        /// </summary>
        /// <value>
        /// The dice.
        /// </value>
        public List<Die> Dice => this._dice;

        /// <summary>
        /// Gets the roll count.
        /// </summary>
        /// <value>
        /// The roll count.
        /// </value>
        public int RollCount { get; private set; }

        /// <summary>
        /// The users die value pick.
        /// </summary>
        public int Pick
        {
            get; private set;
        }

        /// <summary>
        /// Gets a value indicating whether this instance can roll.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can roll; otherwise, <c>false</c>.
        /// </value>
        public bool CanRoll
        {
            get
            {
                /* Check and make sure that
                 * 1. The player still has a roll remaining within this turn
                 * 2. The player still has some dice left to roll.
                 */
                return this.RollCount < _MAX_ROLLS_PER_TURN && this.Dice.Any(x => !x.IsPicked);
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Rolls the remaining dice.
        /// </summary>
        public List<int> RollDice()
        {
            var values = new List<int>();

            if (this.CanRoll)
            {
                foreach (var die in this.Dice.Where(x => !x.IsPicked))
                {
                    die.Roll();

                    /* Add result of this roll within the collection
                     * which be later then added into the dictionary.
                     */
                    values.Add(die.Value);
                }

                this.RollCount++;

                // Add values into the dictionary.
                this._resultsPerRoll.Add(this.RollCount, values);
            }
            else
            {
                Console.WriteLine($"This turn has reached its max roll count or has no more dice left to roll.");
            }

            Console.WriteLine($"Player rolled {string.Join(",", values.OrderBy(x => x))}.");

            return values;
        }

        /// <summary>
        /// Gets the available picks.
        /// </summary>
        /// <value>
        /// The available picks.
        /// </value>
        public List<int> GetAvailablePicks()
        {
            if (this.RollCount > 1)
            {
                return new List<int>();
            }

            if (this._resultsPerRoll.Any())
            {
                var grouping = this.GetRollGroupings(this.RollCount - 1).ToList();

                // Identify max occurence within the collection of values from the first roll.
                var maxScore = grouping.Select(x => x.Count).Max();

                // Return the value(s) with the most number of occurences.
                var result = grouping
                    .Where(x => x.Count == maxScore)
                    .Select(x => x.Value)
                    .OrderBy(x => x)
                    .ToList();

                return result;
            }

            Console.WriteLine("Player needs to roll the dice first.");

            // Avoid returning null to prevent possible null reference exceptions
            return new List<int>();
        }

        /// <summary>
        /// Gets the roll groupings.
        /// </summary>
        /// <param name="rollCount">The roll count.</param>
        /// <returns></returns>
        private IEnumerable<RollGrouping> GetRollGroupings(int rollCount)
        {
            // Setup grouping for this roll.
            var query = from value in this._resultsPerRoll.ElementAt(rollCount).Value
                        group value by value into grouping  // Set grouping by die value
                        let groupCount = grouping.Count()   // Count each item per grouping
                        orderby groupCount descending       // Set result order by desc
                        select new RollGrouping             // Create a new anonymous object that will hold each count per grouping
                        {
                            Value = grouping.Key,
                            Count = groupCount
                        };

            return query;
        }

        /// <summary>
        /// Sets the pick.
        /// </summary>
        /// <param name="pick">The pick.</param>
        public void SetPick(int pick)
        {
            if (this.RollCount > 1)
            {
                Console.WriteLine("You can only assign the pick after the first roll");
            }

            // Validate entry here, still.
            this.Pick = this.GetAvailablePicks().Any(x => x == pick)
                ? pick
                : 0;

            // Set die flags
            foreach (var die in this.Dice.Where(x => x.Value == pick))
            {
                die.IsPicked = true;
            }
        }

        /// <summary>
        /// Gets the score.
        /// </summary>
        /// <returns>The calculated score based on user's pick (die value).</returns>
        public int GetScore()
        {
            return this.Pick == 0
                ? ComputeScoreBySeries()
                : ComputeScoreByPick();
        }

        /// <summary>
        /// Computes the score by pick.
        /// </summary>
        /// <returns></returns>
        private int ComputeScoreByPick()
        {
            var score = 0;

            foreach (var rollResult in this._resultsPerRoll)
            {
                score += rollResult.Value.Count(x => x == this.Pick);
            }

            return score;
        }

        /// <summary>
        /// Computes the score by series.
        /// </summary>
        /// <returns></returns>
        private int ComputeScoreBySeries()
        {
            var score = 0;

            for (int i = 0; i < this._resultsPerRoll.Count; i++)
            {
                var grouping = this.GetRollGroupings(i).ToList();
                var max = grouping.Select(x => x.Count).Max();

                if (score < max)
                {
                    score = max;
                }
            }

            return score;
        }

        #endregion Methods
    }
}