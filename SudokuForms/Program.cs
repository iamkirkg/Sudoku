using System;
using System.Windows.Forms;

/*
    class Game
        objBoard = new Board(this,...)
        public int bitCount
        public bool fSuper
       
    class Board
        public Game objGame = argGame
        public Square[,] rgSquare
        rgSquare[x,y] = new Square(argGame,...)

    class Square
        
*/


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
            Application.Run(new Game());
        }
    }
}
