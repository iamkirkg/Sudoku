using System;
using System.Diagnostics;
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
        // This is the default, when we first start the app.
        // We could save/restore in a regkey.
        public Flavor curFlavor = Flavor.Sudoku;

        // Are we 3x3 or 4x4?
        public bool fSuper {
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
        public int cSector {
            get {
                switch (curFlavor) {
                    case Flavor.Sudoku:
                        return 9;
                    case Flavor.SuperSudoku:
                        return 16;
                    case Flavor.HyperSudoku: // 9 + 4
                        return 13;
                    default:
                        return -1;
                }
            }
        }

        // BUGBUG Get rid of this.
        public int xDelta { // how much to shift right
            get { return 4; }
        }
        // BUGBUG Get rid of this.
        public int xMove { // how much to move
            get { return 0; }
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
        private int xOrigin = 2 + 300;
        private int yOrigin = 4;
        // Board size
        public int iBoardWidth {
            get { return fSuper ? 1168 : 790; }
        }
        public int iBoardHeight
        {
            get { return fSuper ? 996 : 640; }
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

        public Board objBoard;

        public Game()
        {
            Debug.WriteLine("Game(new)");
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            /*
            objBoard = new Board(this, fSuper,
                                 xOrigin, yOrigin, xSize, ySize, font, 
                                 sq_KeyPress, sq_KeyDown, sq_Click,
                                 objLogBox
                                 );
            */
            Debug.WriteLine("Game(new).end");
        }

        public void SetFlavor(Flavor flav)
        {
            Debug.WriteLine("SetFlavor: " + curFlavor.ToString() + " to " + flav.ToString());

            curFlavor = flav;
            switch (flav)
            {
                case Flavor.Sudoku:
                    FlavorSudoku.Checked = true;
                    break;
                case Flavor.SuperSudoku:
                    FlavorSuperSudoku.Checked = true;
                    break;
                case Flavor.HyperSudoku:
                    FlavorHyperSudoku.Checked = true;
                    break;
            }
        }

        public void BoardReset(Flavor objFlavor)
        {
            Debug.WriteLine("BoardReset(" + objFlavor.ToString() + ")");

            if (objBoard != null) { objBoard.Delete(); }

            SetFlavor(objFlavor);

            this.ClientSize = new System.Drawing.Size(iBoardWidth, iBoardHeight);

            objBoard = new Board(this, fSuper,
                                 xOrigin, yOrigin, xSize, ySize, font,
                                 sq_KeyPress, sq_KeyDown, sq_Click
                                 );
            Debug.WriteLine("BoardReset(end)");
        }
    }
}
