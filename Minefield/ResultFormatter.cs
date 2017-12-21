using MinefieldEngine;

namespace Minefield
{
    public static class ResultFormatter
    {
        public static string Format(this MoveResult result)
        {
            string formatString = "";
            switch (result.MoveState)
            {
                case MoveResult.State.Dead:
                    formatString = "BANG! You lost a life, reset to position {0}, {1}, {2} moves taken";
                    break;
                case MoveResult.State.Alive:
                    formatString = "Moved to position {0}, {1}, {2} moves taken";
                    break;
                case MoveResult.State.NoMove:
                    formatString = "You can't move in that direction, still at position {0}, {1}, {2} moves taken";
                    break;
                case MoveResult.State.Succeeded:
                    formatString = "You made it!!!, {1}, {2} moves taken";
                    break;
                case MoveResult.State.GameOver:
                    formatString = "Game over, {1}, {2} moves taken";
                    break;
            }

            return string.Format(formatString, FormatPostion(result), FormatLives(result), result.MovesTaken);
        }


        private static string FormatPostion(MoveResult result)
        {
            return $"{(char)('A' + result.Position.X)}{result.Position.Y + 1}";
        }


        private static string FormatLives(MoveResult result)
        {
            if (result.Lives > 1)
                return $"{ result.Lives } lives remain";
            else if (result.Lives == 1)
                return "last life";
            else
                return "no lives left";
        }
    }
}
