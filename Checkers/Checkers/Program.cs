using Checkers.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checkers
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.Clear();
            Game.start();
            Console.ReadLine();


            /*
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
            */
            /*
            IAI AI = new AI();
            IAI AII = new AI();
            AII.AIColor = CheckerColor.Blue;
            CheckersBoard board = new CheckersBoard();
            board.PlaceChecker();
            board.Move(new Move(2, 1, 3, 2));
            board.Move(new Move(5, 0, 4, 1));
            while (Game.IfGameContinues(board.Board, CheckerColor.Blue, AI.AIColor))
            { 
                board.DrawBoard();
                Console.WriteLine("AI Wykonuje ruch!");
                Console.WriteLine(AI.Game(board.Board));
                board.Move(AI.Game(board.Board));
                board.DrawBoard();
                Console.ReadLine();
                Console.WriteLine("AII Wykonuje ruch!");
                Console.WriteLine(AII.Game(board.Board));
                board.Move(AII.Game(board.Board));
                board.DrawBoard();
                Console.ReadLine();
                Console.Clear();
                //Game.start();
                


            
            }*/




        }
    }
}
