namespace MinefieldEngine
{
    public class MoveResult
    {
        public enum State
        {
            Alive,
            Dead,
            Succeeded
        }
        public State PlayerState { get; set; }
        public int Lives { get; set; }
        public int MovesTaken { get; set; }
        public Position Position { get; set; } = new Position(0, 0);
    }
}
