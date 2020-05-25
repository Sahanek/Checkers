using System;
using System.Linq;

namespace Checkers
{
    static class Game
    {
        //Funkcja odpowiadająca za grę. 
        public static void start()
        {
            IAI AI = new AI();
            CheckersBoard board = new CheckersBoard();
            board.PlaceChecker();
            string lineChecker, lineDestination;
            int checkX, checkY, destX, destY;
            Move moveChecker;
            while(IfGameContinues(board.Board, CheckerColor.Blue, AI.AIColor))
            {
                board.DrawBoard();
                checkX = 0;
                checkY = 0;
                
                do
                {
                    do
                    {
                        Console.WriteLine("Jesteś graczem niebieskim. Podaj pionka którego chcesz ruszyć w formacie x,y");
                        try
                        {
                            lineChecker = Console.ReadLine();
                            checkX = Int32.Parse(lineChecker[0].ToString());
                            checkY = Int32.Parse(lineChecker[2].ToString());
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine("Zły format.");
                        }
                    } while (!Utils.IsValidPosition(checkX, checkY) || board.Board[checkX, checkY].Color != CheckerColor.Blue);

                    destX = 0;
                    destY = 0;
                    do
                    {

                        Console.WriteLine("Podaj na jakie miejsce chcesz ruszyć pionka o współrzędnych " +
                            String.Format("{0} ,{1} formacie x,y", checkX, checkY));
                        try
                        {
                            lineDestination = Console.ReadLine();
                            destX = Int32.Parse(lineDestination[0].ToString());
                            destY = Int32.Parse(lineDestination[2].ToString());
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine("Zły format");
                        }
                    } while (!Utils.IsValidPosition(destX, destY) && destX != 0 && destY != 0 );
                 /*   Console.WriteLine("Possible Moves");
                    foreach (var mv in Utils.PossibleMoves(board.Board, new Position(checkX, checkY)))
                    {
                        Console.WriteLine(mv);
                    }*/

                    moveChecker = Utils.PossibleMoves(board.Board, new Position(checkX, checkY)).
                        FirstOrDefault(move => move.Destination.X == destX && move.Destination.Y == destY);
                    if (moveChecker == null) Console.WriteLine("Niepoprawny ruch!");
                } while (moveChecker == null);
                
                Console.WriteLine("Wykonałeś ruch!");
                board.Move(moveChecker);
                
                //Utils.PossibleMoves(board.Board,new Position(2,3)).ToList()[0].Captures.ForEach(mv => Console.WriteLine(mv));
                Console.WriteLine("AI Wykonuje ruch!");
                Console.WriteLine(AI.Game(board.Board));
                board.Move(AI.Game(board.Board));
                board.DrawBoard();
                Console.WriteLine("Po wcisnieciu klawisza \"ENTER\" przejdziesz do następnej tury.");
                Console.ReadLine();
                //Console.Clear();
            }

            Console.WriteLine("Gra zakonczona");
            Console.ReadLine();
            

        }
        
        //Sprawdza warunki czy gra nadal się toczy. 
        public static bool IfGameContinues(Square[,] board, CheckerColor playerColor, CheckerColor aiColor)
        {
            bool playerCanMove = false;
            bool aiCanMove = false;
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (board[x, y].Color == playerColor && Utils.PossibleMoves(board, new Position(x, y)).Length != 0) playerCanMove = true;
                    if (board[x, y].Color == aiColor && Utils.PossibleMoves(board, new Position(x, y)).Length != 0) aiCanMove = true;
                    if (playerCanMove && aiCanMove) return true;
                }
            }
            return playerCanMove && aiCanMove ? true : false; 
        }

    }
}
