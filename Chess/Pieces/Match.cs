using System.Collections.Generic;
using Chess.Board;

namespace Chess.Pieces
{
    public class Match
    {
        public Board.Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentColor { get; private set; }
        public bool Finished { get; set; }
        private HashSet<Piece> Pieces;
        private HashSet<Piece> Captured;
        public bool Check { get; private set; }
        public Piece VulnerableEnPassant { get; private set; }

        public Match()
        {
            Board = new Board.Board(8, 8);
            Turn = 1;
            CurrentColor = Color.White;
            Finished = false;
            Check = false;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            PlacePieces();
        }

        public Piece ExecuteMove(Position originPosition, Position targetPosition)
        {
            Piece piece = Board.RemovePiece(originPosition);
            piece.IncrementMoveCount();

            Piece capturedPiece = Board.RemovePiece(targetPosition);
            Board.PlacePiece(piece, targetPosition);

            if (capturedPiece != null)
            {
                Captured.Add(capturedPiece);
            }

            // Special Move - Kingside castling

            if (piece is King && targetPosition.Column == originPosition.Column + 2)
            {
                Position rookOrigin = new Position(originPosition.Row, originPosition.Column + 3);
                Position rookTarget = new Position(originPosition.Row, originPosition.Column + 1);
                Piece rook = Board.RemovePiece(rookOrigin);
                rook.IncrementMoveCount();
                Board.PlacePiece(rook, rookTarget);
            }

            // Special Move - Queenside castling

            if (piece is King && targetPosition.Column == originPosition.Column - 2)
            {
                Position rookOrigin = new Position(originPosition.Row, originPosition.Column - 4);
                Position rookTarget = new Position(originPosition.Row, originPosition.Column - 1);
                Piece rook = Board.RemovePiece(rookOrigin);
                rook.IncrementMoveCount();
                Board.PlacePiece(rook, rookTarget);
            }

            // Special Move - En passant

            if (piece is Pawn)
            {
                if (originPosition.Column != targetPosition.Column && capturedPiece == null)
                {
                    Position pawnPosition;

                    if (piece.Color == Color.White)
                    {
                        pawnPosition = new Position(targetPosition.Row + 1, targetPosition.Column);
                    }
                    else
                    {
                        pawnPosition = new Position(targetPosition.Row - 1, targetPosition.Column);
                    }

                    capturedPiece = Board.RemovePiece(pawnPosition);
                    Captured.Add(capturedPiece);
                }
            }

            return capturedPiece;
        }

        public void UndoMove(Position originPosition, Position targetPosition, Piece capturedPiece)
        {
            Piece piece = Board.RemovePiece(targetPosition);
            piece.DecrementMoveCount();
            if (capturedPiece != null)
            {
                Board.PlacePiece(capturedPiece, targetPosition);
                Captured.Remove(capturedPiece);
            }

            Board.PlacePiece(piece, originPosition);

            // Special Move - Kingside castling

            if (piece is King && targetPosition.Column == originPosition.Column + 2)
            {
                Position rookOrigin = new Position(originPosition.Row, originPosition.Column + 3);
                Position rookTarget = new Position(originPosition.Row, originPosition.Column + 1);
                Piece rook = Board.RemovePiece(rookTarget);
                rook.DecrementMoveCount();
                Board.PlacePiece(rook, rookOrigin);
            }

            // Special Move - Queenside castling

            if (piece is King && targetPosition.Column == originPosition.Column - 2)
            {
                Position rookOrigin = new Position(originPosition.Row, originPosition.Column - 4);
                Position rookTarget = new Position(originPosition.Row, originPosition.Column - 1);
                Piece rook = Board.RemovePiece(rookTarget);
                rook.DecrementMoveCount();
                Board.PlacePiece(rook, rookOrigin);
            }

            // Special Move - En passant

            if (piece is Pawn)
            {
                if (originPosition.Column != targetPosition.Column && capturedPiece == VulnerableEnPassant)
                {
                    Piece pawn = Board.RemovePiece(targetPosition);
                    Position pawnPosition;

                    if (pawn.Color == Color.White)
                    {
                        pawnPosition = new Position(3, targetPosition.Column);
                    }
                    else
                    {
                        pawnPosition = new Position(4, targetPosition.Column);
                    }

                    Board.PlacePiece(pawn, pawnPosition);
                }
            }
        }

        public void MakeMove(Position originPosition, Position targetPosition)
        {
            Piece capturedPiece = ExecuteMove(originPosition, targetPosition);

            if (IsInCheck(CurrentColor))
            {
                UndoMove(originPosition, targetPosition, capturedPiece);
                throw new BoardException("You cannot put yourself in check!");
            }

            Piece movedPiece = Board.Piece(targetPosition);

            //Special Move - Promotion

            if (movedPiece is Pawn)
            {
                if ((movedPiece.Color == Color.White && targetPosition.Row == 0) || (movedPiece.Color == Color.Black && targetPosition.Row == 7))
                {
                    movedPiece = Board.RemovePiece(targetPosition);
                    Pieces.Remove(movedPiece);

                    string choice = Screen.PrintPromotion();

                    Piece chosenPiece = CreatePromotedPiece(choice, movedPiece.Color);

                    Board.PlacePiece(chosenPiece, targetPosition);
                    Pieces.Add(chosenPiece);
                }
            }

            if (IsInCheck(OpponentColor(CurrentColor)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }

            if (IsInCheckMate(OpponentColor(CurrentColor)))
            {
                Finished = true;
            }
            else
            {
                Turn++;
                ChangePlayer();
            }

            //Special Move - En passant

            if (movedPiece is Pawn && (targetPosition.Row == originPosition.Row - 2 || targetPosition.Row == originPosition.Row + 2))
            {
                VulnerableEnPassant = movedPiece;
            }
            else
            {
                VulnerableEnPassant = null;
            }
        }

        public Piece CreatePromotedPiece(string choice, Color color)
        {
            Piece chosenPiece;

            if (choice == "Q")
            {
                chosenPiece = new Queen(Board, color);
            }
            else if (choice == "R")
            {
                chosenPiece = new Rook(Board, color);

            }
            else if (choice == "B")
            {
                chosenPiece = new Bishop(Board, color);

            }
            else if (choice == "N")
            {
                chosenPiece = new Knight(Board, color);

            }
            else
            {
                throw new BoardException("Invalid piece");
            }

            return chosenPiece;
        }

        public void ValidateOrigin(Position position)
        {
            if (Board.Piece(position) == null)
            {
                throw new BoardException("There is no piece at the origin position!");
            }
            if (CurrentColor != Board.Piece(position).Color)
            {
                throw new BoardException("The origin piece is not yours!");
            }
            if (!Board.Piece(position).HasPossibleMoves())
            {
                throw new BoardException("There are no possible moves for this piece!");
            }
        }

        public void ValidateTarget(Position origin, Position target)
        {
            if (!Board.Piece(origin).CanMoveTo(target))
            {
                throw new BoardException("Invalid target position");
            }
        }

        private void ChangePlayer()
        {
            if (CurrentColor == Color.White)
            {
                CurrentColor = Color.Black;
            }
            else
            {
                CurrentColor = Color.White;
            }
        }

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (var x in Captured)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }

            return aux;
        }

        public HashSet<Piece> PiecesInPlay(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (var x in Pieces)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }

            aux.ExceptWith(CapturedPieces(color));
            return aux;
        }

        private Color OpponentColor(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece KingPiece(Color color)
        {
            foreach (Piece piece in PiecesInPlay(color))
            {
                if (piece is King)
                {
                    return piece;
                }
            }

            return null;
        }

        public bool IsInCheck(Color color)
        {
            Piece k = KingPiece(color);

            if (k == null)
            {
                throw new BoardException("There is no " + color + " king on the board");
            }

            foreach (Piece piece in PiecesInPlay(OpponentColor(color)))
            {
                bool[,] mat = piece.PossibleMoves();
                if (mat[k.Position.Row, k.Position.Column])
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsInCheckMate(Color color)
        {
            if (!IsInCheck(color))
            {
                return false;
            }

            foreach (Piece piece in PiecesInPlay(color))
            {
                bool[,] mat = piece.PossibleMoves();

                for (int l = 0; l < Board.Rows; l++)
                {
                    for (int c = 0; c < Board.Columns; c++)
                    {
                        if (mat[l, c])
                        {
                            Position origin = piece.Position;
                            Position target = new Position(l, c);
                            Piece capturedPiece = ExecuteMove(origin, target);
                            bool testCheck = IsInCheck(color);
                            UndoMove(origin, target, capturedPiece);
                            if (!testCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        public void PlaceNewPiece(char column, int row, Piece piece)
        {
            var upperColumn = column.ToString().ToUpper().ToCharArray();
            char newColumn = upperColumn[0];
            Board.PlacePiece(piece, new ChessPosition(newColumn, row).ToPosition());
            Pieces.Add(piece);
        }

        private void  PlacePieces()
        {
            PlaceNewPiece('A', 1, new Rook(Board, Color.White));
            PlaceNewPiece('B', 1, new Knight(Board, Color.White));
            PlaceNewPiece('C', 1, new Bishop(Board, Color.White));
            PlaceNewPiece('D', 1, new Queen(Board, Color.White));
            PlaceNewPiece('E', 1, new King(Board, Color.White, this));
            PlaceNewPiece('F', 1, new Bishop(Board, Color.White));
            PlaceNewPiece('G', 1, new Knight(Board, Color.White));
            PlaceNewPiece('H', 1, new Rook(Board, Color.White));
            PlaceNewPiece('A', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('B', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('C', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('D', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('E', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('F', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('G', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('H', 2, new Pawn(Board, Color.White, this));

            PlaceNewPiece('A', 8, new Rook(Board, Color.Black));
            PlaceNewPiece('B', 8, new Knight(Board, Color.Black));
            PlaceNewPiece('C', 8, new Bishop(Board, Color.Black));
            PlaceNewPiece('D', 8, new Queen(Board, Color.Black));
            PlaceNewPiece('E', 8, new King(Board, Color.Black, this));
            PlaceNewPiece('F', 8, new Bishop(Board, Color.Black));
            PlaceNewPiece('G', 8, new Knight(Board, Color.Black));
            PlaceNewPiece('H', 8, new Rook(Board, Color.Black));
            PlaceNewPiece('A', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('B', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('C', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('D', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('E', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('F', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('G', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('H', 7, new Pawn(Board, Color.Black, this));
        }
    }
}
