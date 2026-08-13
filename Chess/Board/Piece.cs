namespace Chess.Board
{
    public abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MoveCount { get; protected set; }
        public Board Board { get; protected set; }

        public abstract bool[,] PossibleMoves();

        public Piece(Board board, Color color)
        {
            Position = null;
            Color = color;
            Board = board;
            MoveCount = 0;
        }

        public void IncrementMoveCount()
        {
            MoveCount++;
        }

        public void DecrementMoveCount()
        {
            MoveCount--;
        }

        public bool HasPossibleMoves()
        {
            bool[,] mat = PossibleMoves();

            for (int l = 0; l < Board.Rows; l++)
            {
                for (int c = 0; c < Board.Columns; c++)
                {
                    if (mat[l, c])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool CanMoveTo(Position position)
        {
            return PossibleMoves()[position.Row, position.Column];
        }
    }
}
