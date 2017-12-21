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
