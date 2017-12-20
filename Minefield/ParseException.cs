using System;

namespace Minefield
{
    public class ParseException : Exception
    {
        public ParseException(string message)
            :base(message)
        {
        }
    }
}
