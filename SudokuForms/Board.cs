using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace SudokuForms
{
    public partial class Board : Form
    {
        public enum Technique
        {
            none,
            Neighbor,
            AllNeighbors,
            SectorSweep,
            ColumnSweep,
            RowSweep,
            TwoPair
        }
        public Technique curTechnique = Technique.none;

        public Square[,] myBoard;
        // These record the last place clicked by the user.
        public int curTab = -1;
        public int curCol = -1;
        public int curRow = -1;
        public char curChar;

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
                    //myBoard[x, y].btn.Click += new EventHandler(sq_Click);
                    myBoard[x, y].btn.Click += sq_Click;

                    this.Controls.Add(myBoard[x, y].btn);

                    xPoint += xDelta;
                }
                yPoint += yDelta;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
        }

        private void Neighbor_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Checked)
            {
                curTechnique = Technique.Neighbor;
            }
        }

        private void AllNeighbors_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Checked)
            {
                curTechnique = Technique.AllNeighbors;
            }
        }

        private void SectorSweep_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Checked)
            {
                curTechnique = Technique.SectorSweep;
            }
        }

        private void ColumnSweep_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Checked)
            {
                curTechnique = Technique.ColumnSweep;
            }
        }

        private void RowSweep_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Checked)
            {
                curTechnique = Technique.RowSweep;
            }
        }

        private void TwoPair_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Checked)
            {
                curTechnique = Technique.TwoPair;
            }
        }
        private void CouldBe_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = sender as CheckBox;
            Color colorNew;
            if (box.Checked)
            {
                colorNew = Color.Black;
            }
            else
            {
                colorNew = Color.LightGray;
            }
            for (int y = 0; y <= 8; y++)
            {
                for (int x = 0; x <= 8; x++)
                {
                    if (myBoard[x, y].iWinner == 0)
                    {
                        myBoard[x, y].btn.ForeColor = colorNew;
                    }
                }
            }
        }

        private void SetSquare(int iTab, char keyChar)
        {
            // Calculate myBoard[col,row] location from the tabindex.
            // TabIndex is [1..81]; the array is [0..8][0..8].
            curTab = iTab;
            curCol = ((iTab - 1) % 9);  // Modulo (remainder)
            curRow = ((iTab - 1) / 9);  // Divide
            curChar = keyChar;

            if (keyChar >= '1' && keyChar <= '9')
            {
                objLogBox.Log("Set: tab " + iTab + ": [" + curCol + "," + curRow + "] key = " + keyChar);
                myBoard[curCol, curRow].Winner(keyChar, Color.Green);
                //Techniques.Neighbor(myBoard, curCol, curRow, keyChar);
            }
        }

        private void ClickSquare(int iTab)
        {
            {
                // Calculate myBoard[col,row] location from the tabindex.
                // TabIndex is [1..81]; the array is [0..8][0..8].
                curTab = iTab;
                curCol = ((iTab - 1) % 9);  // Modulo (remainder)
                curRow = ((iTab - 1) / 9);  // Divide
                Square mySquare = myBoard[curCol, curRow];
                if (mySquare.iWinner != 0)
                {
                    curChar = mySquare.chWinner;
                    objLogBox.Log("Select: tab " + iTab + ": [" + curCol + "," + curRow + "], Winner=" + curChar);
                }
                else
                {
                    // I don't like this, but you can't assign null to a char.
                    curChar = ' ';
                    objLogBox.Log("Select: tab " + iTab + ": [" + curCol + "," + curRow + "]");
                }
            }
        }

        // This is the ButtonClick function for the Save button.
        private void Save_Click(object sender, EventArgs e)
        {
            FileIO f = new FileIO();
            f.SaveFile(myBoard);
        }

        // This is the ButtonClick function for the Load button.
        private void Load_Click(object sender, EventArgs e)
        {
            FileIO f = new FileIO();
            f.LoadFile(myBoard);
        }
    }
}
