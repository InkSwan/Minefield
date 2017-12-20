using Minefield;
using MinefieldEngine;
using Moq;
using NUnit.Framework;

namespace MinfieldConsoleTest
{
    [TestFixture]
    public class ResultsFormatterTest
    {
        [Test]
        public void Format_ReturnsResultAsString()
        {
            var moveResult = new MoveResult { Position = new Position(3, 1), Lives = 3, PlayerState = MoveResult.State.Alive, MovesTaken = 2 };

            var result = moveResult.Format();

            Assert.AreEqual("Moved to position D2, 3 lives remain, 2 moves taken", result);
        }

        [TestCase(4, "4 lives remain")]
        [TestCase(1, "last life")]
        [TestCase(0, "no lives left")]
        public void Format_WithVariousLives_FormatsLives(int lives, string expectedFragment)
        {
            var target = new MoveResult { Lives = lives };

            var result = target.Format();

            StringAssert.Contains(expectedFragment, result);
        }

        [TestCase(0, 0, "A1")]
        [TestCase(5, 0, "F1")]
        [TestCase(7, 7, "H8")]
        public void Format_WithVariousPositions_FormatsPosition(int x, int y, string expectedFragment)
        {
            var target = new MoveResult { Position = new Position(x, y) };

            var result = target.Format();

            StringAssert.Contains(expectedFragment, result);
        }
    }
}
