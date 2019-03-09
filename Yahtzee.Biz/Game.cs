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
            foreach (var name in humanNames.Distinct())
            {
                var player = new HumanPlayer(name);

                InitializePlayer(
                    GameConfiguration.NUMBER_OF_TURNS_PER_GAME,
                    GameConfiguration.NUMBER_OF_DICE_PER_TURN,
                    player);

                this.Players.Add(player);

                Console.WriteLine($"Player {player.Name} is now ready to play!");
            }

            if (compNames == null)
            {
                Console.WriteLine($"No computer players to initialize. Let's continue...");
                return;
            }

            foreach (var name in compNames.Distinct())
            {
                var player = new ComputerPlayer(name);

                InitializePlayer(
                    GameConfiguration.NUMBER_OF_TURNS_PER_GAME,
                    GameConfiguration.NUMBER_OF_DICE_PER_TURN,
                    player);

                this.Players.Add(player);

                Console.WriteLine($"Player {player.Name} is now ready to play!");
            }

            this.State = GameState.Initialized;
        }

        /// <summary>
        /// Initializes the player.
        /// </summary>
        /// <param name="numberOfTurns">The number of turns.</param>
        /// <param name="numberOfDicePerTurn">The number of dice per turn.</param>
        /// <param name="player">The player.</param>
        private void InitializePlayer(int numberOfTurns, int numberOfDicePerTurn, Player player)
        {
            // Add turns
            for (int i = 0; i < numberOfTurns; i++)
            {
                // Add die within each turn
                for (int j = 0; j < numberOfDicePerTurn; j++)
                {
                    player.Turn.Dice.Add(new Die());
                }
            }
        }

        /// <summary>
        /// Initializes the players then starts the game.
        /// </summary>
        public void Play()
        {
            if (this.State != GameState.Initialized)
            {
                Console.WriteLine("Initialize the game instance.");
            }

            this.State = GameState.Playing;

            Console.WriteLine("The game has started!");
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