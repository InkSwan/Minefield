using Minefield;
using MinefieldEngine;
using Moq;
using NUnit.Framework;

namespace MinefieldConsoleTest
{
    [TestFixture]
    public class InterpreterTest
    {
        private Mock<IGame> _mockGame;
        private Interpreter _target;

        [SetUp]
        public void SetUp()
        {
            _mockGame = new Mock<IGame>();
            _target = new Interpreter(_mockGame.Object);

            _mockGame.Setup(game => game.Move(It.IsAny<Direction>())).Returns(new MoveResult());
        }

        [TestCase("left")]
        [TestCase("Left")]
        [TestCase("  left")]
        [TestCase("left   ")]
        public void HandleInput_WithLeft_MovesLeft(string input)
        {
            _target.HandleInput(input);

            _mockGame.Verify(game => game.Move(Direction.Left));
        }

        [TestCase("right", Direction.Right)]
        [TestCase("up", Direction.Up)]
        [TestCase("down", Direction.Down)]
        public void HandleInput_WithDirection_MovesInDirection(string input, Direction expectedDirection)
        {
            _target.HandleInput(input);

            _mockGame.Verify(game => game.Move(expectedDirection));
        }

        [Test]
        public void HandleInput_WithMove_FormatsResult()
        {
            var moveResult = new MoveResult { Position = new Position(3, 1), Lives = 3, MoveState = MoveResult.State.Alive, MovesTaken = 2 };
            _mockGame.Setup(game => game.Move(Direction.Right)).Returns(moveResult);

            var result = _target.HandleInput("right");

            Assert.AreEqual("Moved to position D2, 3 lives remain, 2 moves taken", result);
            Assert.IsFalse(_target.ExitRequested);
        }

        [Test]
        public void HandleInput_WithRubbish_ReturnsHelp()
        {
            var result = _target.HandleInput("invalid input");

            Assert.AreEqual("Please enter a direction: 'left', 'right', 'up' or 'down'. Alternatively enter 'exit' to finish.", result);
            Assert.IsFalse(_target.ExitRequested);
        }


        [Test]
        public void HandleInput_WithExit_SignalsExit()
        {
            var result = _target.HandleInput("exit");

            Assert.IsTrue(_target.ExitRequested);
            Assert.AreEqual("Exiting game", result);
        }
    }
}
