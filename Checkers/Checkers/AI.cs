using System;
using System.Drawing;
using System.Linq;

namespace Checkers
{
    //Interface AI
    interface IAI
    {
        Move Game(Square[,] board);
        CheckerColor AIColor { get; set; }
    }
    //Klasa Ai posiada drzewo wywołań gry, przechowuje możliwe ruchy wraz ich punktacją. 
    //maksymalna głebokość wywoływań wynsoi 2.
    class AI : IAI
    {
        Tree<Move> gameTree;
        CheckerColor aiColor = CheckerColor.Red;
        private int maxDepth = 2;

        public CheckerColor AIColor { get => aiColor; set => aiColor = value; }

        //Funkcja obliczająca jaki ruch powinno wykonać AI
        public Move Game(Square[,] board)
        {
            gameTree = new Tree<Move>(new Move());

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (board[x, y].Color == AIColor) 
                        foreach (Move move in Utils.PossibleMoves(board, new Position(x,y)))
                        {
                            //Console.WriteLine($"myMove: {move}");
                           // gameTree.AddChild(move);
                           CalculateMoves(-1, gameTree.AddChild(move), move, BoardCopy(board));
                        }
                }
            }

           // Console.WriteLine($"Game Tree after: {SumMoves()}");
           // gameTree.Traverse(prop => Console.WriteLine($"{prop}, Score: {prop.Score} "));

/*
            Console.WriteLine("Game Tree after: ");
            gameTree.Traverse(prop => Console.WriteLine($"{prop}, Score: {prop.Score} " ));
            */
            return SumMoves();
        }

        //FUnkcja obliczająca kolejne ruchy na planszy od możliwych ruchów w oryginalnej planszy.
        private void CalculateMoves(int recLevel, Tree<Move> gameTree, Move move, Square[,] pBoard)
        {
            if (recLevel >= maxDepth) return;
            ++recLevel;

            //gameTree.traverse(mv => Console.WriteLine($"{mv}, Score : {mv.Score} "));
            CheckerColor actualPlayer = pBoard[move.Source.X, move.Source.Y].Color;
            pBoard = PotentialMove(move, pBoard);// Make possible move on virtual Board;
            if (CheckersAtRisk(move, pBoard)) return;  //Checikng if after move the checkers will be at risk/worthless move
            //opponent's moves
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (pBoard[x, y].Color != actualPlayer && pBoard[x, y].Color != CheckerColor.Empty)
                        foreach (Move opponentMove in Utils.PossibleMoves(pBoard, new Position(x, y)))
                        {
                            //if (pBoard[x, y].Color != AIColor) Console.WriteLine($"myMove: {move}");
                            CalculateMoves(recLevel, gameTree.AddChild(opponentMove), opponentMove, BoardCopy(pBoard));
                        }

                }
            }
           // gameTree.traverse(mv => Console.WriteLine($"{0}, Score : {1} ", mv, mv.Score));
        }
        //Wykonuje potencjalny ruch na kopii tablicy
        private Square[,] PotentialMove(Move move, Square[,] pBoard)
        {
            pBoard[move.Destination.X, move.Destination.Y].Color = pBoard[move.Source.X, move.Source.Y].Color;
            pBoard[move.Destination.X, move.Destination.Y].Queen = pBoard[move.Source.X, move.Source.Y].Queen;

            pBoard[move.Source.X, move.Source.Y].Color = CheckerColor.Empty;
            pBoard[move.Source.X, move.Source.Y].Queen = false;

            pBoard[move.Destination.X, move.Destination.Y].Queen = IfQueen(move.Destination.X, pBoard[move.Destination.X, move.Destination.Y].Color);
            if (move.Captures.Count > 0) move.Captures.ForEach(mv => ClearPosition(mv.X, mv.Y, pBoard));
            return pBoard;
        }
        //Funkcja czyscząca pozycje.
        private void ClearPosition(int x, int y, Square[,] pBoard)
        {
            pBoard[x, y].Color = CheckerColor.Empty;
            pBoard[x, y].Queen = false;
        }
        //Tworzy kopie tablicy
        private Square[,] BoardCopy(Square[,] board)
        {
            Square[,] pBoard = new Square[8, 8];
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    pBoard[x, y] = new Square(board[x, y].Color, board[x, y].Queen);
                }
            }
            return pBoard;
        }
        //Sprawdza czy pionek nznajduje sie na odpowiedniej pozycji do zostania damką
        private bool IfQueen(int x, CheckerColor color)
        {
            return x == 7 && color == CheckerColor.Red || x == 0 && color == CheckerColor.Blue;
        }

        //Oblicza wartość ruchów i zwraca najkorzystniejszy lub z największa liczbą bić
        private Move SumMoves()
        {

            int scoreSum = 0;

            foreach (Tree<Move> possibleMove in gameTree.Children)
            {
                possibleMove.Traverse(move => scoreSum += move.Score);
                possibleMove.Data.Score += scoreSum;
                scoreSum = 0;
            }
            
            return gameTree.Children.OrderByDescending(tree => tree.Data.Captures.Count).First().Data.Captures.Count > 0?
                gameTree.Children.OrderByDescending(tree => tree.Data.Captures.Count).First().Data :
                gameTree.Children.OrderByDescending(tree => tree.Data.Score).First().Data;
        }
        //Sprawdza czy diany pion jest pod biciem innego piona.
        private bool CheckersAtRisk(Move move, Square[,] pboard)
        {
            if (pboard[move.Destination.X, move.Destination.Y].Color != AIColor) return false ;
            //Console.WriteLine("Check");
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (pboard[x, y].Color != AIColor && pboard[x, y].Color != CheckerColor.Empty)
                    foreach (Move opponentMove in Utils.PossibleMoves(pboard, new Position(x, y)))
                    {
                            //Console.WriteLine(opponentMove.Captures);
                        if (opponentMove.Captures.Contains(move.Destination))
                        {
                           // Console.WriteLine(opponentMove);
                            move.Score -= (int)MoveValue.OpponentCapture;
                            return true;
                        }
                    }
                }
            }
            return false;
            
        }
    }
}
