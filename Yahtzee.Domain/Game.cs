using System;
using System.Collections.Generic;
using System.Linq;

namespace Yahtzee.Domain
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
            Console.WriteLine(Environment.NewLine);

            foreach (var player in this.Players)
            {
                Console.WriteLine($"*****[{player.Name}]*****");

                while (player.Turn.CanRoll)
                {
                    player.ExecuteTurn(setPick);
                }

                var pick = player.Turn.Pick == 0 ? "x" : player.Turn.Pick.ToString();
                Console.WriteLine($"Pick: {pick}");
                Console.WriteLine($"Total Score: {player.GetCurrentTurnScore()}");
                Console.WriteLine(Environment.NewLine);
            }
        }

        /// <summary>
        /// Identifies the winner.
        /// </summary>
        /// <returns></returns>
        public void AnnounceWinner()
        {
            var highScore = this.Players.Max(x => x.GetCurrentTurnScore());
            var highScoringPlayers = this.Players.Where(x => x.GetCurrentTurnScore() == highScore).ToList();

            // There's a tie
            if (highScoringPlayers.Count() > 1)
            {
                // Filter out computer players
                highScoringPlayers = highScoringPlayers.Where(x => x.IsComputer == false).ToList();

                if (highScoringPlayers.Count() > 1)
                {
                    Console.WriteLine($"The winners are {string.Join(", ", highScoringPlayers.SelectMany(x => x.Name))}! Congratulations!");
                }
                else
                {
                    Console.WriteLine($"Congratulations {highScoringPlayers.First().Name}! You are the winner!");
                }
            }

            this.State = GameState.Finished;
        }

        #endregion Methods
    }
}