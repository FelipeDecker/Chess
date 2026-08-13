using System;
using Chess.Board;
using Chess.Pieces;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Match match = new Match();

                while (!match.Finished)
                {
                    try
                    {
                        Console.Clear();
                        Screen.PrintMatch(match);

                        Console.WriteLine();
                        Console.Write("Enter the origin: ");
                        Position origin = Screen.ReadChessPosition().ToPosition();
                        match.ValidateOrigin(origin);

                        bool[,] possiblePositions = match.Board.Piece(origin).PossibleMoves();

                        Console.Clear();
                        Screen.PrintBoard(match.Board, possiblePositions);

                        Console.WriteLine();
                        Console.Write("Enter the target: ");
                        Position target = Screen.ReadChessPosition().ToPosition();
                        match.ValidateTarget(origin, target);


                        match.MakeMove(origin, target);
                    }
                    catch (BoardException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadLine();
                    }
                }

                Console.Clear();
                Screen.PrintMatch(match);
            }
            catch (BoardException ex)
            {
                Console.WriteLine(ex.Message); 
            }

            Console.ReadLine();
        }
    }
}
