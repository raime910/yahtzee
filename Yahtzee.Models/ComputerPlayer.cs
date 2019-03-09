using System;
using System.Linq;

namespace Yahtzee.Models
{
    /// <summary>
    /// Class that represents a computer player.
    /// </summary>
    /// <seealso cref="Player" />
    public class ComputerPlayer : Player
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="ComputerPlayer"/> class.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        public ComputerPlayer(string name)
            : base($"{name} [COMP]", true /* is a computer player */)
        {
        }

        #endregion Ctor

        #region Methods

        /// <summary>
        /// Executes the current turn.
        /// </summary>
        public void ExecuteTurn()
        {
            Console.WriteLine($"Player {this.Name} roll ");
            this.Turn.RollRemainingDice();
        }

        #endregion Methods
    }
}