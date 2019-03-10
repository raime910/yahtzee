using System;

namespace Yahtzee.Domain
{
    /// <summary>
    /// Represents a die within a turn.
    /// </summary>
    public class Die
    {
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
        public bool IsPicked { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Rolls the die and sets the value from 1-6.
        /// </summary>
        public void Roll()
        {
            this.Value = Randomizer.Instance.GenerateValue(1, 6);
        }

        #endregion Methods
    }
}