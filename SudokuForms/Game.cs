using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Page;

namespace SudokuForms
{
    public partial class Game : Form
    {
        public bool fSuper = true; // 3x3x3 or 4x4x4
        public int cDimension = 16;  // 9 or 16

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

            // ------------------------------------
            // Tweak these to change the board size
            int xOrigin;
            int yOrigin;
            int xSize;
            int ySize;
            float font;

            if (fSuper) {
                xOrigin = 2;
                yOrigin = 2;
                //xSize = 64;
                //ySize = 76;
                //font = 10F;
                xSize = 52;
                ySize = 60;
                font = 8F;
            }
            else {
                xOrigin = 2;
                yOrigin = 2;
                xSize = 52;
                ySize = 68;
                font = 12F;
            }

            // ------------------------------------

            objBoard = new Board(this, fSuper,
                                 xOrigin, yOrigin, xSize, ySize, font, 
                                 sq_KeyPress, sq_KeyDown, sq_Click,
                                 objLogBox
                                 );
        }
    }
}


