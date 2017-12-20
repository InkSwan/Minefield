using MinefieldEngine;

namespace Minefield
{
    public static class ResultFormatter
    {
        public static string Format(this MoveResult result)
        {
            return $"Moved to position {FormatPostion(result)}, {FormatLives(result)}, 2 moves taken";
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
