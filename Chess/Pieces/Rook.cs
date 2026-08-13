using Chess.Board;

namespace Chess.Pieces
{
    public class Rook : Piece
    {
        public Rook(Board.Board board, Color color) : base(board, color)
        {

        }

        private bool CanMove(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Rows, Board.Columns];

            Position position = new Position(0, 0);

            //up

            position.SetValues(Position.Row - 1, Position.Column);

            while (Board.IsPositionValid(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.Row--;
            }

            //down

            position.SetValues(Position.Row + 1, Position.Column);

            while (Board.IsPositionValid(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.Row++;
            }

            //right

            position.SetValues(Position.Row, Position.Column + 1);

            while (Board.IsPositionValid(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.Column++;
            }

            //left

            position.SetValues(Position.Row, Position.Column - 1);

            while (Board.IsPositionValid(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.Column--;
            }

            return mat;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
