using MinefieldEngine;

namespace Minefield
{
    public class Interpreter
    {
        private readonly IGame _game;

        public bool ExitRequested { get; private set; }

        public Interpreter(IGame game)
        {
            _game = game;
        }

        public string HandleInput(string input)
        {
            input = input.ToLower().Trim();

            if (input == "exit")
            {
                ExitRequested = true;
                return "Exiting game";
            }
           
            var direction = GetDirectionFrom(input);
            if (direction.HasValue)
            {
                var result = _game.Move(direction.Value);
                return result.Format();
            }
            else
            { 
                return "Please enter a direction: 'left', 'right', 'up' or 'down'. Alternatively enter 'exit' to finish.";
            }
        }

        private bool IsADirection(string input)
        {
            switch (input)
            {
                case "left":
                case "right":
                case "up":
                case "down":
                    return true;
            }
            return false;
        }

        private Direction? GetDirectionFrom(string input)
        {
            switch (input)
            {
                case "left":
                    return Direction.Left;

                case "right":
                    return Direction.Right;

                case "up":
                    return Direction.Up;

                case "down":
                    return Direction.Down;

                default:
                    return null;
            }
        }
    }
}
