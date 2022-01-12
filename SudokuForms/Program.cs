using System;
using System.Windows.Forms;

/*
    Board has an objGame, and Game has an objBoard.  Geez!
		Do I want Board to inherit from Game?

    What gets pulled from objGame.objBoard:
        objBoard.objGame.cDimension
        objBoard.objGame.cSector
        objBoard.objGame.fSuper
        objBoard.objGame.bitCount
    I think we should push these from the Game, down to the Board.

    Game.cs
        public partial class Game : Form
            public Board objBoard;
            objBoard = new Board(this,...)
            public int bitCount
            public bool fSuper
            public Game()
            public void SetFlavor (Flavor flav)
            public void BoardReset (Flavor objFlavor)

    Game.Designer.cs
        partial class Game
            private void InitializeComponent
       
    Board.cs
        public class Board
            public Game objGame = argGame
            public Square[,] rgSquare
            rgSquare[x,y] = new Square(argGame,...)

    Square.cs
        public class Square
            public Game objGame;
            public Square(Game argGame, ...)
                objGame = argGame;
            public void SetBackColor
            public Color MyBackColor
            public void Winner(..., Board objBoard)
                foreach (Square sq in objBoard.rgSquare) {
            public bool FLoser(char chValue, Board objBoard)
            code makes reference to:
                objGame.objLogBox
                objGame.rgSquare
                objGame.curFlavor
                objGame.szAll
                objGame.font
                objGame.emSizeWinner

    Range.cs
        // This is only called from Technique.cs
        class Range
            public Square[] rgSquare;
            public Range(Board objBoard, Type argType, int argI)
                rgSquare = new Square[objBoard.objGame.cDimension]
            public void ResetRangeColor
            Things pulled from objBoard:
                objGame
                rgSquare
            Things pulled from objGame:
                cDimension

    Technique.cs
        class Techniques
            public static bool AllRanges(Board objBoard, LogBox objLogBox)
                Range objRange = new Range(objBoard, Range.Type.Row, i);
                ret |= RangeCheck(objBoard, objRange, objLogBox);
            public static bool FLineFind(Board objBoard, LogBox objLogBox)
            public static bool FSectorsFind(Board objBoard, LogBox objLogBox)
            Things pulled from objBoard:
                objGame
                rgSquare
            Things pulled from objGame:
                fSuper
                cSector
                cDimension
                bitCount
                
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
