using Minefield;
using MinefieldEngine;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var moveResult = new MoveResult { Position = new Position(3, 1), Lives = 3, PlayerState = MoveResult.State.Alive, MovesTaken = 2 };
            _mockGame.Setup(game => game.Move(Direction.Right)).Returns(moveResult);

            var result = _target.HandleInput("right");

            Assert.AreEqual("Moved to position D2, 3 lives remain, 2 moves taken", result);
        }
    }
}
