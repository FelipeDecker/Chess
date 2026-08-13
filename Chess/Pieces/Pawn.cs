using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Board;

namespace Chess.Pieces
{
    class Pawn : Piece
    {
        private Match Match { get; set; }

        public Pawn(Board.Board board, Color color, Match match) : base(board, color)
        {
            Match = match;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool CanMove(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != Color;
        }

        public bool HasEnemy(Position position)
        {
            Piece piece = Board.Piece(position);
            return Board.Piece(position) != null && piece.Color != Color;
        }

        public bool IsFree(Position position)
        {
            return Board.Piece(position) == null;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Rows, Board.Columns];

            Position position = new Position(0, 0);

            if (Color == Color.White)
            {
                // one step forward

                position.SetValues(Position.Row - 1, Position.Column);
                if (Board.IsPositionValid(position) && IsFree(position))
                {
                    mat[position.Row, position.Column] = true;
                }

                // two steps forward

                position.SetValues(Position.Row - 2, Position.Column);
                if (Board.IsPositionValid(position) && IsFree(position) && MoveCount == 0)
                {
                    mat[position.Row, position.Column] = true;
                }

                // left

                position.SetValues(Position.Row - 1, Position.Column - 1);
                if (Board.IsPositionValid(position) && HasEnemy(position))
                {
                    mat[position.Row, position.Column] = true;
                }

                // right

                position.SetValues(Position.Row - 1, Position.Column + 1);
                if (Board.IsPositionValid(position) && HasEnemy(position))
                {
                    mat[position.Row, position.Column] = true;
                }

                // Special Move - En passant
                if (Position.Row == 3)
                {
                    Position left = new Position(Position.Row, Position.Column -1);

                    if (Board.IsPositionValid(left) && HasEnemy(left) && Board.Piece(left) == Match.VulnerableEnPassant)
                    {
                        mat[left.Row - 1, left.Column] = true;
                    }

                    Position right = new Position(Position.Row, Position.Column + 1);

                    if (Board.IsPositionValid(right) && HasEnemy(right) && Board.Piece(right) == Match.VulnerableEnPassant)
                    {
                        mat[right.Row - 1, right.Column] = true;
                    }
                }
            }
            else if (Color == Color.Black)
            {
                // one step forward

                position.SetValues(Position.Row + 1, Position.Column);
                if (Board.IsPositionValid(position) && IsFree(position))
                {
                    mat[position.Row, position.Column] = true;
                }

                // two steps forward

                position.SetValues(Position.Row + 2, Position.Column);
                if (Board.IsPositionValid(position) && IsFree(position) && MoveCount == 0)
                {
                    mat[position.Row, position.Column] = true;
                }

                // left

                position.SetValues(Position.Row + 1, Position.Column + 1);
                if (Board.IsPositionValid(position) && HasEnemy(position))
                {
                    mat[position.Row, position.Column] = true;
                }

                // right

                position.SetValues(Position.Row + 1, Position.Column - 1);
                if (Board.IsPositionValid(position) && HasEnemy(position))
                {
                    mat[position.Row, position.Column] = true;
                }

                // Special Move - En passant
                if (Position.Row == 4)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);

                    if (Board.IsPositionValid(left) && HasEnemy(left) && Board.Piece(left) == Match.VulnerableEnPassant)
                    {
                        mat[left.Row + 1, left.Column] = true;
                    }

                    Position right = new Position(Position.Row, Position.Column + 1);

                    if (Board.IsPositionValid(right) && HasEnemy(right) && Board.Piece(right) == Match.VulnerableEnPassant)
                    {
                        mat[right.Row + 1, right.Column] = true;
                    }
                }
            }
            else
            {
                throw new BoardException("Could not identify the piece's color");
            }

            return mat;
        }
    }
}
