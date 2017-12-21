namespace MinefieldEngine
{
    public class MoveResult
    {
        public enum State
        {
            NoMove,
            Alive,
            Dead,
            Succeeded,
            GameOver
        }
        public State MoveState { get; set; }
        public int Lives { get; set; }
        public int MovesTaken { get; set; }
        public Position Position { get; set; } = new Position(0, 0);
    }
}
