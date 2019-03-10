using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yahtzee.Domain;

namespace Yahtzee.Test
{
    [TestClass]
    public class Die_Test
    {
        [TestMethod]
        public void Die_RollMe_SetsValue1To6()
        {
            var die = new Die();
            die.Roll();

            Assert.IsTrue(die.Value > 0);
        }

        [TestMethod]
        public void Die_PickMe_AfterRoll_ShouldSetFlag()
        {
            var die = new Die();
            die.Roll();
            die.IsPicked = true;

            Assert.IsTrue(die.IsPicked);
        }

        [TestMethod]
        public void Die_PickMe_BeforeRoll_ShouldNotSetFlag()
        {
            var die = new Die();

            Assert.IsFalse(die.IsPicked);
        }
    }
}