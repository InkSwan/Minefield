using MinefieldEngine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var target = new Game(4, 4);

            var result = target.Move(direction);

            Assert.AreEqual(expectedX, result.Position.X);
            Assert.AreEqual(expectedY, result.Position.Y);
        }

        [TestCase(Direction.Left, 0, 4)]
        [TestCase(Direction.Right, 9, 4)]
        [TestCase(Direction.Up, 4, 0)]
        [TestCase(Direction.Down, 4, 9)]
        public void Move_FromEdgeTowardsEdge_DoesNotUpdatePosition(Direction direction, int x, int y)
        {
            var target = new Game(x,y);

            var result = target.Move(direction);

            Assert.AreEqual(x, result.Position.X);
            Assert.AreEqual(y, result.Position.Y);
        }
    }
}
