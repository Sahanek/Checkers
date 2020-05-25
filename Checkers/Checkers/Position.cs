namespace Checkers
{
    //Klasa przechowująca punkt x,y
    class Position
    {
        private int x;
        private int y;
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

        public override bool Equals(object obj)
        {
            return obj is Position position &&
                   x == position.x &&
                   y == position.y &&
                   X == position.X &&
                   Y == position.Y;
        }
        public override string ToString()
        {
            return $"({x}, {y})";
        }
        // public int Value { get; set; }
    }
}
