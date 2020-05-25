namespace Checkers
{
    enum CheckerColor { Empty, Red, Blue };
    //Funkcja określająca Pole w planszy.
    class Square
    {
        private CheckerColor color;
        private bool queen;

        public Square(CheckerColor color, bool queen = false)
        {
            this.Color = color;
            this.Queen = queen;
        }

        public bool Queen { get => queen; set => queen = value; }
        internal CheckerColor Color { get => color; set => color = value; }
    }
}
