using System;
using System.Drawing;
using System.Windows.Forms;

namespace SudokuForms
{
    public partial class Board : Form
    {
        public Square[,] myBoard;

        public Board()
        {
            InitializeComponent();

            // map TabIndex to Sector. There's probably an arithmetic way to 
            // do this (index, 27, 9, 3, modulo, remainders?) but I can't 
            // see it, and it'd probably be a pain to document or maintain.
            int[] mpTabSector =
            {
                0, 0, 0, 1, 1, 1, 2, 2, 2,
                0, 0, 0, 1, 1, 1, 2, 2, 2,
                0, 0, 0, 1, 1, 1, 2, 2, 2,
                3, 3, 3, 4, 4, 4, 5, 5, 5,
                3, 3, 3, 4, 4, 4, 5, 5, 5,
                3, 3, 3, 4, 4, 4, 5, 5, 5,
                6, 6, 6, 7, 7, 7, 8, 8, 8,
                6, 6, 6, 7, 7, 7, 8, 8, 8,
                6, 6, 6, 7, 7, 7, 8, 8, 8
            };

            myBoard = new Square[9, 9];

            // ------------------------------------
            // Tweak these to change the board size
            int xOrigin = 2;
            int yOrigin = 2;
            int xSize = 52;
            int ySize = 68;
            float font = 12F;
            // ------------------------------------

            int xDelta = xSize + 2;
            int yDelta = ySize + 2;
            int xPoint = xOrigin;
            int yPoint = yOrigin;

            int iTab = 0; // [1 ... 81]
            for (int y = 0; y <= 8; y++)
            {
                xPoint = xOrigin;
                for (int x = 0; x <= 8; x++)
                {
                    iTab++;
                    int iSector = mpTabSector[iTab - 1];
                    myBoard[x, y] = new Square(iTab, iSector, xPoint, yPoint, xSize, ySize, font, sq_KeyPress);

                    // Can we do either of these ops inside the Square Constructor?
                    myBoard[x, y].btn.KeyPress += sq_KeyPress;
                    this.Controls.Add(myBoard[x, y].btn);

                    xPoint += xDelta;
                }
                yPoint += yDelta;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RowSweep_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Neighbor_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void SectorSweep_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ColumnSweep_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CouldBes_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void SetSquare(int tabindex, char keyChar)
        {
            // Calculate myBoard[col,row] location from the tabindex.
            int col = ((tabindex-1) % 9);  // Modulo (remainder)
            int row = ((tabindex-1) / 9);  // Divide

            if (keyChar >= '1' && keyChar <= '9')
            {
                objLogBox.Log("tab " + tabindex + ": [" + col + "," + row + "] key = " + keyChar);

                myBoard[col, row].Winner(keyChar - '1', keyChar, Color.Green);

                Techniques.Neighbor(myBoard, col, row, keyChar);
                //Techniques.SectorSweep(myBoard);
            }
        }
    }
}
