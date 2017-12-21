using System;

namespace MinefieldEngine
{
    public class Game : IGame
    {
        private const int Height = 10;
        private const int Width = 10;

        private int _x;
        private int _y;
        private int _initialX;
        private int _initialY;
        private int _lives = 3;
        private int _moves = 0;
        private bool[,] _minefield = new bool[Width, Height];

        public bool Over { get; private set; }

        public Game(int x = 0, int y = 4, double mineDensity = 0.25, int? randomSeed = null)
        {
            _x = _initialX = x;
            _y = _initialY = y;

            BuryMines(mineDensity, randomSeed);
        }

        private void BuryMines(double mineDensity, int? randomSeed)
        {
            var random = (randomSeed == null) ? new Random() : new Random(randomSeed.Value);

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    _minefield[i, j] = random.NextDouble() < mineDensity;
                }
            }
        }

        public MoveResult Move(Direction direction)
        {
            MoveResult.State state;

            if (CanMoveIn(direction))
            {
                state = MakeMove(direction);
            }
            else if (direction == Direction.Right)
            {
                state = MoveResult.State.Succeeded;
                Over = true;
                _moves++;
            }
            else
            {
                state = MoveResult.State.NoMove;
            }

            return new MoveResult { Position = new Position(_x, _y), MoveState = state, Lives = _lives, MovesTaken = _moves };
        }

        private MoveResult.State MakeMove(Direction direction)
        {
            MoveIn(direction);

            if (MineAtPosition())
                return HandleExplosion();

            return MoveResult.State.Alive;
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

        private void MoveIn(Direction direction)
        {
            _moves++;

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

        private bool MineAtPosition()
        {
            return _minefield[_x, _y];
        }

        private MoveResult.State HandleExplosion()
        {
            _x = _initialX;
            _y = _initialY;

            _lives--;
            if (_lives > 0)
                return MoveResult.State.Dead;

            Over = true;
            return MoveResult.State.GameOver;
        }             
    }
}
