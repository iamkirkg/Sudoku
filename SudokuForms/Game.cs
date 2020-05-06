using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Page;

namespace SudokuForms
{
    public partial class Game : Form
    {
        public enum Technique
        {
            none,
            Neighbor,
            AllNeighbors,
            SectorSweep,
            ColumnSweep,
            RowSweep,
            TwoPair,
            ThreesomeRow,
            ThreesomeCol,
            LineFind
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

            // ------------------------------------
            // Tweak these to change the board size
            int xOrigin = 2;
            int yOrigin = 2;
            int xSize = 52;
            int ySize = 68;
            float font = 12F;
            // ------------------------------------

            objBoard = new Board(this,
                                 xOrigin, yOrigin, xSize, ySize, font, 
                                 sq_KeyPress, sq_KeyDown, sq_Click
                                 );
        }
    }
}


