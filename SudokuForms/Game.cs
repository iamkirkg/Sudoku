using System;
using System.Windows.Forms;

namespace SudokuForms
{
    public partial class Game : Form
    {
        public enum Flavor
        {
            Sudoku,         // 3x3x9
            SuperSudoku,    // 4x4x16
            HyperSudoku     // 3x3x(9+4)
        }
        public Flavor curFlavor = Flavor.Sudoku;

        // Are we 3x3 or 4x4?
        public bool fSuper
        {
            get { return curFlavor == Flavor.SuperSudoku; }
        }

        // Are we currently showing possible answers?
        private bool _fCouldBe = true;
        public bool fCouldBe {
            get { return _fCouldBe; }
            set { _fCouldBe = value; }
        }

        public int cDimension { // Are we 3x3 or 4x4?
            get { return fSuper ? 16 : 9; }
        }
        public int xDelta { // how much to shift right
            get { return fSuper ? 380 : 000; }
        }
        public int xMove { // how much to move
            get { return fSuper ? 376 : -376; }
        }
        public string szTitle {
            get { return fSuper ? "SuperSudokirk" : "Sudokirk"; }
        }
        public Single emSizeWinner {
            get { return fSuper ? 36F : 40F; }
        }
        public int bitCount {
            get { return fSuper ? 16 : 9; }
        }
        public string szAll {
            get { return fSuper ? "0 1 2 3 4 5 6 7 8 9 A B C D E F " : "1 2 3 4 5 6 7 8 9 "; }
        }
        public float font {
            get { return fSuper ? 7F : 12F; }
        }
        // Board origin
        private int xOrigin = 2;
        private int yOrigin = 2;
        // Board size
        public int iBoardWidth {
            get { return fSuper ? 1226 : 1100; }
        }
        public int iBoardHeight
        {
            get { return fSuper ? 996 : 900; }
        }
        // Square size
        private int xSize {
            get { return fSuper ? 52 : 52; }
        }
        private int ySize {
            get { return fSuper ? 60 : 68; }
        }

        public enum Technique
        {
            none,
            RangeCheck,
            LineFind,
            SectorFind
        }
        public Technique curTechnique = Technique.none;

        // These record the last place clicked by the user.
        public int curTab = -1;
        public int curCol = -1;
        public int curRow = -1;
        public char curChar;

        Board objBoard;

        public Game()
        {
            InitializeComponent();

            objBoard = new Board(this, fSuper,
                                 xOrigin, yOrigin, xSize, ySize, font, 
                                 sq_KeyPress, sq_KeyDown, sq_Click,
                                 objLogBox
                                 );
        }

        public void BoardReset(Flavor objFlavor)
        {
            objBoard.Delete();

            curFlavor = objFlavor;

            objBoard = new Board(this, fSuper,
                                 xOrigin, yOrigin, xSize, ySize, font,
                                 sq_KeyPress, sq_KeyDown, sq_Click,
                                 objLogBox
                                 );

            MoveButtons();
        }
    }
}
