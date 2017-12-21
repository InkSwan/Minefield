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
        public void Format_GoodMove_ReturnsResultAsString()
        {
            var moveResult = new MoveResult { Position = new Position(3, 1), Lives = 3, MoveState = MoveResult.State.Alive, MovesTaken = 2 };

            var result = moveResult.Format();

            Assert.AreEqual("Moved to position D2, 3 lives remain, 2 moves taken", result);
        }

        [Test]
        public void Format_BadMove_ReturnsBang()
        {
            var moveResult = new MoveResult { Position = new Position(0, 1), Lives = 2, MoveState = MoveResult.State.Dead, MovesTaken = 4 };

            var result = moveResult.Format();

            Assert.AreEqual("BANG! You lost a life, reset to position A2, 2 lives remain, 4 moves taken", result);
        }

        [Test]
        public void Format_NoMove_ReturnsCantMove()
        {
            var moveResult = new MoveResult { Position = new Position(0, 1), Lives = 2, MoveState = MoveResult.State.NoMove, MovesTaken = 4 };

            var result = moveResult.Format();

            Assert.AreEqual("You can't move in that direction, still at position A2, 2 lives remain, 4 moves taken", result);
        }

        [Test]
        public void Format_WinningMove_ReturnsYouMadeIt()
        {
            var moveResult = new MoveResult { Position = new Position(0, 1), Lives = 2, MoveState = MoveResult.State.Succeeded, MovesTaken = 4 };

            var result = moveResult.Format();

            Assert.AreEqual("You made it!!!, 2 lives remain, 4 moves taken", result);
        }

        [Test]
        public void Format_GameOverMove_ReturnsGameOver()
        {
            var moveResult = new MoveResult { Position = new Position(0, 1), Lives = 0, MoveState = MoveResult.State.GameOver, MovesTaken = 4 };

            var result = moveResult.Format();

            Assert.AreEqual("Game over, no lives left, 4 moves taken", result);
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
