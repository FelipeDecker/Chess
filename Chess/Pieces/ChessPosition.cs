using Chess.Board;

namespace Chess.Pieces
{
    public class ChessPosition
    {
        public char Column { get; set; }
        public int Row { get; set; }

        public ChessPosition(char column, int row)
        {
            Column = column;
            Row = row;
        }

        public Position ToPosition()
        {
            return new Position(8 - Row, Column - 'A');
        }

        public override string ToString()
        {
            return "" + Column + Row;
        }
    }
}
