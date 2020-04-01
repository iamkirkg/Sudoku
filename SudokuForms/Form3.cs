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

        public Form3()
        {
            InitializeComponent();

            myBoard = new Square[9, 9]
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

            // I don't understand why these are necessary; why isn't the assignment being made in the Square constructor?
            myBoard[0, 0].btn = btn00;
            myBoard[0, 1].btn = btn01;
            myBoard[0, 2].btn = btn02;
            myBoard[1, 0].btn = btn10;
            myBoard[1, 1].btn = btn11;
            myBoard[1, 2].btn = btn12;
            myBoard[2, 0].btn = btn20;
            myBoard[2, 1].btn = btn21;
            myBoard[2, 2].btn = btn22;

            /*
            myGameBoard = new Board.Square[9, 9]
            {
                { new Board.Square(0, btn00), new Board.Square(0, btn01), new Board.Square(0, btn02), new Board.Square(1, btn03), new Board.Square(1, btn04), new Board.Square(1, btn05), new Board.Square(2, btn06), new Board.Square(2, btn07), new Board.Square(2, btn08) },
                { new Board.Square(0), new Board.Square(0), new Board.Square(0), new Board.Square(1), new Board.Square(1), new Board.Square(1), new Board.Square(2), new Board.Square(2), new Board.Square(2) },
                { new Board.Square(0), new Board.Square(0), new Board.Square(0), new Board.Square(1), new Board.Square(1), new Board.Square(1), new Board.Square(2), new Board.Square(2), new Board.Square(2) },
                { new Board.Square(3), new Board.Square(3), new Board.Square(3), new Board.Square(4), new Board.Square(4), new Board.Square(4), new Board.Square(5), new Board.Square(5), new Board.Square(5) },
                { new Board.Square(3), new Board.Square(3), new Board.Square(3), new Board.Square(4), new Board.Square(4), new Board.Square(4), new Board.Square(5), new Board.Square(5), new Board.Square(5) },
                { new Board.Square(3), new Board.Square(3), new Board.Square(3), new Board.Square(4), new Board.Square(4), new Board.Square(4), new Board.Square(5), new Board.Square(5), new Board.Square(5) },
                { new Board.Square(6), new Board.Square(6), new Board.Square(6), new Board.Square(7), new Board.Square(7), new Board.Square(7), new Board.Square(8), new Board.Square(8), new Board.Square(8) },
                { new Board.Square(6), new Board.Square(6), new Board.Square(6), new Board.Square(7), new Board.Square(7), new Board.Square(7), new Board.Square(8), new Board.Square(8), new Board.Square(8) },
                { new Board.Square(6), new Board.Square(6), new Board.Square(6), new Board.Square(7), new Board.Square(7), new Board.Square(7), new Board.Square(8), new Board.Square(8), new Board.Square(8) }
            };
            */
        }

        private void btnStep_Click(object sender, EventArgs e)
        {
        }

        private void Winner(int row, int col, Button btn, char keyChar)
        {
            if (keyChar >= '1' && keyChar <= '9')
            {
                btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                btn.Text = keyChar.ToString();
                myBoard[row, col].WinnerWinner(keyChar - '1');

                // Walk every square in the board. If it's in this sector, or row, or 
                // column, but isn't us, then keyChar is a Loser.
                for (int x = 0; x <= 8; x++)
                {
                    for (int y = 0; y <= 8; y++)
                    {
                        Square sqTest = myBoard[x, y];

                        if (x == row ||
                            y == col ||
                            sqTest.sector == myBoard[row, col].sector
                            )
                        {
                            if (!(x == row && y == col))
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
