using System;
using System.Windows.Forms;

namespace Checkers
{
    class CheckersBoard
    {
        Square[,] board = new Square[8, 8];

        public CheckersBoard()
        {
            InitializeBoard();
        }
        public Square[,] Board { get => board; set => board = value; }
        public void InitializeBoard()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Board[x, y] = new Square(CheckerColor.Empty);
                }
            }
        }
        //Funkcja umieszczająca pionki na planszy
        public void PlaceChecker()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 1; y < 8; y+=2)
                {
                    if (x < 3) Board[x, y - x % 2] = new Square(CheckerColor.Red);
                    if (x > 4) Board[x, y - x % 2] = new Square(CheckerColor.Blue);
                }
            }
        }
        //Funkcja rysująca pole gry
        public void DrawBoard()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("  01234567");
            for (int x = 0; x < 8; x++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(x+" ");
                for (int y = 0; y < 8; y++)
                {
                    if (Board[x, y].Color == CheckerColor.Red)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("O");
                        }
                    else if (Board[x, y].Color == CheckerColor.Blue)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("O");
                        }
                    else Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        //Funkcja wykonująca ruchy na planszy
        public void Move(Move move)
        {
            Board[move.Destination.X, move.Destination.Y].Color = Board[move.Source.X, move.Source.Y].Color;
            Board[move.Destination.X, move.Destination.Y].Queen = Board[move.Source.X, move.Source.Y].Queen;

            Board[move.Source.X, move.Source.Y].Color = CheckerColor.Empty;
            Board[move.Source.X, move.Source.Y].Queen = false;
            if(Board[move.Destination.X, move.Destination.Y].Queen != true )
            Board[move.Destination.X, move.Destination.Y].Queen = IfQueen(move.Destination.X, Board[move.Destination.X, move.Destination.Y].Color);
            if (move.Captures.Count > 0) move.Captures.ForEach(mv => this.ClearPosition(mv.X,mv.Y));
        }
        private void ClearPosition(int x, int y)
        {
            Board[x, y].Color = CheckerColor.Empty;
            Board[x, y].Queen = false;
        }
        private bool IfQueen(int x, CheckerColor color)
        {
            return x == 7 && color == CheckerColor.Red || x == 0 && color == CheckerColor.Blue;
        }

    }

}

