using System;
using System.Linq;

namespace Yahtzee.Domain
{
    /// <summary>
    /// Class that represents a Human player.
    /// </summary>
    /// <seealso cref="Player" />
    public class HumanPlayer : Player
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="HumanPlayer"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public HumanPlayer(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Executes the player's turn.
        /// </summary>
        /// <param name="setPick">Delegate for setting user pick (testability purpose).</param>
        public override void ExecuteTurn(Func<Turn, string> setPick)
        {
            var values = this.Turn.RollDice();

            Console.WriteLine($"Player {this.Name} rolled {string.Join(",", values)}.");

            if (this.Turn.RollCount == 1)
            {
                var picks = this.Turn.GetAvailablePicks();
                var pick = 0;

                // If available picks > 1, ask user which number he/she wants to match after this turn.
                if (picks.Count > 1)
                {
                    do
                    {
                        Console.WriteLine($"{this.Name}, pick a number from the following options [{string.Join(",", picks)}]");
                    }
                    // Make sure the user picked a number and is a valid option
                    while (!int.TryParse(setPick(this.Turn), out pick) && !picks.Contains(pick));
                }

                this.Turn.SetPick(picks.First());
            }
        }

        #endregion Ctor
    }
}