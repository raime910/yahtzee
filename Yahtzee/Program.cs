using System;
using System.Collections.Generic;
using Yahtzee.Biz;
using Yahtzee.Models;

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
            var playerCount = SetupPlayerCount();

            var game = new Game();

            game.Initialize(new List<Player>
            {
                new HumanPlayer("John"),
                new ComputerPlayer("Yoda")
            });

            game.Play();
        }

        /// <summary>
        /// Setups the player count.
        /// </summary>
        /// <returns></returns>
        private static (int humans, int computer) SetupPlayerCount()
        {
            var humansCount = 0;
            var computersCount = 0;

            Console.WriteLine($"Please enter the number of human players");

            do
            {
                int.TryParse(Console.ReadLine(), out humansCount);
            }
            while (humansCount == 0);

            Console.WriteLine($"Please enter the number of computer players");

            do
            {
                int.TryParse(Console.ReadLine(), out computersCount);
            }
            while (computersCount == 0);

            return (humansCount, computersCount);
        }

        /// <summary>
        /// Setups the player instances.
        /// </summary>
        /// <param name="humansCount">The humans count.</param>
        /// <param name="computerCount">The computer count.</param>
        /// <returns></returns>
        private static List<Player> SetupPlayerInstances(int humansCount, int computerCount)
        {
            throw new NotImplementedException();
        }
    }
}