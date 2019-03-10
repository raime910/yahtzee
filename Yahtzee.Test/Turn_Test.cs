using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Yahtzee.Domain;

namespace Yahtzee.Test
{
    [TestClass]
    public class Turn_Test
    {
        [TestMethod]
        public void Turn_Init()
        {
            var turn = new Turn();

            Assert.IsNotNull(turn.Dice);
        }

        [TestMethod]
        public void Turn_SetPick_NoRoll()
        {
            var turn = new Turn();

            turn.Dice.Add(new Die());
            turn.Dice.Add(new Die());
            turn.Dice.Add(new Die());
            turn.Dice.Add(new Die());
            turn.Dice.Add(new Die());

            turn.SetPick(1);

            Assert.IsTrue(turn.Pick == 0);
        }

        [TestMethod]
        public void Turn_SetPick_WithRoll()
        {
            var turn = new Turn();

            turn.Dice.Add(new Die());
            turn.Dice.Add(new Die());
            turn.Dice.Add(new Die());
            turn.Dice.Add(new Die());
            turn.Dice.Add(new Die());

            turn.RollDice();

            var pick = turn.GetAvailablePicks().First();

            turn.SetPick(pick);

            Assert.IsTrue(turn.Pick == pick);
        }

        [TestMethod]
        public void Turn_GetAvailablePicks_NoRoll_ShouldReturnEmptyList()
        {
            var turn = new Turn();

            turn.Dice.Add(new Die());
            turn.Dice.Add(new Die());
            turn.Dice.Add(new Die());
            turn.Dice.Add(new Die());
            turn.Dice.Add(new Die());

            var result = turn.GetAvailablePicks();

            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void Turn_GetAvailablePicks_WithRoll_ShouldReturnListWithItems()
        {
            var turn = new Turn();

            turn.Dice.Add(new Die());
            turn.Dice.Add(new Die());
            turn.Dice.Add(new Die());
            turn.Dice.Add(new Die());
            turn.Dice.Add(new Die());

            turn.RollDice();
            var result = turn.GetAvailablePicks();

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void Turn_RollAndGetScore_SouldReturnAScore()
        {
            var turn = new Turn();

            turn.Dice.Add(new Die());
            turn.Dice.Add(new Die());
            turn.Dice.Add(new Die());
            turn.Dice.Add(new Die());
            turn.Dice.Add(new Die());

            turn.RollDice();

            var pick = turn.GetAvailablePicks().First();
            turn.SetPick(pick);

            turn.RollDice();
            turn.RollDice();

            Assert.IsTrue(turn.GetScore() > 0);
        }

        [TestMethod]
        public void Turn_RollCount_ShouldNotExceed3Rolls()
        {
            var turn = new Turn();

            turn.Dice.Add(new Die());
            turn.Dice.Add(new Die());
            turn.Dice.Add(new Die());
            turn.Dice.Add(new Die());
            turn.Dice.Add(new Die());

            turn.RollDice();
            turn.RollDice();
            turn.RollDice();
            turn.RollDice();
            turn.RollDice();

            Assert.AreEqual(turn.RollCount, 2);
        }
    }
}