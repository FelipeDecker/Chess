using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Board;

namespace Chess.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(Board.Board board, Color color) : base(board, color)
        {

        }

        public override string ToString()
        {
            return "B";
        }

        public bool CanMove(Position position)
        {
            Piece bishop = Board.Piece(position);
            return bishop == null || bishop.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Rows, Board.Columns];

            Position position = new Position(0, 0);

            //upper left diagonal

            position.SetValues(Position.Row - 1, Position.Column - 1);
            while (Board.IsPositionValid(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.SetValues(position.Row - 1, position.Column - 1);
            }

            //upper right diagonal

            position.SetValues(Position.Row - 1, Position.Column + 1);
            while (Board.IsPositionValid(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.SetValues(position.Row - 1, position.Column + 1);
            }

            //lower right diagonal

            position.SetValues(Position.Row + 1, Position.Column + 1);
            while (Board.IsPositionValid(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.SetValues(position.Row + 1, position.Column + 1);
            }

            //lower left diagonal

            position.SetValues(Position.Row + 1, Position.Column - 1);
            while (Board.IsPositionValid(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.SetValues(position.Row + 1, position.Column - 1);
            }

            return mat;
        }
    }
}
