using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuForms
{
    public partial class Form3 : Form
    {
        public Square[,] myBoard;
        public Square[,] myBoardOld;

        public Form3()
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

            /*
            myBoardOld = new Square[9, 9]
            {
                { new Square(0, btn00), new Square(0, btn01), new Square(0, btn02), new Square(1, null), new Square(1, null), new Square(1, null), new Square(2, null), new Square(2, null), new Square(2, null) },
                { new Square(0, btn10), new Square(0, btn11), new Square(0, btn12), new Square(1,null), new Square(1,null), new Square(1,null), new Square(2,null), new Square(2,null), new Square(2, null) },
                { new Square(0, btn20), new Square(0, btn21), new Square(0, btn22), new Square(1,null), new Square(1,null), new Square(1,null), new Square(2,null), new Square(2,null), new Square(2, null) },
                { new Square(3,null), new Square(3,null), new Square(3,null), new Square(4,null), new Square(4,null), new Square(4,null), new Square(5,null), new Square(5,null), new Square(5, null) },
                { new Square(3,null), new Square(3,null), new Square(3,null), new Square(4,null), new Square(4,null), new Square(4,null), new Square(5,null), new Square(5,null), new Square(5, null) },
                { new Square(3,null), new Square(3,null), new Square(3,null), new Square(4,null), new Square(4,null), new Square(4,null), new Square(5,null), new Square(5,null), new Square(5, null) },
                { new Square(6,null), new Square(6,null), new Square(6,null), new Square(7,null), new Square(7,null), new Square(7,null), new Square(8,null), new Square(8,null), new Square(8, null) },
                { new Square(6,null), new Square(6,null), new Square(6,null), new Square(7,null), new Square(7,null), new Square(7,null), new Square(8,null), new Square(8,null), new Square(8, null) },
                { new Square(6,null), new Square(6,null), new Square(6,null), new Square(7,null), new Square(7,null), new Square(7,null), new Square(8,null), new Square(8,null), new Square(8, null) }
            };
            */

            myBoard = new Square[9, 9];

            // ------------------------------------
            // Tweak these to change the board size
            int xOrigin = 2;
            int yOrigin = 2;
            int xSize = 110;
            int ySize = 144;
            float font = 16F;
            // ------------------------------------

            int xDelta = xSize + 4;
            int yDelta = ySize + 4;
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
                    myBoard[x, y] = new Square(iTab, iSector, xPoint, yPoint, xSize, ySize, font);

                    // Can do either of these ops inside the Square Constructor?
                    myBoard[x, y].btn.KeyPress += sq_KeyPress;
                    this.Controls.Add(myBoard[x, y].btn);

                    xPoint += xDelta;
                }
                yPoint += yDelta;
            }

            // I don't understand why these are necessary; why isn't the assignment being made in the Square constructor?
            //myBoard[c, r]
            /*
            myBoard[0, 0].btn = btn00;
            myBoard[1, 0].btn = btn01;
            myBoard[2, 0].btn = btn02;
            myBoard[0, 1].btn = btn10;
            myBoard[1, 1].btn = btn11;
            myBoard[2, 1].btn = btn12;
            myBoard[0, 2].btn = btn20;
            myBoard[1, 2].btn = btn21;
            myBoard[2, 2].btn = btn22;
            */
        }

        private void btnStep_Click(object sender, EventArgs e)
        {
        }

        private void Winner(int tabindex, char keyChar)
        {
            // We can deduce the myBoard[col,row] location from the tabindex.
            int col = ((tabindex-1) % 9);  // Modulo (remainder)
            int row = ((tabindex-1) / 9);  // Divide
            //if (col != colUnused || row != rowUnused) {
            //    col = col * 1;
            //}

            if (keyChar >= '1' && keyChar <= '9')
            {
                objLogBox.Log("tab " + tabindex + ": [" + col + "," + row + "] key = " + keyChar);

                myBoard[col, row].btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                myBoard[col, row].btn.Text = keyChar.ToString();
                myBoard[col, row].WinnerWinner(keyChar - '1');

                // Walk every square in the board. If it's in this sector, or row, or 
                // column, but isn't us, and isn't already a winner, then keyChar is a Loser.
                for (int y = 0; y <= 8; y++)
                {
                    for (int x = 0; x <= 8; x++)
                    {
                        Square sqTest = myBoard[x, y];
                        if (sqTest.iWinner == 0)
                        {
                            if (x == col ||
                                y == row ||
                                sqTest.sector == myBoard[col, row].sector
                                )
                            {
                                if (!(x == col && y == row))
                                {
                                    sqTest.Loser(keyChar - '1', keyChar);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
