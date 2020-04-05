using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            //Application.Run(new Form2());

            //Board.Square foo;

            /*
            Board.Square[,] GameBoard = new Board.Square[9,9]
            {
                { new Board.Square(0, btn00), new Board.Square(0, btn01), new Board.Square(0, btn02), new Board.Square(1, null), new Board.Square(1, null), new Board.Square(1, null), new Board.Square(2, null), new Board.Square(2, null), new Board.Square(2, null) },
                { new Board.Square(0, btn10), new Board.Square(0, btn11), new Board.Square(0, btn12), new Board.Square(1,null), new Board.Square(1,null), new Board.Square(1,null), new Board.Square(2,null), new Board.Square(2,null), new Board.Square(2, null) },
                { new Board.Square(0, btn20), new Board.Square(0, btn21), new Board.Square(0, btn22), new Board.Square(1,null), new Board.Square(1,null), new Board.Square(1,null), new Board.Square(2,null), new Board.Square(2,null), new Board.Square(2, null) },
                { new Board.Square(3,null), new Board.Square(3,null), new Board.Square(3,null), new Board.Square(4,null), new Board.Square(4,null), new Board.Square(4,null), new Board.Square(5,null), new Board.Square(5,null), new Board.Square(5, null) },
                { new Board.Square(3,null), new Board.Square(3,null), new Board.Square(3,null), new Board.Square(4,null), new Board.Square(4,null), new Board.Square(4,null), new Board.Square(5,null), new Board.Square(5,null), new Board.Square(5, null) },
                { new Board.Square(3,null), new Board.Square(3,null), new Board.Square(3,null), new Board.Square(4,null), new Board.Square(4,null), new Board.Square(4,null), new Board.Square(5,null), new Board.Square(5,null), new Board.Square(5, null) },
                { new Board.Square(6,null), new Board.Square(6,null), new Board.Square(6,null), new Board.Square(7,null), new Board.Square(7,null), new Board.Square(7,null), new Board.Square(8,null), new Board.Square(8,null), new Board.Square(8, null) },
                { new Board.Square(6,null), new Board.Square(6,null), new Board.Square(6,null), new Board.Square(7,null), new Board.Square(7,null), new Board.Square(7,null), new Board.Square(8,null), new Board.Square(8,null), new Board.Square(8, null) },
                { new Board.Square(6,null), new Board.Square(6,null), new Board.Square(6,null), new Board.Square(7,null), new Board.Square(7,null), new Board.Square(7,null), new Board.Square(8,null), new Board.Square(8,null), new Board.Square(8, null) }
            };
            */

            Application.Run(new Board());
        }
    }
}
