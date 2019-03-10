using System;
using System.Linq;

namespace Yahtzee.Domain
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
            this.Turn.RollDice();
        }

        /// <summary>
        /// Executes the player's turn.
        /// </summary>
        /// <param name="setPick">Delegate for setting computer pick (testability purpose).</param>
        public override void ExecuteTurn(Func<Turn, string> setPick)
        {
            this.Turn.RollDice();
        }

        #endregion Methods
    }
}