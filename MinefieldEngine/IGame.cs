using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinefieldEngine
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public interface IGame
    {
        MoveResult Move(Direction direction);
    }
}
