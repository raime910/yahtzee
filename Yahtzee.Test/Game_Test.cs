using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Yahtzee.Biz;
using Yahtzee.Models;

namespace Yahtzee.Test
{
    [TestClass]
    public class Game_Test
    {
        [TestMethod]
        public void Game_DuplicatePlayerName_ShouldNotAllowed()
        {
            var game = new Game();

            game.Initialize(new List<Player>
            {
                new HumanPlayer("John"),
                new HumanPlayer("John")
            });

            Assert.IsTrue(game.Players.Count == 1);
        }

        [TestMethod]
        public void Game_Initialize()
        {
            var game = new Game();

            game.Initialize(new List<Player>
            {
                new HumanPlayer("John"),
                new HumanPlayer("Jane")
            });

            Assert.IsTrue(game.Players.Count == 2);
            Assert.IsTrue(game.Players.ElementAt(0).Turn.Dice.Any());
            Assert.IsTrue(game.Players.ElementAt(1).Turn.Dice.Any());
        }

        [TestMethod]
        public void Game_Start()
        {
            var game = new Game();

            game.Initialize(new List<Player>
            {
                new HumanPlayer("John"),
                new HumanPlayer("Jane")
            });

            game.Play();

            Assert.IsTrue(game.State == GameState.Playing);
        }
    }
}