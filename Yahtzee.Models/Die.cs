using System;

namespace Yahtzee.Models
{
    /// <summary>
    /// Represents a die within a turn.
    /// </summary>
    public class Die
    {
        #region Members

        private static Random _Random = new Random();

        #endregion Members

        #region Properties

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public int Value { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is picked.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this die is picked; otherwise, <c>false</c>.
        /// </value>
        public bool IsPicked { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Rolls the die and sets the value from 1-6.
        /// </summary>
        public void Roll()
        {
            this.Value = _Random.Next(1, 6);
        }

        /// <summary>
        /// Picks me.
        /// </summary>
        /// <param name="isPicked">if set to <c>true</c> [is picked].</param>
        public void PickMe(bool isPicked = true)
        {
            /* parameterized isPicked to give the player the ability to change his/her mind */
            if (this.Value > 0)
            {
                this.IsPicked = isPicked;
                return;
            }

            Console.WriteLine("You'll have to roll me first!");
        }

        #endregion Methods
    }
}