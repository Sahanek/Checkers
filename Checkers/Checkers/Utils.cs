using System;
using System.Collections.Generic;


namespace Checkers
{
    static class Utils
    {
        //Sprawdza poprawność pozycji, nie wykroczone poza tablice
        public static bool IsValidPosition(Position point)
        {
            return IsValidPosition(point.X, point.Y);
        }
        public static bool IsValidPosition(int x, int y)
        {
            if (x >= 0 && x < 8 && y >= 0 && y < 8) return true;
            return false;
        }
        //Funkcja obliczajaca możliwe ruchy piona.
        public static Move[] PossibleMoves(Square[,] board, Position source)
        {

            return PossibleMoves(board, source, new Move(-1, -1, -1, -1));
        }
        private static Move[] PossibleMoves(Square[,] board, Position source, Move lastMove, List<Position> primPosition = null )
        {
            if(primPosition == null)
            {
                primPosition = new List<Position>();
                primPosition.Add(source);
            }
            List<Move> moves = new List<Move>();
            int score = lastMove.Score;
            if (board[primPosition[0].X, primPosition[0].Y].Color == CheckerColor.Empty) return moves.ToArray();
            if (primPosition.Count == 1) score += board[primPosition[0].X, primPosition[0].Y].Queen ? (int)MoveValue.Queen : (int)MoveValue.Checker;
            //left up
            if (board[primPosition[0].X, primPosition[0].Y].Color != CheckerColor.Red || board[primPosition[0].X, primPosition[0].Y].Queen)// stop moving back
            {
                if (IsValidPosition(source.X - 1, source.Y - 1))
                {
                    if (board[source.X - 1, source.Y - 1].Color == CheckerColor.Empty && lastMove.Destination.Y == -1)
                    {// empty space and no jump before
                        score += !board[source.X - 1, source.Y - 1].Queen ? GetSquareWeight(CheckerColor.Blue, source.X - 1) : 0;
                        moves.Add(new Move(source, source.X - 1, source.Y - 1, score));
                    }
                    else if (IsValidPosition(source.X - 2, source.Y - 2) &&
                        board[source.X - 1, source.Y - 1].Color != board[primPosition[0].X, primPosition[0].Y].Color &&
                        (source.X - 2 != lastMove.Destination.X || source.Y - 2 != lastMove.Destination.Y) &&
                        board[source.X - 1, source.Y - 1].Color != CheckerColor.Empty &&
                        board[source.X - 2, source.Y - 2].Color == CheckerColor.Empty)// check jump
                    {
                        Position newDest = new Position(source.X - 2, source.Y - 2);
                        if (!primPosition.Contains(newDest))
                        {
                            //Console.WriteLine("Capture1 ");
                            score += !board[source.X - 2, source.Y - 2].Queen ? GetSquareWeight(CheckerColor.Blue, source.X - 2) : 0;
                            Move moveWithCapture = new Move(primPosition[0], newDest, score += (int)MoveValue.Capture);
                            moveWithCapture.Captures.Add(new Position(source.X - 1, source.Y - 1));
                            moveWithCapture.Captures.AddRange(lastMove.Captures);
                            moves.Add(moveWithCapture);
                            primPosition.Add(newDest);
                            moves.AddRange(PossibleMoves(board, newDest, moveWithCapture, primPosition)); //add next moves if possible

                        }
                    }
                    

                }
            }
            //right up
            if (board[primPosition[0].X, primPosition[0].Y].Color != CheckerColor.Red || board[primPosition[0].X, primPosition[0].Y].Queen)// stop moving back
            {
                if (IsValidPosition(source.X - 1, source.Y + 1))
                {
                    if (board[source.X - 1, source.Y + 1].Color == CheckerColor.Empty && lastMove.Destination.Y == -1)
                    {// empty space and no jump before
                        score += !board[source.X - 1, source.Y + 1].Queen ? GetSquareWeight(CheckerColor.Blue, source.X - 1) : 0;
                        moves.Add(new Move(source, source.X - 1, source.Y + 1, score));
                    }
                    else if (IsValidPosition(source.X - 2, source.Y + 2) &&
                         board[source.X - 1, source.Y + 1].Color != board[primPosition[0].X, primPosition[0].Y].Color &&
                         (source.X - 2 != lastMove.Destination.X || source.Y + 2 != lastMove.Destination.Y) &&
                         board[source.X - 1, source.Y + 1].Color != CheckerColor.Empty &&
                         board[source.X - 2, source.Y + 2].Color == CheckerColor.Empty)// check jump
                    {
                        Position newDest = new Position(source.X - 2, source.Y + 2);
                        if (!primPosition.Contains(newDest))
                        {
                            //Console.WriteLine("Capture2");
                            score += !board[source.X - 2, source.Y + 2].Queen ? GetSquareWeight(CheckerColor.Blue, source.X - 2) : 0;
                            Move moveWithCapture = new Move(primPosition[0], newDest, score += (int)MoveValue.Capture);
                            moveWithCapture.Captures.Add(new Position(source.X - 1, source.Y + 1));
                            moveWithCapture.Captures.AddRange(lastMove.Captures);
                            moves.Add(moveWithCapture);
                            primPosition.Add(newDest);
                            moves.AddRange(PossibleMoves(board, newDest, moveWithCapture, primPosition)); //add next moves if possible

                        }
                    }
                    

                }
            }
            //right down
            if (board[primPosition[0].X, primPosition[0].Y].Color != CheckerColor.Blue || board[primPosition[0].X, primPosition[0].Y].Queen)// stop moving back
            {
                if (IsValidPosition(source.X + 1, source.Y + 1))
                {
                    if (board[source.X + 1, source.Y + 1].Color == CheckerColor.Empty && lastMove.Destination.Y == -1)
                    {// empty space and no jump before
                        score += !board[source.X + 1, source.Y + 1].Queen ? GetSquareWeight(CheckerColor.Red, source.X + 1) : 0;
                        moves.Add(new Move(source, source.X + 1, source.Y + 1, score));
                    }
                    else if (IsValidPosition(source.X + 2, source.Y + 2) &&
                         board[source.X + 1, source.Y + 1].Color != board[primPosition[0].X, primPosition[0].Y].Color &&
                         (source.X + 2 != lastMove.Destination.X || source.Y + 2 != lastMove.Destination.Y)&&
                         board[source.X + 1, source.Y + 1].Color != CheckerColor.Empty &&
                         board[source.X + 2, source.Y + 2].Color == CheckerColor.Empty)// check jump
                    {
                        Position newDest = new Position(source.X + 2, source.Y + 2);
                        if (!primPosition.Contains(newDest))
                        {
                            score += !board[source.X + 2, source.Y + 2].Queen ? GetSquareWeight(CheckerColor.Red, source.X + 1) : 0;
                            Move moveWithCapture = new Move(primPosition[0], newDest, score += (int)MoveValue.Capture);
                            moveWithCapture.Captures.Add(new Position(source.X + 1, source.Y + 1));
                            moveWithCapture.Captures.AddRange(lastMove.Captures);
                            moves.Add(moveWithCapture);

                            primPosition.Add(newDest); 
                            moves.AddRange(PossibleMoves(board, newDest, moveWithCapture, primPosition)); //add next moves if possible
                        }
                    }
                    

                }
            }
            //left down
            if (board[primPosition[0].X, primPosition[0].Y].Color != CheckerColor.Blue || board[primPosition[0].X, primPosition[0].Y].Queen)// stop moving back
            {
                if (IsValidPosition(source.X + 1, source.Y - 1))
                {
                    if (board[source.X + 1, source.Y - 1].Color == CheckerColor.Empty && lastMove.Destination.Y == -1)
                    {// empty space and no jump before
                        score += board[source.X + 1, source.Y - 1].Queen ? GetSquareWeight(CheckerColor.Red, source.X + 1) : 0;
                        moves.Add(new Move(source, source.X + 1, source.Y - 1, score));
                    }
                    else if (IsValidPosition(source.X + 2, source.Y - 2) &&
                        board[source.X + 1, source.Y - 1].Color != board[primPosition[0].X, primPosition[0].Y].Color &&
                        (source.X + 2 != lastMove.Destination.X || source.Y - 2 != lastMove.Destination.Y)&&
                        board[source.X + 1, source.Y - 1].Color != CheckerColor.Empty &&
                        board[source.X + 2, source.Y - 2].Color == CheckerColor.Empty)// check jump
                        {
                            Position newDest = new Position(source.X + 2, source.Y - 2);
                        if (!primPosition.Contains(newDest))
                        {
                            score += board[source.X + 2, source.Y - 2].Queen ? GetSquareWeight(CheckerColor.Red, source.X + 2) : 0;
                            Move moveWithCapture = new Move(primPosition[0], newDest, score += (int)MoveValue.Capture);
                            moveWithCapture.Captures.Add(new Position(source.X + 1, source.Y - 1));
                            moveWithCapture.Captures.AddRange(lastMove.Captures);
                            moves.Add(moveWithCapture);
                            primPosition.Add(newDest);
                            moves.AddRange(PossibleMoves(board, newDest, moveWithCapture, primPosition)); //add next moves if possible
                        }
                        
                    }

                }
            }
            return moves.ToArray();
        }

        //Oblicza wartość pola
        private static int GetSquareWeight(CheckerColor color, int x)
        {
            if (x >= 0 && x < 2) return color == CheckerColor.Blue ? 7 : 1;
            if (x >= 2 && x < 4) return color == CheckerColor.Blue ? 3 : 2;
            if (x >= 4 && x < 6) return color == CheckerColor.Blue ? 2 : 3;
            if (x >= 6 && x < 8) return color == CheckerColor.Blue ? 1 : 7;
            return 1;
        }
    }
    //Wartości poszczegolnych elemntów, ruchów.
    enum MoveValue
    {
        Checker = 1,
        Queen = 10,
        Capture = 20,
        OpponentCapture = 10
    }
    
}
