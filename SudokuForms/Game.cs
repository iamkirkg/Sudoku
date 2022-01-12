using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SudokuForms
{
    public partial class Game : Form
    {
        // I am frustrated that we have two fields duplicated:
        //    Game.cs
        //      public Flavor gameFlav
        //      private bool fSuper
        //    Board.cs
        //      private Flavor boardFlav
        //      public bool fSuper

        public enum Flavor
        {
            Sudoku,         // 3x3x9
            SuperSudoku,    // 4x4x16
            HyperSudoku     // 3x3x(9+4)
        }
        public Flavor gameFlav;
 
        private bool fSuper
        { // Are we 3x3 or 4x4?
            get { return gameFlav == Flavor.SuperSudoku; }
        }
        public int cDimension
        { // Are we 3x3 or 4x4?
            get { return fSuper ? 16 : 9; }
        }
        public string szAll
        {
            get { return fSuper ? "0 1 2 3 4 5 6 7 8 9 A B C D E F " : "1 2 3 4 5 6 7 8 9 "; }
        }

        // Are we currently showing possible answers?
        private bool _fCouldBe = true;
        public bool fCouldBe
        {
            get { return _fCouldBe; }
            set { _fCouldBe = value; }
        }

        public string szTitle {
            get
            {
                switch (gameFlav)
                {
                    case Flavor.Sudoku:
                        return "Sudokirk";
                    case Flavor.SuperSudoku:
                        return "SuperSudokirk";
                    case Flavor.HyperSudoku:
                        return "HyperSudokirk";
                    default:
                        return "error";
                }
            }
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
            objBoard = new Board(this, flav,
                                 sq_KeyPress, sq_KeyDown, sq_Click
                                 );
            */
            Debug.WriteLine("Game(new).end");
        }

        public void SetFlavor(Flavor flav)
        {
            Debug.WriteLine("SetFlavor: set to " + flav.ToString());

            gameFlav = flav;
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

        public void BoardReset(Flavor flav)
        {
            Debug.WriteLine("BoardReset(" + flav.ToString() + ")");

            if (objBoard != null) { objBoard.Delete(); }

            SetFlavor(flav);

            objBoard = new Board(this, flav,
                                 sq_KeyPress, sq_KeyDown, sq_Click
                                 );
            Debug.WriteLine("BoardReset(end)");
        }
    }
}
