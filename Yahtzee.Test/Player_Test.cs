using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yahtzee.Models;

namespace Yahtzee.Test
{
    [TestClass]
    public class Player_Test
    {
        [TestMethod]
        public void HumanPlayer_Init()
        {
            var player = new HumanPlayer("John");

            Assert.IsNotNull(player.Turn);
        }

        [TestMethod]
        public void ComputerPlayer_Init()
        {
            var player = new ComputerPlayer("Jane");

            Assert.IsNotNull(player.Turn);
            Assert.IsTrue(player.Name.EndsWith("[COMP]"));
        }
    }
}