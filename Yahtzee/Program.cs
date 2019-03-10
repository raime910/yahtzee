using System;
using System.Collections.Generic;
using Yahtzee.Domain;

namespace Yahtzee
{
    internal class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
            // Create a new instance of the game.
            var game = new Game();

            // Setup players here...
            game.Initialize(new[] { "John" }, new[] { "Jane" });

            Console.WriteLine($"Press any key to start the game...");
            Console.ReadKey();
            Console.WriteLine(Environment.NewLine);

            game.Play((turn) => { return Console.ReadLine(); });
            game.AnnounceWinner();

            Console.ReadKey();
        }
    }
}