using MinefieldEngine;

namespace Minefield
{
    public class Interpreter
    {
        private readonly IGame _game;

        public Interpreter(IGame game)
        {
            _game = game;
        }

        public string HandleInput(string input)
        {
            input = input.ToLower().Trim();

            var direction = GetDirectionFrom(input);
            var result = _game.Move(direction);
            return result.Format();
        }

       
        private Direction GetDirectionFrom(string input)
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
            }

            throw new ParseException("Unknown direction: " + input);
        }
    }
}
