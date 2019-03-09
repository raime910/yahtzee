using System;
using System.Collections.Generic;
using System.Linq;

namespace Yahtzee.Models
{
    /// <summary>
    /// Represents a single player
    /// </summary>
    public abstract class Player
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="isComputer">if set to <c>true</c> [is computer].</param>
        public Player(string name, bool isComputer = false)
        {
            this.Turn = new Turn();

            this.Name = name;
            this.IsComputer = isComputer;
        }

        #endregion Ctor

        #region Properties

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// The turn index.
        /// </summary>
        protected int TurnIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is computer.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is computer; otherwise, <c>false</c>.
        /// </value>
        public bool IsComputer { get; set; }

        /// <summary>
        /// Gets the turns.
        /// </summary>
        /// <value>
        /// The turns.
        /// </value>
        public Turn Turn { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Executes the player's turn.
        /// </summary>
        public abstract void ExecuteTurn(Func<Turn, string> setPick);

        /// <summary>
        /// Gets the current turn score.
        /// </summary>
        /// <returns></returns>
        public int GetCurrentTurnScore() => this.Turn.GetScore();

        #endregion Methods
    }
}