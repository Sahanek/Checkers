using System;
using System.Collections.Generic;
using System.Xml.Schema;

namespace Checkers
{
    //Klasa reprezentuje wykonywany ruch. Posiada listę pionków które zbija po drodze.
    //Destination to ostatni pozycja w jakiej sie znajdzie.
    class Move
    {
        private Position source;
        private Position destination;
        private List<Position> captures = new List<Position>();
        private int score = 0;

        public Move() : this(-1,-1,-1,-1)
        {
            
        }

        public Move(Position source, Position destination, int score = 0)
        {
            Source = source;
            Destination = destination;
            Score = score;
       
        }

        public Move(Position source, int x, int y, int score = 0)
            : this(source, new Position(x, y), score)
        {
        }
        public Move(int x1, int y1, int x2, int y2, int score = 0)
            : this(new Position(x1, y1), new Position(x2, y2),score)
        {
        }

        public Position Destination { get => destination; set => destination = value; }
        public Position Source { get => source; set => source = value; }
        public int Score { get => score; set => score = value; }
        public List<Position> Captures { get => captures;}

        public override string ToString()
        {
            return $"Source: {Source.ToString()}, Destination: {Destination.ToString()}"; 
        }
    }
    
}
