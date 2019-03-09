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

        #endregion Ctor
    }
}