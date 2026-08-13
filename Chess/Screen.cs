using System;
using System.Collections.Generic;
using Chess.Board;
using Chess.Pieces;

namespace Chess
{
    public class Screen
    {
        public static void PrintMatch(Match match)
        {
            PrintBoard(match.Board);
            Console.WriteLine();
            PrintCapturedPieces(match);
            Console.WriteLine();
            Console.WriteLine("Turn: " + match.Turn);
            if (!match.Finished)
            {
                Console.WriteLine("Current player: " + match.CurrentColor);

                if (match.Check)
                {
                    Console.WriteLine("Check!");
                }
            }
            else
            {
                Console.WriteLine("Checkmate!");
                Console.WriteLine("Winner: " + match.CurrentColor);
            }
        }

        public static void PrintCapturedPieces(Match match)
        {
            Console.WriteLine("Captured Pieces ");
            Console.WriteLine();
            Console.Write("White: ");
            PrintSet(match.CapturedPieces(Color.White));
            Console.WriteLine();
            Console.Write("Black: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintSet(match.CapturedPieces(Color.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void PrintSet(HashSet<Piece> set)
        {
            Console.Write("[");
            foreach (Piece piece in set)
            {
                if (set.Count > 1)
                {
                    Console.Write("" + piece + ", ");
                }
                else
                {
                    Console.Write(piece);
                }
            }

            Console.Write("]");
        }

        public static void PrintBoard(Board.Board board)
        {
            for (int l = 0; l < board.Rows; l++)
            {
                Console.Write(8 - l + " ");
                for (int c = 0; c < board.Columns; c++)
                {
                    PrintPiece(board.Piece(l, c));
                }

                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static void PrintBoard(Board.Board board, bool[,] possiblePositions)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor alteredBackground = ConsoleColor.DarkGray;

            for (int l = 0; l < board.Rows; l++)
            {
                Console.Write(8 - l + " ");
                for (int c = 0; c < board.Columns; c++)
                {
                    if (possiblePositions[l, c])
                    {
                        Console.BackgroundColor = alteredBackground;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBackground;
                    }

                    PrintPiece(board.Piece(l, c));
                    Console.BackgroundColor = originalBackground;
                }

                Console.WriteLine();
            }

            Console.WriteLine("  A B C D E F G H");
            Console.BackgroundColor = originalBackground;

        }

        public static ChessPosition ReadChessPosition()
        {
            string enteredPosition = Console.ReadLine();

            if (string.IsNullOrEmpty((enteredPosition)))
            {
                throw new BoardException("Enter a value");
            }

            enteredPosition = enteredPosition.Trim();

            if (enteredPosition.Length != 2)
            {
                throw new BoardException("Invalid position! Enter it in the format letter + number (e.g., A1).");
            }

            char column = char.ToUpper(enteredPosition[0]);

            if (column < 'A' || column > 'H' || !int.TryParse(enteredPosition[1].ToString(), out int row) || row < 1 || row > 8)
            {
                throw new BoardException("Invalid position! Enter it in the format letter + number (e.g., A1).");
            }

            return new ChessPosition(column, row);
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.Color == Color.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }

                Console.Write(" ");
            }
        }

        public static string PrintPromotion()
        {
            Console.WriteLine();
            Console.WriteLine("Piece promoted!");
            Console.WriteLine();
            Console.WriteLine("Enter one of the options");
            Console.WriteLine();
            Console.WriteLine("Q - Queen");
            Console.WriteLine("R - Rook");
            Console.WriteLine("B - Bishop");
            Console.WriteLine("N - Knight");

            string enteredPosition = Console.ReadLine();

            if (string.IsNullOrEmpty((enteredPosition)))
            {
                throw new BoardException("Enter a value");
            }

            string entered = enteredPosition.ToUpper();

            return entered;
        }
    }
}
