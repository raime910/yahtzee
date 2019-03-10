using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Yahtzee.Domain;

namespace Yahtzee.Test
{
    [TestClass]
    public class Game_Test
    {
        [TestMethod]
        public void Game_DuplicatePlayerName_ShouldNotBeAllowed()
        {
            var game = new Game();

            game.Initialize(new[] { "John", "John" }, new[] { "Jane", "Jane" });

            Assert.IsTrue(game.Players.Count == 2);
        }

        [TestMethod]
        public void Game_DuplicatePlayerNameBetweenHumanAndComps_AreOK()
        {
            var game = new Game();

            game.Initialize(new[] { "John", "Jane" }, new[] { "John", "Jane" });

            Assert.IsTrue(game.Players.Count == 4);
        }

        [TestMethod]
        public void Game_Initialize()
        {
            var game = new Game();

            game.Initialize(new[] { "John", "Jane" });

            Assert.IsTrue(game.Players.Count == 2);
            Assert.IsTrue(game.State == GameState.Initialized);
        }

        [TestMethod]
        public void Game_Playing()
        {
            var game = new Game();

            game.Initialize(new[] { "John", "Jane" });
            game.Play((turn) =>
            {
                return turn.GetAvailablePicks().First().ToString();
            });

            Assert.IsTrue(game.Players.Count == 2);
            Assert.IsTrue(game.State == GameState.Playing);
        }

        [TestMethod]
        public void Game_Finish()
        {
            var game = new Game();

            game.Initialize(new[] { "John", "Jane" });

            game.Play((turn) =>
            {
                return turn.GetAvailablePicks().First().ToString();
            });

            game.AnnounceWinner();

            Assert.IsTrue(game.State == GameState.Finished);
        }
    }
}