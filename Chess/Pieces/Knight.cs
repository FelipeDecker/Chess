using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Board;

namespace Chess.Pieces
{
    class Knight : Piece
    {
        public Knight(Board.Board board, Color color) : base(board, color)
        {

        }

        public override string ToString()
        {
            return "N";
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

            // upper left

            position.SetValues(Position.Row - 1, Position.Column - 2);
            if (Board.IsPositionValid(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            // upper right

            position.SetValues(Position.Row - 1, Position.Column + 2);
            if (Board.IsPositionValid(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            // lower left

            position.SetValues(Position.Row + 1, Position.Column - 2);
            if (Board.IsPositionValid(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            // lower right

            position.SetValues(Position.Row + 1, Position.Column + 2);
            if (Board.IsPositionValid(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            // upper left s

            position.SetValues(Position.Row - 2, Position.Column - 1);
            if (Board.IsPositionValid(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            // upper right s 

            position.SetValues(Position.Row - 2, Position.Column + 1);
            if (Board.IsPositionValid(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            // lower left

            position.SetValues(Position.Row + 2, Position.Column - 1);
            if (Board.IsPositionValid(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            // lower right

            position.SetValues(Position.Row + 2, Position.Column + 1);
            if (Board.IsPositionValid(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            return mat;
        }
    }
}
