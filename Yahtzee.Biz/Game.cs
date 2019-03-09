using System;
using System.Collections.Generic;
using System.Linq;
using Yahtzee.Models;

namespace Yahtzee.Biz
{
    /// <summary>
    /// A Yahtzee game instance.
    /// </summary>
    public class Game
    {
        #region Members

        /// <summary>
        /// The state of the game object.
        /// </summary>
        private GameState _state;

        #endregion Members

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        public Game()
        {
            this.Players = new List<Player>();
            this.State = GameState.NotReady;
        }

        #endregion Ctor

        #region Properties

        /// <summary>
        /// The players list.
        /// </summary>
        public List<Player> Players { get; set; }

        /// <summary>
        /// Gets the state of the game.
        /// </summary>
        /// <value>
        /// The state of the game.
        /// </value>
        public GameState State
        {
            get
            {
                return this._state;
            }
            set
            {
                this._state = value;
                Console.WriteLine($"Game state changed to {value}.");
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes the specified human names.
        /// </summary>
        /// <param name="humanNames">The human names.</param>
        /// <param name="compNames">The comp names.</param>
        public void Initialize(IEnumerable<string> humanNames, IEnumerable<string> compNames = null)
        {
            this.Players.AddRange(humanNames.Distinct().Select(x => new HumanPlayer(x)));

            // Check if there are any computer players, if not do not add any computer player.
            if (compNames != null)
            {
                this.Players.AddRange(compNames.Distinct().Select(x => new ComputerPlayer(x)));
            }

            this.State = GameState.Initialized;
        }

        /// <summary>
        /// Plays the specified set pick.
        /// </summary>
        /// <param name="setPick">The set pick.</param>
        public void Play(Func<Turn, string> setPick)
        {
            if (this.State != GameState.Initialized)
            {
                Console.WriteLine("Initialize the game instance.");
            }

            this.State = GameState.Playing;

            Console.WriteLine("The game has started!");

            foreach (var player in this.Players)
            {
                while (player.Turn.CanRoll)
                {
                    player.ExecuteTurn(setPick);
                }
            }
        }

        /// <summary>
        /// Identifies the winner.
        /// </summary>
        /// <returns></returns>
        public Player IdentifyWinner()
        {
            // Find the winner by looking at the current turn
            return this.Players.Aggregate(
                (player1, player2) =>
                    player1.GetCurrentTurnScore() > player2.GetCurrentTurnScore()
                        ? player1
                        : player2);
        }

        #endregion Methods
    }
}