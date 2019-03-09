using System;
using System.Linq;

namespace Yahtzee.Models
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
            this.Turn.RollDice();

            if (this.Turn.RollCount == 1)
            {
                var picks = this.Turn.GetAvailablePicks();

                // If available picks > 1, ask user which number he/she wants to match after this turn.
                if (picks.Count > 1)
                {
                    Console.WriteLine($"{this.Name}, pick a number from these [{string.Join(",", picks)}]");

                    // Make sure the user picked a number and is a valid option
                    while (int.TryParse(setPick(this.Turn), out var pick) && picks.Contains(pick))
                    {
                        this.Turn.SetPick(pick);
                    }
                }
                else
                {
                    this.Turn.SetPick(picks.First());
                }
            }
        }

        #endregion Ctor
    }
}