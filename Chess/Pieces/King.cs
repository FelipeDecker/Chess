using Chess.Board;

namespace Chess.Pieces
{
    public class King : Piece
    {
        private Match Match { get; set; }

        public King(Board.Board board, Color color, Match match) : base (board, color)
        {
            Match = match;
        }

        private bool CanMove(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != Color;
        }

        private bool TestRookCastling(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece != null && piece is Rook && piece.Color == Color && piece.MoveCount == 0;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Rows, Board.Columns];

            Position position = new Position(0, 0);

            //up

            position.SetValues(Position.Row - 1, Position.Column);

            if (Board.IsPositionValid(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            //northeast

            position.SetValues(Position.Row - 1, Position.Column + 1);

            if (Board.IsPositionValid(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            //right

            position.SetValues(Position.Row, Position.Column + 1);

            if (Board.IsPositionValid(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            //southeast

            position.SetValues(Position.Row + 1, Position.Column + 1);

            if (Board.IsPositionValid(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            //down

            position.SetValues(Position.Row + 1, Position.Column);

            if (Board.IsPositionValid(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            //southwest

            position.SetValues(Position.Row + 1, Position.Column - 1);

            if (Board.IsPositionValid(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            //left

            position.SetValues(Position.Row, Position.Column - 1);

            if (Board.IsPositionValid(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            //northwest

            position.SetValues(Position.Row - 1, Position.Column - 1);

            if (Board.IsPositionValid(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            //Special Move - Castling

            if (MoveCount == 0 && !Match.Check)
            {
                //Kingside castling

                Position rookPosition1 = new Position(Position.Row, Position.Column + 3);
                if (TestRookCastling(rookPosition1)) 
                {
                    Position position1 = new Position(Position.Row, Position.Column + 1);
                    Position position2 = new Position(Position.Row, Position.Column + 2);

                    if (Board.Piece(position1) == null  && Board.Piece(position2) == null )
                    {
                        mat[Position.Row, Position.Column + 2] = true; 
                    }
                }

                //Queenside castling

                Position rookPosition2 = new Position(Position.Row, Position.Column - 4);
                if (TestRookCastling(rookPosition2))
                {
                    Position position1 = new Position(Position.Row, Position.Column - 1);
                    Position position2 = new Position(Position.Row, Position.Column - 2);
                    Position position3 = new Position(Position.Row, Position.Column - 3);

                    if (Board.Piece(position1) == null && Board.Piece(position2) == null && Board.Piece(position3) == null)
                    {
                        mat[Position.Row, Position.Column - 2] = true;
                    }
                }
            }

            return mat;
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
