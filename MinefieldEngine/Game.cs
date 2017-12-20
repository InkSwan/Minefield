using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinefieldEngine
{
    public class Game : IGame
    {
        private int _x;
        private int _y;
        private const int Height = 10;
        private const int Width = 10;

        public Game(int x = 0, int y = 4)
        {
            _x = x;
            _y = y;
        }

        public MoveResult Move(Direction direction)
        {
            if (CanMoveIn(direction))
            {
                switch (direction)
                {
                    case Direction.Left:
                        _x--;
                        break;
                    case Direction.Right:
                        _x++;
                        break;
                    case Direction.Up:
                        _y--;
                        break;
                    case Direction.Down:
                        _y++;
                        break;
                }
            }

            return new MoveResult { Position = new Position(_x, _y) };
        }

        private bool CanMoveIn(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    return _x > 0;
                case Direction.Right:
                    return _x != Width - 1;
                case Direction.Up:
                    return _y > 0;
                case Direction.Down:
                    return _y != Height - 1;
            }

            return true;
        }
    }
}
