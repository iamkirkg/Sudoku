using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using GameEngine;

namespace SudokuForms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            //BoardLayout.Square foo;

            BoardLayout.Square[,] GameBoard = new BoardLayout.Square[9,9]
            {
                { new BoardLayout.Square(0), new BoardLayout.Square(0), new BoardLayout.Square(0), new BoardLayout.Square(1), new BoardLayout.Square(1), new BoardLayout.Square(1), new BoardLayout.Square(2), new BoardLayout.Square(2), new BoardLayout.Square(2) },
                { new BoardLayout.Square(0), new BoardLayout.Square(0), new BoardLayout.Square(0), new BoardLayout.Square(1), new BoardLayout.Square(1), new BoardLayout.Square(1), new BoardLayout.Square(2), new BoardLayout.Square(2), new BoardLayout.Square(2) },
                { new BoardLayout.Square(0), new BoardLayout.Square(0), new BoardLayout.Square(0), new BoardLayout.Square(1), new BoardLayout.Square(1), new BoardLayout.Square(1), new BoardLayout.Square(2), new BoardLayout.Square(2), new BoardLayout.Square(2) },
                { new BoardLayout.Square(3), new BoardLayout.Square(3), new BoardLayout.Square(3), new BoardLayout.Square(4), new BoardLayout.Square(4), new BoardLayout.Square(4), new BoardLayout.Square(5), new BoardLayout.Square(5), new BoardLayout.Square(5) },
                { new BoardLayout.Square(3), new BoardLayout.Square(3), new BoardLayout.Square(3), new BoardLayout.Square(4), new BoardLayout.Square(4), new BoardLayout.Square(4), new BoardLayout.Square(5), new BoardLayout.Square(5), new BoardLayout.Square(5) },
                { new BoardLayout.Square(3), new BoardLayout.Square(3), new BoardLayout.Square(3), new BoardLayout.Square(4), new BoardLayout.Square(4), new BoardLayout.Square(4), new BoardLayout.Square(5), new BoardLayout.Square(5), new BoardLayout.Square(5) },
                { new BoardLayout.Square(6), new BoardLayout.Square(6), new BoardLayout.Square(6), new BoardLayout.Square(7), new BoardLayout.Square(7), new BoardLayout.Square(7), new BoardLayout.Square(8), new BoardLayout.Square(8), new BoardLayout.Square(8) },
                { new BoardLayout.Square(6), new BoardLayout.Square(6), new BoardLayout.Square(6), new BoardLayout.Square(7), new BoardLayout.Square(7), new BoardLayout.Square(7), new BoardLayout.Square(8), new BoardLayout.Square(8), new BoardLayout.Square(8) },
                { new BoardLayout.Square(6), new BoardLayout.Square(6), new BoardLayout.Square(6), new BoardLayout.Square(7), new BoardLayout.Square(7), new BoardLayout.Square(7), new BoardLayout.Square(8), new BoardLayout.Square(8), new BoardLayout.Square(8) }
            };

            for (int i = 0; i <= 8; i++)
                {
                int j = GameBoard[0, i].q;
                Console.WriteLine(j);
                };

            //Application.Run(new Form2());

            Application.Run(new Form3());
        }
    }
}
