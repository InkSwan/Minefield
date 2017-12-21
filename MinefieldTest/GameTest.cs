using MinefieldEngine;
using NUnit.Framework;

namespace MinefieldTest
{
    [TestFixture]
    public class GameTest
    {
        [TestCase(Direction.Left, 3, 4)]
        [TestCase(Direction.Right, 5, 4)]
        [TestCase(Direction.Up, 4, 3)]
        [TestCase(Direction.Down, 4, 5)]
        public void Move_WithInitialPosition_4_4_UpdatesPosition(Direction direction, int expectedX, int expectedY)
        {
            var target = new Game(4, 4, mineDensity: 0.0);

            var result = target.Move(direction);

            Assert.AreEqual(expectedX, result.Position.X, "X position");
            Assert.AreEqual(expectedY, result.Position.Y, "Y position");
            Assert.AreEqual(1, result.MovesTaken);
        }

        [TestCase(Direction.Left, 0, 4)]
        [TestCase(Direction.Right, 9, 4)]
        [TestCase(Direction.Up, 4, 0)]
        [TestCase(Direction.Down, 4, 9)]
        public void Move_FromEdgeTowardsEdge_DoesNotUpdatePosition(Direction direction, int x, int y)
        {
            var target = new Game(x, y, mineDensity: 0.0);

            var result = target.Move(direction);

            Assert.AreEqual(x, result.Position.X, "X position");
            Assert.AreEqual(y, result.Position.Y, "Y position");
        }

        [Test]
        public void Move_RightFromRightEdge_Wins()
        {
            var target = new Game(9, 4);

            var result = target.Move(Direction.Right);

            Assert.AreEqual(1, result.MovesTaken);
            Assert.AreEqual(MoveResult.State.Succeeded, result.MoveState);
            Assert.IsTrue(target.Over);
        }

        [Test]
        public void Move_LeftFromLeftEdge_IsNoMove()
        {
            var target = new Game(0, 4);

            var result = target.Move(Direction.Left);

            Assert.AreEqual(0, result.MovesTaken);
            Assert.AreEqual(MoveResult.State.NoMove, result.MoveState);
        }

        [Test]
        public void Move_ToCellWithNoMine_StaysAlive()
        {
            var target = new Game(4, 4, randomSeed: 42);

            var result = target.Move(Direction.Right);

            Assert.AreEqual(MoveResult.State.Alive, result.MoveState);
            Assert.AreEqual(3, result.Lives);
        }

        [Test]
        public void Move_ToCellWithMine_LosesALife()
        {
            var target = new Game(2, 2, randomSeed: 42);

            var result = target.Move(Direction.Right);

            Assert.AreEqual(MoveResult.State.Dead, result.MoveState);
            Assert.AreEqual(2, result.Lives);
            Assert.IsFalse(target.Over);
            Assert.AreEqual(1, result.MovesTaken);
        }

        [Test]
        public void Move_ToCellWithMineOnLastLife_IsGameOver()
        {
            var target = new Game(2, 2, randomSeed: 42);
            target.Move(Direction.Right);
            target.Move(Direction.Right);

            var result = target.Move(Direction.Right);

            Assert.AreEqual(0, result.Lives);
            Assert.AreEqual(MoveResult.State.GameOver, result.MoveState);
            Assert.IsTrue(target.Over);
            Assert.AreEqual(3, result.MovesTaken);
        }

        [Test]
        public void Move_ToCellWithMine_ResetsPostion()
        {
            var target = new Game(0, 3, randomSeed: 42);
            target.Move(Direction.Right);
            target.Move(Direction.Right);

            var result = target.Move(Direction.Right);

            Assert.AreEqual(MoveResult.State.Dead, result.MoveState);
            Assert.AreEqual(0, result.Position.X, "X position");
            Assert.AreEqual(3, result.Position.Y, "Y position");
            Assert.AreEqual(3, result.MovesTaken);
        }
    }
}
