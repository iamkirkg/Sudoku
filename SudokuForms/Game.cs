using System;
//using System.Drawing;
using System.Windows.Forms;
//using System.Xml;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.Page;

namespace SudokuForms
{
    public partial class Game : Form
    {
        private bool _fSuper = false; // field
        public bool fSuper   // property
        {
            get { return _fSuper; }
            set { _fSuper = value; }
        }
        public int cDimension { // Are we 3x3 or 4x4?
            get { return _fSuper ? 16 : 9; }
        }
        public int xDelta { // how much to shift right
            get { return _fSuper ? 564 : 0; }
            //get { return _fSuper ? 376 : 0; }
        }
        public int xMove { // how much to move
            //get { return _fSuper ? 564 : -564; }
            get { return _fSuper ? 376 : -376; }
        }
        public string szTitle {
            get { return _fSuper ? "SuperSudokirk" : "Sudokirk"; }
        }
        public Single emSizeWinner {
            get { return _fSuper ? 36F : 40F; }
        }
        public int bitCount {
            get { return _fSuper ? 16 : 9; }
        }
        public float font {
            get { return _fSuper ? 8F : 12F; }
        }
        // Board origin
        private int xOrigin = 2;
        private int yOrigin = 2;
        // Board size
        public int iBoardWidth {
            get { return _fSuper ? 1662 : 1100; }
        }
        public int iBoardHeight
        {
            get { return _fSuper ? 1600 : 1000; }
        }
        // Square size
        private int xSize {
            get { return _fSuper ? 52 : 52; }
        }
        private int ySize {
            get { return _fSuper ? 69 : 68; }
        }

        public enum Technique
        {
            none,
            //Neighbor,
            //AllNeighbors,
            RangeCheck,
            LineFind,
            SectorFind
            //SectorSweep,
            //ColumnSweeps,
            //RowSweeps
            //TwoPair,
            //ThreesomeRows,
            //ThreesomeCols,
            //FoursomeRows,
            //FoursomeCols,
        }
        public Technique curTechnique = Technique.none;

        //public Square[,] myBoard;
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
        public void SuperToggle()
        {
            //objLogBox.Log("SuperToggle(1); fSuper=" + fSuper + ", xDelta=" + xDelta);

            objBoard.Delete();

            // Toggle us!
            fSuper = !fSuper;

            //objLogBox.Log("SuperToggle(2); fSuper=" + fSuper + ", xDelta=" + xDelta);

            objBoard = new Board(this, fSuper,
                                 xOrigin, yOrigin, xSize, ySize, font,
                                 sq_KeyPress, sq_KeyDown, sq_Click,
                                 objLogBox
                                 );

            MoveButtons();

        }
    }
}
